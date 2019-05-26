using System;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Practises;

namespace OffSync.Mapping.Mappert
{
    public abstract class AbstractMapper<TSource, TTarget> :
        MapperBuilder<TSource, TTarget>,
        IMapper<TSource, TTarget>
    {
        protected AbstractMapper()
        {
        }

        protected AbstractMapper(
            Action<IMapperBuilder<TSource, TTarget>> configure) :
            base(configure)
        {
        }

        protected abstract TTarget CreateTarget();

        public TTarget Map(
            TSource source)
        {
            var mappingDelegate = GetValidatedMappingDelegate();

            if (source == null)
            {
                return default(TTarget);
            }

            var target = CreateTarget();

            mappingDelegate(
                source,
                target);

            return target;
        }
    }
}
