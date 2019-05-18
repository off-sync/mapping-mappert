using System;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.Common;

namespace OffSync.Mapping.Mappert.MappingRules
{
    public static partial class MappingRulesExtensions
    {
        public static MappingRule WithSource<TFrom, TFrom1>(
            this MappingRule mappingRule,
            Expression<Func<TFrom, TFrom1>> source,
            Type sourceType = null)
        {
            return mappingRule.WithSource(
                ExpressionsUtil.GetPropertyFromExpression(source),
                sourceType);
        }

        public static MappingRule WithTarget<TTo, TTo1>(
            this MappingRule mappingRule,
            Expression<Func<TTo, TTo1>> target,
            Type targetType = null)
        {
            return mappingRule.WithTarget(
                ExpressionsUtil.GetPropertyFromExpression(target),
                targetType);
        }
    }
}
