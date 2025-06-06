using System.Runtime.InteropServices;

namespace NumCSharp.Core
{
    public static class OpenBlasWrapper
    {
        [DllImport("libopenblas.so", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cblas_sgemm(int layout, int transa, int transb, int m, int n, int k,
            float alpha, float[] a, int lda, float[] b, int ldb, float beta, float[] c, int ldc);

        public static NdArray<float> Dot(NdArray<float> a, NdArray<float> b)
        {
            if (a._shape[1] != b._shape[0]) throw new ArgumentException("Matrix dimensions must match.");
            var result = new NdArray<float>(new[] { a._shape[0], b._shape[1] });
            cblas_sgemm(101, 111, 111, a._shape[0], b._shape[1], a._shape[1], 1.0f, a.GetData(), a._shape[1],
                b.GetData(), b._shape[1], 0.0f, result.GetData(), b._shape[1]);
            return result;
        }
    }
}