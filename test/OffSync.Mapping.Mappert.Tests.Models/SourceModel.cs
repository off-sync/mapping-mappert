using System.Collections.Generic;
using System.Linq;

namespace OffSync.Mapping.Mappert.Tests.Models
{
    public class SourceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SourceNested Nested { get; set; }

        public string Values { get; set; }

        public bool Ignored { get; set; }

        public string LookupValue { get; set; }

        public IEnumerable<SourceNested> ItemsEnumerable { get; set; }

        public SourceNested[] ItemsArray { get; set; }

        public IReadOnlyList<int> Numbers { get; set; }

        public SharedSub Shared { get; set; }

        public IOrderedEnumerable<SourceNested> MoreItems { get; set; }
    }

    public class SourceNested
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }
}
