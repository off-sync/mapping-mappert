# Off-Sync.com Mappert Benchmarks

```ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-4700HQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
```

|           Method |  Job | Runtime | MappingDelegateBuilder |       Mean |     Error |    StdDev | Ratio | RatioSD | Rank |
|----------------- |----- |-------- |----------------------- |-----------:|----------:|----------:|------:|--------:|-----:|
|         **MapEmpty** |  **Clr** |     **Clr** |             **Reflection** |   **176.7 ns** | **0.8031 ns** | **0.7513 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |  Clr |     Clr |             Reflection | 1,010.5 ns | 1.0255 ns | 0.9091 ns |  5.72 |    0.02 |    2 |
| MapArraySplitter |  Clr |     Clr |             Reflection | 1,382.1 ns | 1.4384 ns | 1.2011 ns |  7.82 |    0.03 |    3 |
| MapTupleSplitter |  Clr |     Clr |             Reflection | 1,718.2 ns | 2.1069 ns | 1.8677 ns |  9.72 |    0.05 |    4 |
|                  |      |         |                        |            |           |           |       |         |      |
|         MapEmpty | Core |    Core |             Reflection |   150.7 ns | 0.3314 ns | 0.2938 ns |  1.00 |    0.00 |    1 |
|        MapSimple | Core |    Core |             Reflection |   873.0 ns | 0.4940 ns | 0.4126 ns |  5.79 |    0.01 |    2 |
| MapArraySplitter | Core |    Core |             Reflection | 1,263.1 ns | 1.8828 ns | 1.6690 ns |  8.38 |    0.02 |    3 |
| MapTupleSplitter | Core |    Core |             Reflection | 1,601.8 ns | 1.4615 ns | 1.2204 ns | 10.63 |    0.02 |    4 |
|                  |      |         |                        |            |           |           |       |         |      |
|         **MapEmpty** |  **Clr** |     **Clr** |          **DynamicMethod** |   **181.5 ns** | **0.7033 ns** | **0.6235 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |  Clr |     Clr |          DynamicMethod |   213.2 ns | 0.3086 ns | 0.2887 ns |  1.17 |    0.00 |    2 |
| MapArraySplitter |  Clr |     Clr |          DynamicMethod |   325.4 ns | 0.3486 ns | 0.2911 ns |  1.79 |    0.01 |    4 |
| MapTupleSplitter |  Clr |     Clr |          DynamicMethod |   305.8 ns | 0.3322 ns | 0.3107 ns |  1.68 |    0.01 |    3 |
|                  |      |         |                        |            |           |           |       |         |      |
|         MapEmpty | Core |    Core |          DynamicMethod |   151.5 ns | 0.1343 ns | 0.1122 ns |  1.00 |    0.00 |    2 |
|        MapSimple | Core |    Core |          DynamicMethod |   149.6 ns | 0.5707 ns | 0.5338 ns |  0.99 |    0.00 |    1 |
| MapArraySplitter | Core |    Core |          DynamicMethod |   255.0 ns | 0.2112 ns | 0.1872 ns |  1.68 |    0.00 |    4 |
| MapTupleSplitter | Core |    Core |          DynamicMethod |   238.6 ns | 0.2093 ns | 0.1855 ns |  1.57 |    0.00 |    3 |
