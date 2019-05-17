using System;
using System.Linq.Expressions;

namespace OffSync.Mapping.Mappert.MapperBuilders
{
    public partial interface IMapperBuilder<TSource, TTarget>
    {
        void IgnoreSource<TFrom>(
            Expression<Func<TSource, TFrom>> from);

        void IgnoreTarget<TTo>(
            Expression<Func<TTarget, TTo>> to);
    }
}
