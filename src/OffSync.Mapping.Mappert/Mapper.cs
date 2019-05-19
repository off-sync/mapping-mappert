using System;

using OffSync.Mapping.Mappert.MapperBuilders;

namespace OffSync.Mapping.Mappert
{
    public class Mapper<TSource, TTarget> :
        AbstractMapper<TSource, TTarget>
        where TTarget : new()
    {
        public Mapper()
        {
        }

        public Mapper(
            Action<IMapperBuilder<TSource, TTarget>> withMappingRules) :
            base(withMappingRules)
        {
        }

        override protected TTarget CreateTarget()
        {
            return new TTarget();
        }
    }
}
