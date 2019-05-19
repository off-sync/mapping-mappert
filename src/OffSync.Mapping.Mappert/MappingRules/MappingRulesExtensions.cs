using System;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.Common;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public static partial class MappingRulesExtensions
    {
        public static MappingRule WithSource<TFrom, TFrom1>(
            this MappingRule mappingRule,
            Expression<Func<TFrom, TFrom1>> source)
        {
            return mappingRule.WithSource(
                ExpressionsUtil.GetPropertyFromExpression(source));
        }

        public static MappingRule WithSourceItems<TFrom, TFrom1>(
            this MappingRule mappingRule,
            Expression<Func<TFrom, TFrom1>> source,
            Type sourceItemsType)
        {
            return mappingRule.WithSourceItems(
                ExpressionsUtil.GetPropertyFromExpression(source),
                sourceItemsType);
        }

        public static MappingRule WithTarget<TTo, TTo1>(
            this MappingRule mappingRule,
            Expression<Func<TTo, TTo1>> target)
        {
            return mappingRule.WithTarget(
                ExpressionsUtil.GetPropertyFromExpression(target));
        }

        public static MappingRule WithTargetItems<TTo, TTo1>(
            this MappingRule mappingRule,
            Expression<Func<TTo, TTo1>> target,
            Type targetItemsType)
        {
            return mappingRule.WithTargetItems(
                ExpressionsUtil.GetPropertyFromExpression(target),
                targetItemsType);
        }
    }
}
