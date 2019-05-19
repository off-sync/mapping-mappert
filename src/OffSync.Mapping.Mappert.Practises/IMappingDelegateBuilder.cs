using System.Collections.Generic;

using OffSync.Mapping.Mappert.Practises.MappingRules;

namespace OffSync.Mapping.Mappert.Practises
{
    public interface IMappingDelegateBuilder
    {
        MappingDelegate<TSource, TTarget> CreateMappingDelegate<TSource, TTarget>(
            IEnumerable<IMappingRule> mappingRules);
    }
}
