using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public sealed class MappingItemsRuleBuilder<TFrom, TTarget> :
        AbstractMappingRuleBuilder
    {
        public MappingItemsRuleBuilder(
            MappingRule mappingRule) :
            base(mappingRule)
        {
        }

        public MappingRuleBuilderFrom1To1<TFrom, TTo> To<TTo>(
            Expression<Func<TTarget, ICollection<TTo>>> to)
        {
            _mappingRule
                .WithTarget(to, typeof(TTo))
                .WithStrategy(MappingStrategies.MapToCollection);

            return new MappingRuleBuilderFrom1To1<TFrom, TTo>(_mappingRule);
        }

        public MappingRuleBuilderFrom1To1<TFrom, TTo> To<TTo>(
            Expression<Func<TTarget, TTo[]>> to)
        {
            _mappingRule
                .WithTarget(to, typeof(TTo))
                .WithStrategy(MappingStrategies.MapToArray);

            return new MappingRuleBuilderFrom1To1<TFrom, TTo>(_mappingRule);
        }
    }
}
