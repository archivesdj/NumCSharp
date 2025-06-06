# NumCSharp
A C# numerical computing library inspired by NumPy and NumCpp, leveraging System.Numerics for high-performance vector and matrix operations.

## Benchmark Results

// Benchmark Process Environment Information:
// BenchmarkDotNet v0.15.0
// Runtime=.NET 8.0.16 (8.0.1625.21506), Arm64 RyuJIT AdvSIMD
// GC=Concurrent Workstation
// HardwareIntrinsics=AdvSIMD,AES,CRC32,DP,RDM,SHA1,SHA256 VectorSize=128
// Job: DefaultJob

...

// Benchmark Process 3502 has exited with code 0.

Mean = 63.353 us, StdErr = 2.148 us (3.39%), N = 99, StdDev = 21.372 us
Min = 37.886 us, Q1 = 44.165 us, Median = 57.575 us, Q3 = 78.609 us, Max = 126.344 us
IQR = 34.444 us, LowerFence = -7.501 us, UpperFence = 130.275 us
ConfidenceInterval = [56.066 us; 70.640 us] (CI 99.9%), Margin = 7.287 us (11.50% of Mean)
Skewness = 0.8, Kurtosis = 2.8, MValue = 2.46

// ** Remained 0 (0.0%) benchmark(s) to run. Estimated finish 2025-06-06 2:47 (0h 0m from now) **
// ***** BenchmarkRunner: Finish  *****

// * Export *
  BenchmarkDotNet.Artifacts/results/NumCSharp.Benchmarks.GemmBenchmark-report.csv
  BenchmarkDotNet.Artifacts/results/NumCSharp.Benchmarks.GemmBenchmark-report-github.md
  BenchmarkDotNet.Artifacts/results/NumCSharp.Benchmarks.GemmBenchmark-report.html

// * Detailed results *
GemmBenchmark.SystemNumericsGemm: DefaultJob
Runtime = .NET 8.0.16 (8.0.1625.21506), Arm64 RyuJIT AdvSIMD; GC = Concurrent Workstation
Mean = 6.717 ms, StdErr = 0.033 ms (0.48%), N = 16, StdDev = 0.130 ms
Min = 6.604 ms, Q1 = 6.622 ms, Median = 6.633 ms, Q3 = 6.788 ms, Max = 6.978 ms
IQR = 0.165 ms, LowerFence = 6.374 ms, UpperFence = 7.036 ms
ConfidenceInterval = [6.585 ms; 6.850 ms] (CI 99.9%), Margin = 0.132 ms (1.97% of Mean)
Skewness = 0.88, Kurtosis = 2.15, MValue = 2
-------------------- Histogram --------------------
[6.598 ms ; 7.002 ms) | @@@@@@@@@@@@@@@@
---------------------------------------------------

GemmBenchmark.OpenBlasGemm: DefaultJob
Runtime = .NET 8.0.16 (8.0.1625.21506), Arm64 RyuJIT AdvSIMD; GC = Concurrent Workstation
Mean = 63.353 us, StdErr = 2.148 us (3.39%), N = 99, StdDev = 21.372 us
Min = 37.886 us, Q1 = 44.165 us, Median = 57.575 us, Q3 = 78.609 us, Max = 126.344 us
IQR = 34.444 us, LowerFence = -7.501 us, UpperFence = 130.275 us
ConfidenceInterval = [56.066 us; 70.640 us] (CI 99.9%), Margin = 7.287 us (11.50% of Mean)
Skewness = 0.8, Kurtosis = 2.8, MValue = 2.46
-------------------- Histogram --------------------
[ 37.414 us ;  49.542 us) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[ 49.542 us ;  62.199 us) | @@@@@@@@@@@@@@@@@@@@@@@
[ 62.199 us ;  73.843 us) | @@@@@@@@@@
[ 73.843 us ;  85.970 us) | @@@@@@@@@@@@@@@@
[ 85.970 us ;  99.766 us) | @@@@@@@@
[ 99.766 us ; 111.893 us) | @@@@@
[111.893 us ; 116.650 us) | 
[116.650 us ; 128.777 us) | @@
---------------------------------------------------

// * Summary *

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