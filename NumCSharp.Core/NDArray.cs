using System;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace NumCSharp.Core
{
    public class NdArray<T> where T : unmanaged
    {
        private readonly T[] _data;
        public int[] Shape {get; private set;}

        public NdArray(int[] shape)
        {
            Shape = shape;
            int size = 1;
            foreach (var dim in shape) size *= dim;
            _data = new T[size];
        }

        public T this[int i, int j]
        {
            get => _data[i * Shape[1] + j];
            set => _data[i * Shape[1] + j] = value;
        }

        public T[] GetData() => _data;
        public static NdArray<float> Dot(NdArray<float> a, NdArray<float> b)
        {
            if (a.Shape[1] != b.Shape[0]) throw new ArgumentException("Matrix dimensions must match.");
            var result = new NdArray<float>(new[] { a.Shape[0], b.Shape[1] });
            if (Avx2.IsSupported)
            {
                unsafe
                {
                    fixed (float* pa = a._data, pb = b._data, pc = result._data)
                    {
                        for (int i = 0; i < a.Shape[0]; i++)
                        {
                            for (int j = 0; j < b.Shape[1]; j++)
                            {
                                Vector256<float> sum = Vector256<float>.Zero;
                                for (int k = 0; k < a.Shape[1]; k += 8)
                                {
                                    if (k + 8 <= a.Shape[1])
                                    {
                                        Vector256<float> va = Avx2.LoadVector256(pa + i * a.Shape[1] + k);
                                        Vector256<float> vb = Avx2.LoadVector256(pb + k * b.Shape[1] + j);
                                        sum = Avx2.Add(sum, Avx2.Multiply(va, vb));
                                    }
                                    else
                                    {
                                        for (int kk = k; kk < a.Shape[1]; kk++)
                                            pc[i * b.Shape[1] + j] += pa[i * a.Shape[1] + kk] * pb[kk * b.Shape[1] + j];
                                    }
                                }
                                pc[i * b.Shape[1] + j] = SumVector256(sum);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < a.Shape[0]; i++)
                    for (int j = 0; j < b.Shape[1]; j++)
                        for (int k = 0; k < a.Shape[1]; k++)
                            result[i, j] += a[i, k] * b[k, j];
            }
            return result;
        }

        private static float SumVector256(Vector256<float> vector)
        {
            // 상위/하위 128비트 분리
            Vector128<float> low = Avx2.ExtractVector128(vector, 0);
            Vector128<float> high = Avx2.ExtractVector128(vector, 1);
            // 128비트 내 합산
            Vector128<float> sum = Sse.Add(low, high);
            sum = Sse.Add(sum, Sse.Shuffle(sum, sum, 0x0E));
            sum = Sse.Add(sum, Sse.Shuffle(sum, sum, 0x01));
            return sum.ToScalar();
        }
    }
}