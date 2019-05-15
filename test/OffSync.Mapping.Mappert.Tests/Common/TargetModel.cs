namespace OffSync.Mapping.Mappert.Tests.Common
{
    public class TargetModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TargetNested Nested { get; set; }
    }

    public class TargetNested
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }
}
