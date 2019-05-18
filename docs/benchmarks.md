# Off-Sync.com Mappert Benchmarks

``` ini
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.475 (1809/October2018Update/Redstone5)
Intel Core i7-4700HQ CPU 2.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview4-011223
  [Host] : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT
  Core   : .NET Core 2.2.5 (CoreCLR 4.6.27617.05, CoreFX 4.6.27618.01), 64bit RyuJIT

Job=Core  Runtime=Core  
```

|           Method |       Mean |     Error |   StdDev | Ratio | RatioSD | Rank |
|----------------- |-----------:|----------:|---------:|------:|--------:|-----:|
|         MapEmpty |   131.3 ns |  1.747 ns | 1.634 ns |  1.00 |    0.00 |    1 |
|        MapSimple |   828.3 ns |  1.079 ns | 1.010 ns |  6.31 |    0.08 |    2 |
| MapArraySplitter | 1,949.2 ns | 10.387 ns | 9.716 ns | 14.85 |    0.22 |    3 |
| MapTupleSplitter | 2,419.6 ns |  3.317 ns | 2.940 ns | 18.42 |    0.23 |    4 |
