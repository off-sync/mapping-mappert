/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using OffSync.Mapping.Mappert.Common;
using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public static class MappingRulesUtil
    {
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
            }

            if (targetType.IsAssignableFrom(sourceType))
            {
                // properties/items have assignable types: no builder required
                return mappingRule;
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
