# Off-Sync.com Mappert Benchmarks

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.475 (1809/October2018Update/Redstone5)
Intel Core i7-4700HQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview4-011223
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Core   : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Job=Core  Runtime=Core  
```

|           Method | MappingDelegateBuilder |       Mean |      Error |     StdDev | Ratio | RatioSD | Rank |
|----------------- |----------------------- |-----------:|-----------:|-----------:|------:|--------:|-----:|
|         **MapEmpty** |             **Reflection** |   **136.2 ns** |   **2.761 ns** |   **2.448 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |             Reflection |   814.9 ns |   6.944 ns |   6.155 ns |  5.99 |    0.11 |    2 |
| MapArraySplitter |             Reflection | 1,934.0 ns |  11.061 ns |  10.347 ns | 14.21 |    0.27 |    3 |
| MapTupleSplitter |             Reflection | 2,388.4 ns |  41.148 ns |  36.476 ns | 17.55 |    0.42 |    4 |
|                  |                        |            |            |            |       |         |      |
|         **MapEmpty** |          **DynamicMethod** |   **133.8 ns** |   **2.688 ns** |   **2.383 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |          DynamicMethod |   137.1 ns |   2.569 ns |   2.403 ns |  1.02 |    0.02 |    1 |
| MapArraySplitter |          DynamicMethod | 1,040.9 ns |  17.747 ns |  15.733 ns |  7.78 |    0.24 |    2 |
| MapTupleSplitter |          DynamicMethod | 1,515.0 ns | 144.411 ns | 425.800 ns | 11.87 |    1.86 |    3 |
