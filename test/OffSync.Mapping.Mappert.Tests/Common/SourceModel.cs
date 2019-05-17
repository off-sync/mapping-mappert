namespace OffSync.Mapping.Mappert.Tests.Common
{
    public class SourceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SourceNested Nested { get; set; }

        public string Values { get; set; }

        public bool Ignored { get; set; }
    }

    public class SourceNested
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }
}
