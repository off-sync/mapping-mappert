namespace OffSync.Mapping.Mappert.Tests.Common
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
    }

    public class TargetNested
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }
}
