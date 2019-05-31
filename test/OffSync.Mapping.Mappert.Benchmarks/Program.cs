#if !PROFILER
using BenchmarkDotNet.Running;
#endif

namespace OffSync.Mapping.Mappert.Benchmarks
{
    static class Program
    {
        static void Main(
            string[] args)
        {
#if PROFILER
            var mapperPerformance = new MapperPerformance();

            mapperPerformance.SetUp();

            for (int i = 0; i < 5_000_000; i++)
            {
                mapperPerformance.MapTupleSplitter();
            }
#else
            BenchmarkRunner.Run<MapperPerformance>();
#endif
        }
    }
}
