namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class ArraySplitterSourceModel
    {
        public string Values { get; set; } = "3,4";
    }

    public class ArraySplitterTargetModel
    {
        public string Value1 { get; set; }

        public string Value2 { get; set; }
    }

    public class ArraySplitterMapper :
        Mapper<ArraySplitterSourceModel, ArraySplitterTargetModel>
    {
        public ArraySplitterMapper()
        {
            Map(s => s.Values)
                .To(t => t.Value1, t => t.Value2)
                .Using(StringArraySplitter);
        }

        private object[] StringArraySplitter(
            string values)
        {
            var splitted = values.Split(',');

            return new object[] { splitted[0], splitted[1] };
        }
    }
}