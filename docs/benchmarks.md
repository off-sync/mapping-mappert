# Off-Sync.com Mappert Benchmarks

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.475 (1809/October2018Update/Redstone5)
Intel Core i7-4700HQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview4-011223
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Core   : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Job=Core  Runtime=Core  
```

|           Method |       Mean |     Error |    StdDev | Ratio | RatioSD | Rank |
|----------------- |-----------:|----------:|----------:|------:|--------:|-----:|
|         MapEmpty |   130.9 ns | 0.1667 ns | 0.1477 ns |  1.00 |    0.00 |    1 |
|        MapSimple |   836.9 ns | 1.8635 ns | 1.7431 ns |  6.39 |    0.02 |    2 |
| MapArraySplitter | 2,010.6 ns | 3.8129 ns | 3.1840 ns | 15.36 |    0.03 |    3 |
| MapTupleSplitter | 2,431.0 ns | 6.6690 ns | 6.2382 ns | 18.57 |    0.05 |    4 |
