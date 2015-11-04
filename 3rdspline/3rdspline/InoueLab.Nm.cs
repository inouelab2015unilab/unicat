using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;  // projectの参照設定でSystem.Numericsを追加

namespace System.Linq
{
    #region Extension class

    public static partial class EnumerableEx
    {
        #region BigInteger
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, BigInteger> selector) { return source.Select(element => selector(element)).Average(); }
        public static BigInteger Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, BigInteger> selector) { return source.Select(element => selector(element)).Sum(); }
        public static double Average(this IEnumerable<BigInteger> source) { var r = source._Sum(); return (double)r.v0 / r.v1; }
        public static BigInteger Sum(this IEnumerable<BigInteger> source) { return source._Sum().v0; }
        static InoueLab.V2<BigInteger, int> _Sum(this IEnumerable<BigInteger> source)
        {
            var a = default(BigInteger);
            int c = 0;
            foreach (var e in source) { c++; a += e; }
            return InoueLab.New.V2(a, c);
        }
        public static double GeometricalAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, BigInteger> selector) { return source.Select(element => selector(element)).GeometricalAverage(); }
        public static BigInteger Product<TSource>(this IEnumerable<TSource> source, Func<TSource, BigInteger> selector) { return source.Select(element => selector(element)).Product(); }
        public static double GeometricalAverage(this IEnumerable<BigInteger> source) { var r = source._Product(); return Math.Pow((double)r.v0, 1.0 / r.v1); }
        public static BigInteger Product(this IEnumerable<BigInteger> source) { return source._Product().v0; }
        static InoueLab.V2<BigInteger, int> _Product(this IEnumerable<BigInteger> source)
        {
            BigInteger a = 1;
            int c = 0;
            foreach (var e in source) { c++; a *= e; }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region Complex
        public static Complex Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Complex> selector) { return source.Select(element => selector(element)).Average(); }
        public static Complex Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Complex> selector) { return source.Select(element => selector(element)).Sum(); }
        public static Complex Average(this IEnumerable<Complex> source) { var r = source._Sum(); return r.v0 / r.v1; }
        public static Complex Sum(this IEnumerable<Complex> source) { return source._Sum().v0; }
        static InoueLab.V2<Complex, int> _Sum(this IEnumerable<Complex> source)
        {
            var a = default(Complex);
            int c = 0;
            foreach (var e in source) { c++; a += e; }
            return InoueLab.New.V2(a, c);
        }
        public static Complex GeometricalAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, Complex> selector) { return source.Select(element => selector(element)).GeometricalAverage(); }
        public static Complex Product<TSource>(this IEnumerable<TSource> source, Func<TSource, Complex> selector) { return source.Select(element => selector(element)).Product(); }
        public static Complex GeometricalAverage(this IEnumerable<Complex> source) { var r = source._Product(); return Complex.Pow(r.v0, 1.0 / r.v1); }
        public static Complex Product(this IEnumerable<Complex> source) { return source._Product().v0; }
        static InoueLab.V2<Complex, int> _Product(this IEnumerable<Complex> source)
        {
            Complex a = 1;
            int c = 0;
            foreach (var e in source) { c++; a *= e; }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region Complex[]
        public static Complex[] Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Complex[]> selector) { return source.Select(element => selector(element)).Average(); }
        public static Complex[] Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Complex[]> selector) { return source.Select(element => selector(element)).Sum(); }
        public static Complex[] Average(this IEnumerable<Complex[]> source) { var r = source._Sum(); return InoueLab.Mt.LetDiv(r.v0, r.v1); }
        public static Complex[] Sum(this IEnumerable<Complex[]> source) { return source._Sum().v0; }
        static InoueLab.V2<Complex[], int> _Sum(this IEnumerable<Complex[]> source)
        {
            var a = default(Complex[]);
            int c = 0;
            foreach (var e in source) { c++; if (a == null) a = (Complex[])e.Clone(); else InoueLab.Mt.LetAdd(a, e); }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region Complex[,]
        public static Complex[,] Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Complex[,]> selector) { return source.Select(element => selector(element)).Average(); }
        public static Complex[,] Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Complex[,]> selector) { return source.Select(element => selector(element)).Sum(); }
        public static Complex[,] Average(this IEnumerable<Complex[,]> source) { var r = source._Sum(); return InoueLab.Mt.LetDiv(r.v0, r.v1); }
        public static Complex[,] Sum(this IEnumerable<Complex[,]> source) { return source._Sum().v0; }
        static InoueLab.V2<Complex[,], int> _Sum(this IEnumerable<Complex[,]> source)
        {
            var a = default(Complex[,]);
            int c = 0;
            foreach (var e in source) { c++; if (a == null) a = (Complex[,])e.Clone(); else InoueLab.Mt.LetAdd(a, e); }
            return InoueLab.New.V2(a, c);
        }
        #endregion
    }

    #endregion
}

namespace InoueLab
{
    #region Spline classes
    public class Spline3
    {
        double[] DataX, DataY, DataZ;
        public Spline3() { }
        public Spline3(IEnumerable<double> dataX, IEnumerable<double> dataY) { Set(dataX, dataY); }

        int FindSection(double value)
        {
            int i = 0;
            for (int j = DataX.Length - 1; i < j; )
            {
                int k = (i + j) / 2;
                if (DataX[k] < value) i = k + 1; else j = k;
            }
            if (i > 0) --i;
            return i;
        }
        public double Interpolate(double value)
        {
            int i = FindSection(value);
            double h = DataX[i + 1] - DataX[i];
            double d = value - DataX[i];
            return (((DataZ[i + 1] - DataZ[i]) * d / h + DataZ[i] * 3) * d + ((DataY[i + 1] - DataY[i]) / h - (DataZ[i] * 2 + DataZ[i + 1]) * h)) * d + DataY[i];
        }
        public double CalcGrad(double value)
        {
            int i = FindSection(value);
            double h = DataX[i + 1] - DataX[i];
            double d = value - DataX[i];
            return ((DataZ[i + 1] - DataZ[i]) * d / h * 3 + DataZ[i] * 6) * d + ((DataY[i + 1] - DataY[i]) / h - (DataZ[i] * 2 + DataZ[i + 1]) * h);
        }

        public void Set(IEnumerable<double> dataX, IEnumerable<double> dataY)
        {
            if (dataY == null) ThrowException.ArgumentException("dataY");
            DataY = dataY.ToArray();
            int N = DataY.Length;
            if (N == 0) ThrowException.ArgumentException("dataY");
            if (N == 1) DataY = new double[] { DataY[0], DataY[0], DataY[0] };
            if (N == 2) DataY = new double[] { DataY[0], (DataY[0] + DataY[1]) / 2, DataY[1] };

            if (dataX != null)
            {
                DataX = dataX.ToArray();
                if (DataX.Length == 1) DataX = new double[] { DataX[0], DataX[0], DataX[0] };
                if (DataX.Length == 2) DataX = new double[] { DataX[0], (DataX[0] + DataX[1]) / 2, DataX[1] };
            }
            if (dataX == null || DataX.Length == 0)
            {
                DataX = new double[N];
                DataX[0] = 0;
                for (int i = 1; i < N; i++) DataX[i] = DataX[i - 1] + Math.Max(Math.Abs(DataY[i] - DataY[i - 1]), 1e-10);
                for (int i = 1; i < N; i++) DataX[i] /= DataX[N - 1];
            }
            if (DataX.Length != N) ThrowException.ArgumentOutOfRangeException("dataX, dataY");

            DataZ = new double[N];
            double[] h = new double[N];
            double[] d = new double[N];
            DataZ[0] = DataZ[N - 1] = 0;
            for (int i = 0; i < N - 1; i++)
            {
                h[i] = DataX[i + 1] - DataX[i];
                d[i + 1] = (DataY[i + 1] - DataY[i]) / h[i];
            }
            DataZ[1] = d[2] - d[1] - h[0] * DataZ[0];
            d[1] = 2 * (DataX[2] - DataX[0]);
            for (int i = 1; i < N - 2; i++)
            {
                double t = h[i] / d[i];
                DataZ[i + 1] = d[i + 2] - d[i + 1] - DataZ[i] * t;
                d[i + 1] = 2 * (DataX[i + 2] - DataX[i]) - h[i] * t;
            }
            DataZ[N - 2] -= h[N - 2] * DataZ[N - 1];
            for (int i = N - 2; i > 0; i--)
                DataZ[i] = (DataZ[i] - h[i] * DataZ[i + 1]) / d[i];
        }
    }

    public class Spline1
    {
        double[] DataX, DataY;
        public Spline1() { }
        public Spline1(IEnumerable<double> dataX, IEnumerable<double> dataY) { Set(dataX, dataY); }

        int FindSection(double value)
        {
            int i = 0;
            for (int j = DataX.Length - 1; i < j; )
            {
                int k = (i + j) / 2;
                if (DataX[k] < value) i = k + 1; else j = k;
            }
            if (i > 0) --i;
            return i;
        }
        public double Interpolate(double value)
        {
            int i = FindSection(value);
            return (DataY[i + 1] - DataY[i]) / (DataX[i + 1] - DataX[i]) * (value - DataX[i]) + DataY[i];
        }
        public void Set(IEnumerable<double> dataX, IEnumerable<double> dataY)
        {
            if (dataX == null || dataY == null) ThrowException.ArgumentException("dataX, dataY");
            DataX = dataX.ToArray();
            DataY = dataY.ToArray();
            if (DataX.Length == 0 || DataY.Length == 0 || DataX.Length != DataY.Length) ThrowException.ArgumentException("dataX, dataY");
        }
    }
    #endregion

    #region Extension class
    public static partial class Ex
    {
        #region Sorting
        public static T[] LetSort<T>(this T[] array, Func<T, BigInteger> selector) { return array.LetSort((x, y) => selector(x).CompareTo(selector(y))); }
        public static List<T> LetSort<T>(this List<T> list, Func<T, BigInteger> selector) { return list.LetSort((x, y) => selector(x).CompareTo(selector(y))); }
        #endregion
    }
    #endregion

    #region Unsafe array calculations
    public unsafe static partial class Us
    {
        public static double SqAbs(Complex* p) { return Mt.Sq(((double*)p)[0]) + Mt.Sq(((double*)p)[1]); }
        public static void Swap(Complex* p, Complex* q)
        {
            double pr = ((double*)p)[0];
            double pi = ((double*)p)[1];
            ((double*)p)[0] = ((double*)q)[0];
            ((double*)p)[1] = ((double*)q)[1];
            ((double*)q)[0] = pr;
            ((double*)q)[1] = pi;
        }
        public static void Let(Complex* p, Complex* q)
        {
            ((double*)p)[0] = ((double*)q)[0];
            ((double*)p)[1] = ((double*)q)[1];
        }
        public static void Add(Complex* r, Complex* p, Complex* q)
        {
            ((double*)r)[0] = ((double*)p)[0] + ((double*)q)[0];
            ((double*)r)[1] = ((double*)p)[1] + ((double*)q)[1];
        }
        public static void Sub(Complex* r, Complex* p, Complex* q)
        {
            ((double*)r)[0] = ((double*)p)[0] - ((double*)q)[0];
            ((double*)r)[1] = ((double*)p)[1] - ((double*)q)[1];
        }
        public static void Mul(Complex* r, Complex* p, double q)
        {
            ((double*)r)[0] = ((double*)p)[0] * q;
            ((double*)r)[1] = ((double*)p)[1] * q;
        }
        public static void Div(Complex* r, double p, Complex* q)
        {
            double qr = ((double*)q)[0];
            double qi = ((double*)q)[1];
            double ql = p / (qr * qr + qi * qi);
            ((double*)r)[0] = +qr * ql;
            ((double*)r)[1] = -qi * ql;
        }
        public static void LetAddMul(Complex* r, Complex* p, Complex* q)
        {
            double pr = ((double*)p)[0];
            double pi = ((double*)p)[1];
            double qr = ((double*)q)[0];
            double qi = ((double*)q)[1];
            ((double*)r)[0] += pr * qr - pi * qi;
            ((double*)r)[1] += pr * qi + pi * qr;
        }
        public static void LetAddCul(Complex* r, Complex* p, Complex* q)
        {
            double pr = ((double*)p)[0];
            double pi = ((double*)p)[1];
            double qr = ((double*)q)[0];
            double qi = ((double*)q)[1];
            ((double*)r)[0] += pr * qr + pi * qi;
            ((double*)r)[1] += pr * qi - pi * qr;
        }
        public static void Mul(Complex* r, Complex* p, Complex* q)
        {
            double pr = ((double*)p)[0];
            double pi = ((double*)p)[1];
            double qr = ((double*)q)[0];
            double qi = ((double*)q)[1];
            ((double*)r)[0] = pr * qr - pi * qi;
            ((double*)r)[1] = pr * qi + pi * qr;
        }
        public static void Div(Complex* r, Complex* p, Complex* q)
        {
            double pr = ((double*)p)[0];
            double pi = ((double*)p)[1];
            double qr = ((double*)q)[0];
            double qi = ((double*)q)[1];
            double ql = qr * qr + qi * qi;
            ((double*)r)[0] = (pi * qi + pr * qr) / ql;
            ((double*)r)[1] = (pi * qr - pr * qi) / ql;
        }
        public static void Cul(Complex* r, Complex* p, Complex* q)
        {
            double pr = ((double*)p)[0];
            double pi = ((double*)p)[1];
            double qr = ((double*)q)[0];
            double qi = ((double*)q)[1];
            ((double*)r)[0] = pr * qr + pi * qi;
            ((double*)r)[1] = pr * qi - pi * qr;
        }
        public static double SqAbsSub(Complex* p, Complex* q) { return Mt.Sq(((double*)p)[0] - ((double*)q)[0]) + Mt.Sq(((double*)p)[1] - ((double*)q)[1]); }

        public static double SumAbs(Complex* p, int n) { double a = 0; for (int i = n; --i >= 0; ) a += Math.Sqrt(SqAbs(&p[i])); return a; }
        public static double SumSqAbs(Complex* p, int n) { double a = 0; for (int i = n; --i >= 0; ) a += SqAbs(&p[i]); return a; }
        public static double MaxSqAbs(Complex* p, int n) { double a = 0; for (int i = n; --i >= 0; ) Mt.LetMax(ref a, SqAbs(&p[i])); return a; }
        public static double SumPowAbs(Complex* p, int n, double nu) { nu *= 0.5; double a = 0; for (int i = n; --i >= 0; ) a += Math.Pow(SqAbs(&p[i]), nu); return a; }
        public static Complex Sum(Complex* p, int n) { Complex a = default(Complex); for (int i = n; --i >= 0; ) Add(&a, &a, &p[i]); return a; }
        public static Complex SumMul(Complex* p, Complex* q, int n) { var a = default(Complex); for (int i = n; --i >= 0; ) LetAddMul(&a, &p[i], &q[i]); return a; }
        public static Complex SumCul(Complex* p, Complex* q, int n) { var a = default(Complex); for (int i = n; --i >= 0; ) LetAddCul(&a, &p[i], &q[i]); return a; }
        public static Complex SumMul(Complex* p, double* q, int n) { var a = default(Complex); for (int i = n; --i >= 0; ) { ((double*)&a)[1] += *((double*)p + i * 2 + 1) * q[i]; ((double*)&a)[0] += *((double*)p + i * 2) * q[i]; } return a; }
        public static Complex SumCul(Complex* p, double* q, int n) { var a = SumMul(p, q, n); ((double*)&a)[1] *= -1; return a; }
        public static double SumAbsSub(Complex* p, Complex* q, int n) { double a = 0; for (int i = n; --i >= 0; ) a += Math.Sqrt(SqAbsSub(&p[i], &q[i])); return a; }
        public static double SumSqSub(Complex* p, Complex* q, int n) { double a = 0; for (int i = n; --i >= 0; ) a += SqAbsSub(&p[i], &q[i]); return a; }
        public static double SumPowSub(Complex* p, Complex* q, int n, double nu) { nu *= 0.5; double a = 0; for (int i = n; --i >= 0; ) a += Math.Pow(SqAbsSub(&p[i], &q[i]), nu); return a; }

