using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using OffSync.Mapping.Mappert.Common;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public static class MappingRulesUtil
    {
        public static void EnsureNoDuplicateTargetMappings(
            IEnumerable<MappingRule> mappingRules)
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

        public static void EnsureAllTargetPropertiesMapped<TSource, TTarget>(
            ICollection<MappingRule> mappingRules,
            Func<MappingRule> addMappingRule)
        {
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
                    addMappingRule,
                    out var mappingRule,
                    out var exception))
                {
                    throw new InvalidOperationException(
                        $"unable to create auto-mapping for property '{targetProperty.Name}'",
                        exception);
                }
            }
        }

        public static void EnsureAllSourcePropertiesMapped<TSource>(
            IEnumerable<MappingRule> mappingRules)
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

        public static void RemoveIgnoringMappingRules(
            ICollection<MappingRule> mappingRules)
        {
            // copy mapping rules to allow removal during enumeration
            var mappingRulesCopy = mappingRules.ToArray();

            foreach (var mappingRule in mappingRulesCopy)
            {
                if (!mappingRule.SourceProperties.Any() ||
                    !mappingRule.TargetProperties.Any())
                {
                    // remove mapping rule
                    mappingRules.Remove(mappingRule);
                }
            }
        }

        public static void EnsureValidBuilders(
            IEnumerable<MappingRule> mappingRules)
        {
            // builders must be present for unassignable types
            var missingBuilders = mappingRules
                .Where(r =>
                    r.Builder == null &&
                    r.SourceProperties.Count == 1 &&
                    r.TargetProperties.Count == 1 &&
                    !r.TargetTypes[0].IsAssignableFrom(r.SourceTypes[0]));

            foreach (var rule in missingBuilders)
            {
                try
                {
                    // create auto-mapper
                    var mapper = MappersUtil.CreateAutoMapper(
                        rule.SourceTypes[0],
                        rule.TargetTypes[0]);

                    // create builder from mapper
                    var builder = MappersUtil.CreateMapperDelegate(mapper);

                    rule.WithBuilder(builder);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"unable to create auto-mapper for ['{rule.SourceProperties[0].Name}' -> '{rule.TargetProperties[0].Name}']",
                        ex);
                }
            }

            // builders must be present for mappings from/to multiple properties
            missingBuilders = mappingRules
                .Where(r =>
                    r.Builder == null &&
                        (r.SourceProperties.Count > 1 ||
                        r.TargetProperties.Count > 1));

            if (missingBuilders.Any())
            {
                var rules = string.Join(
                    ", ",
                    missingBuilders.Select(
                        r => $"['{string.Join("', '", r.SourceProperties.Select(pi => pi.Name))}' -> '{string.Join("', '", r.TargetProperties.Select(pi => pi.Name))}']"));

                throw new InvalidOperationException(
                    $"builder missing for multi-property mapping rules: {rules}");
            }
        }

        public static void EnsureValidItemsMappings(
            IEnumerable<MappingRule> mappingRules)
        {
            var itemsRules = mappingRules
                .Where(r => r.MappingStrategy == MappingStrategies.MapToArray ||
                    r.MappingStrategy == MappingStrategies.MapToCollection);

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
            PropertyInfo targetProperty,
            Func<MappingRule> addMappingRule)
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
            var sourceType = sourceProperty.PropertyType;

            var targetType = targetProperty.PropertyType;

            if (targetType.IsAssignableFrom(sourceType))
            {
                // properties have assignable types: no builder required
                return addMappingRule()
                    .WithSource(sourceProperty)
                    .WithTarget(targetProperty);
            }

            MappingRule mappingRule;

            // check if both properties have items
            if (sourceType.TryGetSourceItemsType(out var sourceItemsType) &&
                targetType.TryGetTargetItemsType(out var targetItemsType))
            {
                mappingRule = addMappingRule()
                    .WithSource(sourceProperty, sourceItemsType)
                    .WithTarget(targetProperty, targetItemsType)
                    .WithStrategy(targetType.IsArray ?
                        MappingStrategies.MapToArray :
                        MappingStrategies.MapToCollection);

                sourceType = sourceItemsType;

                targetType = targetItemsType;

                if (targetType.IsAssignableFrom(sourceType))
                {
                    // items have assignable types: no builder required
                    return mappingRule;
                }
            }
            else
            {
                mappingRule = addMappingRule()
                    .WithSource(sourceProperty)
                    .WithTarget(targetProperty);
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
            Func<MappingRule> addMappingRule,
            out MappingRule mappingRule,
            out Exception exception)
        {
            try
            {
                mappingRule = CreateAutoMapping<TSource>(
                    targetProperty,
                    addMappingRule);

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
