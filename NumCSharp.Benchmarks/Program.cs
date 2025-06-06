using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NumCSharp.Core;
using System;

namespace NumCSharp.Benchmarks
{
    [MemoryDiagnoser]
    public class GemmBenchmark
    {
        private NdArray<float> a, b, c;
        private const int m = 128, n = 128, k = 128;

        [GlobalSetup]
        public void Setup()
        {
            a = new NdArray<float>(new[] { m, k });
            b = new NdArray<float>(new[] { k, n });
            c = new NdArray<float>(new[] { m, n });
            var rand = new Random(42);
            for (int i = 0; i < m; i++)
                for (int j = 0; j < k; j++)
                    a[i, j] = (float)rand.NextDouble();
            for (int i = 0; i < k; i++)
                for (int j = 0; j < n; j++)
                    b[i, j] = (float)rand.NextDouble();
        }

        [Benchmark]
        public void SystemNumericsGemm()
        {
            var result = NdArray<float>.Dot(a, b);
        }

        [Benchmark]
        public void OpenBlasGemm()
        {
            var result = OpenBlasWrapper.Dot(a, b);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<GemmBenchmark>();
        }
    }
}