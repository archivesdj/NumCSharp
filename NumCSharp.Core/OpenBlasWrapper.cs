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
            if (a.Shape[1] != b.Shape[0]) throw new ArgumentException("Matrix dimensions must match.");
            var result = new NdArray<float>(new[] { a.Shape[0], b.Shape[1] });
            cblas_sgemm(101, 111, 111, a.Shape[0], b.Shape[1], a.Shape[1], 1.0f, a.GetData(), a.Shape[1],
                b.GetData(), b.Shape[1], 0.0f, result.GetData(), b.Shape[1]);
            return result;
        }
    }
}