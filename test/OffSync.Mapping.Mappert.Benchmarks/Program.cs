/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Off-Sync.com. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Diagnostics.CodeAnalysis;

#if !PROFILER
using BenchmarkDotNet.Running;
#else
using static OffSync.Mapping.Mappert.Benchmarks.MapperPerformance;
#endif

namespace OffSync.Mapping.Mappert.Benchmarks
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        static void Main(
            string[] args)
        {
#if PROFILER
            var mapperPerformance = new MapperPerformance();

            mapperPerformance.MappingDelegateBuilder = MappingDelegateBuilderTypes.DynamicMethod;

            mapperPerformance.SetUp();

            for (int i = 0; i < 5_000_000; i++)
            {
                mapperPerformance.MapCyclic();
            }
#else
            BenchmarkRunner.Run<MapperPerformance>();
#endif
        }
    }
}
