using NUnit.Framework;

namespace OffSync.Mapping.Mappert.Benchmarks.Tests
{
    [TestFixture]
    public class MapperPerformanceTest
    {
        private MapperPerformance _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MapperPerformance();

            _sut.SetUp();
        }

        [Test]
        public void ShouldRunBenchmarkMethods()
        {
            _sut.MapEmpty();

            _sut.MapSimple();

            _sut.MapArraySplitter();

            _sut.MapTupleSplitter();

            _sut.MapCyclic();
        }
    }
}
