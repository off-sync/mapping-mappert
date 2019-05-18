namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class TupleSplitterSourceModel
    {
        public string Values { get; set; } = "3,4";
    }

    public class TupleSplitterTargetModel
    {
        public string Value1 { get; set; }

        public string Value2 { get; set; }
    }

    public class TupleSplitterMapper :
        Mapper<TupleSplitterSourceModel, TupleSplitterTargetModel>
    {
        public TupleSplitterMapper()
        {
            Map(s => s.Values)
                .To(t => t.Value1, t => t.Value2)
                .Using(StringTupleSplitter);
        }

        private (string, string) StringTupleSplitter(
            string values)
        {
            var splitted = values.Split(',');

            return (splitted[0], splitted[1]);
        }
    }
}