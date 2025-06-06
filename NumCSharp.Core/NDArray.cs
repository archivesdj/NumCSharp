using System;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace NumCSharp.Core
{
    public class NdArray<T> where T : unmanaged
    {
        private readonly T[] _data;
        private readonly int[] _shape;

        public NdArray(int[] shape)
        {
            _shape = shape;
            int size = 1;
            foreach (var dim in shape) size *= dim;
            _data = new T[size];
        }

        public T this[int i, int j]
        {
            get => _data[i * _shape[1] + j];
            set => _data[i * _shape[1] + j] = value;
        }

        public T[] GetData() => _data;
        public static NdArray<float> Dot(NdArray<float> a, NdArray<float> b)
        {
            if (a._shape[1] != b._shape[0]) throw new ArgumentException("Matrix dimensions must match.");
            var result = new NdArray<float>(new[] { a._shape[0], b._shape[1] });
            if (Avx2.IsSupported)
            {
                unsafe
                {
                    fixed (float* pa = a._data, pb = b._data, pc = result._data)
                    {
                        for (int i = 0; i < a._shape[0]; i++)
                        {
                            for (int j = 0; j < b._shape[1]; j++)
                            {
                                Vector256<float> sum = Vector256<float>.Zero;
                                for (int k = 0; k < a._shape[1]; k += 8)
                                {
                                    if (k + 8 <= a._shape[1])
                                    {
                                        Vector256<float> va = Avx2.LoadVector256(pa + i * a._shape[1] + k);
                                        Vector256<float> vb = Avx2.LoadVector256(pb + k * b._shape[1] + j);
                                        sum = Avx2.Add(sum, Avx2.Multiply(va, vb));
                                    }
                                    else
                                    {
                                        for (int kk = k; kk < a._shape[1]; kk++)
                                            pc[i * b._shape[1] + j] += pa[i * a._shape[1] + kk] * pb[kk * b._shape[1] + j];
                                    }
                                }
                                pc[i * b._shape[1] + j] = Avx2.HorizontalAdd(sum);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < a._shape[0]; i++)
                    for (int j = 0; j < b._shape[1]; j++)
                        for (int k = 0; k < a._shape[1]; k++)
                            result[i, j] += a[i, k] * b[k, j];
            }
            return result;
        }
    }
}