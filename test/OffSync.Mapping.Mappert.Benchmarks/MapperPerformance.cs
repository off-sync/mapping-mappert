/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using BenchmarkDotNet.Attributes;

using OffSync.Mapping.Mappert.DynamicMethods;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Mappert.Reflection;
using OffSync.Mapping.Practises;

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

        private Mapper<CyclicSourceModel, CyclicTargetModel> _cyclicMapper;

        private readonly CyclicSourceModel _cyclicSource = CyclicSourceModel.Create();

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

            _cyclicMapper = new CyclicMapper(mappingDelegateBuilder);

            _cyclicMapper.MapRoot(_cyclicSource);
        }

        [Benchmark(Baseline = true)]
        public EmptyTargetModel MapEmpty() => _emptyMapper.Map(_emptySource);

        [Benchmark]
        public SimpleTargetModel MapSimple() => _simpleMapper.Map(_simpleSource);

        [Benchmark]
        public ArraySplitterTargetModel MapArraySplitter() => _arraySplitterMapper.Map(_arraySplitterSource);

        [Benchmark]
        public TupleSplitterTargetModel MapTupleSplitter() => _tupleSplitterMapper.Map(_tupleSplitterSource);

        [Benchmark]
        public CyclicTargetModel MapCyclic() => _cyclicMapper.MapRoot(_cyclicSource);
    }
}
