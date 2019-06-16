/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises.MappingRules;

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
                .WithTargetItems(
                    to,
                    typeof(TTo))
                .WithType(MappingRuleTypes.MapToCollection);

            return new MappingRuleBuilderFrom1To1<TFrom, TTo>(_mappingRule);
        }

        public MappingRuleBuilderFrom1To1<TFrom, TTo> To<TTo>(
            Expression<Func<TTarget, TTo[]>> to)
        {
            _mappingRule
                .WithTargetItems(
                    to,
                    typeof(TTo))
                .WithType(MappingRuleTypes.MapToArray);

            return new MappingRuleBuilderFrom1To1<TFrom, TTo>(_mappingRule);
        }
    }
}
