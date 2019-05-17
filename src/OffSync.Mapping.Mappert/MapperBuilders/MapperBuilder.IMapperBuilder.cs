using System;
using System.Linq.Expressions;

using OffSync.Mapping.Mappert.MappingRules;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial class MapperBuilder<TSource, TTarget> :
        IMapperBuilder<TSource, TTarget>
    {
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
