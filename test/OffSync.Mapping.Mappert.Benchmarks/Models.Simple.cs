using OffSync.Mapping.Mappert.Practises;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class SimpleSourceModel
    {
        public int Id { get; set; } = 1;

        public string Name { get; set; } = "2";

        public bool Ignored { get; set; }
    }

    public class SimpleTargetModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Excluded { get; set; }
    }

    public class SimpleMapper :
        Mapper<SimpleSourceModel, SimpleTargetModel>
    {
        public SimpleMapper(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            WithMappingDelegateBuilder(mappingDelegateBuilder);

            Map(s => s.Name)
                .To(t => t.Description);

            IgnoreSource(s => s.Ignored);

            IgnoreTarget(t => t.Excluded);
        }
    }
}