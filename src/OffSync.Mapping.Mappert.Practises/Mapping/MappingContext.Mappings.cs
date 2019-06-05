using System.Collections.Generic;

namespace OffSync.Mapping.Mappert.Practises.Mapping
{
    public sealed partial class MappingContext :
        IMappingContext
    {
        private readonly IDictionary<object, object> _mappings = new Dictionary<object, object>();

        public void AddMapping(
            object source,
            object target)
            => _mappings.Add(
                source,
                target);

        public bool TryGetMapping(
            object source,
            out object target)
            => _mappings.TryGetValue(
                source,
                out target);
    }
}
