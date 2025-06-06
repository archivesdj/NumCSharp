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
