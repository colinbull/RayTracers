``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 7 SP1 (6.1.7601.0)
Intel Xeon CPU E5-2687W v2 3.40GHz, 1 CPU, 16 logical and 8 physical cores
Frequency=3312382 Hz, Resolution=301.8975 ns, Timer=TSC
.NET Core SDK=2.1.500
  [Host]     : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT DEBUG
  DefaultJob : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT


```
|                 Method |     Mean |    Error |   StdDev |  Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------- |---------:|---------:|---------:|-------------:|------------:|------------:|--------------------:|
|                Classes |  6.761 s | 0.0279 s | 0.0261 s | 1752000.0000 |   1000.0000 |           - |         10523.26 MB |
|                Records |  7.004 s | 0.0224 s | 0.0210 s | 1828000.0000 |   1000.0000 |           - |         10975.32 MB |
|          StructRecords | 14.778 s | 0.2386 s | 0.1992 s |   10000.0000 |           - |           - |            72.04 MB |
|        StructRecords32 | 13.975 s | 0.2757 s | 0.3175 s |    9000.0000 |           - |           - |            63.97 MB |
| StructRecordsNoMethods | 11.648 s | 0.2308 s | 0.5350 s |    8000.0000 |           - |           - |            54.93 MB |
