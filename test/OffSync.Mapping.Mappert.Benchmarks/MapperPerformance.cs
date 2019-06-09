using BenchmarkDotNet.Attributes;

using OffSync.Mapping.Mappert.DynamicMethods;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Reflection;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    [CoreJob, ClrJob]
    [RankColumn]
    public class MapperPerformance
    {
        public enum MappingDelegateBuilderTypes
        {
            Reflection,
            DynamicMethod,
        }

        private Mapper<EmptySourceModel, EmptyTargetModel> _emptyMapper;

        private readonly EmptySourceModel _emptySource = new EmptySourceModel();

        private Mapper<SimpleSourceModel, SimpleTargetModel> _simpleMapper;

        private readonly SimpleSourceModel _simpleSource = new SimpleSourceModel();

        private Mapper<ArraySplitterSourceModel, ArraySplitterTargetModel> _arraySplitterMapper;

        private readonly ArraySplitterSourceModel _arraySplitterSource = new ArraySplitterSourceModel();

        private Mapper<TupleSplitterSourceModel, TupleSplitterTargetModel> _tupleSplitterMapper;

        private readonly TupleSplitterSourceModel _tupleSplitterSource = new TupleSplitterSourceModel();

        private readonly IMappingDelegateBuilder[] _mappingDelegateBuilders = new IMappingDelegateBuilder[]
        {
            new ReflectionMappingDelegateBuilder(),
            new DynamicMethodMappingDelegateBuilder(),
        };

        [Params(MappingDelegateBuilderTypes.Reflection, MappingDelegateBuilderTypes.DynamicMethod)]
        public MappingDelegateBuilderTypes MappingDelegateBuilder { get; set; }

        [GlobalSetup]
        public void SetUp()
        {
            var mappingDelegateBuilder = _mappingDelegateBuilders[(int)MappingDelegateBuilder];

            _emptyMapper = new EmptyMapper(mappingDelegateBuilder);

            _emptyMapper.Map(_emptySource);

            _simpleMapper = new SimpleMapper(mappingDelegateBuilder);

            _simpleMapper.Map(_simpleSource);

            _arraySplitterMapper = new ArraySplitterMapper(mappingDelegateBuilder);

            _arraySplitterMapper.Map(_arraySplitterSource);

            _tupleSplitterMapper = new TupleSplitterMapper(mappingDelegateBuilder);

            _tupleSplitterMapper.Map(_tupleSplitterSource);
        }

        [Benchmark(Baseline = true)]
        public EmptyTargetModel MapEmpty() => _emptyMapper.Map(_emptySource);

        [Benchmark]
        public SimpleTargetModel MapSimple() => _simpleMapper.Map(_simpleSource);

        [Benchmark]
        public ArraySplitterTargetModel MapArraySplitter() => _arraySplitterMapper.Map(_arraySplitterSource);

        [Benchmark]
        public TupleSplitterTargetModel MapTupleSplitter() => _tupleSplitterMapper.Map(_tupleSplitterSource);
    }
}
