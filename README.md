# NumCSharp
A C# numerical computing library inspired by NumPy and NumCpp, leveraging System.Numerics for high-performance vector and matrix operations.

## Benchmark Results

BenchmarkDotNet v0.15.0, Linux Debian GNU/Linux 12 (bookworm) (container)
-
.NET SDK 9.0.300
  [Host]     : .NET 8.0.16 (8.0.1625.21506), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 8.0.16 (8.0.1625.21506), Arm64 RyuJIT AdvSIMD


| Method             | Mean        | Error      | StdDev    | Median      | Gen0    | Allocated |
|------------------- |------------:|-----------:|----------:|------------:|--------:|----------:|
| SystemNumericsGemm | 6,717.28 us | 132.374 us | 130.01 us | 6,632.99 us | 15.6250 |  64.09 KB |
| OpenBlasGemm       |    63.35 us |   7.287 us |  21.37 us |    57.58 us | 15.6250 |  64.09 KB |

// * Hints *
Outliers
  GemmBenchmark.SystemNumericsGemm: Default -> 3 outliers were removed (7.51 ms..8.25 ms)
  GemmBenchmark.OpenBlasGemm: Default       -> 1 outlier  was  removed (136.34 us)

// * Legends *
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Median    : Value separating the higher half of all measurements (50th percentile)
  Gen0      : GC Generation 0 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 us      : 1 Microsecond (0.000001 sec)

// * Diagnostic Output - MemoryDiagnoser *


// ***** BenchmarkRunner: End *****
Run time: 00:02:07 (127.26 sec), executed benchmarks: 2

Global total time: 00:02:11 (131.9 sec), executed benchmarks: 2