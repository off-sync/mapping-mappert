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
|         **MapEmpty** |             **Reflection** |   **132.0 ns** | **0.4217 ns** | **0.3944 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |             Reflection |   808.4 ns | 0.8801 ns | 0.7802 ns |  6.12 |    0.02 |    2 |
| MapArraySplitter |             Reflection | 1,342.9 ns | 2.9925 ns | 2.6528 ns | 10.17 |    0.04 |    3 |
| MapTupleSplitter |             Reflection | 1,577.1 ns | 3.0172 ns | 2.8223 ns | 11.95 |    0.03 |    4 |
|                  |                        |            |           |           |       |         |      |
|         **MapEmpty** |          **DynamicMethod** |   **131.4 ns** | **0.2893 ns** | **0.2564 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |          DynamicMethod |   130.1 ns | 0.2000 ns | 0.1871 ns |  0.99 |    0.00 |    1 |
| MapArraySplitter |          DynamicMethod |   236.6 ns | 0.5836 ns | 0.5459 ns |  1.80 |    0.01 |    3 |
| MapTupleSplitter |          DynamicMethod |   222.3 ns | 0.5963 ns | 0.5578 ns |  1.69 |    0.00 |    2 |
