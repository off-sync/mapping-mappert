using OffSync.Mapping.Mappert.Practises;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class ArraySplitterSourceModel
    {
        public string Values { get; set; } = "1,2,3";
    }

    public class ArraySplitterTargetModel
    {
        public string Value1 { get; set; }

        public string Value2 { get; set; }

        public string Value3 { get; set; }
    }

    public class ArraySplitterMapper :
        Mapper<ArraySplitterSourceModel, ArraySplitterTargetModel>
    {
        public ArraySplitterMapper(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            WithMappingDelegateBuilder(mappingDelegateBuilder);

            Map(s => s.Values)
                .To(t => t.Value1, t => t.Value2, t => t.Value3)
                .Using(StringArraySplitter);
        }

        private object[] StringArraySplitter(
            string values)
        {
            return values.Split(',');
        }
    }
}