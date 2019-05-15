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
            ICollection<MappingRule> mappingRules)
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
                    out var mappingRule,
                    out var exception))
                {
                    throw new InvalidOperationException(
                        $"unable to create auto-mapping for property '{targetProperty.Name}'",
                        exception);
                }

                // save auto-mapping rule
                mappingRules.Add(mappingRule);
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
            var missingBuilders = mappingRules
                .Where(r =>
                    r.Builder == null &&
                        (r.SourceProperties.Count != 1 ||
                        r.TargetProperties.Count != 1));

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
                throw new InvalidOperationException(
                    $"no source property found named '{targetProperty.Name}'");
            }

            // create new mapping rule
            var mappingRule = new MappingRule()
                .WithSource(sourceProperty)
                .WithTarget(targetProperty);

            if (targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
            {
                // properties have assignable types: no builder required
                return mappingRule;
            }

            // create auto-mapper
            var mapper = MappersUtil.CreateAutoMapper(
                sourceProperty.PropertyType,
                targetProperty.PropertyType);

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
