using System;
using System.Collections.Generic;

using OffSync.Mapping.Mappert.MapperBuilders;
using OffSync.Mapping.Mappert.MappingRules;
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
            Action<IMapperBuilder<TSource, TTarget>> withMappingRules) :
            base(withMappingRules)
        {
        }

        protected abstract TTarget CreateTarget();

        public TTarget Map(
            TSource source)
        {
            if (source == null)
            {
                return default;
            }

            var mappingRules = GetCheckedMappingRules();

            var target = CreateTarget();

            ApplyMappingRules(
                source,
                target,
                mappingRules);

            return target;
        }

        protected virtual void ApplyMappingRules(
            TSource source,
            TTarget target,
            IEnumerable<MappingRule> mappingRules)
        {
            foreach (var mappingRule in mappingRules)
            {
                mappingRule.Apply(
                    source,
                    target);
            }
        }
    }
}
