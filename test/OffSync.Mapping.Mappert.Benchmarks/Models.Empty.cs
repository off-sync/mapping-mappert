using OffSync.Mapping.Mappert.Practises;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    public class EmptySourceModel
    {
    }

    public class EmptyTargetModel
    {
    }

    public class EmptyMapper :
        Mapper<EmptySourceModel, EmptyTargetModel>
    {
        public EmptyMapper(
            IMappingDelegateBuilder mappingDelegateBuilder)
        {
            WithMappingDelegateBuilder(mappingDelegateBuilder);
        }
    }
}