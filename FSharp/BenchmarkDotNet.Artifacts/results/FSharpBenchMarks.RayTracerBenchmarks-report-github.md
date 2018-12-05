``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17746
Intel Core i7-6600U CPU 2.60GHz (Skylake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.403
  [Host]     : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT DEBUG
  DefaultJob : .NET Core 2.1.5 (CoreCLR 4.6.26919.02, CoreFX 4.6.26919.02), 64bit RyuJIT


```
|                      Method |     Mean |    Error |   StdDev |   Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |---------:|---------:|---------:|--------------:|------------:|------------:|--------------------:|
|                     Classes |  51.26 s | 0.8837 s | 0.7834 s | 42337000.0000 |   5000.0000 |           - |         84675.73 MB |
|                     Records |  51.18 s | 0.9521 s | 0.9351 s | 44203000.0000 |   4000.0000 |           - |         88408.34 MB |
|               StructRecords |  42.52 s | 0.5947 s | 0.5272 s |   535000.0000 |   4000.0000 |           - |           1070.3 MB |
|             StructRecords32 |  40.18 s | 0.4516 s | 0.4003 s |   472000.0000 |   4000.0000 |           - |            945.2 MB |
|      StructRecordsNoMethods |  64.06 s | 0.6129 s | 0.5733 s |   466000.0000 |   6000.0000 |           - |           933.37 MB |
|                  AllStructs | 108.22 s | 1.0598 s | 0.9395 s |   154000.0000 |  10000.0000 |   1000.0000 |           308.94 MB |
| AllStructsNoCompareEquality | 107.97 s | 0.4609 s | 0.4312 s |   154000.0000 |  10000.0000 |   1000.0000 |           308.81 MB |
