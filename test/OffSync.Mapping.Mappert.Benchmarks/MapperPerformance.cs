using BenchmarkDotNet.Attributes;

namespace OffSync.Mapping.Mappert.Benchmarks
{
    [CoreJob]
    //[ClrJob]
    [RankColumn]
    //[EtwProfiler]
    public class MapperPerformance
    {
        private readonly Mapper<EmptySourceModel, EmptyTargetModel> _emptyMapper = new EmptyMapper();

        private readonly EmptySourceModel _emptySource = new EmptySourceModel();

        private readonly Mapper<SimpleSourceModel, SimpleTargetModel> _simpleMapper = new SimpleMapper();

        private readonly SimpleSourceModel _simpleSource = new SimpleSourceModel();

        private readonly Mapper<ArraySplitterSourceModel, ArraySplitterTargetModel> _arraySplitterMapper = new ArraySplitterMapper();

        private readonly ArraySplitterSourceModel _arraySplitterSource = new ArraySplitterSourceModel();

        private readonly Mapper<TupleSplitterSourceModel, TupleSplitterTargetModel> _tupleSplitterMapper = new TupleSplitterMapper();

        private readonly TupleSplitterSourceModel _tupleSplitterSource = new TupleSplitterSourceModel();

        [GlobalSetup]
        public void SetUp()
        {
            _emptyMapper.Map(_emptySource);

            _simpleMapper.Map(_simpleSource);

            _arraySplitterMapper.Map(_arraySplitterSource);

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
