using System;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.Practises.Mapping;
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
            if (source == null)
            {
                return default(TTarget);
            }

            if (MappingContext.Current.TryGetMapping(
                source,
                out var targetObject))
            {
                // source was mapped before: returned corresponding target
                return (TTarget)targetObject;
            }

            var mappingDelegate = GetValidatedMappingDelegate();

            var target = CreateTarget();

            // add mapping before calling the delegate to support cyclic object graphs
            MappingContext.Current.AddMapping(
                source,
                target);

            mappingDelegate(
                source,
                target);

            return target;
        }
    }
}
