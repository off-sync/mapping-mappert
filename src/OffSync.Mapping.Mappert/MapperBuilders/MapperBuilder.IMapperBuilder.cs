using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.MappingRules;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Practises.Validation;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial class MapperBuilder<TSource, TTarget> :
        IMapperBuilder<TSource, TTarget>
    {
        private IMappingDelegateBuilder _mappingDelegateBuilder;

        IMapperBuilder<TSource, TTarget> IMapperBuilder<TSource, TTarget>.WithMappingDelegateBuilder(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            _mappingDelegateBuilder = mappingDelegateBuilder;

            return this;
        }

        protected IMapperBuilder<TSource, TTarget> WithMappingDelegateBuilder(
            IMappingDelegateBuilder mappingDelegateBuilder)
            => ((IMapperBuilder<TSource, TTarget>)this).WithMappingDelegateBuilder(mappingDelegateBuilder);

        IMapperBuilder<TSource, TTarget> IMapperBuilder<TSource, TTarget>.WithValidator(
            IMappingValidator validator)
        {
            _validators.Add(validator);

            return this;
        }

        protected IMapperBuilder<TSource, TTarget> WithValidator(
            IMappingValidator validator)
            => ((IMapperBuilder<TSource, TTarget>)this).WithValidator(validator);

        MappingItemsRuleBuilder<TFrom, TTarget> IMapperBuilder<TSource, TTarget>.MapItems<TFrom>(
            Expression<Func<TSource, IEnumerable<TFrom>>> from)
        {
            var mappingRule = AddMappingRule()
                .WithSourceItems(from, typeof(TFrom));

            return new MappingItemsRuleBuilder<TFrom, TTarget>(mappingRule);
        }

        protected MappingItemsRuleBuilder<TFrom, TTarget> MapItems<TFrom>(
            Expression<Func<TSource, IEnumerable<TFrom>>> from)
            => ((IMapperBuilder<TSource, TTarget>)this).MapItems<TFrom>(from);

        void IMapperBuilder<TSource, TTarget>.IgnoreSource<TFrom>(
            Expression<Func<TSource, TFrom>> from)
        {
            AddMappingRule()
                .WithSource(from);
        }

        protected void IgnoreSource<TFrom>(
            Expression<Func<TSource, TFrom>> from)
            => ((IMapperBuilder<TSource, TTarget>)this).IgnoreSource(from);

        void IMapperBuilder<TSource, TTarget>.IgnoreTarget<TTo>(
            Expression<Func<TTarget, TTo>> to)
        {
            AddMappingRule()
                .WithTarget(to);
        }

        protected void IgnoreTarget<TTo>(
            Expression<Func<TTarget, TTo>> to)
            => ((IMapperBuilder<TSource, TTarget>)this).IgnoreTarget(to);
    }
}
