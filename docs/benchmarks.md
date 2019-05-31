# Off-Sync.com Mappert Benchmarks

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.475 (1809/October2018Update/Redstone5)
Intel Core i7-4700HQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview4-011223
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Core   : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Job=Core  Runtime=Core  
```

|           Method | MappingDelegateBuilder |       Mean |     Error |    StdDev | Ratio | RatioSD | Rank |
|----------------- |----------------------- |-----------:|----------:|----------:|------:|--------:|-----:|
|         **MapEmpty** |             **Reflection** |   **150.1 ns** | **0.1731 ns** | **0.1619 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |             Reflection |   820.2 ns | 4.5959 ns | 4.2990 ns |  5.46 |    0.03 |    2 |
| MapArraySplitter |             Reflection | 1,225.5 ns | 0.6744 ns | 0.5632 ns |  8.17 |    0.01 |    3 |
| MapTupleSplitter |             Reflection | 1,571.2 ns | 9.6144 ns | 8.9933 ns | 10.47 |    0.06 |    4 |
|                  |                        |            |           |           |       |         |      |
|         **MapEmpty** |          **DynamicMethod** |   **133.2 ns** | **0.1891 ns** | **0.1579 ns** |  **1.00** |    **0.00** |    **2** |
|        MapSimple |          DynamicMethod |   129.0 ns | 0.2428 ns | 0.2027 ns |  0.97 |    0.00 |    1 |
| MapArraySplitter |          DynamicMethod |   236.9 ns | 0.5915 ns | 0.5533 ns |  1.78 |    0.01 |    4 |
| MapTupleSplitter |          DynamicMethod |   218.9 ns | 0.3969 ns | 0.3713 ns |  1.64 |    0.00 |    3 |