        public static void ToComplex(Complex* r, double* p, double* q, int n) { for (int i = n; --i >= 0; ) { *((double*)r + i * 2 + 1) = q[i]; *((double*)r + i * 2) = p[i]; } }
        public static void Real(double* r, Complex* p, int n) { for (int i = n; --i >= 0; ) r[i] = *((double*)p + i * 2 + 0); }
        public static void Imag(double* r, Complex* p, int n) { for (int i = n; --i >= 0; ) r[i] = *((double*)p + i * 2 + 1); }
        public static void Let(Complex* p, Complex* q, int n) { Let((double*)p, (double*)q, n * 2); }
        public static void Let(Complex* p, double* q, int n) { for (int i = n; --i >= 0; ) { *((double*)p + i * 2 + 1) = 0; *((double*)p + i * 2 + 0) = q[i]; } }
        public static void Neg(Complex* r, Complex* p, int n) { Neg((double*)r, (double*)p, n * 2); }
        public static void Conj(Complex* r, Complex* p, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i) = *((double*)p + i); *((double*)r + i + 1) = -*((double*)p + i + 1); } }
        public static void Add(Complex* r, Complex* p, Complex* q, int n) { Add((double*)r, (double*)p, (double*)q, n * 2); }
        public static void Sub(Complex* r, Complex* p, Complex* q, int n) { Sub((double*)r, (double*)p, (double*)q, n * 2); }
        public static void Mul(Complex* r, Complex* p, Complex* q, int n) { for (int i = n; --i >= 0; ) Mul(&r[i], &p[i], &q[i]); }
        public static void Div(Complex* r, Complex* p, Complex* q, int n) { for (int i = n; --i >= 0; ) Div(&r[i], &p[i], &q[i]); }
        public static void Cul(Complex* r, Complex* p, Complex* q, int n) { for (int i = n; --i >= 0; ) Cul(&r[i], &p[i], &q[i]); }
        public static void Add(Complex* r, Complex* p, Complex q, int n) { for (int i = n; --i >= 0; ) Add(&r[i], &p[i], &q); }
        public static void Sub(Complex* r, Complex* p, Complex q, int n) { for (int i = n; --i >= 0; ) Sub(&r[i], &p[i], &q); }
        public static void Mul(Complex* r, Complex* p, Complex q, int n) { for (int i = n; --i >= 0; ) Mul(&r[i], &p[i], &q); }
        public static void Div(Complex* r, Complex* p, Complex q, int n) { Div(&q, 1, &q); Mul(r, p, q, n); }
        public static void Cul(Complex* r, Complex* p, Complex q, int n) { for (int i = n; --i >= 0; ) Cul(&r[i], &p[i], &q); }
        public static void Sub(Complex* r, Complex p, Complex* q, int n) { for (int i = n; --i >= 0; ) Sub(&r[i], &p, &q[i]); }
        public static void Div(Complex* r, Complex p, Complex* q, int n) { for (int i = n; --i >= 0; ) Div(&r[i], &p, &q[i]); }

        public static void Add(Complex* r, Complex* p, double* q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = *((double*)p + i + 1); *((double*)r + i) = *((double*)p + i) + q[i >> 1]; } }
        public static void Sub(Complex* r, Complex* p, double* q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = *((double*)p + i + 1); *((double*)r + i) = *((double*)p + i) - q[i >> 1]; } }
        public static void Mul(Complex* r, Complex* p, double* q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = *((double*)p + i + 1) * q[i >> 1]; *((double*)r + i) = *((double*)p + i) * q[i >> 1]; } }
        public static void Div(Complex* r, Complex* p, double* q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = *((double*)p + i + 1) / q[i >> 1]; *((double*)r + i) = *((double*)p + i) / q[i >> 1]; } }
        public static void Cul(Complex* r, Complex* p, double* q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = -*((double*)p + i + 1) * q[i >> 1]; *((double*)r + i) = *((double*)p + i) * q[i >> 1]; } }
        public static void Sub(Complex* r, double* p, Complex* q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = -*((double*)q + i + 1); *((double*)r + i) = p[i >> 1] - *((double*)q + i); } }
        public static void Div(Complex* r, double* p, Complex* q, int n) { for (int i = n; --i >= 0; ) Div(&r[i], p[i], &q[i]); }
        public static void Add(Complex* r, Complex* p, double q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = *((double*)p + i + 1); *((double*)r + i) = *((double*)p + i) + q; } }
        public static void Sub(Complex* r, Complex* p, double q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = *((double*)p + i + 1); *((double*)r + i) = *((double*)p + i) - q; } }
        public static void Mul(Complex* r, Complex* p, double q, int n) { Mul((double*)r, (double*)p, q, n * 2); }
        public static void Div(Complex* r, Complex* p, double q, int n) { Div((double*)r, (double*)p, q, n * 2); }
        public static void Cul(Complex* r, Complex* p, double q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = -*((double*)p + i + 1) * q; *((double*)r + i) = *((double*)p + i) * q; } }
        public static void Sub(Complex* r, double p, Complex* q, int n) { for (int i = n * 2; (i -= 2) >= 0; ) { *((double*)r + i + 1) = -*((double*)q + i + 1); *((double*)r + i) = p - *((double*)q + i); } }
        public static void Div(Complex* r, double p, Complex* q, int n) { for (int i = n; --i >= 0; ) Div(&r[i], p, &q[i]); }

        public static void LetAddMul(Complex* r, Complex* p, Complex v, int n) { for (int i = n; --i >= 0; ) LetAddMul(&r[i], &p[i], &v); }
        public static void LetAddMul(Complex* r, Complex* p, double v, int n) { LetAddMul((double*)r, (double*)p, v, n * 2); }
        public static void LetMulAdd(Complex* r, double v, Complex* p, int n) { LetMulAdd((double*)r, v, (double*)p, n * 2); }
        public static void SqAbs(double* r, Complex* p, int n) { for (int i = n; --i >= 0; ) r[i] = SqAbs(&p[i]); }
    }
    public unsafe static partial class Us
    {
        public static void LetAdd(Complex* p, Complex* q)
        {
            ((double*)p)[0] += ((double*)q)[0];
            ((double*)p)[1] += ((double*)q)[1];
        }
        public static void LetSub(Complex* p, Complex* q)
        {
            ((double*)p)[0] -= ((double*)q)[0];
            ((double*)p)[1] -= ((double*)q)[1];
        }
        public static void LetMul(Complex* p, Complex* q)
        {
            double rr = ((double*)p)[0];
            ((double*)p)[0] = rr * ((double*)q)[0] - ((double*)p)[1] * ((double*)q)[1];
            ((double*)p)[1] = rr * ((double*)q)[1] + ((double*)p)[1] * ((double*)q)[0];
        }
    }
    #endregion

    public static partial class Mt
    {
        #region Miscellaneous functions

        public static Complex Phase(double phase) { return new Complex(Math.Cos(phase), Math.Sin(phase)); }
        // denominator > 0
        public static Complex Phase(int numerator, int denominator)
        {
            numerator %= denominator;
            if (numerator >= 0)
            {
                if (numerator * 2 > denominator) numerator -= denominator;
            }
            else
            {
                if (numerator * -2 > denominator) numerator += denominator;
            }
            return Phase(Mt.PI2 * numerator / denominator);
        }
        public static double SqAbs(this Complex value) { return Mt.Sq(value.Real) + Mt.Sq(value.Imaginary); }
        public static Complex Conj(this Complex value) { return new Complex(value.Real, -value.Imaginary); }
        public static Complex Mul(Complex value0, Complex value1)
        {
            unsafe
            {
                return new Complex(
                    ((double*)&value0)[0] * ((double*)&value1)[0] - ((double*)&value0)[1] * ((double*)&value1)[1],
                    ((double*)&value0)[0] * ((double*)&value1)[1] + ((double*)&value0)[1] * ((double*)&value1)[0]);
            }
        }
        public static Complex ConjMul(Complex value0, Complex value1)
        {
            unsafe
            {
                return new Complex(
                    ((double*)&value0)[0] * ((double*)&value1)[0] + ((double*)&value0)[1] * ((double*)&value1)[1],
                    ((double*)&value0)[0] * ((double*)&value1)[1] - ((double*)&value0)[1] * ((double*)&value1)[0]);
            }
        }
        public static Complex Mul(Complex value0, double value1) { return new Complex(value0.Real * value1, value0.Imaginary * value1); }
        public static Complex ConjMul(Complex value0, double value1) { return new Complex(value0.Real * value1, -value0.Imaginary * value1); }

        public static BigInteger MultinomialInteger(IEnumerable<int> source)
        {
            int total = 0;
            BigInteger product = 1;
            foreach (int element in source)
            {
                if (element < 0) ThrowException.ArgumentException("element");
                total += element;
                product *= FactorialInteger(element);
            }
            return FactorialInteger(total) / product;
        }

        static List<BigInteger> FactorialIntegerBuffer = new List<BigInteger>() { 1 };
        static BigInteger FactorialInteger_(int value)
        {
            BigInteger product = FactorialIntegerBuffer.Last();
            for (int i = FactorialIntegerBuffer.Count; i <= value; i++)
            {
                product *= i;
                FactorialIntegerBuffer.Add(product);
            }
            return product;
        }
        public static BigInteger FactorialInteger(int value)
        {
            if (value < 0) ThrowException.ArgumentOutOfRangeException("value");
            return value < FactorialIntegerBuffer.Count ? FactorialIntegerBuffer[value] : FactorialInteger_(value);
        }

        public static BigInteger GreatestCommonDivisor(BigInteger value0, BigInteger value1)
        {
            while (true)
            {
                if (value0 < value1) Mt.Swap(ref value0, ref value1);
                var z = BigInteger.Remainder(value0, value1);
                if (z == 0) break;
                value0 = z;
            }
            return value1;
        }

        #endregion

        #region Basic calculations

        #region BigInteger
        public static double Average(int count, Func<int, BigInteger> function) { return (double)Sum(count, function) / count; }
        public static BigInteger Sum(int count, Func<int, BigInteger> function)
        {
            var a = default(BigInteger);
            for (int i = 0; i < count; i++) a += function(i);
            return a;
        }
        public static double GeometricalAverage(int count, Func<int, BigInteger> function) { return Math.Pow((double)Sum(count, function), 1.0 / count); }
        public static BigInteger Product(int count, Func<int, BigInteger> function)
        {
            BigInteger a = 1;
            for (int i = 0; i < count; i++) a *= function(i);
            return a;
        }
        #endregion

        #region Complex
        public static Complex Average(int count, Func<int, Complex> function) { return Sum(count, function) / count; }
        public static Complex Sum(int count, Func<int, Complex> function)
        {
            var a = default(Complex);
            for (int i = 0; i < count; i++) a += function(i);
            return a;
        }
        public static Complex GeometricalAverage(int count, Func<int, Complex> function) { return Complex.Pow(Sum(count, function), 1.0 / count); }
        public static Complex Product(int count, Func<int, Complex> function)
        {
            Complex a = 1;
            for (int i = 0; i < count; i++) a *= function(i);
            return a;
        }
        #endregion

        #region Complex[]
        public static Complex[] Average(int count, Func<int, Complex[]> function) { return Sum(count, function).LetDiv(count); }
        public static Complex[] Sum(int count, Func<int, Complex[]> function)
        {
            var a = (count == 0) ? default(Complex[]) : function(0).CloneX();
            for (int i = 1; i < count; i++) a.LetAdd(function(i));
            return a;
        }
        #endregion

        #region Complex[,]
        public static Complex[,] Average(int count, Func<int, Complex[,]> function) { return Sum(count, function).LetDiv(count); }
        public static Complex[,] Sum(int count, Func<int, Complex[,]> function)
        {
            var a = (count == 0) ? default(Complex[,]) : function(0).CloneX();
            for (int i = 1; i < count; i++) a.LetAdd(function(i));
            return a;
        }
        #endregion

        #endregion

        #region Linear functions
        #region Complex[]
        public static Complex Sum(this Complex[] array) { unsafe { fixed (Complex* p = array) return Us.Sum(p, array.Length); } }
        public static Complex Average(this Complex[] array) { return array.Sum() / array.Length; }
        public static Complex Inner(Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumMul(p, q, array0.Length); } }
        public static Complex InnerConj(Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumCul(p, q, array0.Length); } }
        public static Complex Inner(Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) return Us.SumMul(p, q, array0.Length); } }
        public static Complex InnerConj(Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) return Us.SumCul(p, q, array0.Length); } }
        public static double Norm(this Complex[] array, double nu) { unsafe { fixed (Complex* p = array) return Math.Pow(Us.SumPowAbs(p, array.Length, nu), 1 / nu); } }
        public static double Norm1(this Complex[] array) { unsafe { fixed (Complex* p = array) return Us.SumAbs(p, array.Length); } }
        public static double Norm2(this Complex[] array) { return Math.Sqrt(SqNorm2(array)); }
        public static double SqNorm2(this Complex[] array) { unsafe { fixed (Complex* p = array) return Us.SumSqAbs(p, array.Length); } }
        public static double MaxAbs(this Complex[] array) { return Math.Sqrt(MaxSqAbs(array)); }
        public static double MaxSqAbs(this Complex[] array) { unsafe { fixed (Complex* p = array) return Us.MaxSqAbs(p, array.Length); } }
        public static double NormSub(Complex[] array0, Complex[] array1, double nu) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumPowSub(p, q, array0.Length, nu); } }
        public static double Norm1Sub(Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumAbsSub(p, q, array0.Length); } }
        public static double Norm2Sub(Complex[] array0, Complex[] array1) { return Math.Sqrt(SqNorm2Sub(array0, array1)); }
        public static double SqNorm2Sub(Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumSqSub(p, q, array0.Length); } }

        public static Complex[] LetConj(this Complex[] array) { unsafe { fixed (Complex* p = array) Us.Conj(p, p, array.Length); } return array; }
        public static Complex[] LetNeg(this Complex[] array) { unsafe { fixed (Complex* p = array) Us.Neg(p, p, array.Length); } return array; }
        public static Complex[] Let(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Let(p, q, array0.Length); } return array0; }
        public static Complex[] LetAdd(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Add(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetSub(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Sub(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetMul(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Mul(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetDiv(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Div(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetSubr(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Sub(p, q, p, array0.Length); } return array0; }
        public static Complex[] LetDivr(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Div(p, q, p, array0.Length); } return array0; }
        public static Complex[] LetConjMul(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Cul(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetMulConj(this Complex[] array0, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Cul(p, q, p, array0.Length); } return array0; }
        public static Complex[] Let(this Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Let(p, q, array0.Length); } return array0; }
        public static Complex[] LetAdd(this Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Add(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetSub(this Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Sub(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetMul(this Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Mul(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetDiv(this Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Div(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetConjMul(this Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Cul(p, p, q, array0.Length); } return array0; }
        public static Complex[] LetSubr(this Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Sub(p, q, p, array0.Length); } return array0; }
        public static Complex[] LetDivr(this Complex[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Div(p, q, p, array0.Length); } return array0; }
        public static Complex[] LetAdd(this Complex[] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Add(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetSub(this Complex[] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Sub(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetMul(this Complex[] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Mul(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetDiv(this Complex[] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Div(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetConjMul(this Complex[] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Cul(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetSubr(this Complex[] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Sub(p, scalar, p, array.Length); } return array; }
        public static Complex[] LetDivr(this Complex[] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Div(p, scalar, p, array.Length); } return array; }
        public static Complex[] LetAdd(this Complex[] array, double scalar) { if (scalar != 0) unsafe { fixed (Complex* p = array) Us.Add(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetSub(this Complex[] array, double scalar) { if (scalar != 0) unsafe { fixed (Complex* p = array) Us.Sub(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetMul(this Complex[] array, double scalar) { if (scalar != 1) unsafe { fixed (Complex* p = array) Us.Mul(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetDiv(this Complex[] array, double scalar) { if (scalar != 1) unsafe { fixed (Complex* p = array) Us.Div(p, p, scalar, array.Length); } return array; }
        public static Complex[] LetConjMul(this Complex[] array, double scalar) { unsafe { fixed (Complex* p = array) Us.Cul(p, p, scalar, array.Length); } return array; }
        public static Complex[] ToComplex(double[] array0, double[] array1) { SizeCheck0(array0, array1); var result = new Complex[array0.Length]; unsafe { fixed (Complex* r = result) fixed (double* p = array0, q = array1) Us.ToComplex(r, p, q, result.Length); } return result; }
        public static Complex[] ToComplex(this double[] array) { var result = new Complex[array.Length]; unsafe { fixed (Complex* r = result) fixed (double* p = array) Us.Let(r, p, result.Length); } return result; }
        public static double[] Real(this Complex[] array) { var result = new double[array.Length]; unsafe { fixed (double* r = result) fixed (Complex* p = array) Us.Real(r, p, result.Length); } return result; }
        public static double[] Imag(this Complex[] array) { var result = new double[array.Length]; unsafe { fixed (double* r = result) fixed (Complex* p = array) Us.Imag(r, p, result.Length); } return result; }
        public static Complex[] Conj(this Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Conj(r, p, result.Length); } return result; }
        public static Complex[] Pos(this Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Let(r, p, result.Length); } return result; }
        public static Complex[] Neg(this Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Neg(r, p, result.Length); } return result; }
        public static Complex[] Add(Complex[] array0, Complex[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Add(r, p, q, result.Length); } return result; }
        public static Complex[] Sub(Complex[] array0, Complex[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Sub(r, p, q, result.Length); } return result; }
        public static Complex[] Mul(Complex[] array0, Complex[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Mul(r, p, q, result.Length); } return result; }
        public static Complex[] Div(Complex[] array0, Complex[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Div(r, p, q, result.Length); } return result; }
        public static Complex[] ConjMul(Complex[] array0, Complex[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Cul(r, p, q, result.Length); } return result; }
        public static Complex[] Add(Complex[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Add(r, p, q, result.Length); } return result; }
        public static Complex[] Sub(Complex[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Sub(r, p, q, result.Length); } return result; }
        public static Complex[] Mul(Complex[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Mul(r, p, q, result.Length); } return result; }
        public static Complex[] Div(Complex[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Div(r, p, q, result.Length); } return result; }
        public static Complex[] ConjMul(Complex[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Cul(r, p, q, result.Length); } return result; }
        public static Complex[] Add(Complex[] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static Complex[] Sub(Complex[] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Sub(r, p, scalar, result.Length); } return result; }
        public static Complex[] Mul(Complex[] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static Complex[] Div(Complex[] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Div(r, p, scalar, result.Length); } return result; }
        public static Complex[] ConjMul(Complex[] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Cul(r, p, scalar, result.Length); } return result; }
        public static Complex[] Add(Complex scalar, Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static Complex[] Sub(Complex scalar, Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Sub(r, scalar, p, result.Length); } return result; }
        public static Complex[] Mul(Complex scalar, Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static Complex[] Div(Complex scalar, Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Div(r, scalar, p, result.Length); } return result; }
        public static Complex[] Add(Complex[] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static Complex[] Sub(Complex[] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Sub(r, p, scalar, result.Length); } return result; }
        public static Complex[] Mul(Complex[] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static Complex[] Div(Complex[] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Div(r, p, scalar, result.Length); } return result; }
        public static Complex[] ConjMul(Complex[] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Cul(r, p, scalar, result.Length); } return result; }
        public static Complex[] Add(double scalar, Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static Complex[] Sub(double scalar, Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Sub(r, scalar, p, result.Length); } return result; }
        public static Complex[] Mul(double scalar, Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static Complex[] Div(double scalar, Complex[] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Div(r, scalar, p, result.Length); } return result; }

        public static Complex[] LetAddMul(this Complex[] array0, Complex[] array1, double scalar) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.LetAddMul(p, q, scalar, array0.Length); } return array0; }
        public static Complex[] LetMulAdd(this Complex[] array0, double scalar, Complex[] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.LetMulAdd(p, scalar, q, array0.Length); } return array0; }
        public static Complex[] AddMul(Complex[] array0, Complex[] array1, double scalar) { SizeCheck0(array0, array1); var result = array0.CloneX(); unsafe { fixed (Complex* r = result, p = array1) Us.LetAddMul(r, p, scalar, result.Length); } return result; }
        #endregion

        #region Complex[,]
        public static Complex Sum(this Complex[,] array) { unsafe { fixed (Complex* p = array) return Us.Sum(p, array.Length); } }
        public static Complex Average(this Complex[,] array) { return array.Sum() / array.Length; }
        public static Complex Inner(Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumMul(p, q, array0.Length); } }
        public static Complex InnerConj(Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumCul(p, q, array0.Length); } }
        public static Complex Inner(Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) return Us.SumMul(p, q, array0.Length); } }
        public static Complex InnerConj(Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) return Us.SumCul(p, q, array0.Length); } }
        public static double Norm(this Complex[,] array, double nu) { unsafe { fixed (Complex* p = array) return Math.Pow(Us.SumPowAbs(p, array.Length, nu), 1 / nu); } }
        public static double Norm1(this Complex[,] array) { unsafe { fixed (Complex* p = array) return Us.SumAbs(p, array.Length); } }
        public static double Norm2(this Complex[,] array) { return Math.Sqrt(SqNorm2(array)); }
        public static double SqNorm2(this Complex[,] array) { unsafe { fixed (Complex* p = array) return Us.SumSqAbs(p, array.Length); } }
        public static double MaxAbs(this Complex[,] array) { return Math.Sqrt(MaxSqAbs(array)); }
        public static double MaxSqAbs(this Complex[,] array) { unsafe { fixed (Complex* p = array) return Us.MaxSqAbs(p, array.Length); } }
        public static double NormSub(Complex[,] array0, Complex[,] array1, double nu) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumPowSub(p, q, array0.Length, nu); } }
        public static double Norm1Sub(Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumAbsSub(p, q, array0.Length); } }
        public static double Norm2Sub(Complex[,] array0, Complex[,] array1) { return Math.Sqrt(SqNorm2Sub(array0, array1)); }
        public static double SqNorm2Sub(Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) return Us.SumSqSub(p, q, array0.Length); } }

        public static Complex[,] LetConj(this Complex[,] array) { unsafe { fixed (Complex* p = array) Us.Conj(p, p, array.Length); } return array; }
        public static Complex[,] LetNeg(this Complex[,] array) { unsafe { fixed (Complex* p = array) Us.Neg(p, p, array.Length); } return array; }
        public static Complex[,] Let(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Let(p, q, array0.Length); } return array0; }
        public static Complex[,] LetAdd(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Add(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetSub(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Sub(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetMul(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Mul(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetDiv(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Div(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetSubr(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Sub(p, q, p, array0.Length); } return array0; }
        public static Complex[,] LetDivr(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Div(p, q, p, array0.Length); } return array0; }
        public static Complex[,] LetConjMul(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Cul(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetMulConj(this Complex[,] array0, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.Cul(p, q, p, array0.Length); } return array0; }
        public static Complex[,] Let(this Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Let(p, q, array0.Length); } return array0; }
        public static Complex[,] LetAdd(this Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Add(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetSub(this Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Sub(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetMul(this Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Mul(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetDiv(this Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Div(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetConjMul(this Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Cul(p, p, q, array0.Length); } return array0; }
        public static Complex[,] LetSubr(this Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Sub(p, q, p, array0.Length); } return array0; }
        public static Complex[,] LetDivr(this Complex[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0) fixed (double* q = array1) Us.Div(p, q, p, array0.Length); } return array0; }
        public static Complex[,] LetAdd(this Complex[,] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Add(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetSub(this Complex[,] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Sub(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetMul(this Complex[,] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Mul(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetDiv(this Complex[,] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Div(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetConjMul(this Complex[,] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Cul(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetSubr(this Complex[,] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Sub(p, scalar, p, array.Length); } return array; }
        public static Complex[,] LetDivr(this Complex[,] array, Complex scalar) { unsafe { fixed (Complex* p = array) Us.Div(p, scalar, p, array.Length); } return array; }
        public static Complex[,] LetAdd(this Complex[,] array, double scalar) { unsafe { fixed (Complex* p = array) Us.Add(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetSub(this Complex[,] array, double scalar) { unsafe { fixed (Complex* p = array) Us.Sub(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetMul(this Complex[,] array, double scalar) { unsafe { fixed (Complex* p = array) Us.Mul(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetDiv(this Complex[,] array, double scalar) { unsafe { fixed (Complex* p = array) Us.Div(p, p, scalar, array.Length); } return array; }
        public static Complex[,] LetConjMul(this Complex[,] array, double scalar) { unsafe { fixed (Complex* p = array) Us.Cul(p, p, scalar, array.Length); } return array; }
        public static Complex[,] ToComplex(double[,] array0, double[,] array1) { SizeCheck0(array0, array1); var result = new Complex[array0.GetLength(0), array0.GetLength(1)]; unsafe { fixed (Complex* r = result) fixed (double* p = array0, q = array1) Us.ToComplex(r, p, q, result.Length); } return result; }
        public static Complex[,] ToComplex(this double[,] array) { var result = new Complex[array.GetLength(0), array.GetLength(1)]; unsafe { fixed (Complex* r = result) fixed (double* p = array) Us.Let(r, p, result.Length); } return result; }
        public static double[,] Real(this Complex[,] array) { var result = new double[array.GetLength(0), array.GetLength(1)]; unsafe { fixed (double* r = result) fixed (Complex* p = array) Us.Real(r, p, result.Length); } return result; }
        public static double[,] Imag(this Complex[,] array) { var result = new double[array.GetLength(0), array.GetLength(1)]; unsafe { fixed (double* r = result) fixed (Complex* p = array) Us.Imag(r, p, result.Length); } return result; }
        public static Complex[,] Conj(this Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Conj(r, p, result.Length); } return result; }
        public static Complex[,] Pos(this Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Let(r, p, result.Length); } return result; }
        public static Complex[,] Neg(this Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Neg(r, p, result.Length); } return result; }
        public static Complex[,] Add(Complex[,] array0, Complex[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Add(r, p, q, result.Length); } return result; }
        public static Complex[,] Sub(Complex[,] array0, Complex[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Sub(r, p, q, result.Length); } return result; }
        public static Complex[,] Mul(Complex[,] array0, Complex[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Mul(r, p, q, result.Length); } return result; }
        public static Complex[,] Div(Complex[,] array0, Complex[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Div(r, p, q, result.Length); } return result; }
        public static Complex[,] ConjMul(Complex[,] array0, Complex[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0, q = array1) Us.Cul(r, p, q, result.Length); } return result; }
        public static Complex[,] Add(Complex[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Add(r, p, q, result.Length); } return result; }
        public static Complex[,] Sub(Complex[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Sub(r, p, q, result.Length); } return result; }
        public static Complex[,] Mul(Complex[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Mul(r, p, q, result.Length); } return result; }
        public static Complex[,] Div(Complex[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Div(r, p, q, result.Length); } return result; }
        public static Complex[,] ConjMul(Complex[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (Complex* r = result, p = array0) fixed (double* q = array1) Us.Cul(r, p, q, result.Length); } return result; }
        public static Complex[,] Add(Complex[,] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Sub(Complex[,] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Sub(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Mul(Complex[,] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Div(Complex[,] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Div(r, p, scalar, result.Length); } return result; }
        public static Complex[,] ConjMul(Complex[,] array, Complex scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Cul(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Add(Complex scalar, Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Sub(Complex scalar, Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Sub(r, scalar, p, result.Length); } return result; }
        public static Complex[,] Mul(Complex scalar, Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Div(Complex scalar, Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Div(r, scalar, p, result.Length); } return result; }
        public static Complex[,] Add(Complex[,] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Sub(Complex[,] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Sub(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Mul(Complex[,] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Div(Complex[,] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Div(r, p, scalar, result.Length); } return result; }
        public static Complex[,] ConjMul(Complex[,] array, double scalar) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Cul(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Add(double scalar, Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Sub(double scalar, Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Sub(r, scalar, p, result.Length); } return result; }
        public static Complex[,] Mul(double scalar, Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static Complex[,] Div(double scalar, Complex[,] array) { var result = Zero(array); unsafe { fixed (Complex* r = result, p = array) Us.Div(r, scalar, p, result.Length); } return result; }

        public static Complex[,] LetAddMul(this Complex[,] array0, Complex[,] array1, double scalar) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.LetAddMul(p, q, scalar, array0.Length); } return array0; }
        public static Complex[,] LetMulAdd(this Complex[,] array0, double scalar, Complex[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (Complex* p = array0, q = array1) Us.LetMulAdd(p, scalar, q, array0.Length); } return array0; }
        public static Complex[,] AddMul(Complex[,] array0, Complex[,] array1, double scalar) { SizeCheck0(array0, array1); var result = array0.CloneX(); unsafe { fixed (Complex* r = result, p = array1) Us.LetAddMul(r, p, scalar, result.Length); } return result; }
        #endregion

        #region Complex matrix
        public static double[,] SqAbs(this Complex[,] array)
        {
            var result = new double[array.GetLength(0), array.GetLength(1)];
            unsafe { fixed (double* r = result) fixed (Complex* p = array) Us.SqAbs(r, p, result.Length); } return result;
        }


        public static Complex[,] T(this Complex[,] matrix)
        {
            Complex[,] result = new Complex[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = result.GetLength(0); --i >= 0; )
                for (int j = result.GetLength(1); --j >= 0; )
                    result[i, j] = matrix[j, i];
            return result;
        }
        public static Complex[,] H(this Complex[,] matrix)
        {
            Complex[,] result = new Complex[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = result.GetLength(0); --i >= 0; )
                for (int j = result.GetLength(1); --j >= 0; )
                    result[i, j] = Complex.Conjugate(matrix[j, i]);
            return result;
        }

        public unsafe static Complex[] Multiply(Complex[,] matrix, Complex[] vector)
        {
            int n = vector.Length;
            if (matrix.GetLength(1) != n) ThrowException.SizeMismatch();
            var result = new Complex[matrix.GetLength(0)];
            fixed (Complex* r = result, p = matrix, q = vector)
                for (int i = result.Length; --i >= 0; )
                    r[i] = Us.SumMul(&p[n * i], q, n);
            return result;
        }
        public unsafe static Complex[] Multiply(Complex[] vector, Complex[,] matrix)
        {
            int n = matrix.GetLength(1);
            if (matrix.GetLength(0) != vector.Length) ThrowException.SizeMismatch();
            var result = new Complex[n];
            fixed (Complex* r = result, p = matrix, q = vector)
                for (int i = vector.Length; --i >= 0; )
                    Us.LetAddMul(r, &p[n * i], q[i], n);
            return result;
        }
        public unsafe static Complex[,] Multiply(Complex[,] matrix0, Complex[,] matrix1)
        {
            int o = matrix0.GetLength(0);
            int n = matrix1.GetLength(0);
            int m = matrix1.GetLength(1);
            if (n != matrix0.GetLength(1)) ThrowException.SizeMismatch();
            var result = new Complex[o, m];
            fixed (Complex* r = result, p = matrix0, q = matrix1)
                for (int i = o; --i >= 0; )
                {
                    Complex* ri = &r[m * i];
                    Complex* pi = &p[n * i];
                    for (int j = n; --j >= 0; )
                        Us.LetAddMul(ri, &q[m * j], pi[j], m);
                }
            return result;
        }

        static int[] LUDecomposition(Complex[][] matrix)
        {
            var n = matrix.GetLength(0);
            var pivot = new int[n];
            var vv = new Complex[n];
            for (int i = 0; i < n; i++)
            {
                double max = Ex.Range(n).Max(j => Complex.Abs(matrix[i][j]));
                if (max == 0.0) ThrowException.WriteLine("LUDecomposition: singular matrix");
                vv[i] = 1.0 / max;
            }

            for (int k = 0; k < n; k++)
            {
                int p = k + Ex.FromTo(k, n).Select(i => Complex.Abs(vv[i] * matrix[i][k])).MaxIndex();
                if (p != k)
                {
                    Mt.Swap(ref matrix[p], ref matrix[k]);
                    vv[p] = vv[k];
                }
                pivot[k] = p;
                var mk = matrix[k];
                if (mk[k] == 0.0) { ThrowException.WriteLine("LUDecomposition: singular matrix"); mk[k] = 1.0e-40; }
                for (int i = k; ++i < n; )  //順不同並列可
                {
                    var mi = matrix[i];
                    var temp = mi[k] /= mk[k];
                    for (int j = k; ++j < n; )
                        mi[j] -= temp * mk[j];
                }
            }
            return pivot;
        }
        public static Complex[,] Inverse(this Complex[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1)) ThrowException.SizeMismatch();
            //if (n == 1) return Inverse1by1(matrix);
            //if (n == 2) return Inverse2by2(matrix);
            //if (n == 3) return Inverse3by3(matrix);

            Complex[][] LU = New.Array(n, i => New.Array(n, j => matrix[j, i]));
            int[] pivot = LUDecomposition(LU);
            int[] index = New.Array(n, i => i);
            for (int i = 0; i < n; i++)
                Mt.Swap(ref index[i], ref index[pivot[i]]);

            var result = new Complex[n, n];
            var vec = new Complex[n];
            for (int i = n; --i >= 0; )  //順不同並列可
            {
                vec.Clear();
                vec[i] = 1.0;
                for (int j = i; ++j < n; )
                {
                    Complex sum = 0;
                    for (int k = i; k < j; k++)
                        sum -= LU[j][k] * vec[k];
                    vec[j] = sum;
                }
                for (int j = n; --j >= 0; )
                {
                    Complex sum = vec[j];
                    for (int k = j; ++k < n; )
                        sum -= LU[j][k] * vec[k];
                    vec[j] = sum / LU[j][j];
                }
                int idx = index[i];
                for (int j = n; --j >= 0; )
                    result[idx, j] = vec[j];
            }
            return result;
        }

        public static Complex[,] PseudoInverse(this Complex[,] matrix)
        {
            var transpose = matrix.H();
            if (matrix.GetLength(0) >= matrix.GetLength(1))
                return Mt.Multiply(Mt.Multiply(transpose, matrix).Inverse(), transpose);
            else
                return Mt.Multiply(transpose, Mt.Multiply(matrix, transpose).Inverse());
        }
        #endregion
        #endregion
    }

    public static partial class Nm
    {
        #region Interporation
        // NumericalRecipes3.Base_Interp
        public abstract class InterporationBase
        {
            protected int n;
            protected int mm;
            protected int jsav = 0;
            protected int cor = 0;
            protected int dj;
            protected double[] xx;
            protected double[] yy;

            protected InterporationBase(double[] x, double[] y, int m)
            {
                this.n = x.Length;
                this.mm = m;
                this.xx = x;
                this.yy = y;
                this.dj = Math.Min(1, (int)Math.Pow((double)n, 0.25));
            }
            protected double interp(double x)
            {
                int jlo = cor != 0 ? hunt(x) : locate(x);
                return rawinterp(jlo, x);
            }
            public int locate(double x)
            {
                if (n < 2 || mm < 2 || mm > n) { ThrowException.ArgumentException("InterporationBase: locate size error"); return 0; }
                bool ascnd = (xx[n - 1] >= xx[0]);
                var jl = 0;
                var ju = n - 1;
                while (ju - jl > 1)
                {
                    var jm = (ju + jl) >> 1;
                    if (x >= xx[jm] == ascnd) jl = jm;
                    else ju = jm;
                }
                cor = Math.Abs(jl - jsav) > dj ? 0 : 1;
                jsav = jl;
                return Math.Max(0, Math.Min(n - mm, jl - ((mm - 2) >> 1)));
            }
            public int hunt(double x)
            {
                int jl = jsav, jm, ju, inc = 1;
                if (n < 2 || mm < 2 || mm > n) { ThrowException.ArgumentException("InterporationBase: hunt size error"); return 0; }
                bool ascnd = (xx[n - 1] >= xx[0]);
                if (jl < 0 || jl > n - 1) { jl = 0; ju = n - 1; }
                else
                {
                    if (x >= xx[jl] == ascnd)
                    {
                        for (; ; )
                        {
                            ju = jl + inc;
                            if (ju >= n - 1) { ju = n - 1; break; }
                            if (x < xx[ju] == ascnd) break;
                            jl = ju; inc += inc;
                        }
                    }
                    else
                    {
                        ju = jl;
                        for (; ; )
                        {
                            jl = jl - inc;
                            if (jl <= 0) { jl = 0; break; }
                            if (x >= xx[jl] == ascnd) break;
                            ju = jl; inc += inc;
                        }
                    }
                }
                while (ju - jl > 1)
                {
                    jm = (ju + jl) >> 1;
                    if (x >= xx[jm] == ascnd) jl = jm;
                    else ju = jm;
                }
                cor = Math.Abs(jl - jsav) > dj ? 0 : 1;
                jsav = jl;
                return Math.Max(0, Math.Min(n - mm, jl - ((mm - 2) >> 1)));
            }
            public abstract double rawinterp(int jlo, double x);
        }

        // NumericalRecipes3.Poly_Interp
        public static Tuple<double, double> InterporatePolynomial(IList<double> xx, IList<double> yy, int order, double x)
        {
            double dy = 0.0;
            var c = yy.Take(order).ToArray();
            var d = c.CloneX();
            var ns = xx.MinIndex(v => Math.Abs(v - x));
            var y = yy[ns--];
            for (int m = 1; m < order; m++)
            {
                for (int i = 0; i < order - m; i++)
                {
                    var ho = xx[i] - x;
                    var hp = xx[i + m] - x;
                    var w = c[i + 1] - d[i];
                    if (ho - hp == 0.0) { ThrowException.InvalidOperationException("InterporationPoly: divide by 0"); return Tuple.Create(double.NaN, double.NaN); }
                    var den = w / (ho - hp);
                    d[i] = hp * den;
                    c[i] = ho * den;
                }
                dy = 2 * (ns + 1) < (order - m) ? c[ns + 1] : d[ns--];
                y += dy;
            }
            return Tuple.Create(y, dy);
        }

        #endregion

        #region Quadrature

        // NumericalRecipes3.Quadrature
        // NumericalRecipes3.Trapzd
        static Func<double> QuadratureTrapezoid(Func<double, double> function, double start, double end)
        {
            int n = 0;
            double area = 0.0;
            Func<double> next = () =>
            {
                n++;
                if (n == 1)
                {
                    area = 0.5 * (end - start) * (function(start) + function(end));
                }
                else
                {
                    var m = 1 << (n - 2);
                    var w = (end - start) / m;
                    var sum = 0.0;
                    for (int i = 0; i < m; i++)
                        sum += function(start + w * (i + 0.5));
                    area = (area + w * sum) * 0.5;
                }
                return area;
            };
            return next;
        }

        // NumericalRecipes3.qtrap
        public static double IntegrateTrapezoid(Func<double, double> function, double start, double end, double tolerance = 1e-10)
        {
            const int JMAX = 20;
            var quadrature = QuadratureTrapezoid(function, start, end);
            double area = 0.0;
            for (int i = 0; ; i++)
            {
                if (i == JMAX) { ThrowException.WriteLine("IntegrateTrapezoid: too many steps"); break; }
                var old = area;
                area = quadrature();
                if (i > 5)
                    if (Math.Abs(area - old) < tolerance * Math.Abs(old) || (area == 0.0 && old == 0.0)) break;
            }
            return area;
        }

        // NumericalRecipes3.qsimp
        public static double IntegrateSimpson(Func<double, double> function, double start, double end, double tolerance = 1e-10)
        {
            const int JMAX = 20;
            var quadrature = QuadratureTrapezoid(function, start, end);
            double area = 0.0, orig = 0.0;
            for (int i = 0; ; i++)
            {
                if (i == JMAX) { ThrowException.WriteLine("IntegrateSimpson: too many steps"); break; }
                var oldo = orig;
                var olda = area;
                orig = quadrature();
                area = (4.0 * orig - oldo) / 3.0;
                if (i > 5)
                    if (Math.Abs(area - olda) < tolerance * Math.Abs(olda) || (area == 0.0 && olda == 0.0)) break;
            }
            return area;
        }

        // NumericalRecipes3.Midpnt
        static Func<double> QuadratureMidpoint(Func<double, double> function, double start, double end)
        {
            int n = 0;
            double area = 0.0;
            Func<double> next = () =>
            {
                n++;
                if (n == 1)
                {
                    area = (end - start) * function(0.5 * (start + end));
                }
                else
                {
                    int m = (int)Math.Pow(3, n - 2);
                    var w = (end - start) / m;
                    var sum = 0.0;
                    for (int i = 0; i < m; i++)
                    {
                        sum += function(start + w * (i + 1 / 6.0));
                        sum += function(start + w * (i + 5 / 6.0));
                    }
                    area = (area + w * sum) / 3.0;
                }
                return area;
            };
            return next;
        }
        static Func<double[]> QuadratureMidpoint(Func<double, double[]> function, double start, double end)
        {
            int n = 0;
            double[] area = null;
            Func<double[]> next = () =>
            {
                n++;
                if (n == 1)
                {
                    area = Mt.Mul(function(0.5 * (start + end)), end - start);
                }
                else
                {
                    int m = (int)Math.Pow(3, n - 2);
                    var w = (end - start) / m;
                    var sum = new double[area.Length];
                    for (int i = 0; i < m; i++)
                    {
                        sum.LetAdd(function(start + w * (i + 1 / 6.0)));
                        sum.LetAdd(function(start + w * (i + 5 / 6.0)));
                    }
                    area.LetAddMul(sum, w).LetDiv(3);
                }
                return area;
            };
            return next;
        }

        // NumericalRecipes3.Midinf
        static Func<double> QuadratureMidinf(Func<double, double> function, double start, double end)
        {
            Func<double, double> func = x => function(1 / x) / (x * x);
            return QuadratureMidpoint(func, 1 / end, 1 / start);
        }
        // NumericalRecipes3.Midsql
        static Func<double> QuadratureMidsql(Func<double, double> function, double start, double end)
        {
            Func<double, double> func = x => 2.0 * x * function(start + x * x);
            return QuadratureMidpoint(func, 0, Math.Sqrt(end - start));
        }
        // NumericalRecipes3.Midsqu
        static Func<double> QuadratureMidsqu(Func<double, double> function, double start, double end)
        {
            Func<double, double> func = x => 2.0 * x * function(end - x * x);
            return QuadratureMidpoint(func, 0, Math.Sqrt(end - start));
        }
        // NumericalRecipes3.Midexp
        static Func<double> QuadratureMidexp(Func<double, double> function, double start, double end)
        {
            Func<double, double> func = x => function(-Math.Log(x)) / x;
            return QuadratureMidpoint(func, 0, Math.Exp(-start));
        }

        // NumericalRecipes3.qromb
        // NumericalRecipes3.qromo
        static double IntegrateRomberg(Func<double> quadrature, double tolerance, int div, int JMAX)
        {
            const int Order = 5;
            var xxxx = new List<double>(JMAX);
            var area = new List<double>(JMAX);
            var estimated = 0.0;
            for (int i = 0; ; i++)
            {
                if (i == JMAX) { ThrowException.WriteLine("IntegrateRomberg: too many steps"); break; }
                xxxx.Insert(0, 1 / Math.Pow(div, i));
                area.Insert(0, quadrature());
                if (i + 1 >= Order)
                {
                    var r = InterporatePolynomial(xxxx, area, Order, 0.0);
                    estimated = r.Item1;
                    var error = r.Item2;
                    if (Math.Abs(error) <= Math.Abs(estimated) * tolerance) break;
                }
            }
            return estimated;
        }
        public static double IntegrateRombergTrapezoid(Func<double, double> function, double start, double end, double tolerance = 1e-10)
        {
            var quadrature = QuadratureTrapezoid(function, start, end);
            return IntegrateRomberg(quadrature, tolerance, 4, 20);
        }
        public static double IntegrateRombergMidpoint(Func<double, double> function, double start, double end, double tolerance = 3e-9)
        {
            var quadrature = QuadratureMidpoint(function, start, end);
            return IntegrateRomberg(quadrature, tolerance, 9, 14);
        }

        // vector version
        static double[] IntegrateRomberg(Func<double[]> quadrature, double tolerance, int div, int JMAX)
        {
            const int Order = 5;
            var xxxx = new List<double>(JMAX);
            var area = new List<double[]>(JMAX);
            double[] estimated = null;
            for (int i = 0; ; i++)
            {
                if (i == JMAX) { ThrowException.WriteLine("IntegrateRomberg: too many steps"); break; }
                xxxx.Insert(0, 1 / Math.Pow(div, i));
                area.Insert(0, quadrature().CloneX());
                int D = area[0].Length;
                if (i + 1 >= Order)
                {
                    var r = New.Array(D, d => InterporatePolynomial(xxxx, area.Select(v => v[d]).ToArray(), Order, 0.0));
                    estimated = r.Select(v => v.Item1).ToArray();
                    var error = r.Select(v => v.Item2).ToArray();
                    if (Ex.Range(D).All(d => Math.Abs(error[d]) <= Math.Abs(estimated[d]) * tolerance)) break;
                }
            }
            return estimated;
        }
        public static double[] IntegrateRombergMidpoint(Func<double, double[]> function, double start, double end, double tolerance = 3e-9)
        {
            var quadrature = QuadratureMidpoint(function, start, end);
            return IntegrateRomberg(quadrature, tolerance, 9, 14);
        }
        #endregion

        #region Signal processing functions

        #region DataWindow
        public enum DataWindowType { Box, Hanning, Hamming, Blackman, Parzen, Welch, NormalDistribution = -1 }
        public static double[] GetDataWindow(DataWindowType type, int size)
        {
            double[] Table = new double[size];
            Table = new double[size];

            double h = Mt.PI2 / (size - 1);
            switch (type)
            {
                case DataWindowType.Box:
                    for (int i = size; --i >= 0; ) Table[i] = 1;
                    break;
                case DataWindowType.Hanning:
                    for (int i = size; --i >= 0; ) Table[i] = 0.50 - 0.50 * Math.Cos(h * i);
                    break;
                case DataWindowType.Hamming:
                    for (int i = size; --i >= 0; ) Table[i] = 0.54 - 0.46 * Math.Cos(h * i);
                    break;
                case DataWindowType.Blackman:
                    for (int i = size; --i >= 0; ) Table[i] = 0.42 - 0.50 * Math.Cos(h * i) + 0.08 * Math.Cos(2 * h * i);
                    break;
                case DataWindowType.Parzen:
                    for (int i = size; --i >= 0; ) Table[i] = 1.0 - Math.Abs((i * 2 - (size - 1)) / (double)(size + 1));
                    break;
                case DataWindowType.Welch:
                    for (int i = size; --i >= 0; ) Table[i] = 1.0 - Mt.Sq((i * 2 - (size - 1)) / (double)(size + 1));
                    break;
            }
            //double c = Math.Sqrt(n / Table.Sum(x => Sq(x)));
            //for (int i = n; --i >= 0; ) Table[i] *= c;
            return Table;
        }

        public static void Windowing(double[] data, DataWindowType type)
        {
            double[] window = GetDataWindow(type, data.Length);
            for (int i = data.Length; --i >= 0; ) data[i] *= window[i];
        }
        #endregion

        #region Fourier Transform
        static void FftFactor2_IdealCode0(Complex[] data, int isign, int step)
        {
            int n = data.Length;
            var d = Mt.Phase(isign * Math.PI / step);
            var w = Complex.One;
            for (int k = 0; k < step; k++)
            {
                for (int i = k; i < n; i += step * 2)  //stepping access, low calculation
                {
                    int j = i + step;
                    Complex t = data[j] * w;  //w = Mt.Phase(isign * Math.PI / step * k);
                    data[j] = data[i] - t;
                    data[i] += t;
                }
                w *= d;
            }
        }
        static void FftFactor2_IdealCode1(Complex[] data, int isign, int step)
        {
            int n = data.Length;
            var d = Mt.Phase(isign * Math.PI / step);
            for (int h = 0; h < n; h += step * 2)
            {
                var w = Complex.One;
                for (int k = 0; k < step; k++)  //sequential access, high calculation
                {
                    int i = h + k;
                    int j = i + step;
                    Complex t = data[j] * w;  //w = Mt.Phase((Mt.PI2 / n) * (isign * (n / (step * 2))) * k);
                    data[j] = data[i] - t;
                    data[i] += t;
                    w *= d;
                }
            }
        }
        unsafe static void FftFactor2(Complex* datacomplex, int n, int isign, int step)
        {
            double* data = (double*)datacomplex;
            double theta = isign * Math.PI / step;
            double ddxr = Math.Cos(theta);
            double ddxi = Math.Sin(theta);
            for (int h = 0; h < n; h += step * 2)
            {
                double dxr = 1.0;
                double dxi = 0.0;
                for (int k = 0; k < step; k++)  //sequential access, high calculation
                {
                    int i = h + k;
                    int j = i + step;
                    double tr = data[j * 2 + 0] * dxr - data[j * 2 + 1] * dxi;
                    double ti = data[j * 2 + 0] * dxi + data[j * 2 + 1] * dxr;
                    data[j * 2 + 0] = data[i * 2 + 0] - tr;
                    data[j * 2 + 1] = data[i * 2 + 1] - ti;
                    data[i * 2 + 0] += tr;
                    data[i * 2 + 1] += ti;
                    double _r = dxr;
                    dxr = _r * ddxr - dxi * ddxi;
                    dxi = _r * ddxi + dxi * ddxr;
                }
            }
        }
        static void FftFactorN_IdealCode1(Complex[] data, int isign, int step, int factor)
        {
            int n = data.Length;
            var temp = new Complex[factor];
            var ddx = Mt.Phase(isign * Mt.PI2 / (step * factor));
            var dy = Mt.Phase(isign * Mt.PI2 / factor);
            for (int h = 0; h < n; h += step * factor)
            {
                var dx = Complex.One;
                for (int s = 0; s < step; s++)
                {
                    for (int ff = 0; ff < factor; ff++) temp[ff] = data[h + s];
                    var x = dx;
                    var y = dy;
                    for (int f = 1; f < factor; f++)
                    {
                        Complex z = data[h + s + f * step] * x;  //x = s * f
                        for (int ff = 0; ff < factor; ff++)
                        {
                            temp[ff] += z;  //z = data[h + s + f * step] * Mt.Phase(isign * Mt.PI2 / (step * factor) * (s + step * ff) * f);
                            z *= y;  //y = step * f
                        }
                        y *= dy;
                        x *= dx;
                    }
                    for (int f = 0; f < factor; f++) data[h + s + f * step] = temp[f];
                    dx *= ddx;
                }
            }
        }
        unsafe static void FftFactorN_TestCode1(Complex* datacomplex, int n, int isign, int step, int factor)
        {
            fixed (Complex* tempcomplex = new Complex[factor])
            {
                double* data = (double*)datacomplex;
                double* temp = (double*)tempcomplex;
                double _r;
                double theta = isign * Mt.PI2 / (step * factor);
                double ddxr = Math.Cos(theta);
                double ddxi = Math.Sin(theta);
                double rho = isign * Mt.PI2 / factor;
                double dyr = Math.Cos(rho);
                double dyi = Math.Sin(rho);
                for (int h = 0; h < n; h += step * factor)
                {
                    double dxr = 1.0;
                    double dxi = 0.0;
                    for (int s = 0; s < step; s++)
                    {
                        for (int ff = 0; ff < factor; ff++) Us.Let(&tempcomplex[ff], &datacomplex[h + s]);
                        double xr = dxr;
                        double xi = dxi;
                        double yr = dyr;
                        double yi = dyi;
                        for (int f = 1; f < factor; f++)
                        {
                            int i = h + s + f * step;
                            double zr = data[i * 2 + 0] * xr - data[i * 2 + 1] * xi;
                            double zi = data[i * 2 + 0] * xi + data[i * 2 + 1] * xr;
                            for (int ff = 0; ff < factor; ff++)
                            {
                                temp[ff * 2 + 0] += zr;
                                temp[ff * 2 + 1] += zi;
                                _r = zr;
                                zr = _r * yr - zi * yi;
                                zi = _r * yi + zi * yr;
                            }
                            _r = yr;
                            yr = _r * dyr - yi * dyi;
                            yi = _r * dyi + yi * dyr;
                            _r = xr;
                            xr = _r * dxr - xi * dxi;
                            xi = _r * dxi + xi * dxr;
                        }
                        for (int f = 0; f < factor; f++) Us.Let(&datacomplex[h + s + f * step], &tempcomplex[f]);
                        _r = dxr;
                        dxr = _r * ddxr - dxi * ddxi;
                        dxi = _r * ddxi + dxi * ddxr;
                    }
                }
            }
        }

        static void FftFactorN_IdealCode2(Complex[] data, int isign, int step, int factor)
        {
            int n = data.Length;
            if (step > 1)
            {
                var ddx = Mt.Phase(isign * Mt.PI2 / (step * factor));
                for (int h = 0; h < n; h += step * factor)
                {
                    var dx = Complex.One;
                    for (int f = 1; f < factor; f++)
                    {
                        dx *= ddx;
                        var x = Complex.One;
                        for (int s = 1; s < step; s++)
                        {
                            x *= dx;
                            data[h + s + f * step] *= x;  //x = Mt.Phase(isign * Mt.PI2 / (step * factor) * (s * f))
                        }
                    }
                }
            }
            var temp = new Complex[factor];
            var dy = Mt.Phase(isign * Mt.PI2 / factor);
            for (int h = 0; h < n; h += step * factor)
            {
                for (int s = 0; s < step; s++)
                {
                    for (int f = 0; f < factor; f++) temp[f] = data[h + s];
                    var y = Complex.One;
                    for (int f = 1; f < factor; f++)
                    {
                        y *= dy;
                        var z = data[h + s + f * step];
                        for (int ff = 0; ff < factor; ff++)
                        {
                            temp[ff] += z;
                            z *= y;
                        }
                    }
                    for (int f = 0; f < factor; f++) data[h + s + f * step] = temp[f];
                }
            }
        }
        unsafe static void FftFactorN(Complex* datacomplex, int n, int isign, int step, int factor)
        {
            double* data = (double*)datacomplex;
            double _r, _i;
            if (step > 1)
            {
                double theta = isign * Mt.PI2 / (step * factor);
                double ddxr = Math.Cos(theta);
                double ddxi = Math.Sin(theta);
                for (int h = 0; h < n; h += step * factor)
                {
                    double dxr = 1.0;
                    double dxi = 0.0;
                    for (int f = 1; f < factor; f++)
                    {
                        _r = dxr;
                        dxr = _r * ddxr - dxi * ddxi;
                        dxi = _r * ddxi + dxi * ddxr;
                        double xr = 1.0;
                        double xi = 0.0;
                        for (int k = 1; k < step; k++)
                        {
                            _r = xr;
                            xr = _r * dxr - xi * dxi;
                            xi = _r * dxi + xi * dxr;
                            int i = h + k + f * step;
                            _r = data[i * 2 + 0];
                            _i = data[i * 2 + 1];
                            data[i * 2 + 0] = _r * xr - _i * xi;
                            data[i * 2 + 1] = _r * xi + _i * xr;
                        }
                    }
                }
            }
            fixed (Complex* tempcomplex = new Complex[factor])
            {
                double* temp = (double*)tempcomplex;
                double rho = isign * Mt.PI2 / factor;
                double ddyr = Math.Cos(rho);
                double ddyi = Math.Sin(rho);
                for (int h = 0; h < n; h += step * factor)
                {
                    for (int s = 0; s < step; s++)
                    {
                        for (int f = 0; f < factor; f++) Us.Let(&tempcomplex[f], &datacomplex[h + s]);
                        double dyr = 1.0;
                        double dyi = 0.0;
                        for (int f = 1; f < factor; f++)
                        {
                            _r = dyr;
                            dyr = _r * ddyr - dyi * ddyi;
                            dyi = _r * ddyi + dyi * ddyr;
                            int i = h + s + f * step;
                            double zr = data[i * 2 + 0];
                            double zi = data[i * 2 + 1];
                            temp[0] += zr;
                            temp[1] += zi;
                            for (int ff = 1; ff < factor; ff++)
                            {
                                _r = zr;
                                temp[ff * 2 + 0] += zr = _r * dyr - zi * dyi;
                                temp[ff * 2 + 1] += zi = _r * dyi + zi * dyr;
                            }
                        }
                        for (int f = 0; f < factor; f++) Us.Let(&datacomplex[h + s + f * step], &tempcomplex[f]);
                    }
                }
            }
        }

        unsafe static void ReversePower2(Complex* data, int n, int interval, Complex* result)
        {
            if (data == result || result == null)
            {
                if (interval != 1) ThrowException.ArgumentException("ReversePower2: interval");
                for (int j = 0, i = 0; i < n; i++)
                {
                    if (i < j) Us.Swap(&data[i], &data[j]);
                    for (int m = n; j < m; j ^= (m >>= 1)) ;
                }
            }
            else
            {
                for (int j = 0, i = 0; i < n; i++)
                {
                    result[i] = data[j * interval];
                    for (int m = n; j < m; j ^= (m >>= 1)) ;
                }
            }
        }
        unsafe static void ReverseFactors(Complex* data, int n, int interval, Complex* result, int[] factors)
        {
            Action func = () =>
            {
                var dim = factors.Length;
                var mm = new int[dim];
                var ii = new int[dim];
                for (int nn = 1, d = dim; --d >= 0; ) { mm[d] = nn; nn *= factors[d]; }
                for (int j = 0, i = 0; i < n; i++)
                {
                    result[i] = data[j * interval];
                    for (int d = 0; d < dim; d++)
                    {
                        if (++ii[d] < factors[d]) { j += mm[d]; break; }
                        ii[d] = 0; j -= mm[d] * (factors[d] - 1);
                    }
                }
            };
            if (data == result || result == null)
            {
                if (interval != 1) ThrowException.ArgumentException("ReverseFactors: interval");
                var temp = new Complex[n];
                fixed (Complex* tempcomplex = temp)
                {
                    Us.Let(tempcomplex, data, n);
                    data = tempcomplex;
                    func();
                }
            }
            else
                func();
        }
        public static Func<int, int[]> FactorIntegerExpandedReverse = New.Cache<int, int[]>(Mt.FactorIntegerExpanded);
        unsafe static void FFT(Complex* data, int n, int interval, Complex* result, int isign)
        {
            if (data == null) ThrowException.ArgumentException("FFT: data");
            if (n < 2) ThrowException.ArgumentException("FFT: n");
            if (interval < 1) ThrowException.ArgumentException("FFT: interval");
            if (isign != -1 && isign != +1) ThrowException.ArgumentException("FFT: isign");
            if ((n & (n - 1)) == 0)
            {
                ReversePower2(data, n, interval, result);
                for (int step = 1; step < n; step *= 2)
                    FftFactor2(result, n, isign, step);
            }
            else
            {
                var factors = FactorIntegerExpandedReverse(n);
                ReverseFactors(data, n, interval, result, factors);
                for (int d = 0, step = 1; step < n; d++)
                {
                    var factor = factors[d];
                    if (factor == 2)
                        FftFactor2(result, n, isign, step);
                    else
                        FftFactorN(result, n, isign, step, factor);
                    step *= factor;
                }
            }
        }
        unsafe static void LetFFT(Complex* data, int n, int isign)
        {
            FFT(data, n, 1, null, isign);
        }

        static void LetFFT_IdealCode(Complex[] data, int isign)
        {
            int n = data.Length;
            if ((n & (n - 1)) == 0)
            {
                for (int j = 0, i = 0; i < n; i++)
                {
                    if (i < j) Mt.Swap(ref data[i], ref data[j]);
                    for (int m = n; j < m; j ^= (m >>= 1)) ;
                }
                for (int step = 1; step < n; step *= 2)
                    FftFactor2_IdealCode1(data, isign, step);
            }
            else
            {
                var factors = FactorIntegerExpandedReverse(n);
                {
                    var dim = factors.Length;
                    var mm = new int[dim];
                    var ii = new int[dim];
                    for (int nn = 1, d = dim; --d >= 0; ) { mm[d] = nn; nn *= factors[d]; }
                    int j = 0;
                    var temp = data.CloneX();
                    for (int i = 0; i < n; i++)
                    {
                        data[i] = temp[j];
                        for (int d = 0; d < dim; d++)
                        {
                            if (++ii[d] < factors[d]) { j += mm[d]; break; }
                            ii[d] = 0; j -= mm[d] * (factors[d] - 1);
                        }
                    }
                }
                for (int d = 0, step = 1; step < n; d++)
                {
                    var factor = factors[d];
                    if (factor == 2)
                        FftFactor2_IdealCode1(data, isign, step);
                    else
                        FftFactorN_IdealCode1(data, isign, step, factor);
                    step *= factor;
                }
            }
        }
        public unsafe static Complex[] FastFourierTransform_IdealCode(Complex[] data)
        {
            var result = data.CloneX();
            LetFFT_IdealCode(result, -1);
            return result;
        }
        public unsafe static Complex[] InverseFastFourierTransform_IdealCode(Complex[] data)
        {
            var result = data.CloneX();
            LetFFT_IdealCode(result, +1);
            result.LetDiv(result.Length);  //divide by n in inverse Fourier transform
            return result;
        }

        public unsafe static Complex[] FastFourierTransform_(Complex[] data, int isign)
        {
            int n = data.Length;
            var result = new Complex[n];
            fixed (Complex* datacomplex = data, resultcomplex = result)
                FFT(datacomplex, n, 1, resultcomplex, isign);
            return result;
        }
        public unsafe static Complex[] FastFourierTransform(Complex[] data)
        {
            return FastFourierTransform_(data, -1);
        }
        public unsafe static Complex[] InverseFastFourierTransform(Complex[] data)
        {
            return FastFourierTransform_(data, +1).LetDiv(data.Length);  //divide by n in inverse Fourier transform
        }

        unsafe static Complex[,] FastFourierTransform2_(Complex[,] data, int isign)
        {
            var l = data.Lengths();
            var result = new Complex[l.v1, l.v0];
            fixed (Complex* datacomplex = data, resultcomplex = result)
                for (int i = 0; i < l.v1; i++)
                    FFT(&datacomplex[i], l.v0, l.v1, &resultcomplex[i * l.v0], isign);
            return result;
        }
        public static Complex[,] FastFourierTransform2(Complex[,] data)
        {
            var result = FastFourierTransform2_(data, -1);
            result = FastFourierTransform2_(result, -1);
            return result;
        }
        public static Complex[,] InverseFastFourierTransform2(Complex[,] data)
        {
            var result = FastFourierTransform2_(data, +1);
            result = FastFourierTransform2_(result, +1);
            result.LetDiv(result.Length);  //divide by n in inverse Fourier transform
            return result;
        }

        static int Remainder2(int x, int y) { x %= y; return (x * 2 <= y) ? x : x - y; }
        public static Complex[] DiscreteFourierTransform(Complex[] data)
        {
            int n = data.Length;
            return New.Array(n, i => Mt.Sum(n, j => data[j] * Mt.Phase(-i * j, n)));
        }
        public static Complex[] InverseDiscreteFourierTransform(Complex[] data)
        {
            int n = data.Length;
            return New.Array(n, i => Mt.Sum(n, j => data[j] * Mt.Phase(+i * j, n)) / n);
        }

        //NumericalRecipesのバグを修正
        unsafe static void LetRealFastFourierTransform_(double* data, int n, int isign)
        {
            int n2 = n >> 1;
            if (isign == -1)
            {
                LetFFT((Complex*)data, n2, isign);
            }
            double theta = -isign * Mt.PI2 / n;
            double dr = Math.Cos(theta);
            double di = Math.Sin(theta);
            double wr = 1.0;
            double wi = 0.0;
            for (int i = (n >> 2); i > 0; i--)
            {
                int j = n2 - i;
                double h1r = data[i * 2 + 0] + data[j * 2 + 0];
                double h1i = data[i * 2 + 1] - data[j * 2 + 1];
                double h2r = data[i * 2 + 0] - data[j * 2 + 0];
                double h2i = data[i * 2 + 1] + data[j * 2 + 1];
                double tr = h2r * wr - h2i * wi;
                double ti = h2r * wi + h2i * wr;
                data[i * 2 + 0] = +0.5 * (h1r - tr);
                data[i * 2 + 1] = +0.5 * (h1i - ti);
                data[j * 2 + 0] = +0.5 * (h1r + tr);
                data[j * 2 + 1] = -0.5 * (h1i + ti);
                double _r = wr;
                wr = _r * dr - wi * di;
                wi = _r * di + wi * dr;
            }
            double d0 = data[0];
            data[0] = d0 + data[1];
            data[1] = d0 - data[1];
            if (isign == +1)
            {
                data[0] *= 0.5;
                data[1] *= 0.5;
                LetFFT((Complex*)data, n2, isign);
            }
        }

        public unsafe static Complex[] RealFastFourierTransform(double[] data)
        {
            int n = data.Length;
            var result = new Complex[n / 2 + 1];
            fixed (Complex* d = result)
            {
                double* data_ = (double*)d;
                for (int i = 0; i < n; i++) data_[i] = data[i];
                LetRealFastFourierTransform_(data_, n, -1);
                data_[n + 0] = data_[1];
                data_[1] = 0;
            }
            return result;
        }
        public unsafe static double[] RealInverseFastFourierTransform(Complex[] data)
        {
            int n = (data.Length - 1) * 2;
            var result = new double[n];
            fixed (double* data_ = result)
            fixed (Complex* d = data)
            {
                double* srcd = (double*)d;
                double c = 2.0 / n;
                for (int i = 0; i < n; i++) data_[i] = srcd[i] * c;
                data_[1] = srcd[n + 0] * c;
                LetRealFastFourierTransform_(data_, n, +1);
            }
            return result;
        }

        // Power Spectral Densityを求めたい。nの増加に伴いそれぞれの周波数カラムの幅(Δf)は狭くなるため、結果のData値は増加する
        // ΣResult   != 単位時間辺りのpower
        // ∫Result df = 単位時間辺りのpower
        // y=a*sin(x) の結果は、a*a*n/2 振幅はa/2でパワーはa*a/4、one-sidedなので2倍してa*a/2、nは上記
        // これは (1/n)*Σ(y*y) の結果と一致する
        // y=a の結果は a*a*n
        public static double[] PowerSpectrumFFT(double[] data, double amplitude)
        {
            Complex[] Freq = RealFastFourierTransform(data);
            Freq.LetMul(Math.Sqrt(2 * amplitude / data.Length));
            double[] Powr = new double[Freq.Length];
            for (int i = Powr.Length; --i >= 0; ) Powr[i] = Mt.Sq(Freq[i].Real) + Mt.Sq(Freq[i].Imaginary);
            Powr[0] /= 2;
            Powr[Powr.Length - 1] /= 2;
            return Powr;
        }

        public static Complex[] PowerPhaseSpectrumFFT(double[] data, double amplitude)
        {
            Complex[] Freq = RealFastFourierTransform(data);
            Freq.LetMul(Math.Sqrt(2 * amplitude / data.Length));
            Complex[] Powr = new Complex[Freq.Length];
            for (int i = Powr.Length; --i >= 0; ) Powr[i] = new Complex(Mt.Sq(Freq[i].Real) + Mt.Sq(Freq[i].Imaginary), Freq[i].Phase);
            var a = Powr[0]; Powr[0] = new Complex(a.Real / 2, a.Imaginary);
            var b = Powr[Powr.Length - 1]; Powr[Powr.Length - 1] = new Complex(b.Real / 2, b.Imaginary);
            return Powr;
        }

        public static Complex[,] RealFastFourierTransform2(double[,] data)
        {
            return FastFourierTransform2(data.NewTo(v => new Complex(v, 0)));
        }
        public static double[,] RealInverseFastFourierTransform2(Complex[,] data)
        {
            return InverseFastFourierTransform2(data).NewTo(v => v.Real);
        }

        unsafe static void FastFourierTransformDimensions_(double* data, int[] nn, int isign)
        {
            Complex* datacomplex = (Complex*)data;
            int ntot = 1, ndim = nn.Length;
            for (int idim = ndim; --idim >= 0; ) ntot *= nn[idim];
            if (ntot < 2 || (ntot & (ntot - 1)) != 0) ThrowException.ArgumentException("must have powers of 2 in fourn");
            int step = 1;
            for (int idim = ndim; --idim >= 0; )
            {
                int nprd = nn[idim] * step;
                int nrem = ntot / nprd;
                int i2rev = 0;
                for (int i2 = 0; i2 < nprd; i2 += step)
                {
                    if (i2 < i2rev)
                    {
                        for (int i1 = i2; i1 < i2 + step; i1++)
                        {
                            for (int i3 = i1; i3 < ntot; i3 += nprd)
                            {
                                int i3rev = i2rev + i3 - i2;
                                Us.Swap(&datacomplex[i3], &datacomplex[i3rev]);
                            }
                        }
                    }
                    int ibit = nprd;
                    while (ibit >= step * 2 && i2rev * 2 + 1 > ibit)
                    {
                        ibit >>= 1;
                        i2rev -= ibit;
                    }
                    i2rev += ibit >> 1;
                }
                for (int ifp1 = step; ifp1 < nprd; ifp1 *= 2)
                {
                    double theta = isign * (Math.PI / (ifp1 / step));
                    double wpr = Math.Cos(theta);
                    double wpi = Math.Sin(theta);
                    double wr = 1.0;
                    double wi = 0.0;
                    for (int i3 = 0; i3 < ifp1; i3 += step)
                    {
                        for (int i1 = i3; i1 < i3 + step; i1++)
                        {
                            for (int i2 = i1; i2 < ntot; i2 += ifp1 * 2)
                            {
                                int k2 = i2 + ifp1;
                                double tr = data[k2 * 2 + 0] * wr - data[k2 * 2 + 1] * wi;
                                double ti = data[k2 * 2 + 0] * wi + data[k2 * 2 + 1] * wr;
                                data[k2 * 2 + 0] = data[i2 * 2 + 0] - tr;
                                data[k2 * 2 + 1] = data[i2 * 2 + 1] - ti;
                                data[i2 * 2 + 0] += tr;
                                data[i2 * 2 + 1] += ti;
                            }
                        }
                        double _r = wr;
                        wr = _r * wpr - wi * wpi;
                        wi = wi * wpr + _r * wpi;
                    }
                }
                step = nprd;
            }
        }

        unsafe static void RealFastFourierTransformDimension3_(double* data, double* speq, int isign, int nn1, int nn2, int nn3)
        {
            int[] nn = new int[3] { nn1, nn2, nn3 >> 1 };
            double*[] spq = new double*[nn1];
            for (int i1 = 0; i1 < nn1; i1++) spq[i1] = speq + 2 * nn2 * i1;
            double c1 = 0.5;
            double c2 = -0.5 * isign;
            double theta = isign * (Mt.PI2 / nn3);
            double wpr = Math.Cos(theta);
            double wpi = Math.Sin(theta);
            if (isign == 1)
            {
                FastFourierTransformDimensions_(data, nn, isign);
                int k1 = 0;
                for (int i1 = 0; i1 < nn1; i1++)
                    for (int i2 = 0, j2 = 0; i2 < nn2; i2++, k1 += nn3)
                    {
                        spq[i1][j2++] = data[k1];
                        spq[i1][j2++] = data[k1 + 1];
                    }
            }
            for (int i1 = 0; i1 < nn1; i1++)
            {
                int j1 = (i1 != 0 ? nn1 - i1 : 0);
                double wr = 1.0;
                double wi = 0.0;
                for (int i3 = 0; i3 <= (nn3 >> 1); i3 += 2)
                {
                    int k1 = i1 * nn2 * nn3;
                    int k3 = j1 * nn2 * nn3;
                    for (int i2 = 0; i2 < nn2; i2++, k1 += nn3)
                    {
                        if (i3 == 0)
                        {
                            int j2 = (i2 != 0 ? ((nn2 - i2) << 1) : 0);
                            double h1r = +c1 * (data[k1 + 0] + spq[j1][j2 + 0]);
                            double h1i = +c1 * (data[k1 + 1] - spq[j1][j2 + 1]);
                            double h2i = +c2 * (data[k1 + 0] - spq[j1][j2 + 0]);
                            double h2r = -c2 * (data[k1 + 1] + spq[j1][j2 + 1]);
                            data[k1 + 0] = h1r + h2r;
                            data[k1 + 1] = h1i + h2i;
                            spq[j1][j2 + 0] = h1r - h2r;
                            spq[j1][j2 + 1] = h2i - h1i;
                        }
                        else
                        {
                            int j2 = (i2 != 0 ? nn2 - i2 : 0);
                            int j3 = nn3 - i3;
                            int k2 = k1 + i3;
                            int k4 = k3 + j2 * nn3 + j3;
                            double h1r = +c1 * (data[k2 + 0] + data[k4 + 0]);
                            double h1i = +c1 * (data[k2 + 1] - data[k4 + 1]);
                            double h2i = +c2 * (data[k2 + 0] - data[k4 + 0]);
                            double h2r = -c2 * (data[k2 + 1] + data[k4 + 1]);
                            data[k2 + 0] = h1r + wr * h2r - wi * h2i;
                            data[k2 + 1] = h1i + wr * h2i + wi * h2r;
                            data[k4 + 0] = h1r - wr * h2r + wi * h2i;
                            data[k4 + 1] = -h1i + wr * h2i + wi * h2r;
                        }
                    }
                    double _r = wr;
                    wr = _r * wpr - wi * wpi;
                    wi = _r * wpi + wi * wpr;
                }
            }
            if (isign == -1) FastFourierTransformDimensions_(data, nn, isign);
        }

        #endregion

        #region Convolution
        static double[] Convolution_(double[] data, double[] response, int isign)
        {
            var n = data.Length;
            var m = response.Length;
            var resp = new double[n];
            for (int i = 0; i < m; i++)
                resp[i + (i < (m + 1) / 2 ? 0 : n - m)] = response[i];
            var datacomplex = RealFastFourierTransform(data);
            var respcomplex = RealFastFourierTransform(resp);
            if (isign == 1)
                datacomplex.LetMul(respcomplex);
            else
                datacomplex.LetDiv(respcomplex);
            datacomplex.LetDiv(n);
            return RealInverseFastFourierTransform(datacomplex);
        }
        static double[,] Convolution_(double[,] data, double[,] response, int isign)
        {
            var n = data.Lengths();
            var m = response.Lengths();
            var resp = new double[n.v0, n.v1];

            for (int i = 0; i < m.v0; i++)
                for (int j = 0; j < m.v1; j++)
                    resp[i + (i < (m.v0 + 1) / 2 ? 0 : n.v0 - m.v0), j + (j < (m.v1 + 1) / 2 ? 0 : n.v1 - m.v1)] = response[i, j];

            var datacomplex = RealFastFourierTransform2(data);
            var respcomplex = RealFastFourierTransform2(resp);
            if (isign == 1)
                datacomplex.LetMul(respcomplex);
            else
                datacomplex.LetDiv(respcomplex);
            datacomplex.LetDiv(n.v0 * n.v1);
            return RealInverseFastFourierTransform2(datacomplex);
        }
        public static double[] Convolution(double[] data, double[] response)
        {
            return Convolution_(data, response, +1);
        }
        public static double[] Deconvolution(double[] data, double[] response)
        {
            return Convolution_(data, response, -1);
        }
        public static double[,] Convolution(double[,] data, double[,] response)
        {
            return Convolution_(data, response, +1);
        }
        public static double[,] Deconvolution(double[,] data, double[,] response)
        {
            return Convolution_(data, response, -1);
        }
        #endregion

        #region Wavelet Transform
        public enum WaveletType
        {
            Daubechies
        }

        static double[] Daubechies4 = new double[] {
            +0.482962913144534143, +0.836516303737807906, +0.224143868042013381, -0.129409522551260381
        };
        static double[] Daubechies6 = new double[] {
            +0.332670552950082616, +0.806891509311092576, +0.459877502118491570, -0.135011020010254589, -0.0854412738820266617, +0.0352262918857095366
        };
        static double[] Daubechies12 = new double[] {
            +0.111540743350, +0.494623890398, +0.751133908021, +0.315250351709, -0.226264693965, -0.129766867567,
            +0.097501605587, +0.027522865530, -0.031582039318, +0.000553842201, +0.004777257511, -0.001077301085
        };
        static double[] Daubechies20 = new double[] {
            +0.026670057901, +0.188176800078, +0.527201188932, +0.688459039454, +0.281172343661, -0.249846424327,
            -0.195946274377, +0.127369340336, +0.093057364604, -0.071394147166, -0.029457536822, +0.033212674059,
            +0.003606553567, -0.010733175483, +0.001395351747, +0.001992405295, -0.000685856695, -0.000116466855,
            +0.000093588670, -0.000013264203
        };

        static double[] GetWaveletFilter(WaveletType type, int order)
        {
            switch (type)
            {
                case WaveletType.Daubechies:
                    switch (order)
                    {
                        case 4: return Daubechies4;
                        case 6: return Daubechies6;
                        case 12: return Daubechies12;
                        case 20: return Daubechies20;
                    }
                    break;
            }
            ThrowException.WriteLine("unknown wavelet filter");
            return null;
        }

        public static double[] WaveletTransform_(double[] data, WaveletType type, int filterlength, int level, int isign)
        {
            var filter0 = GetWaveletFilter(type, filterlength);
            var filter1 = New.Array(filter0.Length, i => filter0[filter0.Length - 1 - i] * ((i & 1) == 0 ? 1 : -1));

            var result = new double[data.Length];
            var source = default(double[]);
            for (int n = data.Length, l = 0; n >= filterlength && l < level; n /= 2, l++)
            {
                source = source == null ? data : New.Array(n, i => result[i]);
                int mask = n - 1;
                int h = n >> 1;
                int nmod = filterlength * n - filterlength / 2 + 2;
                if (isign == 1)
                {
                    for (int ii = 0, i = 0; i < n; i += 2, ii++)
                    {
                        int nn = nmod + i;
                        for (int k = 0; k < filterlength; k++)
                        {
                            var a = source[(nn + k) & mask];
                            result[ii + 0] += filter0[k] * a;
                            result[ii + h] += filter1[k] * a;
                        }
                    }
                }
                else
                {
                    for (int ii = 0, i = 0; i < n; i += 2, ii++)
                    {
                        var a0 = source[ii + 0];
                        var a1 = source[ii + h];
                        int nn = nmod + i;
                        for (int k = 0; k < filterlength; k++)
                        {
                            result[(nn + k) & mask] += filter0[k] * a0 + filter1[k] * a1;
                        }
                    }
                }
            }
            return result;
        }
        static Complex[] WaveletTransform_(Complex[] data, WaveletType type, int filterlength, int level, int isign)
        {
            var real = WaveletTransform_(data.Real(), type, filterlength, level, isign);
            var imag = WaveletTransform_(data.Imag(), type, filterlength, level, isign);
            return Mt.ToComplex(real, imag);
        }

        public static double[] WaveletTransform(double[] data, WaveletType type, int filterlength, int level)
        {
            return WaveletTransform_(data, type, filterlength, level, +1);
        }
        public static double[] InverseWaveletTransform(double[] data, WaveletType type, int filterlength, int level)
        {
            return WaveletTransform_(data, type, filterlength, level, -1);
        }
        public static Complex[] WaveletTransform(Complex[] data, WaveletType type, int filterlength, int level)
        {
            return WaveletTransform_(data, type, filterlength, level, +1);
        }
        public static Complex[] InverseWaveletTransform(Complex[] data, WaveletType type, int filterlength, int level)
        {
            return WaveletTransform_(data, type, filterlength, level, -1);
        }

        public static double[,] WaveletTransform(double[,] data, WaveletType type, int filterlength, int level)
        {
            var l = data.Lengths();
            var data0 = New.Array(l.v0, v0 => Nm.WaveletTransform_(New.Array(l.v1, v1 => data[v0, v1]), type, filterlength, level, +1));
            var data1 = New.Array(l.v1, v1 => Nm.WaveletTransform_(New.Array(l.v0, v0 => data0[v0][v1]), type, filterlength, level, +1));
            return New.Array(l, (i0, i1) => data1[i1][i0]);
        }
        public static double[,] InverseWaveletTransform(double[,] data, WaveletType type, int filterlength, int level)
        {
            var l = data.Lengths();
            var data0 = New.Array(l.v0, v0 => Nm.WaveletTransform_(New.Array(l.v1, v1 => data[v0, v1]), type, filterlength, level, -1));
            var data1 = New.Array(l.v1, v1 => Nm.WaveletTransform_(New.Array(l.v0, v0 => data0[v0][v1]), type, filterlength, level, -1));
            return New.Array(l, (i0, i1) => data1[i1][i0]);
        }
        public static Complex[,] WaveletTransform(Complex[,] data, WaveletType type, int filterlength, int level)
        {
            var l = data.Lengths();
            var data0 = New.Array(l.v0, v0 => Nm.WaveletTransform_(New.Array(l.v1, v1 => data[v0, v1]), type, filterlength, level, +1));
            var data1 = New.Array(l.v1, v1 => Nm.WaveletTransform_(New.Array(l.v0, v0 => data0[v0][v1]), type, filterlength, level, +1));
            return New.Array(l, (i0, i1) => data1[i1][i0]);
        }
        public static Complex[,] InverseWaveletTransform(Complex[,] data, WaveletType type, int filterlength, int level)
        {
            var l = data.Lengths();
            var data0 = New.Array(l.v0, v0 => Nm.WaveletTransform_(New.Array(l.v1, v1 => data[v0, v1]), type, filterlength, level, -1));
            var data1 = New.Array(l.v1, v1 => Nm.WaveletTransform_(New.Array(l.v0, v0 => data0[v0][v1]), type, filterlength, level, -1));
            return New.Array(l, (i0, i1) => data1[i1][i0]);
        }


        unsafe static void unpackdouble(double* x, int n, int nc, int k, double* y)
        {
            for (int i = 0; i < n; i++) *y++ = x[k + nc * i];
        }

        unsafe static void packdouble(double* x, int n, int nc, int k, double* y)
        {
            for (int i = 0; i < n; i++) y[k + nc * i] = *x++;
        }
        unsafe static void copydouble(double* x, double* y, int n)
        {
            while (n-- > 0) *y++ = *x++;
        }
        unsafe static void adddouble(double* x, double* y, int n, double* z)
        {
            while (n-- > 0) *z++ = *x++ + *y++;
        }


        // sig: data
        // nr: # of rows
        // nc: # of columns
        // ell: level
        // J: log2(nr)
        // hpf: highpass filter
        // lpf: lowpass filter
        // lenfil: length of filter
        // wc: result
        // temp: working memory
        //unsafe static void dpwt2(double* sig, int nr, int nc, int ell, int J, double* hpf, double* lpf, int lenfil, double* wc, double* temp)
        unsafe static void dpwt2(double* sig, int nr, int nc, int ell, int J, double* hpf, double* lpf, int lenfil, double* wc, double* temp)
        {
            double* wcplo, wcphi;
            copydouble(sig, wc, nr * nc);
            double* templo = &temp[nr];
            double* temphi = &temp[2 * nr];

            for (int nj = nr, j = J; --j >= ell; nj /= 2)
            {
                for (int k = 0; k < nj; k++)
                {
                    wcplo = &wc[k * nr];
                    wcphi = &wc[k * nr + nj / 2];
                    copydouble(wcplo, temp, nj);
                    downlo(temp, nj, lpf, lenfil, wcplo);
                    downhi(temp, nj, hpf, lenfil, wcphi);
                }
                for (int k = 0; k < nj; k++)
                {
                    unpackdouble(wc, nj, nc, k, temp);
                    downlo(temp, nj, lpf, lenfil, templo);
                    downhi(temp, nj, hpf, lenfil, temphi);
                    packdouble(templo, nj / 2, nc, k, wc);
                    packdouble(temphi, nj / 2, nc, k, &wc[nj / 2 * nr]);
                }
            }
        }
        unsafe static void downlo(double* x, int n, double* lpf, int m, double* y)
        {
            /*lowpass version */
            var n2 = n / 2;
            var mlo = m / 2;
            var mhi = n2 - mlo;
            if (2 * mhi + (m - 1) >= n) --mhi;
            if (mhi < 0) mhi = -1;
            for (int i = 0; i <= mhi; i++)
            {
                var s = 0.0;
                for (int h = 0; h < m; h++)
                    s += lpf[h] * x[2 * i + h];
                y[i] = s;
            }

            /* fix up edge values */
            for (int i = mhi + 1; i < n2; i++)
            {
                var s = 0.0;
                var j = 2 * i;
                for (int h = 0; h < m; h++)
                {
                    if (j >= n) j -= n;
                    s += lpf[h] * x[j];
                    j++;
                }
                y[i] = s;
            }
        }
        unsafe static void downhi(double* x, int n, double* hpf, int m, double* y)
        {
            /* highpass version */
            var n2 = n / 2;
            var mlo = m / 2 - 1;
            if (2 * mlo + 1 - (m - 1) < 0) mlo++;
            for (int i = mlo; i < n2; i++)
            {
                var s = 0.0;
                for (int h = 0; h < m; h++)
                    s += hpf[h] * x[2 * i + 1 - h];
                y[i] = s;
            }
            if (mlo > n2) mlo = n2;
            /* fix up edge values */
            for (int i = 0; i < mlo; i++)
            {
                var s = 0.0;
                int j = 2 * i + 1;
                for (int h = 0; h < m; h++)
                {
                    if (j < 0) j += n;
                    s += hpf[h] * x[j];
                    --j;
                }
                y[i] = s;
            }
        }
        unsafe static void idpwt2(double* wc, int nr, int nc, int ell, int J, double* hpf, double* lpf, int lenfil, double* img, double* temp)
        {
            double* wcplo, wcphi;
            copydouble(wc, img, nr * nc);
            var templo = &temp[nr];
            var temphi = &temp[2 * nr];
            var temptop = &temp[3 * nr];

            int nj = 1;
            for (int k = 0; k < ell; k++) nj *= 2;

            for (int j = ell; j < J; j++)
            {
                for (int k = 0; k < 2 * nj; k++)
                {
                    unpackdouble(img, nj, nc, k, templo);
                    unpackdouble(&img[nj * nr], nj, nc, k, temphi);
                    uplo(templo, nj, lpf, lenfil, temp);
                    uphi(temphi, nj, hpf, lenfil, temptop);
                    adddouble(temp, temptop, nj * 2, temp);
                    packdouble(temp, nj * 2, nc, k, img);
                }

                for (int k = 0; k < 2 * nj; k++)
                {
                    wcplo = &img[k * nr];
                    wcphi = &img[k * nr + nj];
                    copydouble(wcplo, temp, nj);
                    uplo(wcplo, nj, lpf, lenfil, templo);
                    uphi(wcphi, nj, hpf, lenfil, temphi);
                    adddouble(templo, temphi, nj * 2, wcplo);
                }
                nj *= 2;
            }
        }

        unsafe static void uplo(double* x, int n, double* lpf, int m, double* y)
        {
            int meven, modd, j, mmax;
            double s;
            /*lowpass version */
            /* away from edges */
            meven = (m + 1) / 2; modd = m / 2;
            for (int i = meven; i < n; i++)
            {
                s = 0.0;
                for (int h = 0; h < meven; h++)
                    s += lpf[2 * h] * x[i - h];
                y[2 * i] = s;
                s = 0.0;
                for (int h = 0; h < modd; h++)
                    s += lpf[2 * h + 1] * x[i - h];
                y[2 * i + 1] = s;
            }
            /* fix up edge values */
            mmax = meven;
            if (mmax > n) mmax = n;
            for (int i = 0; i < mmax; i++)
            {

                s = 0.0;
                j = i;
                for (int h = 0; h < meven; h++)
                {
                    if (j < 0) j += n;
                    s += lpf[2 * h] * x[j];
                    --j;
                }
                y[2 * i] = s;

                s = 0.0;
                j = i;
                for (int h = 0; h < modd; h++)
                {
                    if (j < 0) j += n;
                    s += lpf[2 * h + 1] * x[j];
                    --j;
                }
                y[2 * i + 1] = s;
            }
        }
        unsafe static void uphi(double* x, int n, double* hpf, int m, double* y)
        {
            int meven, modd, j, mmin;
            double s;
            /*hipass version */
            meven = (m + 1) / 2;
            modd = m / 2;
            /* away from edges */
            for (int i = 0; i + meven < n; i++)
            {
                s = 0.0;
                for (int h = 0; h < meven; h++)
                    s += hpf[2 * h] * x[i + h];
                y[2 * i + 1] = s;
                s = 0.0;
                for (int h = 0; h < modd; h++)
                    s += hpf[2 * h + 1] * x[i + h];
                y[2 * i] = s;
            }
            /* fix up edge values */
            mmin = n - meven;
            if (mmin < 0) mmin = 0;
            for (int i = mmin; i < n; i++)
            {

                s = 0.0;
                j = i;
                for (int h = 0; h < meven; h++)
                {
                    if (j >= n) j -= n;
                    s += hpf[2 * h] * x[j];
                    j++;
                }
                y[2 * i + 1] = s;

                s = 0.0;
                j = i;
                for (int h = 0; h < modd; h++)
                {
                    if (j >= n) j -= n;
                    s += hpf[2 * h + 1] * x[j];
                    j++;
                }
                y[2 * i] = s;
            }
        }

        public static double[,] WaveletTransform2(double[,] data, WaveletType type, int filterlength, int level)
        {
            var filter0 = GetWaveletFilter(type, filterlength);
            var filter1 = New.Array(filter0.Length, i => filter0[i] * ((i & 1) == 0 ? 1 : -1));
            var size = data.Lengths();
            var result = new double[size.v0, size.v1];
            var workingmem = new double[size.v0, 3];
            unsafe
            {
                fixed (double* sig = data, hpf = filter1, lpf = filter0, wc = result, temp = workingmem)
                {
                    int J = Mt.Log2Ceiling(size.v0);
                    dpwt2(sig, size.v0, size.v1, level, J, hpf, lpf, filterlength, wc, temp);
                }
            }
            return result;
        }
        public static double[,] InverseWaveletTransform2(double[,] data, WaveletType type, int filterlength, int level)
        {
            var filter0 = GetWaveletFilter(type, filterlength);
            var filter1 = New.Array(filter0.Length, i => filter0[i] * ((i & 1) == 0 ? 1 : -1));
            var size = data.Lengths();
            var result = new double[size.v0, size.v1];
            var workingmem = new double[size.v0, 4];
            unsafe
            {
                fixed (double* sig = data, hpf = filter1, lpf = filter0, wc = result, temp = workingmem)
                {
                    int J = Mt.Log2Ceiling(size.v0);
                    idpwt2(sig, size.v0, size.v1, level, J, hpf, lpf, filterlength, wc, temp);
                }
            }
            return result;
        }
        public static Complex[,] WaveletTransform2(Complex[,] data, WaveletType type, int filterlength, int level)
        {
            var real = WaveletTransform2(data.Real(), type, filterlength, level);
            var imag = WaveletTransform2(data.Imag(), type, filterlength, level);
            return Mt.ToComplex(real, imag);
        }
        public static Complex[,] InverseWaveletTransform2(Complex[,] data, WaveletType type, int filterlength, int level)
        {
            var real = InverseWaveletTransform2(data.Real(), type, filterlength, level);
            var imag = InverseWaveletTransform2(data.Imag(), type, filterlength, level);
            return Mt.ToComplex(real, imag);
        }
        #endregion

        #endregion

        #region Numerical optimization functions

        // NumericalRecipes3.rtbis
        public static double rtbis(Func<double, double> func, double x1, double x2, double xacc)
        {
            const int maxIteration = 50;
            double dx, xmid, rtb;
            double f = func(x1);
            double fmid = func(x2);
            if (f * fmid >= 0) ThrowException.WriteLine("Root must be bracketed for bisection in rtbis");
            if (f < 0) { dx = x2 - x1; rtb = x1; }
            else { dx = x1 - x2; rtb = x2; }
            for (int j = 0; j < maxIteration; j++)
            {
                fmid = func(xmid = rtb + (dx *= 0.5));
                if (fmid <= 0) rtb = xmid;
                if (Math.Abs(dx) < xacc || fmid == 0) return rtb;
            }
            ThrowException.WriteLine("Too many bisections in rtbis");
            return double.NaN;
        }

        // NumericalRecipes3.rtflsp
        public static double rtflsp(Func<double, double> func, double x1, double x2, double xacc)
        {
            const int maxIteration = 30;
            double xl, xh, del;
            double fl = func(x1);
            double fh = func(x2);
            if (fl * fh > 0) ThrowException.WriteLine("Root must be bracketed in rtflsp");
            if (fl < 0) { xl = x1; xh = x2; }
            else { xl = x2; xh = x1; Mt.Swap(ref fl, ref fh); }
            double dx = xh - xl;
            for (int j = 0; j < maxIteration; j++)
            {
                double rtf = xl + dx * fl / (fl - fh);
                double f = func(rtf);
                if (f < 0.0) { del = xl - rtf; xl = rtf; fl = f; }
                else { del = xh - rtf; xh = rtf; fh = f; }
                dx = xh - xl;
                if (Math.Abs(del) < xacc || f == 0) return rtf;
            }
            ThrowException.WriteLine("Maximum number of iterations exceeded in rtflsp");
            return double.NaN;
        }

        // NumericalRecipes3.rtsec
        public static double rtsec(Func<double, double> func, double x1, double x2, double xacc)
        {
            const int maxIteration = 30;
            double xl, rts;
            double fl = func(x1);
            double f = func(x2);
            if (Math.Abs(fl) < Math.Abs(f)) { rts = x1; xl = x2; Mt.Swap(ref fl, ref f); }
            else { xl = x1; rts = x2; }
            for (int j = 0; j < maxIteration; j++)
            {
                double dx = (xl - rts) * f / (f - fl);
                xl = rts;
                fl = f;
                rts += dx;
                f = func(rts);
                if (Math.Abs(dx) < xacc || f == 0) return rts;
            }
            ThrowException.WriteLine("Maximum number of iterations exceeded in rtsec");
            return double.NaN;
        }

        // NumericalRecipes3.zriddr
        public static double zriddr(Func<double, double> func, double x1, double x2, double xacc)
        {
            const int maxIteration = 60;
            double fl = func(x1);
            double fh = func(x2);
            if ((fl > 0 && fh < 0) || (fl < 0 && fh > 0))
            {
                double xl = x1;
                double xh = x2;
                double ans = -9.99e99;
                for (int j = 0; j < maxIteration; j++)
                {
                    double xm = 0.5 * (xl + xh);
                    double fm = func(xm);
                    double s = Math.Sqrt(fm * fm - fl * fh);
                    if (s == 0.0) return ans;
                    double xnew = xm + (xm - xl) * ((fl >= fh ? 1 : -1) * fm / s);
                    if (Math.Abs(xnew - ans) <= xacc) return ans;
                    ans = xnew;
                    double fnew = func(ans);
                    if (fnew == 0.0) return ans;
                    if (samesign(fm, fnew) != fm) { xl = xm; fl = fm; xh = ans; fh = fnew; }
                    else if (samesign(fl, fnew) != fl) { xh = ans; fh = fnew; }
                    else if (samesign(fh, fnew) != fh) { xl = ans; fl = fnew; }
                    else ThrowException.WriteLine("never get here.");
                    if (Math.Abs(xh - xl) <= xacc) return ans;
                }
                ThrowException.WriteLine("zriddr exceed maximum iterations");
            }
            else
            {
                if (fl == 0) return x1;
                if (fh == 0) return x2;
                ThrowException.WriteLine("root must be bracketed in zriddr.");
            }
            return double.NaN;
        }

        // NumericalRecipes3.zbrent
        public static double FindRootBrent(Func<double, double> function, double start, double end, double tolerance)
        {
            const int maxIteration = 100;
            double a = start, b = end, c = end, d = 0, e = 0;
            double fa = function(a), fb = function(b), fc = fb;
            if ((fa > 0 && fb > 0) || (fa < 0 && fb < 0)) { ThrowException.ArgumentException("Root must be bracketed in zbrent"); return double.NaN; }
            for (int i = 0; i < maxIteration; i++)
            {
                if ((fb > 0 && fc > 0) || (fb < 0 && fc < 0)) { c = a; fc = fa; e = d = b - a; }
                if (Math.Abs(fc) < Math.Abs(fb)) { a = b; b = c; c = a; fa = fb; fb = fc; fc = fa; }
                double tol = 2 * Mt.DoubleEps * Math.Abs(b) + tolerance / 2;
                double xm = (c - b) / 2;
                if (Math.Abs(xm) <= tol || fb == 0) return b;
                if (Math.Abs(e) >= tol && Math.Abs(fa) > Math.Abs(fb))
                {  // 逆二乗補間
                    double s = fb / fa, p, q;
                    if (a == c)
                    {
                        p = 2 * xm * s;
                        q = 1 - s;
                    }
                    else
                    {
                        double t = fa / fc;
                        double r = fb / fc;
                        p = s * (2 * xm * t * (t - r) - (b - a) * (r - 1));
                        q = (t - 1) * (r - 1) * (s - 1);
                    }
                    if (p > 0) q *= -1; else p *= -1;
                    if (2 * p < Math.Min(3 * xm * q - Math.Abs(tol * q), Math.Abs(e * q))) { e = d; d = p / q; }  // 成功
                    else { d = xm; e = d; }  // 二分法
                }
                else { d = xm; e = d; }  // 二分法
                a = b; fa = fb;
                b += (Math.Abs(d) > tol) ? d : (xm >= 0 ? tol : -tol);
                fb = function(b);
            }
            ThrowException.WriteLine("FindRootBrent: cannot find a root");
            return double.NaN;
        }

        // NumericalRecipes3.rtnewt
        public static double rtnewt(Func<double, double> funcd, Func<double, double> funcddf, double x1, double x2, double xacc)
        {
            const int JMAX = 20;
            double rtn = 0.5 * (x1 + x2);
            for (int j = 0; j < JMAX; j++)
            {
                double f = funcd(rtn);
                double df = funcddf(rtn);
                double dx = f / df;
                rtn -= dx;
                if ((x1 - rtn) * (rtn - x2) < 0)
                    ThrowException.WriteLine("Jumped out of brackets in rtnewt");
                if (Math.Abs(dx) < xacc) return rtn;
            }
            ThrowException.WriteLine("Maximum number of iterations exceeded in rtnewt");
            return double.NaN;
        }

        // NumericalRecipes3.rtsafe
        public static double rtsafe(Func<double, double> funcd, Func<double, double> funcddf, double x1, double x2, double xacc)
        {
            const int maxIteration = 100;
            double xh, xl;
            double fl = funcd(x1);
            double fh = funcd(x2);
            if ((fl > 0 && fh > 0) || (fl < 0 && fh < 0))
                ThrowException.WriteLine("Root must be bracketed in rtsafe");
            if (fl == 0) return x1;
            if (fh == 0) return x2;
            if (fl < 0) { xl = x1; xh = x2; }
            else { xh = x1; xl = x2; }
            double rts = 0.5 * (x1 + x2);
            double dxold = Math.Abs(x2 - x1);
            double dx = dxold;
            double f = funcd(rts);
            double df = funcddf(rts);
            for (int j = 0; j < maxIteration; j++)
            {
                if ((((rts - xh) * df - f) * ((rts - xl) * df - f) > 0) || (Math.Abs(2 * f) > Math.Abs(dxold * df)))
                {
                    dxold = dx;
                    dx = 0.5 * (xh - xl);
                    rts = xl + dx;
                    if (xl == rts) return rts;
                }
                else
                {
                    dxold = dx;
                    dx = f / df;
                    double temp = rts;
                    rts -= dx;
                    if (temp == rts) return rts;
                }
                if (Math.Abs(dx) < xacc) return rts;
                f = funcd(rts);
                df = funcddf(rts);
                if (f < 0) xl = rts;
                else xh = rts;
            }
            ThrowException.WriteLine("Maximum number of iterations exceeded in rtsafe");
            return double.NaN;
        }



        static double samesign(double a, double b) { return b >= 0 ? (a >= 0 ? a : -a) : (a >= 0 ? -a : a); }
        static double mulsign(double a, double b) { return b >= 0 ? a : -a; }

        // NumericalRecipes3.Bracketmethod
        static V3<double, double, double> CalcBracket(double a, double b, Func<double, double> func)
        {
            const double GOLD = 1.618034;
            const double GLIMIT = 100.0;
            const double TINY = 1.0e-20;
            var ax = a;
            var bx = b;
            var fa = func(ax);
            var fb = func(bx);
            if (fb > fa)
            {
                Mt.Swap(ref ax, ref bx);
                Mt.Swap(ref fb, ref fa);
            }
            var cx = bx + GOLD * (bx - ax);
            var fc = func(cx);
            while (fb > fc)
            {
                double r = (bx - ax) * (fb - fc);
                double q = (bx - cx) * (fb - fa);
                double u = bx - ((bx - cx) * q - (bx - ax) * r) / (2.0 * samesign(Math.Max(Math.Abs(q - r), TINY), q - r));
                double ulim = bx + GLIMIT * (cx - bx);
                double fu;
                if ((bx - u) * (u - cx) > 0.0)
                {
                    fu = func(u);
                    if (fu < fc)
                    {
                        ax = bx; bx = u;
                        fa = fb; fb = fu;
                        break;
                    }
                    else if (fu > fb)
                    {
                        cx = u;
                        fc = fu;
                        break;
                    }
                    u = cx + GOLD * (cx - bx);
                    fu = func(u);
                }
                else if ((cx - u) * (u - ulim) > 0.0)
                {
                    fu = func(u);
                    if (fu < fc)
                    {
                        bx = cx; cx = u; u += GOLD * (u - cx);
                        fb = fc; fc = fu; fu = func(u);
                    }
                }
                else if ((u - ulim) * (ulim - cx) >= 0.0)
                {
                    u = ulim;
                    fu = func(u);
                }
                else
                {
                    u = cx + GOLD * (cx - bx);
                    fu = func(u);
                }
                ax = bx; bx = cx; cx = u;
                fa = fb; fb = fc; fc = fu;
            }
            return New.V3(ax, bx, cx);
        }

        // NumericalRecipes3.Golden
        static V2<double, double> MinimizeGolden(Func<double, double> func, double ax, double bx, double cx)
        {
            const double tol = 3.0e-8;
            const double R = 0.61803399;
            const double C = 1.0 - R;

            double x1, x2;
            double x0 = ax;
            double x3 = cx;
            if (Math.Abs(cx - bx) > Math.Abs(bx - ax))
            {
                x1 = bx;
                x2 = bx + C * (cx - bx);
            }
            else
            {
                x2 = bx;
                x1 = bx - C * (bx - ax);
            }
            double f1 = func(x1);
            double f2 = func(x2);
            while (Math.Abs(x3 - x0) > tol * (Math.Abs(x1) + Math.Abs(x2)))
            {
                if (f2 < f1)
                {
                    x0 = x1; x1 = x2; x2 = R * x2 + C * x3;
                    f1 = f2; f2 = func(x2);
                }
                else
                {
                    x3 = x2; x2 = x1; x1 = R * x1 + C * x0;
                    f2 = f1; f1 = func(x1);
                }
            }
            if (f1 < f2)
                return New.V2(x1, f1);
            else
                return New.V2(x2, f2);
        }

        // NumericalRecipes3.Brent
        static V2<double, double> MinimizeBrent(Func<double, double> func, V3<double, double, double> bracket)
        {
            //Console.Write("Brent:");
            const double tolerance = 3e-8;
            const int maxIteration = 100;
            const double goldSection = 0.3819660;
            const double ZEPS = Mt.DoubleEps * 1e-3;

            var a = bracket.v0;
            var b = bracket.v2;
            Mt.LetOrder(ref a, ref b);
            double v, w, x; v = w = x = bracket.v1;
            double fv, fw, fx; fv = fw = fx = func(x);
            double d = 0.0, e = 0.0;
            for (int iteration = 0; ; iteration++)
            {
                if (iteration == maxIteration) { ThrowException.WriteLine("MinimizeBrent: too many iterations"); break; }
                var m = 0.5 * (a + b);
                var tol1 = tolerance * Math.Abs(x) + ZEPS;
                var tol2 = 2.0 * tol1;
                if (Math.Abs(x - m) <= (tol2 - 0.5 * (b - a))) break;

                double be = e, bd = d;
                e = (x >= m ? a : b) - x; d = goldSection * e;  //黄金分割
                if (Math.Abs(be) > tol1)
                {
                    var vw = (x - v) * (fx - fw);
                    var wv = (x - w) * (fx - fv);
                    var p = (x - v) * vw - (x - w) * wv;
                    var q = 2.0 * (vw - wv);
                    if (q > 0) p *= -1; else q *= -1;
                    if (Math.Abs(p) < Math.Abs(0.5 * q * be) && p > q * (a - x) && p < q * (b - x))
                    {
                        e = bd; d = p / q;  //放物線補間
                        if (x + d - a < tol2 || b - (x + d) < tol2) d = mulsign(tol1, m - x);
                    }
                }

                var u = x + (Math.Abs(d) >= tol1 ? d : mulsign(tol1, d));
                var fu = func(u);
                if (fu <= fx)
                {
                    if (u >= x) a = x; else b = x;
                    v = w; fv = fw;
                    w = x; fw = fx;
                    x = u; fx = fu;
                }
                else
                {
                    if (u < x) a = u; else b = u;
                    if (fu <= fw || w == x)
                    {
                        v = w; fv = fw;
                        w = u; fw = fu;
                    }
                    else if (fu <= fv || v == x || v == w)
                    {
                        v = u; fv = fu;
                    }
                }
            }
            //Console.WriteLine();
            return New.V2(x, fx);
        }

        // NumericalRecipes3.Dbrent
        static V2<double, double> MinimizeDbrent(Func<double, double> funcd, Func<double, double> gradient, V3<double, double, double> bracket)
        {
            const double tol = 3.0e-8;
            const int ITMAX = 100;
            const double ZEPS = Mt.DoubleEps * 1.0e-3;

            var a = bracket.v0;
            var b = bracket.v2;
            Mt.LetOrder(ref a, ref b);
            double v, w, x; x = w = v = bracket.v1;
            double fv, fw, fx; fw = fv = fx = funcd(x);
            double dv, dw, dx; dw = dv = dx = gradient(x);
            double d = 0.0, e = 0.0;
            for (int iterations = 0; ; iterations++)
            {
                if (iterations == ITMAX) { ThrowException.WriteLine("MinimizeDbrent: too many iterations"); break; }
                var m = 0.5 * (a + b);
                var tol1 = tol * Math.Abs(x) + ZEPS;
                var tol2 = 2.0 * tol1;
                if (Math.Abs(x - m) <= (tol2 - 0.5 * (b - a))) break;
                if (Math.Abs(e) > tol1)
                {
                    var d1 = 2.0 * (b - a);
                    var d2 = d1;
                    if (dw != dx) d1 = (w - x) * dx / (dx - dw);
                    if (dv != dx) d2 = (v - x) * dx / (dx - dv);
                    var u1 = x + d1;
                    var u2 = x + d2;
                    var ok1 = (a - u1) * (u1 - b) > 0.0 && dx * d1 <= 0.0;
                    var ok2 = (a - u2) * (u2 - b) > 0.0 && dx * d2 <= 0.0;
                    var olde = e;
                    e = d;
                    if (ok1 || ok2)
                    {
                        if (ok1 && ok2)
                            d = (Math.Abs(d1) < Math.Abs(d2) ? d1 : d2);
                        else if (ok1)
                            d = d1;
                        else
                            d = d2;
                        if (Math.Abs(d) <= Math.Abs(0.5 * olde))
                        {
                            if (x + d - a < tol2 || b - (x + d) < tol2) d = samesign(tol1, m - x);
                        }
                        else
                        {
                            e = (dx >= 0.0 ? a - x : b - x);
                            d = 0.5 * e;
                        }
                    }
                    else
                    {
                        e = (dx >= 0.0 ? a - x : b - x);
                        d = 0.5 * e;
                    }
                }
                else
                {
                    e = (dx >= 0.0 ? a - x : b - x);
                    d = 0.5 * e;
                }

                double u;
                double fu;
                if (Math.Abs(d) >= tol1)
                {
                    u = x + d;
                    fu = funcd(u);
                }
                else
                {
                    u = x + mulsign(tol1, d);
                    fu = funcd(u);
                    if (fu > fx) break;
                }
                var du = gradient(u);
                if (fu <= fx)
                {
                    if (u >= x) a = x; else b = x;
                    v = w; fv = fw; dv = dw;
                    w = x; fw = fx; dw = dx;
                    x = u; fx = fu; dx = du;
                }
                else
                {
                    if (u < x) a = u; else b = u;
                    if (fu <= fw || w == x)
                    {
                        v = w; fv = fw; dv = dw;
                        w = u; fw = fu; dw = du;
                    }
                    else if (fu < fv || v == x || v == w)
                    {
                        v = u; fv = fu; dv = du;
                    }
                }
            }
            return New.V2(fx, x);
        }

        // NumericalRecipes3.Linemethod
        static V2<double[], double> ArgminAlongLine(Func<double[], double> function, double[] argument, double[] direction)
        {
            Func<double, double> newfunction = (double x) => function(Mt.AddMul(argument, direction, x));
            var bracket = CalcBracket(0.0, 1.0, newfunction);
            var brent = MinimizeBrent(newfunction, bracket);
            var arg = Mt.AddMul(argument, direction, brent.v0);
            return New.V2(arg, brent.v1);
        }

        // NumericalRecipes3.Frprmn
        public static Tuple<double[], double> ArgminConjugateGradient(Func<double[], double> function, Func<double[], double[]> gradient, double[] initialArgument, int iterationsMax = 200)
        {
            const double EPS = 1e-18;
            const double toleranceFunction = 3e-8;
            const double toleranceGradient = 1e-8;

            int n = initialArgument.Length;
            var arg = initialArgument.CloneX();
            var func = function(arg);
            var grad = gradient(arg).LetNeg();
            var g = grad.CloneX();
            for (int iterations = 0; ; iterations++)
            {
                if (iterations == iterationsMax)
                {
                    ThrowException.WriteLine("ArgminConjugateGradient: too many iterations");
                    break;
                }
                var h = grad.CloneX();
                var backup = func;
                var result = ArgminAlongLine(function, arg, grad);
                arg = result.v0;
                func = result.v1;
                if (2.0 * Math.Abs(func - backup) / (Math.Abs(func) + Math.Abs(backup) + EPS) <= toleranceFunction) break;
                grad = gradient(arg).LetNeg();
                if (Ex.Range(n).Max(j => Math.Abs(grad[j]) * Math.Max(Math.Abs(arg[j]), 1.0)) / Math.Max(func, 1.0) < toleranceGradient) break;

                var gg = g.SqNorm2();
                if (gg == 0.0) break;
                double gam = (grad.SqNorm2() - Mt.Inner(grad, g)) / gg;
                g = grad.CloneX();
                grad = Mt.AddMul(g, h, gam);
            }
            return Tuple.Create(arg, func);
        }

        // NumericalRecipes3.Amoeba
        public static Tuple<double[], double> ArgminDownhillSimplex(Func<double[], double> function, double[] initialArgment, double delta, double tolerance, int maxFunctionCall = 5000)
        {
            return ArgminDownhillSimplex(function, initialArgment, New.Array(initialArgment.Length, delta), tolerance, maxFunctionCall);
        }
        public static Tuple<double[], double> ArgminDownhillSimplex(Func<double[], double> function, double[] initialArgment, double[] delta, double tolerance, int maxFunctionCall = 5000)
        {
            var points = New.Array(initialArgment.Length + 1, i =>
            {
                var p = initialArgment.CloneX();
                if (i > 0) p[i - 1] += delta[i - 1];
                return p;
            });
            return ArgminDownhillSimplex(function, points, tolerance, maxFunctionCall);
        }
        public static Tuple<double[], double> ArgminDownhillSimplex(Func<double[], double> function, double[][] initialArgs, double tolerance, int maxFunctionCall = 5000)
        {
            const double TINY = 1e-10;
            var args = initialArgs.Select(v => v.CloneX()).ToArray();
            var argSum = args.Sum();
            var values = args.Select(p => function(p)).ToArray();

            // NumericalRecipes3.Amoeba.amotry
            Func<int, double, double> tryfunc = (int iMax, double fac) =>
            {
                double fac1 = (1.0 - fac) / argSum.Length;
                double fac2 = fac1 - fac;
                var argTry = Mt.Mul(argSum, fac1).LetAddMul(args[iMax], -fac2);
                double valueTry = function(argTry);
                if (valueTry < values[iMax])
                {
                    values[iMax] = valueTry;
                    argSum.LetAdd(argTry).LetSub(args[iMax]);
                    args[iMax] = argTry;
                }
                return valueTry;
            };

            int countFunctionCall = 0;
            int iMin;
            while (true)
            {
                iMin = 0;
                int iMax = values[0] > values[1] ? 0 : 1;
                int iMax2 = 1 - iMax;
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] <= values[iMin]) iMin = i;
                    if (values[i] > values[iMax]) { iMax2 = iMax; iMax = i; }
                    else if (values[i] > values[iMax2] && i != iMax) iMax2 = i;
                }
                if (2.0 * Math.Abs(values[iMax] - values[iMin]) / (Math.Abs(values[iMax]) + Math.Abs(values[iMin]) + TINY) < tolerance) break;
                if (countFunctionCall >= maxFunctionCall)
                {
                    //ThrowException.WriteLine("ArgminDownhillSimplex: too many iterations");
                    break;
                }

                countFunctionCall += 2;
                double valueTry = tryfunc(iMax, -1.0);
                if (valueTry <= values[iMin]) { tryfunc(iMax, 2.0); continue; }
                if (valueTry < values[iMax2]) { countFunctionCall--; continue; }

                double valueSave = values[iMax];
                valueTry = tryfunc(iMax, 0.5);
                if (valueTry >= valueSave)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (i == iMin) continue;
                        args[i].LetAdd(args[iMin]).LetMul(0.5);
                        values[i] = function(args[i]);
                    }
                    countFunctionCall += values.Length - 1;
                    argSum = args.Sum();
                }
            }
            return Tuple.Create(args[iMin].CloneX(), values[iMin]);
        }

        // NumericalRecipes1.Ameba
        public static double[] ArgminDownhillSimplex1(Func<double[], double> func, double[] initialArgument, double[] delta, double tolerance)
        {
            const int maxFunctionCall = 5000;
            int countFunctionCall = 0;
            int D = initialArgument.Length;

            double[][] args = New.Array(D + 1, j => New.Array(D, i => initialArgument[i] + (i == j ? delta[i] : 0.0)));
            double[] argSum = New.Array(D, i => Mt.Sum(D + 1, j => args[j][i]));
            double[] argTry = new double[D];
            double[] values = args.Select(arg => func(arg)).ToArray();

            Func<int, double, double> tryfunc = (int i, double fac) =>
            {
                double[] arg = args[i];
                double fac1 = (1 - fac) / D;
                double fac2 = fac1 - fac;
                for (int j = D; --j >= 0; )
                    argTry[j] = argSum[j] * fac1 - arg[j] * fac2;

                double y = func(argTry);
                if (values[i] > y)
                {
                    values[i] = y;
                    for (int j = D; --j >= 0; )
                    {
                        argSum[j] += argTry[j] - arg[j];
                        arg[j] = argTry[j];
                    }
                }
                return y;
            };

            while (true)
            {
                int iMin = 0;
                int iMax = values[0] > values[1] ? 0 : 1;
                int iMax2 = 1 - iMax;
                for (int i = 0; i < D + 1; i++)
                {
                    double v = values[i];
                    if (v <= values[iMin]) iMin = i;
                    if (v > values[iMax]) { iMax2 = iMax; iMax = i; }
                    else if (v > values[iMax2] && i != iMax) iMax2 = i;
                }

                double d = 2.0 * Math.Abs(values[iMax] - values[iMin])
                    / (Math.Abs(values[iMax]) + Math.Abs(values[iMin]) + Mt.DoubleEps);
                if (d < tolerance) break;
                if (countFunctionCall >= maxFunctionCall)
                {
                    ThrowException.WriteLine("ArgminSimplex: too many function estimations");
                    break;
                }

                countFunctionCall += 2;
                double valueTry = tryfunc(iMax, -1);
                if (valueTry <= values[iMin]) { tryfunc(iMax, 2); continue; }
                if (valueTry < values[iMax2]) { countFunctionCall--; continue; }

                double valueSave = values[iMax];
                valueTry = tryfunc(iMax, 0.5);
                if (valueTry < valueSave) continue;

                countFunctionCall += D;
                for (int i = 0; i < D + 1; i++)
                {
                    if (i == iMin) continue;
                    for (int j = 0; j < D; j++)
                        args[i][j] = (args[i][j] + args[iMin][j]) / 2;
                    values[i] = func(args[i]);
                }
                for (int i = D; --i >= 0; )
                    argSum[i] = Mt.Sum(D + 1, j => args[j][i]);
            }
            return New.Array(D, i => Mt.Average(D + 1, j => args[j][i]));
        }
        #endregion

        #region Association Tests

        public static Tuple<BigInteger, BigInteger> FisherExactTestInteger(int[,] ctable)
        {
            int lengthY = ctable.GetLength(0);
            int lengthX = ctable.GetLength(1);
            int[] restY = New.Array(lengthY, y => Mt.Sum(lengthX, x => ctable[y, x]));
            int[] restX = New.Array(lengthX, x => Mt.Sum(lengthY, y => ctable[y, x]));

            BigInteger nCasesMultinomialX = Mt.MultinomialInteger(restX);
            BigInteger nCasesThis = Mt.Product(lengthY, i => Mt.MultinomialInteger(Ex.Select(lengthX, x => ctable[i, x])));
            BigInteger nCasesLess = 0;
            {
                Action<int, int, BigInteger> function = null;
                function = (int yy, int xx, BigInteger nCasesPrev) =>
                {
                    int backupY = restY[yy];
                    int backupX = restX[xx];
                    int min = Math.Max(0, backupY - Mt.Sum(xx, x => restX[x]));
                    int max = Math.Min(backupY, backupX);
                    if (min > max) ThrowException.ArgumentException("FisherExactTestInteger: ctable");
                    for (int k = max; k >= min; k--)
                    {
                        restY[yy] = backupY - k;
                        restX[xx] = backupX - k;
                        BigInteger nCases = nCasesPrev / Mt.FactorialInteger(k);
                        if (xx > 0)
                        {
                            function(yy, xx - 1, nCases);
                            continue;
                        }
                        if (yy > 1)
                        {
                            function(yy - 1, lengthX - 1, nCases * Mt.FactorialInteger(restY[yy - 1]));
                            continue;
                        }
                        nCases *= Mt.MultinomialInteger(restX);
                        if (nCases <= nCasesThis) nCasesLess += nCases;
                    }
                    restY[yy] = backupY;
                    restX[xx] = backupX;
                };
                function(lengthY - 1, lengthX - 1, Mt.FactorialInteger(restY[lengthY - 1]));
            }
            var gcd = Mt.GreatestCommonDivisor(nCasesMultinomialX, nCasesLess);
            return Tuple.Create(nCasesLess / gcd, nCasesMultinomialX / gcd);
        }

        public static double FisherExactTestDouble(int[,] ctable)
        {
            int lengthY = ctable.GetLength(0);
            int lengthX = ctable.GetLength(1);
            int[] restY = New.Array(lengthY, y => Mt.Sum(lengthX, x => ctable[y, x]));
            int[] restX = New.Array(lengthX, x => Mt.Sum(lengthY, y => ctable[y, x]));

            double nCasesTotal = restX.Sum(c => Mt.LogFactorial(c)) + restY.Sum(c => Mt.LogFactorial(c)) - Mt.LogFactorial(restY.Sum());
            double nCasesThis = ctable.ToEnumerable().Sum(c => Mt.LogFactorial(c)) * (1 - 16 * Mt.DoubleEps);
            double nCasesLess = 0;
            {
                Action<int, int, double> function = null;
                function = (int yy, int xx, double nCasesPrev) =>
                {
                    int backupY = restY[yy];
                    int backupX = restX[xx];
                    int min = Math.Max(0, backupY - Mt.Sum(xx, x => restX[x]));
                    int max = Math.Min(backupY, backupX);
                    if (min > max) ThrowException.ArgumentException("FisherExactTestDouble: ctable");
                    for (int k = max; k >= min; k--)
                    {
                        restY[yy] = backupY - k;
                        restX[xx] = backupX - k;
                        double nCases = nCasesPrev + Mt.LogFactorial(k);
                        if (xx > 0)
                        {
                            function(yy, xx - 1, nCases);
                            continue;
                        }
                        if (yy > 1)
                        {
                            function(yy - 1, lengthX - 1, nCases);
                            continue;
                        }
                        nCases += restX.Sum(c => Mt.LogFactorial(c));
                        if (nCases >= nCasesThis) nCasesLess += Math.Exp(nCasesTotal - nCases);
                    }
                    restY[yy] = backupY;
                    restX[xx] = backupX;
                };
                function(lengthY - 1, lengthX - 1, 0);
            }
            return nCasesLess;
        }

        public static double PearsonChiSquareTest(int[,] ctable)
        {
            int lengthY = ctable.GetLength(0);
            int lengthX = ctable.GetLength(1);
            int[] totalY = New.Array(lengthY, y => Mt.Sum(lengthX, x => ctable[y, x]));
            int[] totalX = New.Array(lengthX, x => Mt.Sum(lengthY, y => ctable[y, x]));
            int total = totalY.Sum();

            double sum = 0;
            for (int y = lengthY; --y >= 0; )
                for (int x = lengthX; --x >= 0; )
                    sum += Mt.Sq(ctable[y, x] - totalY[y] * totalX[x] / (double)total) / (totalY[y] * totalX[x]);
            return Mt.ChiSquareDistributionUpper((lengthY - 1) * (lengthX - 1), sum * total);
        }

        public static double YatesChiSquareTest(int[,] ctable)
        {
            return 1;
        }

        #endregion
    }
}
