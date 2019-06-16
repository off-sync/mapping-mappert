# Off-Sync.com Mappert Benchmarks

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-4700HQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
```

|           Method |  Job | Runtime | MappingDelegateBuilder |       Mean |      Error |     StdDev | Ratio | RatioSD | Rank |
|----------------- |----- |-------- |----------------------- |-----------:|-----------:|-----------:|------:|--------:|-----:|
|         **MapEmpty** |  **Clr** |     **Clr** |             **Reflection** |   **196.9 ns** |  **1.3938 ns** |  **1.2356 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |  Clr |     Clr |             Reflection | 4,074.9 ns |  9.7963 ns |  8.6842 ns | 20.70 |    0.15 |    4 |
| MapArraySplitter |  Clr |     Clr |             Reflection | 1,675.4 ns |  5.3016 ns |  4.9591 ns |  8.51 |    0.05 |    2 |
| MapTupleSplitter |  Clr |     Clr |             Reflection | 2,201.7 ns |  5.2548 ns |  4.6582 ns | 11.19 |    0.08 |    3 |
|        MapCyclic |  Clr |     Clr |             Reflection | 4,089.4 ns | 13.3577 ns | 12.4948 ns | 20.77 |    0.12 |    4 |
|                  |      |         |                        |            |            |            |       |         |      |
|         MapEmpty | Core |    Core |             Reflection |   164.3 ns |  0.4258 ns |  0.3983 ns |  1.00 |    0.00 |    1 |
|        MapSimple | Core |    Core |             Reflection | 3,531.0 ns | 25.9977 ns | 24.3183 ns | 21.50 |    0.17 |    4 |
| MapArraySplitter | Core |    Core |             Reflection | 1,562.4 ns |  2.6693 ns |  2.4969 ns |  9.51 |    0.03 |    2 |
| MapTupleSplitter | Core |    Core |             Reflection | 2,078.2 ns |  6.6921 ns |  6.2598 ns | 12.65 |    0.05 |    3 |
|        MapCyclic | Core |    Core |             Reflection | 3,807.8 ns | 10.6963 ns | 10.0053 ns | 23.18 |    0.08 |    5 |
|                  |      |         |                        |            |            |            |       |         |      |
|         **MapEmpty** |  **Clr** |     **Clr** |          **DynamicMethod** |   **201.6 ns** |  **0.6267 ns** |  **0.5556 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |  Clr |     Clr |          DynamicMethod |   577.1 ns |  1.9049 ns |  1.7819 ns |  2.86 |    0.01 |    4 |
| MapArraySplitter |  Clr |     Clr |          DynamicMethod |   364.6 ns |  5.4565 ns |  4.5564 ns |  1.81 |    0.03 |    2 |
| MapTupleSplitter |  Clr |     Clr |          DynamicMethod |   367.8 ns |  0.8093 ns |  0.7174 ns |  1.82 |    0.00 |    3 |
|        MapCyclic |  Clr |     Clr |          DynamicMethod | 1,677.2 ns |  5.7254 ns |  5.3555 ns |  8.32 |    0.03 |    5 |
|                  |      |         |                        |            |            |            |       |         |      |
|         MapEmpty | Core |    Core |          DynamicMethod |   167.8 ns |  1.7577 ns |  1.6442 ns |  1.00 |    0.00 |    1 |
|        MapSimple | Core |    Core |          DynamicMethod |   364.0 ns |  1.0778 ns |  1.0082 ns |  2.17 |    0.02 |    4 |
| MapArraySplitter | Core |    Core |          DynamicMethod |   289.7 ns |  1.2905 ns |  1.0777 ns |  1.73 |    0.02 |    2 |
| MapTupleSplitter | Core |    Core |          DynamicMethod |   296.1 ns |  1.6434 ns |  1.5372 ns |  1.76 |    0.02 |    3 |
|        MapCyclic | Core |    Core |          DynamicMethod | 1,414.7 ns |  2.2651 ns |  2.1188 ns |  8.43 |    0.09 |    5 |
