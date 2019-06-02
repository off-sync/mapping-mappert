# Off-Sync.com Mappert Benchmarks

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.475 (1809/October2018Update/Redstone5)
Intel Core i7-4700HQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3761.0
  Core   : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
```

|           Method |  Job | Runtime | MappingDelegateBuilder |       Mean |      Error |     StdDev | Ratio | RatioSD | Rank |
|----------------- |----- |-------- |----------------------- |-----------:|-----------:|-----------:|------:|--------:|-----:|
|         **MapEmpty** |  **Clr** |     **Clr** |             **Reflection** |   **157.6 ns** |  **0.3851 ns** |  **0.3215 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |  Clr |     Clr |             Reflection | 1,033.1 ns |  5.7447 ns |  5.3736 ns |  6.55 |    0.04 |    2 |
| MapArraySplitter |  Clr |     Clr |             Reflection | 1,394.5 ns |  6.9451 ns |  5.4223 ns |  8.85 |    0.03 |    3 |
| MapTupleSplitter |  Clr |     Clr |             Reflection | 1,761.0 ns | 13.4323 ns | 12.5646 ns | 11.17 |    0.09 |    4 |
|                  |      |         |                        |            |            |            |       |         |      |
|         MapEmpty | Core |    Core |             Reflection |   134.0 ns |  0.5336 ns |  0.4991 ns |  1.00 |    0.00 |    1 |
|        MapSimple | Core |    Core |             Reflection |   822.0 ns | 14.6643 ns | 13.7170 ns |  6.14 |    0.10 |    2 |
| MapArraySplitter | Core |    Core |             Reflection | 1,225.9 ns |  4.6792 ns |  3.9073 ns |  9.15 |    0.04 |    3 |
| MapTupleSplitter | Core |    Core |             Reflection | 1,548.4 ns |  2.9322 ns |  2.5993 ns | 11.56 |    0.06 |    4 |
|                  |      |         |                        |            |            |            |       |         |      |
|         **MapEmpty** |  **Clr** |     **Clr** |          **DynamicMethod** |   **161.5 ns** |  **0.1275 ns** |  **0.1131 ns** |  **1.00** |    **0.00** |    **1** |
|        MapSimple |  Clr |     Clr |          DynamicMethod |   189.2 ns |  1.0810 ns |  1.0111 ns |  1.17 |    0.01 |    2 |
| MapArraySplitter |  Clr |     Clr |          DynamicMethod |   301.5 ns |  0.2781 ns |  0.2171 ns |  1.87 |    0.00 |    4 |
| MapTupleSplitter |  Clr |     Clr |          DynamicMethod |   284.6 ns |  2.0257 ns |  1.7957 ns |  1.76 |    0.01 |    3 |
|                  |      |         |                        |            |            |            |       |         |      |
|         MapEmpty | Core |    Core |          DynamicMethod |   135.3 ns |  2.7886 ns |  3.5267 ns |  1.00 |    0.00 |    1 |
|        MapSimple | Core |    Core |          DynamicMethod |   132.3 ns |  0.7516 ns |  0.7030 ns |  0.98 |    0.02 |    1 |
| MapArraySplitter | Core |    Core |          DynamicMethod |   235.0 ns |  0.4121 ns |  0.3855 ns |  1.74 |    0.04 |    3 |
| MapTupleSplitter | Core |    Core |          DynamicMethod |   220.7 ns |  1.5930 ns |  1.4901 ns |  1.63 |    0.04 |    2 |
