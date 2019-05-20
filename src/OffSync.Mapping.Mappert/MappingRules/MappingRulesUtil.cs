using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using OffSync.Mapping.Mappert.Common;
using OffSync.Mapping.Mappert.Practises.Builders;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public static class MappingRulesUtil
    {
        public static void EnsureNoDuplicateTargetMappings(
            IEnumerable<IMappingRule> mappingRules)
        {
            var targetDuplicates = mappingRules
                .SelectMany(mi => mi.TargetProperties)
                .GroupBy(pi => pi.Name)
                .Where(g => g.Count() > 1);

            if (targetDuplicates.Any())
            {
                var names = string.Join(
                    "', '",
                    targetDuplicates.Select(g => g.Key));

                throw new InvalidOperationException(
                    $"duplicate mapping found for target properties: '{names}'");
            }
        }

        public static IEnumerable<IMappingRule> WithAllTargetPropertiesMapped<TSource, TTarget>(
            IEnumerable<IMappingRule> mappingRules)
        {
            var rules = new List<IMappingRule>(mappingRules);

            // create lookup of mapped target property names
            var mappedTargetPropertyNames = new HashSet<string>(mappingRules
                .SelectMany(mi => mi.TargetProperties)
                .Select(mi => mi.Name));

            // get all mappable target properties
            var targetProperties = GetMappableTargetProperties<TTarget>();

            // check if all target properties are mapped
            foreach (var targetProperty in targetProperties)
            {
                if (mappedTargetPropertyNames.Contains(targetProperty.Name))
                {
                    // target property is mapped
                    continue;
                }

                // not mapped, try to add an auto-mapping
                if (!TryCreateAutoMapping<TSource>(
                    targetProperty,
                    out var mappingRule,
                    out var exception))
                {
                    throw new InvalidOperationException(
                        $"unable to create auto-mapping for property '{targetProperty.Name}'",
                        exception);
                }

                // add auto-mapping rule
                rules.Add(mappingRule);
            }

            return rules;
        }

        public static void EnsureAllSourcePropertiesMapped<TSource>(
            IEnumerable<IMappingRule> mappingRules)
        {
            // create lookup of mapped source property names
            var mappedSourcePropertyNames = new HashSet<string>(mappingRules
                .SelectMany(mi => mi.SourceProperties)
                .Select(mi => mi.Name));

            // get all mappable source properties
            var sourceProperties = GetMappableSourceProperties<TSource>();

            // check if all source properties are mapped
            foreach (var sourceProperty in sourceProperties)
            {
                if (!mappedSourcePropertyNames.Contains(sourceProperty.Name))
                {
                    throw new InvalidOperationException(
                        $"source property '{sourceProperty.Name}' not mapped");
                }
            }
        }

        /// <summary>
        /// Filters from the provided mapping rules the rules that:
        /// <list type="bullet">
        /// <item><description>Don't have any target properties.</description></item>
        /// <item><description>Don't have any source properties, and no builder.</description></item>
        /// </list>
        /// </summary>
        /// <param name="mappingRules">The mapping rules to process.</param>
        public static IEnumerable<IMappingRule> WithoutIgnoringMappingRules(
            IEnumerable<IMappingRule> mappingRules)
        {
            return mappingRules
                .Where(r => !IsIgnoringMappingRule(r))
                .ToArray();
        }

        private static bool IsIgnoringMappingRule(
            IMappingRule mappingRule)
        {
            if (!mappingRule.TargetProperties.Any())
            {
                // no target properties
                return true;
            }

            if (!mappingRule.SourceProperties.Any() &&
                mappingRule.Builder == null)
            {
                // no source properties, and no builder
                return true;
            }

            return false;
        }

        // FIXME refactor
        public static IEnumerable<IMappingRule> WithAutoMappingBuilders(
            IEnumerable<IMappingRule> mappingRules)
        {
            var rules = new List<IMappingRule>(mappingRules);

            for (int i = 0; i < rules.Count; i++)
            {
                // builders must be present for unassignable types
                if (rules[i].Builder == null &&
                    rules[i].SourceProperties.Count == 1 &&
                    rules[i].TargetProperties.Count == 1)
                {
                    object mapper;
                    Exception exception;

                    switch (rules[i].Type)
                    {
                        case MappingRuleTypes.MapToArray:
                        case MappingRuleTypes.MapToCollection:
                            if (rules[i].TargetItemsType.IsAssignableFrom(rules[i].SourceItemsType))
                            {
                                // target items type can be assigned from source items type
                                continue;
                            }

                            // create auto-mapper for items
                            if (!MappersUtil.TryCreateAutoMapper(
                                rules[i].SourceItemsType,
                                rules[i].TargetItemsType,
                                out mapper,
                                out exception))
                            {
                                throw new InvalidOperationException(
                                    $"unable to create auto-mapper for ['{rules[i]}']",
                                    exception);
                            }

                            break;

                        // MappingRuleTypes.MapToValue
                        default:
                            if (rules[i].TargetProperties[0].PropertyType.IsAssignableFrom(rules[i].SourceProperties[0].PropertyType))
                            {
                                // target property can be assigned from source property
                                continue;
                            }

                            // create auto-mapper for properties
                            if (!MappersUtil.TryCreateAutoMapper(
                                rules[i].SourceProperties[0].PropertyType,
                                rules[i].TargetProperties[0].PropertyType,
                                out mapper,
                                out exception))
                            {
                                throw new InvalidOperationException(
                                    $"unable to create auto-mapper for ['{rules[i]}']",
                                    exception);
                            }

                            break;
                    }

                    // create builder from mapper
                    var builder = MappersUtil.CreateMapperDelegate(mapper);

                    // create new mapping rule with builder
                    var rule = new MappingRule()
                        .WithType(rules[i].Type)
                        .WithBuilder(builder);

                    switch (rules[i].Type)
                    {
                        case MappingRuleTypes.MapToArray:
                        case MappingRuleTypes.MapToCollection:
                            rule
                                .WithSourceItems(
                                    rules[i].SourceProperties[0],
                                    rules[i].SourceItemsType)
                                .WithTargetItems(
                                    rules[i].TargetProperties[0],
                                    rules[i].TargetItemsType);

                            break;

                        // MappingRuleTypes.MapToValue
                        default:
                            rule
                                .WithSource(rules[i].SourceProperties[0])
                                .WithTarget(rules[i].TargetProperties[0]);

                            break;
                    }

                    rules[i] = rule;
                }
            }

            return rules;
        }

        public static void EnsureValidBuilders(
            IEnumerable<IMappingRule> mappingRules)
        {
            foreach (var rule in mappingRules)
            {
                if (rule.Builder == null)
                {
                    // builders must be present for mappings from/to multiple properties
                    if (rule.SourceProperties.Count > 1 ||
                        rule.TargetProperties.Count > 1)
                    {
                        throw new InvalidOperationException(
                            $"builder missing for multi-property mapping rule: {rule}");
                    }

                    // builders must be present for mappings without source properties
                    if (rule.SourceProperties.Count == 0)
                    {
                        throw new InvalidOperationException(
                            $"builder missing for target properties only mapping rule: {rule}");
                    }

                    continue;
                }

                // check builder parameter and return types
                BuildersUtil.GetBuilderType(rule);
            }
        }

        public static void EnsureValidItemsMappings(
            IEnumerable<IMappingRule> mappingRules)
        {
            var itemsRules = mappingRules
                .Where(r => r.Type == MappingRuleTypes.MapToArray ||
                    r.Type == MappingRuleTypes.MapToCollection);

            foreach (var rule in itemsRules)
            {
                if (rule.SourceProperties.Count != 1)
                {
                    var sourceNames = string.Join(
                        "', '",
                        rule
                            .SourceProperties
                            .Select(pi => pi.Name));

                    throw new InvalidOperationException(
                        $"exactly one source property must be mapped for an items mapping, found '{sourceNames}'");
                }

                if (rule.TargetProperties.Count != 1)
                {
                    var targetNames = string.Join(
                        "', '",
                        rule
                            .TargetProperties
                            .Select(pi => pi.Name));

                    throw new InvalidOperationException(
                        $"exactly one target property must be mapped for an items mapping, found '{targetNames}'");
                }

                if (!ItemsUtil.TryGetSourceItemsType(
                    rule.SourceProperties[0].PropertyType,
                    out var sourceItemsType))
                {
                    throw new InvalidOperationException(
                        $"unable to determine source items type for property '{rule.SourceProperties[0].Name}'");
                }

                if (!ItemsUtil.TryGetTargetItemsType(
                    rule.TargetProperties[0].PropertyType,
                    out var targetItemsType))
                {
                    throw new InvalidOperationException(
                        $"unable to determine target items type for property '{rule.TargetProperties[0].Name}'");
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetMappableTargetProperties<TTarget>()
        {
            return typeof(TTarget)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite);
        }

        public static IEnumerable<PropertyInfo> GetMappableSourceProperties<TSource>()
        {
            return typeof(TSource)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead);
        }

        public static MappingRule CreateAutoMapping<TSource>(
            PropertyInfo targetProperty)
        {
            // try to get the same-named property from the source type
            var sourceProperty = typeof(TSource)
                .GetProperty(
                    targetProperty.Name,
                    BindingFlags.Instance | BindingFlags.Public);

            if (sourceProperty == null)
            {
                throw new ArgumentException(
                    $"no source property found named '{targetProperty.Name}'");
            }

            // create new mapping rule
            var mappingRule = new MappingRule()
                .WithSource(sourceProperty)
                .WithTarget(targetProperty);

            var sourceType = sourceProperty.PropertyType;

            var targetType = targetProperty.PropertyType;

            if (targetType.IsAssignableFrom(sourceType))
            {
                // properties have assignable types: no builder required
                return mappingRule;
            }

            // check if both properties have items
            if (sourceType.TryGetSourceItemsType(out var sourceItemsType) &&
                targetType.TryGetTargetItemsType(out var targetItemsType))
            {
                mappingRule = new MappingRule()
                    .WithSourceItems(sourceProperty, sourceItemsType)
                    .WithTargetItems(targetProperty, targetItemsType)
                    .WithType(targetType.IsArray ?
                        MappingRuleTypes.MapToArray :
                        MappingRuleTypes.MapToCollection);

                sourceType = sourceItemsType;

                targetType = targetItemsType;

                if (targetType.IsAssignableFrom(sourceType))
                {
                    // items have assignable types: no builder required
                    return mappingRule;
                }
            }

            // create auto-mapper
            var mapper = MappersUtil.CreateAutoMapper(
                sourceType,
                targetType);

            // create builder from mapper
            var builder = MappersUtil.CreateMapperDelegate(mapper);

            // use the builder for this rule
            return mappingRule.WithBuilder(builder);
        }

        public static bool TryCreateAutoMapping<TSource>(
            PropertyInfo targetProperty,
            out MappingRule mappingRule,
            out Exception exception)
        {
            try
            {
                mappingRule = CreateAutoMapping<TSource>(targetProperty);

                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                mappingRule = null;

                exception = ex;

                return false;
            }
        }
    }
}
