using System.Collections.Generic;

namespace OffSync.Mapping.Mappert.Tests.Models
{
    public class TargetModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TargetNested Nested { get; set; }

        public string Value1 { get; set; }

        public string Value2 { get; set; }

        public bool Excluded { get; set; }

        public int LookupId { get; set; }

        public TargetNested[] ItemsArray { get; set; }

        public IList<TargetNested> ItemsCollection { get; set; }

        public List<TargetNested> ItemsList { get; set; }

        public int[] Numbers { get; set; }

        public ICollection<int> NumbersCollection { get; set; }

        public Shared Shared { get; set; }

        public IEnumerable<TargetNested> MoreItems { get; set; }
    }

    public class TargetNested
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }
}
