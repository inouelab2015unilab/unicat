using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Linq
{
    #region Extension class

    public static partial class EnumerableEx
    {
        #region Int32
        public static double GeometricalAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) { return source.Select(element => selector(element)).GeometricalAverage(); }
        public static int Product<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) { return source.Select(element => selector(element)).Product(); }
        public static double GeometricalAverage(this IEnumerable<int> source) { var r = source._Product(); return Math.Pow((double)r.v0, 1.0 / r.v1); }
        public static int Product(this IEnumerable<int> source) { return source._Product().v0; }
        static InoueLab.V2<int, int> _Product(this IEnumerable<int> source)
        {
            int a = 1;
            int c = 0;
            foreach (var e in source) { c++; checked { a *= e; } }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region Double
        public static double GeometricalAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { return source.Select(element => selector(element)).GeometricalAverage(); }
        public static double Product<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { return source.Select(element => selector(element)).Product(); }
        public static double GeometricalAverage(this IEnumerable<double> source) { var r = source._Product(); return Math.Pow(r.v0, 1.0 / r.v1); }
        public static double Product(this IEnumerable<double> source) { return source._Product().v0; }
        static InoueLab.V2<double, int> _Product(this IEnumerable<double> source)
        {
            double a = 1;
            int c = 0;
            foreach (var e in source) { c++; a *= e; }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region UInt32
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector) { return source.Select(element => selector(element)).Average(); }
        public static uint Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector) { return source.Select(element => selector(element)).Sum(); }
        public static double Average(this IEnumerable<uint> source) { var r = source._Sum(); return (double)r.v0 / r.v1; }
        public static uint Sum(this IEnumerable<uint> source) { return source._Sum().v0; }
        static InoueLab.V2<uint, int> _Sum(this IEnumerable<uint> source)
        {
            var a = default(uint);
            int c = 0;
            foreach (var e in source) { c++; checked { a += e; } }
            return InoueLab.New.V2(a, c);
        }
        public static double GeometricalAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector) { return source.Select(element => selector(element)).GeometricalAverage(); }
        public static uint Product<TSource>(this IEnumerable<TSource> source, Func<TSource, uint> selector) { return source.Select(element => selector(element)).Product(); }
        public static double GeometricalAverage(this IEnumerable<uint> source) { var r = source._Product(); return Math.Pow((double)r.v0, 1.0 / r.v1); }
        public static uint Product(this IEnumerable<uint> source) { return source._Product().v0; }
        static InoueLab.V2<uint, int> _Product(this IEnumerable<uint> source)
        {
            uint a = 1;
            int c = 0;
            foreach (var e in source) { c++; checked { a *= e; } }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region UInt64
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector) { return source.Select(element => selector(element)).Average(); }
        public static ulong Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector) { return source.Select(element => selector(element)).Sum(); }
        public static double Average(this IEnumerable<ulong> source) { var r = source._Sum(); return (double)r.v0 / r.v1; }
        public static ulong Sum(this IEnumerable<ulong> source) { return source._Sum().v0; }
        static InoueLab.V2<ulong, int> _Sum(this IEnumerable<ulong> source)
        {
            var a = default(ulong);
            int c = 0;
            foreach (var e in source) { c++; checked { a += e; } }
            return InoueLab.New.V2(a, c);
        }
        public static double GeometricalAverage<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector) { return source.Select(element => selector(element)).GeometricalAverage(); }
        public static ulong Product<TSource>(this IEnumerable<TSource> source, Func<TSource, ulong> selector) { return source.Select(element => selector(element)).Product(); }
        public static double GeometricalAverage(this IEnumerable<ulong> source) { var r = source._Product(); return Math.Pow((double)r.v0, 1.0 / r.v1); }
        public static ulong Product(this IEnumerable<ulong> source) { return source._Product().v0; }
        static InoueLab.V2<ulong, int> _Product(this IEnumerable<ulong> source)
        {
            ulong a = 1;
            int c = 0;
            foreach (var e in source) { c++; checked { a *= e; } }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region TimeSpan
        public static TimeSpan Average<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector) { return source.Select(element => selector(element)).Average(); }
        public static TimeSpan Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector) { return source.Select(element => selector(element)).Sum(); }
        public static TimeSpan Average(this IEnumerable<TimeSpan> source) { var r = source._Sum(); return TimeSpan.FromMilliseconds(r.v0.TotalMilliseconds / r.v1); }
        public static TimeSpan Sum(this IEnumerable<TimeSpan> source) { return source._Sum().v0; }
        static InoueLab.V2<TimeSpan, int> _Sum(this IEnumerable<TimeSpan> source)
        {
            var a = default(TimeSpan);
            int c = 0;
            foreach (var e in source) { c++; checked { a += e; } }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region Double[]
        public static double[] Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double[]> selector) { return source.Select(element => selector(element)).Average(); }
        public static double[] Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double[]> selector) { return source.Select(element => selector(element)).Sum(); }
        public static double[] Average(this IEnumerable<double[]> source) { var r = source._Sum(); return InoueLab.Mt.LetDiv(r.v0, r.v1); }
        public static double[] Sum(this IEnumerable<double[]> source) { return source._Sum().v0; }
        static InoueLab.V2<double[], int> _Sum(this IEnumerable<double[]> source)
        {
            var a = default(double[]);
            int c = 0;
            foreach (var e in source) { c++; if (a == null) a = (double[])e.Clone(); else InoueLab.Mt.LetAdd(a, e); }
            return InoueLab.New.V2(a, c);
        }
        #endregion

        #region Double[,]
        public static double[,] Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double[,]> selector) { return source.Select(element => selector(element)).Average(); }
        public static double[,] Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double[,]> selector) { return source.Select(element => selector(element)).Sum(); }
        public static double[,] Average(this IEnumerable<double[,]> source) { var r = source._Sum(); return InoueLab.Mt.LetDiv(r.v0, r.v1); }
        public static double[,] Sum(this IEnumerable<double[,]> source) { return source._Sum().v0; }
        static InoueLab.V2<double[,], int> _Sum(this IEnumerable<double[,]> source)
        {
            var a = default(double[,]);
            int c = 0;
            foreach (var e in source) { c++; if (a == null) a = (double[,])e.Clone(); else InoueLab.Mt.LetAdd(a, e); }
            return InoueLab.New.V2(a, c);
        }
        #endregion
    }

    #endregion
}

namespace InoueLab
{
    #region Error class
    public static class ThrowException
    {
        public static void WriteLine(string message)
        {
            Console.Error.WriteLine(message);
        }
        public static void ArgumentException(string message)
        {
            throw new ArgumentException(message);
        }
        public static void ArgumentNullException(string paramName)
        {
            throw new ArgumentNullException(paramName);
        }
        public static void ArgumentOutOfRangeException(string paramName)
        {
            throw new ArgumentOutOfRangeException(paramName);
        }
        public static void InvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }
        public static void NoElements()
        {
            throw new InvalidOperationException("no elements");
        }
        public static void SizeMismatch()
        {
            throw new InvalidOperationException("size mismatch");
        }
    }
    #endregion

    #region Concatenated classes
    public struct V2<T0, T1>
    {
        public T0 v0;
        public T1 v1;
        public V2(T0 v0, T1 v1) { this.v0 = v0; this.v1 = v1; }
        public override string ToString() { return string.Format("{0}, {1}", v0, v1); }
    }
    public struct V3<T0, T1, T2>
    {
        public T0 v0;
        public T1 v1;
        public T2 v2;
        public V3(T0 v0, T1 v1, T2 v2) { this.v0 = v0; this.v1 = v1; this.v2 = v2; }
        public override string ToString() { return string.Format("{0}, {1}, {2}", v0, v1, v2); }
    }
    public struct V4<T0, T1, T2, T3>
    {
        public T0 v0;
        public T1 v1;
        public T2 v2;
        public T3 v3;
        public V4(T0 v0, T1 v1, T2 v2, T3 v3) { this.v0 = v0; this.v1 = v1; this.v2 = v2; this.v3 = v3; }
        public override string ToString() { return string.Format("{0}, {1}, {2}, {3}", v0, v1, v2, v3); }
    }

    public struct Array1<T> : IList<T>
    {
        public T v0;
        public Array1(T v0)
        {
            this.v0 = v0;
        }
        public int IndexOf(T item)
        {
            if (item.Equals(v0)) return 0;
            return -1;
        }
        public void Insert(int index, T item) { throw new NotImplementedException(); }
        public void RemoveAt(int index) { throw new NotImplementedException(); }
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return v0;
                }
                ThrowException.ArgumentException("index"); return default(T);
            }
            set
            {
                switch (index)
                {
                    case 0: v0 = value; return;
                }
                ThrowException.ArgumentException("index");
            }
        }
        public void Add(T item) { throw new NotImplementedException(); }
        public void Clear() { throw new NotImplementedException(); }
        public bool Contains(T item) { return IndexOf(item) != -1; }
        public void CopyTo(T[] array, int arrayIndex)
        {
            array[arrayIndex] = v0;
        }
        public int Count { get { return 1; } }
        public bool IsReadOnly { get { return false; } }
        public bool Remove(T item) { throw new NotImplementedException(); }
        public IEnumerator<T> GetEnumerator()
        {
            yield return v0;
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
    public struct Array2<T> : IList<T>
    {
        public T v0;
        public T v1;
        public Array2(T v0, T v1)
        {
            this.v0 = v0;
            this.v1 = v1;
        }
        public int IndexOf(T item)
        {
            if (item.Equals(v0)) return 0;
            if (item.Equals(v1)) return 1;
            return -1;
        }
        public void Insert(int index, T item) { throw new NotImplementedException(); }
        public void RemoveAt(int index) { throw new NotImplementedException(); }
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return v0;
                    case 1: return v1;
                }
                ThrowException.ArgumentException("index"); return default(T);
            }
            set
            {
                switch (index)
                {
                    case 0: v0 = value; return;
                    case 1: v1 = value; return;
                }
                ThrowException.ArgumentException("index");
            }
        }
        public void Add(T item) { throw new NotImplementedException(); }
        public void Clear() { throw new NotImplementedException(); }
        public bool Contains(T item) { return IndexOf(item) != -1; }
        public void CopyTo(T[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = v0;
            array[arrayIndex + 1] = v1;
        }
        public int Count { get { return 2; } }
        public bool IsReadOnly { get { return false; } }
        public bool Remove(T item) { throw new NotImplementedException(); }
        public IEnumerator<T> GetEnumerator()
        {
            yield return v0;
            yield return v1;
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
    public struct Array3<T> : IList<T>
    {
        public T v0;
        public T v1;
        public T v2;
        public Array3(T v0, T v1, T v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
        }
        public int IndexOf(T item)
        {
            if (item.Equals(v0)) return 0;
            if (item.Equals(v1)) return 1;
            if (item.Equals(v2)) return 2;
            return -1;
        }
        public void Insert(int index, T item) { throw new NotImplementedException(); }
        public void RemoveAt(int index) { throw new NotImplementedException(); }
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return v0;
                    case 1: return v1;
                    case 2: return v2;
                }
                ThrowException.ArgumentException("index"); return default(T);
            }
            set
            {
                switch (index)
                {
                    case 0: v0 = value; return;
                    case 1: v1 = value; return;
                    case 2: v2 = value; return;
                }
                ThrowException.ArgumentException("index");
            }
        }
        public void Add(T item) { throw new NotImplementedException(); }
        public void Clear() { throw new NotImplementedException(); }
        public bool Contains(T item) { return IndexOf(item) != -1; }
        public void CopyTo(T[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = v0;
            array[arrayIndex + 1] = v1;
            array[arrayIndex + 2] = v2;
        }
        public int Count { get { return 2; } }
        public bool IsReadOnly { get { return false; } }
        public bool Remove(T item) { throw new NotImplementedException(); }
        public IEnumerator<T> GetEnumerator()
        {
            yield return v0;
            yield return v1;
            yield return v2;
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
    public struct Array4<T> : IList<T>
    {
        public T v0;
        public T v1;
        public T v2;
        public T v3;
        public Array4(T v0, T v1, T v2, T v3)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
        public int IndexOf(T item)
        {
            if (item.Equals(v0)) return 0;
            if (item.Equals(v1)) return 1;
            if (item.Equals(v2)) return 2;
            if (item.Equals(v3)) return 3;
            return -1;
        }
        public void Insert(int index, T item) { throw new NotImplementedException(); }
        public void RemoveAt(int index) { throw new NotImplementedException(); }
        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return v0;
                    case 1: return v1;
                    case 2: return v2;
                    case 3: return v3;
                }
                ThrowException.ArgumentException("index"); return default(T);
            }
            set
            {
                switch (index)
                {
                    case 0: v0 = value; return;
                    case 1: v1 = value; return;
                    case 2: v2 = value; return;
                    case 3: v3 = value; return;
                }
                ThrowException.ArgumentException("index");
            }
        }
        public void Add(T item) { throw new NotImplementedException(); }
        public void Clear() { throw new NotImplementedException(); }
        public bool Contains(T item) { return IndexOf(item) != -1; }
        public void CopyTo(T[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = v0;
            array[arrayIndex + 1] = v1;
            array[arrayIndex + 2] = v2;
            array[arrayIndex + 3] = v3;
        }
        public int Count { get { return 2; } }
        public bool IsReadOnly { get { return false; } }
        public bool Remove(T item) { throw new NotImplementedException(); }
        public IEnumerator<T> GetEnumerator()
        {
            yield return v0;
            yield return v1;
            yield return v2;
            yield return v3;
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }

    public partial struct Int2 : IEquatable<Int2>, IComparable<Int2>
    {
        readonly int x;
        readonly int y;
        public Int2(int valueX, int valueY) { this.x = valueX; this.y = valueY; }
        public Int2(Int2 value) { this.x = value.x; this.y = value.y; }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int v0 { get { return x; } }
        public int v1 { get { return y; } }
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                }
                ThrowException.ArgumentOutOfRangeException("index"); return default(int);
            }
        }
        public static bool operator ==(Int2 left, Int2 right) { return ((left.x - right.x) | (left.y - right.y)) == 0; }
        public static bool operator !=(Int2 left, Int2 right) { return ((left.x - right.x) | (left.y - right.y)) != 0; }
        public static bool operator <=(Int2 left, Int2 right) { return left.CompareTo(right) <= 0; }
        public static bool operator >=(Int2 left, Int2 right) { return left.CompareTo(right) >= 0; }
        public static bool operator <(Int2 left, Int2 right) { return left.CompareTo(right) < 0; }
        public static bool operator >(Int2 left, Int2 right) { return left.CompareTo(right) > 0; }
        public static Int2 operator ~(Int2 value) { return new Int2(~value.x, ~value.y); }
        public static Int2 operator -(Int2 value) { return new Int2(-value.x, -value.y); }
        public static Int2 operator +(Int2 left, Int2 right) { return new Int2(left.x + right.x, left.y + right.y); }
        public static Int2 operator -(Int2 left, Int2 right) { return new Int2(left.x - right.x, left.y - right.y); }
        public static Int2 operator *(Int2 left, Int2 right) { return new Int2(left.x * right.x, left.y * right.y); }
        public static Int2 operator /(Int2 left, Int2 right) { return new Int2(left.x / right.x, left.y / right.y); }
        public static Int2 operator %(Int2 left, Int2 right) { return new Int2(left.x % right.x, left.y % right.y); }
        public static Int2 operator |(Int2 left, Int2 right) { return new Int2(left.x | right.x, left.y | right.y); }
        public static Int2 operator &(Int2 left, Int2 right) { return new Int2(left.x & right.x, left.y & right.y); }
        public static Int2 operator ^(Int2 left, Int2 right) { return new Int2(left.x ^ right.x, left.y ^ right.y); }
        public static Int2 operator |(Int2 left, int right) { return new Int2(left.x | right, left.y | right); }
        public static Int2 operator &(Int2 left, int right) { return new Int2(left.x & right, left.y & right); }
        public static Int2 operator ^(Int2 left, int right) { return new Int2(left.x ^ right, left.y ^ right); }
        public static Int2 operator +(Int2 left, int right) { return new Int2(left.x + right, left.y + right); }
        public static Int2 operator -(Int2 left, int right) { return new Int2(left.x - right, left.y - right); }
        public static Int2 operator *(Int2 left, int right) { return new Int2(left.x * right, left.y * right); }
        public static Int2 operator /(Int2 left, int right) { return new Int2(left.x / right, left.y / right); }
        public static Int2 operator %(Int2 left, int right) { return new Int2(left.x % right, left.y % right); }
        public static Int2 OnesComplement(Int2 value) { return new Int2(~value.x, ~value.y); }
        public static Int2 Negate(Int2 value) { return new Int2(-value.x, -value.y); }
        public static Int2 Add(Int2 left, Int2 right) { return new Int2(left.x + right.x, left.y + right.y); }
        public static Int2 Subtract(Int2 left, Int2 right) { return new Int2(left.x - right.x, left.y - right.y); }
        public static Int2 Multiply(Int2 left, Int2 right) { return new Int2(left.x * right.x, left.y * right.y); }
        public static Int2 Divide(Int2 left, Int2 right) { return new Int2(left.x / right.x, left.y / right.y); }
        public static Int2 Mod(Int2 left, Int2 right) { return new Int2(left.x % right.x, left.y % right.y); }
        public static Int2 BitwiseOr(Int2 left, Int2 right) { return new Int2(left.x | right.x, left.y | right.y); }
        public static Int2 BitwiseAnd(Int2 left, Int2 right) { return new Int2(left.x & right.x, left.y & right.y); }
        public static Int2 Xor(Int2 left, Int2 right) { return new Int2(left.x ^ right.x, left.y ^ right.y); }
        public static Int2 BitwiseOr(Int2 left, int right) { return new Int2(left.x | right, left.y | right); }
        public static Int2 BitwiseAnd(Int2 left, int right) { return new Int2(left.x & right, left.y & right); }
        public static Int2 Xor(Int2 left, int right) { return new Int2(left.x ^ right, left.y ^ right); }
        public static Int2 Add(Int2 left, int right) { return new Int2(left.x + right, left.y + right); }
        public static Int2 Subtract(Int2 left, int right) { return new Int2(left.x - right, left.y - right); }
        public static Int2 Multiply(Int2 left, int right) { return new Int2(left.x * right, left.y * right); }
        public static Int2 Divide(Int2 left, int right) { return new Int2(left.x / right, left.y / right); }
        public static Int2 Mod(Int2 left, int right) { return new Int2(left.x % right, left.y % right); }
        public override bool Equals(object obj) { return (obj is Int2) && (this == (Int2)obj); }
        public override int GetHashCode() { return ((y << 16) | (int)((uint)y >> 16)) ^ x; }
        public override string ToString() { return string.Format("{0}, {1}", x, y); }
        public bool Equals(Int2 other) { return ((x - other.x) | (y - other.y)) == 0; }
        public int CompareTo(Int2 other) { return y != other.y ? y - other.y : x - other.x; }
    }
    public partial struct Int3 : IEquatable<Int3>, IComparable<Int3>
    {
        readonly int x;
        readonly int y;
        readonly int z;
        public Int3(int valueX, int valueY, int valueZ) { this.x = valueX; this.y = valueY; this.z = valueZ; }
        public Int3(Int3 value) { this.x = value.x; this.y = value.y; this.z = value.z; }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Z { get { return z; } }
        public int v0 { get { return x; } }
        public int v1 { get { return y; } }
        public int v2 { get { return z; } }
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                }
                ThrowException.ArgumentOutOfRangeException("index"); return default(int);
            }
        }
        public static bool operator ==(Int3 left, Int3 right) { return ((left.x - right.x) | (left.y - right.y) | (left.z - right.z)) == 0; }
        public static bool operator !=(Int3 left, Int3 right) { return ((left.x - right.x) | (left.y - right.y) | (left.z - right.z)) != 0; }
        public static bool operator <=(Int3 left, Int3 right) { return left.CompareTo(right) <= 0; }
        public static bool operator >=(Int3 left, Int3 right) { return left.CompareTo(right) >= 0; }
        public static bool operator <(Int3 left, Int3 right) { return left.CompareTo(right) < 0; }
        public static bool operator >(Int3 left, Int3 right) { return left.CompareTo(right) > 0; }
        public static Int3 operator ~(Int3 value) { return new Int3(~value.x, ~value.y, ~value.z); }
        public static Int3 operator -(Int3 value) { return new Int3(-value.x, -value.y, -value.z); }
        public static Int3 operator +(Int3 left, Int3 right) { return new Int3(left.x + right.x, left.y + right.y, left.z + right.z); }
        public static Int3 operator -(Int3 left, Int3 right) { return new Int3(left.x - right.x, left.y - right.y, left.z - right.z); }
        public static Int3 operator *(Int3 left, Int3 right) { return new Int3(left.x * right.x, left.y * right.y, left.z * right.z); }
        public static Int3 operator /(Int3 left, Int3 right) { return new Int3(left.x / right.x, left.y / right.y, left.z / right.z); }
        public static Int3 operator %(Int3 left, Int3 right) { return new Int3(left.x % right.x, left.y % right.y, left.z % right.z); }
        public static Int3 operator |(Int3 left, Int3 right) { return new Int3(left.x | right.x, left.y | right.y, left.z | right.z); }
        public static Int3 operator &(Int3 left, Int3 right) { return new Int3(left.x & right.x, left.y & right.y, left.z & right.z); }
        public static Int3 operator ^(Int3 left, Int3 right) { return new Int3(left.x ^ right.x, left.y ^ right.y, left.z ^ right.z); }
        public static Int3 operator |(Int3 left, int right) { return new Int3(left.x | right, left.y | right, left.z | right); }
        public static Int3 operator &(Int3 left, int right) { return new Int3(left.x & right, left.y & right, left.z & right); }
        public static Int3 operator ^(Int3 left, int right) { return new Int3(left.x ^ right, left.y ^ right, left.z ^ right); }
        public static Int3 operator +(Int3 left, int right) { return new Int3(left.x + right, left.y + right, left.z + right); }
        public static Int3 operator -(Int3 left, int right) { return new Int3(left.x - right, left.y - right, left.z - right); }
        public static Int3 operator *(Int3 left, int right) { return new Int3(left.x * right, left.y * right, left.z * right); }
        public static Int3 operator /(Int3 left, int right) { return new Int3(left.x / right, left.y / right, left.z / right); }
        public static Int3 operator %(Int3 left, int right) { return new Int3(left.x % right, left.y % right, left.z % right); }
        public static Int3 OnesComplement(Int3 value) { return new Int3(~value.x, ~value.y, ~value.z); }
        public static Int3 Negate(Int3 value) { return new Int3(-value.x, -value.y, -value.z); }
        public static Int3 Add(Int3 left, Int3 right) { return new Int3(left.x + right.x, left.y + right.y, left.z + right.z); }
        public static Int3 Subtract(Int3 left, Int3 right) { return new Int3(left.x - right.x, left.y - right.y, left.z - right.z); }
        public static Int3 Multiply(Int3 left, Int3 right) { return new Int3(left.x * right.x, left.y * right.y, left.z * right.z); }
        public static Int3 Divide(Int3 left, Int3 right) { return new Int3(left.x / right.x, left.y / right.y, left.z / right.z); }
        public static Int3 Mod(Int3 left, Int3 right) { return new Int3(left.x % right.x, left.y % right.y, left.z % right.z); }
        public static Int3 BitwiseOr(Int3 left, Int3 right) { return new Int3(left.x | right.x, left.y | right.y, left.z | right.z); }
        public static Int3 BitwiseAnd(Int3 left, Int3 right) { return new Int3(left.x & right.x, left.y & right.y, left.z & right.z); }
        public static Int3 Xor(Int3 left, Int3 right) { return new Int3(left.x ^ right.x, left.y ^ right.y, left.z ^ right.z); }
        public static Int3 BitwiseOr(Int3 left, int right) { return new Int3(left.x | right, left.y | right, left.z | right); }
        public static Int3 BitwiseAnd(Int3 left, int right) { return new Int3(left.x & right, left.y & right, left.z & right); }
        public static Int3 Xor(Int3 left, int right) { return new Int3(left.x ^ right, left.y ^ right, left.z ^ right); }
        public static Int3 Add(Int3 left, int right) { return new Int3(left.x + right, left.y + right, left.z + right); }
        public static Int3 Subtract(Int3 left, int right) { return new Int3(left.x - right, left.y - right, left.z - right); }
        public static Int3 Multiply(Int3 left, int right) { return new Int3(left.x * right, left.y * right, left.z * right); }
        public static Int3 Divide(Int3 left, int right) { return new Int3(left.x / right, left.y / right, left.z / right); }
        public static Int3 Mod(Int3 left, int right) { return new Int3(left.x % right, left.y % right, left.z % right); }
        public override bool Equals(object obj) { return (obj is Int3) && (this == (Int3)obj); }
        public override int GetHashCode() { return ((y << 16) | (int)((uint)y >> 16)) ^ x ^ z; }
        public override string ToString() { return string.Format("{0}, {1}, {2}", x, y, z); }
        public bool Equals(Int3 other) { return ((x - other.x) | (y - other.y) | (z - other.z)) == 0; }
        public int CompareTo(Int3 other) { return z != other.z ? z - other.z : (y != other.y ? y - other.y : x - other.x); }
    }
    public partial struct Double2 : IEquatable<Double2>, IComparable<Double2>
    {
        readonly double x;
        readonly double y;
        public Double2(double valueX, double valueY) { this.x = valueX; this.y = valueY; }
        public Double2(Double2 value) { this.x = value.x; this.y = value.y; }
        public Double2(Int2 value) { this.x = value.X; this.y = value.Y; }
        public double X { get { return x; } }
        public double Y { get { return y; } }
        public double v0 { get { return x; } }
        public double v1 { get { return y; } }
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                }
                ThrowException.ArgumentOutOfRangeException("index"); return default(double);
            }
        }
        public static bool operator ==(Double2 left, Double2 right) { return (left.x == right.x) && (left.y == right.y); }
        public static bool operator !=(Double2 left, Double2 right) { return (left.x != right.x) || (left.y != right.y); }
        public static bool operator <=(Double2 left, Double2 right) { return left.CompareTo(right) <= 0; }
        public static bool operator >=(Double2 left, Double2 right) { return left.CompareTo(right) >= 0; }
        public static bool operator <(Double2 left, Double2 right) { return left.CompareTo(right) < 0; }
        public static bool operator >(Double2 left, Double2 right) { return left.CompareTo(right) > 0; }
        public static Double2 operator -(Double2 value) { return new Double2(-value.x, -value.y); }
        public static Double2 operator +(Double2 left, Double2 right) { return new Double2(left.x + right.x, left.y + right.y); }
        public static Double2 operator -(Double2 left, Double2 right) { return new Double2(left.x - right.x, left.y - right.y); }
        public static Double2 operator *(Double2 left, Double2 right) { return new Double2(left.x * right.x, left.y * right.y); }
        public static Double2 operator /(Double2 left, Double2 right) { return new Double2(left.x / right.x, left.y / right.y); }
        public static Double2 operator +(Double2 left, double right) { return new Double2(left.x + right, left.y + right); }
        public static Double2 operator -(Double2 left, double right) { return new Double2(left.x - right, left.y - right); }
        public static Double2 operator *(Double2 left, double right) { return new Double2(left.x * right, left.y * right); }
        public static Double2 operator /(Double2 left, double right) { return new Double2(left.x / right, left.y / right); }
        public static Double2 Negate(Double2 value) { return new Double2(-value.x, -value.y); }
        public static Double2 Add(Double2 left, Double2 right) { return new Double2(left.x + right.x, left.y + right.y); }
        public static Double2 Subtract(Double2 left, Double2 right) { return new Double2(left.x - right.x, left.y - right.y); }
        public static Double2 Multiply(Double2 left, Double2 right) { return new Double2(left.x * right.x, left.y * right.y); }
        public static Double2 Divide(Double2 left, Double2 right) { return new Double2(left.x / right.x, left.y / right.y); }
        public static Double2 Add(Double2 left, double right) { return new Double2(left.x + right, left.y + right); }
        public static Double2 Subtract(Double2 left, double right) { return new Double2(left.x - right, left.y - right); }
        public static Double2 Multiply(Double2 left, double right) { return new Double2(left.x * right, left.y * right); }
        public static Double2 Divide(Double2 left, double right) { return new Double2(left.x / right, left.y / right); }
        public static bool IsNaN(Double2 value) { return double.IsNaN(value.x) || double.IsNaN(value.y); }
        public static readonly Double2 Zero = new Double2(0, 0);
        public static readonly Double2 One = new Double2(1, 1);
        public override bool Equals(object obj) { return (obj is Double2) && (this == (Double2)obj); }
        public override int GetHashCode() { return (int)(y - x); }
        public override string ToString() { return string.Format("{0}, {1}", x, y); }
        public bool Equals(Double2 other) { return (x == other.x) && (y == other.y); }
        public int CompareTo(Double2 other) { return y != other.y ? (y < other.y ? -1 : 1) : (x == other.x ? 0 : x < other.x ? -1 : 1); }
    }
    public partial struct Double3 : IEquatable<Double3>, IComparable<Double3>
    {
        readonly double x;
        readonly double y;
        readonly double z;
        public Double3(double valueX, double valueY, double valueZ) { this.x = valueX; this.y = valueY; this.z = valueZ; }
        public Double3(Double3 value) { this.x = value.x; this.y = value.y; this.z = value.z; }
        public Double3(Int3 value) { this.x = value.X; this.y = value.Y; this.z = value.Z; }
        public double X { get { return x; } }
        public double Y { get { return y; } }
        public double Z { get { return z; } }
        public double v0 { get { return x; } }
        public double v1 { get { return y; } }
        public double v2 { get { return z; } }
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                }
                ThrowException.ArgumentOutOfRangeException("index"); return default(double);
            }
        }
        public static bool operator ==(Double3 left, Double3 right) { return (left.x == right.x) && (left.y == right.y) && (left.z == right.z); }
        public static bool operator !=(Double3 left, Double3 right) { return (left.x != right.x) || (left.y != right.y) || (left.z != right.z); }
        public static bool operator <=(Double3 left, Double3 right) { return left.CompareTo(right) <= 0; }
        public static bool operator >=(Double3 left, Double3 right) { return left.CompareTo(right) >= 0; }
        public static bool operator <(Double3 left, Double3 right) { return left.CompareTo(right) < 0; }
        public static bool operator >(Double3 left, Double3 right) { return left.CompareTo(right) > 0; }
        public static Double3 operator -(Double3 value) { return new Double3(-value.x, -value.y, -value.z); }
        public static Double3 operator +(Double3 left, Double3 right) { return new Double3(left.x + right.x, left.y + right.y, left.z + right.z); }
        public static Double3 operator -(Double3 left, Double3 right) { return new Double3(left.x - right.x, left.y - right.y, left.z - right.z); }
        public static Double3 operator *(Double3 left, Double3 right) { return new Double3(left.x * right.x, left.y * right.y, left.z * right.z); }
        public static Double3 operator /(Double3 left, Double3 right) { return new Double3(left.x / right.x, left.y / right.y, left.z / right.z); }
        public static Double3 operator +(Double3 left, double right) { return new Double3(left.x + right, left.y + right, left.z + right); }
        public static Double3 operator -(Double3 left, double right) { return new Double3(left.x - right, left.y - right, left.z - right); }
        public static Double3 operator *(Double3 left, double right) { return new Double3(left.x * right, left.y * right, left.z * right); }
        public static Double3 operator /(Double3 left, double right) { return new Double3(left.x / right, left.y / right, left.z / right); }
        public static Double3 Negate(Double3 value) { return new Double3(-value.x, -value.y, -value.z); }
        public static Double3 Add(Double3 left, Double3 right) { return new Double3(left.x + right.x, left.y + right.y, left.z + right.z); }
        public static Double3 Subtract(Double3 left, Double3 right) { return new Double3(left.x - right.x, left.y - right.y, left.z - right.z); }
        public static Double3 Multiply(Double3 left, Double3 right) { return new Double3(left.x * right.x, left.y * right.y, left.z * right.z); }
        public static Double3 Divide(Double3 left, Double3 right) { return new Double3(left.x / right.x, left.y / right.y, left.z / right.z); }
        public static Double3 Add(Double3 left, double right) { return new Double3(left.x + right, left.y + right, left.z + right); }
        public static Double3 Subtract(Double3 left, double right) { return new Double3(left.x - right, left.y - right, left.z - right); }
        public static Double3 Multiply(Double3 left, double right) { return new Double3(left.x * right, left.y * right, left.z * right); }
        public static Double3 Divide(Double3 left, double right) { return new Double3(left.x / right, left.y / right, left.z / right); }
        public static bool IsNaN(Double3 value) { return double.IsNaN(value.x) || double.IsNaN(value.y) || double.IsNaN(value.z); }
        public static readonly Double3 Zero = new Double3(0, 0, 0);
        public static readonly Double3 One = new Double3(1, 1, 1);
        public override bool Equals(object obj) { return (obj is Double3) && (this == (Double3)obj); }
        public override int GetHashCode() { return (int)(y - x + z); }
        public override string ToString() { return string.Format("{0}, {1}, {2}", x, y, z); }
        public bool Equals(Double3 other) { return (x == other.x) && (y == other.y) && (z == other.z); }
        public int CompareTo(Double3 other) { return z != other.z ? (z < other.y ? -1 : 1) : (y != other.y ? (y < other.y ? -1 : 1) : (x == other.x ? 0 : x < other.x ? -1 : 1)); }
    }

    class Ints : IComparable, IEquatable<Ints>, IComparable<Ints>, IList<int>
    {
        readonly int[] _Items;
        public Ints(params int[] items)
        {
            this._Items = items.CloneX();
        }
        public Ints(IEnumerable<int> collections)
        {
            this._Items = collections.ToArray();
        }
        public int Length
        {
            get { return _Items.Length; }
        }
        public static bool operator ==(Ints left, Ints right) { return Equals(left, right); }
        public static bool operator !=(Ints left, Ints right) { return !Equals(left, right); }
        public static bool operator <=(Ints left, Ints right) { return Compare(left, right) <= 0; }
        public static bool operator >=(Ints left, Ints right) { return Compare(left, right) >= 0; }
        public static bool operator <(Ints left, Ints right) { return Compare(left, right) < 0; }
        public static bool operator >(Ints left, Ints right) { return Compare(left, right) > 0; }
        public static Ints operator +(Ints x, Ints y) { return new Ints(x._Items.Concat(y._Items)); }
        public static int Compare(Ints left, Ints right)
        {
            if ((object)left == null) return ((object)right == null) ? 0 : -1;
            return left.CompareTo(right);
        }
        public static bool Equals(Ints left, Ints right)
        {
            if ((object)left == null) return (object)right == null;
            return left.Equals(right);
        }
        public override bool Equals(object obj) { return (obj is Ints) && Equals((Ints)obj); }
        public override int GetHashCode()
        {
            int h = 0;
            for (int i = 0; i < _Items.Length; i++)
                h = h * 3 - 13 + _Items[i];
            return h;
        }
        public override string ToString() { return _Items.Select(l => l.ToString()).Join(","); }

        #region IComparable メンバー
        public int CompareTo(object obj)
        {
            var other = obj as Ints;
            if (other == null) ThrowException.ArgumentException("obj");
            return CompareTo(other);
        }
        #endregion
        #region IEquatable<Ints> メンバ
        public bool Equals(Ints other)
        {
            if ((object)other == null) return false;
            if (_Items.Length != other._Items.Length) return false;
            for (int i = 0; i < _Items.Length; i++)
                if (_Items[i] != other._Items[i]) return false;
            return true;
        }
        #endregion
        #region IComparable<Ints> メンバ
        public int CompareTo(Ints other)
        {
            if ((object)other == null) return 1;
            int l = Math.Min(_Items.Length, other._Items.Length);
            for (int i = 0; i < l; i++)
                if (_Items[i] != other._Items[i]) return _Items[i] - other._Items[i];
            return _Items.Length - other._Items.Length;
        }
        #endregion
        #region IList<int> メンバー
        public int IndexOf(int item) { return _Items.IndexOf(item); }
        public void Insert(int index, int item) { throw new NotImplementedException(); }
        public void RemoveAt(int index) { throw new NotImplementedException(); }
        public int this[int index]
        {
            get { return _Items[index]; }
            set { throw new NotImplementedException(); }
        }
        #endregion
        #region ICollection<int> メンバー
        public void Add(int item) { throw new NotImplementedException(); }
        public void Clear() { throw new NotImplementedException(); }
        public bool Contains(int item) { return _Items.Contains(item); }
        public void CopyTo(int[] array, int arrayIndex) { _Items.CopyTo(array, arrayIndex); }
        public int Count
        {
            get { return _Items.Length; }
        }
        public bool IsReadOnly
        {
            get { return true; }
        }
        public bool Remove(int item) { throw new NotImplementedException(); }
        #endregion
        #region IEnumerable<int> メンバー
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < _Items.Length; i++)
                yield return _Items[i];
        }
        #endregion
        #region IEnumerable メンバー
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        #endregion
    }
    #endregion

    #region New class
    public static partial class New
    {
        public static T Let<T>(ref T obj) where T : new() { return obj = new T(); }
        public static T Type<T>() where T : new() { return new T(); }
        public static TResult Func<TResult>(Func<TResult> func) { return func(); }
        public static KeyValuePair<TKey, TValue> KeyValuePair<TKey, TValue>(TKey key, TValue value) { return new KeyValuePair<TKey, TValue>(key, value); }
        public static V2<T0, T1> V2<T0, T1>(T0 v0, T1 v1) { return new V2<T0, T1>(v0, v1); }
        public static V3<T0, T1, T2> V3<T0, T1, T2>(T0 v0, T1 v1, T2 v2) { return new V3<T0, T1, T2>(v0, v1, v2); }
        public static V4<T0, T1, T2, T3> V4<T0, T1, T2, T3>(T0 v0, T1 v1, T2 v2, T3 v3) { return new V4<T0, T1, T2, T3>(v0, v1, v2, v3); }
        public static Array1<T> Array1<T>(T v0) { return new Array1<T>(v0); }
        public static Array2<T> Array2<T>(T v0, T v1) { return new Array2<T>(v0, v1); }

        #region Array
        public static T[] Array<T>(int length, Func<int, T> selector)
        {
            var array = new T[length];
            for (int i = 0; i < length; i++)
                array[i] = selector(i);
            return array;
        }
        public static T[,] Array<T>(int length0, int length1, Func<int, int, T> selector)
        {
            var array = new T[length0, length1];
            for (int i0 = 0; i0 < length0; i0++)
                for (int i1 = 0; i1 < length1; i1++)
                    array[i0, i1] = selector(i0, i1);
            return array;
        }
        public static T[, ,] Array<T>(int length0, int length1, int length2, Func<int, int, int, T> selector)
        {
            var array = new T[length0, length1, length2];
            for (int i0 = 0; i0 < length0; i0++)
                for (int i1 = 0; i1 < length1; i1++)
                    for (int i2 = 0; i2 < length2; i2++)
                        array[i0, i1, i2] = selector(i0, i1, i2);
            return array;
        }
        public static T[, , ,] Array<T>(int length0, int length1, int length2, int length3, Func<int, int, int, int, T> selector)
        {
            var array = new T[length0, length1, length2, length3];
            for (int i0 = 0; i0 < length0; i0++)
                for (int i1 = 0; i1 < length1; i1++)
                    for (int i2 = 0; i2 < length2; i2++)
                        for (int i3 = 0; i3 < length3; i3++)
                            array[i0, i1, i2, i3] = selector(i0, i1, i2, i3);
            return array;
        }
        public static T[,] Array<T>(Int2 lengths, Func<int, int, T> selector) { return Array(lengths.v0, lengths.v1, selector); }
        public static T[, ,] Array<T>(Int3 lengths, Func<int, int, int, T> selector) { return Array(lengths.v0, lengths.v1, lengths.v2, selector); }

        public static List<T> List<T>(int length, Func<int, T> selector)
        {
            var list = new List<T>(length);
            for (int i = 0; i < length; i++)
                list.Add(selector(i));
            return list;
        }

        public static T[] Array<T>(int length, T item)
        {
            T[] array = new T[length];
            for (int i = 0; i < length; i++)
                array[i] = item;
            return array;
        }
        public static T[,] Array<T>(int length0, int length1, T item)
        {
            T[,] array = new T[length0, length1];
            for (int i0 = 0; i0 < length0; i0++)
                for (int i1 = 0; i1 < length1; i1++)
                    array[i0, i1] = item;
            return array;
        }
        public static T[, ,] Array<T>(int length0, int length1, int length2, T item)
        {
            T[, ,] array = new T[length0, length1, length2];
            for (int i0 = 0; i0 < length0; i0++)
                for (int i1 = 0; i1 < length1; i1++)
                    for (int i2 = 0; i2 < length2; i2++)
                        array[i0, i1, i2] = item;
            return array;
        }
        public static T[, , ,] Array<T>(int length0, int length1, int length2, int length3, T item)
        {
            T[, , ,] array = new T[length0, length1, length2, length3];
            for (int i0 = 0; i0 < length0; i0++)
                for (int i1 = 0; i1 < length1; i1++)
                    for (int i2 = 0; i2 < length2; i2++)
                        for (int i3 = 0; i3 < length3; i3++)
                            array[i0, i1, i2, i3] = item;
            return array;
        }
        public static T[,] Array<T>(Int2 lengths, T item) { return Array(lengths.v0, lengths.v1, item); }
        public static T[, ,] Array<T>(Int3 lengths, T item) { return Array(lengths.v0, lengths.v1, lengths.v2, item); }
        public static List<T> List<T>(int length, T item)
        {
            var list = new List<T>(length);
            for (int i = 0; i < length; i++)
                list.Add(item);
            return list;
        }

        public static T[] Array<T>(int length) { return new T[length]; }
        public static T[,] Array<T>(int length0, int length1) { return new T[length0, length1]; }
        public static T[, ,] Array<T>(int length0, int length1, int length2) { return new T[length0, length1, length2]; }
        public static T[, , ,] Array<T>(int length0, int length1, int length2, int length3) { return new T[length0, length1, length2, length3]; }
        public static T[,] Array<T>(Int2 lengths) { return new T[lengths.v0, lengths.v1]; }
        public static T[, ,] Array<T>(Int3 lengths) { return new T[lengths.v0, lengths.v1, lengths.v2]; }

        public static TResult[] NewTo<T, TResult>(this T[] array, Func<T, TResult> selector)
        {
            return New.Array(array.Length, i => selector(array[i]));
        }
        public static TResult[,] NewTo<T, TResult>(this T[,] array, Func<T, TResult> selector)
        {
            return New.Array(array.GetLength(0), array.GetLength(1), (i0, i1) => selector(array[i0, i1]));
        }
        public static TResult[, ,] NewTo<T, TResult>(this T[, ,] array, Func<T, TResult> selector)
        {
            return New.Array(array.GetLength(0), array.GetLength(1), array.GetLength(2), (i0, i1, i2) => selector(array[i0, i1, i2]));
        }
        public static TResult[, , ,] NewTo<T, TResult>(this T[, , ,] array, Func<T, TResult> selector)
        {
            return New.Array(array.GetLength(0), array.GetLength(1), array.GetLength(2), array.GetLength(3), (i0, i1, i2, i3) => selector(array[i0, i1, i2, i3]));
        }

        public static TResult[] Zip<TSource0, TSource1, TResult>(TSource0[] array0, TSource1[] array1, Func<TSource0, TSource1, TResult> selector)
        {
            var l = array0.Length;
            if (l != array1.Length) ThrowException.SizeMismatch();
            var a = new TResult[l];
            for (int i = 0; i < l; i++)
                a[i] = selector(array0[i], array1[i]);
            return a;
        }
        public static TResult[,] Zip<TSource0, TSource1, TResult>(TSource0[,] array0, TSource1[,] array1, Func<TSource0, TSource1, TResult> selector)
        {
            var l = array0.Lengths();
            if (l != array1.Lengths()) ThrowException.SizeMismatch();
            var a = new TResult[l.v0, l.v1];
            for (int i0 = 0; i0 < l.v0; i0++)
                for (int i1 = 0; i1 < l.v1; i1++)
                    a[i0, i1] = selector(array0[i0, i1], array1[i0, i1]);
            return a;
        }
        public static TResult[, ,] Zip<TSource0, TSource1, TResult>(TSource0[, ,] array0, TSource1[, ,] array1, Func<TSource0, TSource1, TResult> selector)
        {
            var l = array0.Lengths();
            if (l != array1.Lengths()) ThrowException.SizeMismatch();
            var a = new TResult[l.v0, l.v1, l.v2];
            for (int i0 = 0; i0 < l.v0; i0++)
                for (int i1 = 0; i1 < l.v1; i1++)
                    for (int i2 = 0; i2 < l.v2; i2++)
                        a[i0, i1, i2] = selector(array0[i0, i1, i2], array1[i0, i1, i2]);
            return a;
        }
        #endregion

        #region Cache
        public static Func<T, TResult> Cache<T, TResult>(this Func<T, TResult> function)
        {
            var dictionary = new Dictionary<T, TResult>();
            return (arg) =>
            {
                lock (dictionary)
                {
                    if (!dictionary.ContainsKey(arg)) dictionary.Add(arg, function(arg));
                    return dictionary[arg];
                }
            };
        }
        public static Func<T1, T2, TResult> Cache<T1, T2, TResult>(this Func<T1, T2, TResult> function)
        {
            var dictionary = new Dictionary<Tuple<T1, T2>, TResult>();
            return (arg1, arg2) =>
            {
                var key = Tuple.Create(arg1, arg2);
                lock (dictionary)
                {
                    if (!dictionary.ContainsKey(key)) dictionary.Add(key, function(arg1, arg2));
                    return dictionary[key];
                }
            };
        }
        public static Func<T1, T2, T3, TResult> Cache<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> function)
        {
            var dictionary = new Dictionary<Tuple<T1, T2, T3>, TResult>();
            return (arg1, arg2, arg3) =>
            {
                var key = Tuple.Create(arg1, arg2, arg3);
                lock (dictionary)
                {
                    if (!dictionary.ContainsKey(key)) dictionary.Add(key, function(arg1, arg2, arg3));
                    return dictionary[key];
                }
            };
        }
        public static Func<T1, T2, T3, T4, TResult> Cache<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> function)
        {
            var dictionary = new Dictionary<Tuple<T1, T2, T3, T4>, TResult>();
            return (arg1, arg2, arg3, arg4) =>
            {
                var key = Tuple.Create(arg1, arg2, arg3, arg4);
                lock (dictionary)
                {
                    if (!dictionary.ContainsKey(key)) dictionary.Add(key, function(arg1, arg2, arg3, arg4));
                    return dictionary[key];
                }
            };
        }
        #endregion

        public static StreamReader StreamReader(string path)
        {
            var encoding = Gb.DetectEncoding(path);
            if (encoding == null) return null;
            return new StreamReader(path, encoding);
        }
    }
    #endregion

    #region Extension class
    public static partial class Ex
    {
        #region IEnumerable
        public static void For(int count, Action action)
        {
            for (int i = 0; i < count; i++)
                action();
        }
        public static void For(int count0, int count1, Action action)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    action();
        }
        public static void For(int count0, int count1, int count2, Action action)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    for (int i2 = 0; i2 < count2; i2++)
                        action();
        }
        public static void For(int count0, int count1, int count2, int count3, Action action)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    for (int i2 = 0; i2 < count2; i2++)
                        for (int i3 = 0; i3 < count3; i3++)
                            action();
        }
        public static void For(Int2 counts, Action action) { For(counts.v0, counts.v1, action); }
        public static void For(Int3 counts, Action action) { For(counts.v0, counts.v1, counts.v2, action); }

        public static void For(int count, Action<int> action)
        {
            for (int i = 0; i < count; i++)
                action(i);
        }
        public static void For(int count0, int count1, Action<int, int> action)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    action(i0, i1);
        }
        public static void For(int count0, int count1, int count2, Action<int, int, int> action)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    for (int i2 = 0; i2 < count2; i2++)
                        action(i0, i1, i2);
        }
        public static void For(int count0, int count1, int count2, int count3, Action<int, int, int, int> action)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    for (int i2 = 0; i2 < count2; i2++)
                        for (int i3 = 0; i3 < count3; i3++)
                            action(i0, i1, i2, i3);
        }
        public static void For(Int2 counts, Action<int, int> action) { For(counts.v0, counts.v1, action); }
        public static void For(Int3 counts, Action<int, int, int> action) { For(counts.v0, counts.v1, counts.v2, action); }

        public static IEnumerable<T> Select<T>(int count, Func<int, T> selector)
        {
            for (int i = 0; i < count; i++)
                yield return selector(i);
        }
        public static IEnumerable<T> Select<T>(int count0, int count1, Func<int, int, T> selector)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    yield return selector(i0, i1);
        }
        public static IEnumerable<T> Select<T>(int count0, int count1, int count2, Func<int, int, int, T> selector)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    for (int i2 = 0; i2 < count2; i2++)
                        yield return selector(i0, i1, i2);
        }
        public static IEnumerable<T> Select<T>(int count0, int count1, int count2, int count3, Func<int, int, int, int, T> selector)
        {
            for (int i0 = 0; i0 < count0; i0++)
                for (int i1 = 0; i1 < count1; i1++)
                    for (int i2 = 0; i2 < count2; i2++)
                        for (int i3 = 0; i3 < count3; i3++)
                            yield return selector(i0, i1, i2, i3);
        }
        public static IEnumerable<T> Select<T>(Int2 counts, Func<int, int, T> selector) { return Select<T>(counts.v0, counts.v1, selector); }
        public static IEnumerable<T> Select<T>(Int3 counts, Func<int, int, int, T> selector) { return Select<T>(counts.v0, counts.v1, counts.v2, selector); }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var element in source) action(element);
        }
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action)
        {
            int c = 0;
            foreach (var element in source) action(element, c++);
        }

        public static IEnumerable<int> Range(int count) { return Enumerable.Range(0, count); }
        public static IEnumerable<Int2> Range(Int2 lengths)
        {
            for (int v0 = 0; v0 < lengths.v0; v0++)
                for (int v1 = 0; v1 < lengths.v1; v1++)
                    yield return new Int2(v0, v1);
        }
        public static IEnumerable<Int3> Range(Int3 lengths)
        {
            for (int v0 = 0; v0 < lengths.v0; v0++)
                for (int v1 = 0; v1 < lengths.v1; v1++)
                    for (int v2 = 0; v2 < lengths.v2; v2++)
                        yield return new Int3(v0, v1, v2);
        }
        public static IEnumerable<int[]> Range(this int[] lengths)
        {
            int[] index = EnumeratorReset(lengths);
            if (index == null) yield break;
            do
                yield return index;
            while (index.EnumeratorMoveNext(lengths));
        }
        public static int[] EnumeratorReset(int[] lengths)
        {
            if (lengths == null || lengths.Length == 0) return null;
            for (int i = 0; i < lengths.Length; i++)
                if (lengths[i] <= 0) return null;
            return new int[lengths.Length];
        }
        public static bool EnumeratorMoveNext(this int[] index, int[] lengths)
        {
            for (int i = index.Length; --i >= 0; )
            {
                if (++index[i] < lengths[i]) return true;
                index[i] = 0;
            }
            return false;
        }

        public static IEnumerable<int> FromTo(int start, int end)
        {
            if (start <= end)
                for (int i = start; i < end; i++)
                    yield return i;
            else
                for (int i = start; --i >= end; )
                    yield return i;
        }
        public static IEnumerable<int> FromToStep(int start, int end, int step)
        {
            if (start <= end)
                for (int i = start; i < end; i += step)
                    yield return i;
            else
                for (int i = start; (i -= step) >= end; )
                    yield return i;
        }
        public static IEnumerable<int> FromUntil(int start, int end)
        {
            if (start <= end)
                for (int i = start; i <= end; i++)
                    yield return i;
            else
                for (int i = start; i >= end; i--)
                    yield return i;
        }
        public static IEnumerable<int> FromUntilStep(int start, int end, int step)
        {
            if (start <= end)
                for (int i = start; i <= end; i += step)
                    yield return i;
            else
                for (int i = start; i >= end; i -= step)
                    yield return i;
        }

        public static IEnumerable<TResult> Select<T, TResult>(this T[,] array, Func<T, Int2, TResult> selector)
        {
            Int2 lengths = array.Lengths();
            for (int v0 = 0; v0 < lengths.v0; v0++)
                for (int v1 = 0; v1 < lengths.v1; v1++)
                    yield return selector(array[v0, v1], new Int2(v0, v1));
        }
        public static IEnumerable<TResult> Select<T, TResult>(this T[, ,] array, Func<T, Int3, TResult> selector)
        {
            Int3 lengths = array.Lengths();
            for (int v0 = 0; v0 < lengths.v0; v0++)
                for (int v1 = 0; v1 < lengths.v1; v1++)
                    for (int v2 = 0; v2 < lengths.v2; v2++)
                        yield return selector(array[v0, v1, v2], new Int3(v0, v1, v2));
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, TSource value, Func<TSource, bool> predicate)
        {
            foreach (var element in source)
                if (predicate(element)) return element;
            return value;
        }
        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source) { return source.OrderBy(x => x); }
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source) { return source.OrderByDescending(x => x); }

        public static IEnumerable<T> Row<T>(this T[,] matrix, int row)
        {
            return Enumerable.Range(0, matrix.GetLength(1)).Select(j => matrix[row, j]);
        }
        public static IEnumerable<T> Col<T>(this T[,] matrix, int col)
        {
            return Enumerable.Range(0, matrix.GetLength(0)).Select(i => matrix[i, col]);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            foreach (var pair in source)
                dictionary.Add(pair.Key, pair.Value);
            return dictionary;
        }

        public static Dictionary<TKey, int> ToHistogram<TKey>(this IEnumerable<TKey> source, IEqualityComparer<TKey> comparer = null)
        {
            var dictionary = comparer == null ? new Dictionary<TKey, int>() : new Dictionary<TKey, int>(comparer);
            foreach (var e in source)
                if (dictionary.ContainsKey(e)) dictionary[e]++;
                else dictionary.Add(e, 1);
            return dictionary;
        }

        public static IEnumerable<string> SelectString<TSource>(this IEnumerable<TSource> source) { return source.Select(e => e.ToString()); }
        public static IEnumerable<string> SelectString(this IEnumerable<byte> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<sbyte> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<ushort> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<short> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<uint> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<int> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<ulong> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<long> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<float> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<double> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<decimal> source, string format) { return source.Select(e => e.ToString(format)); }
        public static IEnumerable<string> SelectString(this IEnumerable<DateTime> source, string format) { return source.Select(e => e.ToString(format)); }

        #region MinMax
        public static V2<V2<int, TSource>, V2<int, TSource>> MinMaxIndexItem<TSource>(this IEnumerable<TSource> source)
            where TSource : IComparable<TSource>
        {
            var minIndex = default(int);
            var maxIndex = default(int);
            var minItem = default(TSource);
            var maxItem = default(TSource);
            int index = 0;
            foreach (var item in source)
            {
                if (index == 0 || minItem.CompareTo(item) > 0) { minItem = item; minIndex = index; }
                if (index == 0 || maxItem.CompareTo(item) < 0) { maxItem = item; maxIndex = index; }
                index++;
            }
            if (index == 0) ThrowException.NoElements();
            return New.V2(New.V2(minIndex, minItem), New.V2(maxIndex, maxItem));
        }
        public static V2<int, TSource> MinIndexItem<TSource>(this IEnumerable<TSource> source)
            where TSource : IComparable<TSource>
        {
            var minIndex = default(int);
            var minItem = default(TSource);
            int index = 0;
            foreach (var item in source)
            {
                if (index == 0 || minItem.CompareTo(item) > 0) { minItem = item; minIndex = index; }
                index++;
            }
            if (index == 0) ThrowException.NoElements();
            return New.V2(minIndex, minItem);
        }
        public static V2<int, TSource> MaxIndexItem<TSource>(this IEnumerable<TSource> source)
            where TSource : IComparable<TSource>
        {
            var maxIndex = default(int);
            var maxItem = default(TSource);
            int index = 0;
            foreach (var item in source)
            {
                if (index == 0 || maxItem.CompareTo(item) < 0) { maxItem = item; maxIndex = index; }
                index++;
            }
            if (index == 0) ThrowException.NoElements();
            return New.V2(maxIndex, maxItem);
        }
        public static int MinIndex<TSource>(this IEnumerable<TSource> source) where TSource : IComparable<TSource> { return source.MinIndexItem().v0; }
        public static int MaxIndex<TSource>(this IEnumerable<TSource> source) where TSource : IComparable<TSource> { return source.MaxIndexItem().v0; }

        public static V2<V3<int, TSource, TValue>, V3<int, TSource, TValue>> MinMaxIndexItemValue<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
            where TValue : IComparable<TValue>
        {
            var minIndex = default(int);
            var maxIndex = default(int);
            var minItem = default(TSource);
            var maxItem = default(TSource);
            var minValue = default(TValue);
            var maxValue = default(TValue);
            int index = 0;
            foreach (var item in source)
            {
                var value = selector(item);
                if (index == 0 || minValue.CompareTo(value) > 0) { minValue = value; minItem = item; minIndex = index; }
                if (index == 0 || maxValue.CompareTo(value) < 0) { maxValue = value; maxItem = item; maxIndex = index; }
                index++;
            }
            if (index == 0) ThrowException.NoElements();
            return New.V2(New.V3(minIndex, minItem, minValue), New.V3(maxIndex, maxItem, maxValue));
        }
        public static V3<int, TSource, TValue> MinIndexItemValue<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
            where TValue : IComparable<TValue>
        {
            var minIndex = default(int);
            var minItem = default(TSource);
            var minValue = default(TValue);
            int index = 0;
            foreach (var item in source)
            {
                var value = selector(item);
                if (index == 0 || minValue.CompareTo(value) > 0) { minValue = value; minItem = item; minIndex = index; }
                index++;
            }
            if (index == 0) ThrowException.NoElements();
            return New.V3(minIndex, minItem, minValue);
        }
        public static V3<int, TSource, TValue> MaxIndexItemValue<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
            where TValue : IComparable<TValue>
        {
            var maxIndex = default(int);
            var maxItem = default(TSource);
            var maxValue = default(TValue);
            int index = 0;
            foreach (var item in source)
            {
                var value = selector(item);
                if (index == 0 || maxValue.CompareTo(value) < 0) { maxValue = value; maxItem = item; maxIndex = index; }
                index++;
            }
            if (index == 0) ThrowException.NoElements();
            return New.V3(maxIndex, maxItem, maxValue);
        }
        public static int MinIndex<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector) where TValue : IComparable<TValue> { return source.MinIndexItemValue(selector).v0; }
        public static int MaxIndex<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector) where TValue : IComparable<TValue> { return source.MaxIndexItemValue(selector).v0; }
        public static TSource MinItem<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector) where TValue : IComparable<TValue> { return source.MinIndexItemValue(selector).v1; }
        public static TSource MaxItem<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector) where TValue : IComparable<TValue> { return source.MaxIndexItemValue(selector).v1; }
        #endregion
        #endregion

        #region Array
        public static int IndexOf<T>(this T[] array, T value) { return Array.IndexOf(array, value); }
        public static void Clear(this Array array) { Array.Clear(array, 0, array.Length); }
        public static void CopyTo(this Array sourceArray, Array destinationArray) { sourceArray.CopyTo(destinationArray, 0); }
        public static void CopyTo(this Array sourceArray, int srcIndex, Array destinationArray, int dstinationIndex, int length) { Array.Copy(sourceArray, srcIndex, destinationArray, dstinationIndex, length); }
        public static T[] CloneX<T>(this T[] array) { return (T[])array.Clone(); }
        public static T[,] CloneX<T>(this T[,] array) { return (T[,])array.Clone(); }
        public static T[, ,] CloneX<T>(this T[, ,] array) { return (T[, ,])array.Clone(); }
        public static T[, , ,] CloneX<T>(this T[, , ,] array) { return (T[, , ,])array.Clone(); }
        public static T[][] DeepClone<T>(this T[][] array) { return New.Array(array.Length, i => array[i].CloneX()); }
        public static T[][][] DeepClone<T>(this T[][][] array) { return New.Array(array.Length, i => array[i].DeepClone()); }
        public static T[][][][] DeepClone<T>(this T[][][][] array) { return New.Array(array.Length, i => array[i].DeepClone()); }

        public static int Lengths<T>(this T[] array) { return array.Length; }
        public static Int2 Lengths<T>(this T[,] array) { return new Int2(array.GetLength(0), array.GetLength(1)); }
        public static Int3 Lengths<T>(this T[, ,] array) { return new Int3(array.GetLength(0), array.GetLength(1), array.GetLength(2)); }
        public static int[] Lengths(this Array array) { return New.Array(array.Rank, i => array.GetLength(i)); }

        public static IEnumerable<T> ToEnumerable<T>(this T[,] array)
        {
            foreach (var item in array)
                yield return item;
        }
        public static IEnumerable<T> ToEnumerable<T>(this T[, ,] array)
        {
            foreach (var item in array)
                yield return item;
        }
        public static IEnumerable<T> ToEnumerable<T>(this T[, , ,] array)
        {
            foreach (var item in array)
                yield return item;
        }
        public static T[] ToArray<T>(this T[,] array)
        {
            var a = new T[array.Length];
            int c = 0;
            foreach (var item in array) a[c++] = item;
            return a;
        }
        public static T[] ToArray<T>(this T[, ,] array)
        {
            var a = new T[array.Length];
            int c = 0;
            foreach (var item in array) a[c++] = item;
            return a;
        }
        public static T[] ToArray<T>(this T[, , ,] array)
        {
            var a = new T[array.Length];
            int c = 0;
            foreach (var item in array) a[c++] = item;
            return a;
        }
        public static T[,] ToArray<T>(this IEnumerable<T> source, Int2 lengths)
        {
            if (lengths.v0 <= 0 || lengths.v1 <= 0) ThrowException.ArgumentOutOfRangeException("lengths");
            T[,] array = new T[lengths.v0, lengths.v1];
            int i0 = 0, i1 = 0;
            foreach (var element in source)
            {
                array[i0, i1] = element;
                if (++i1 == lengths.v1) { i1 = 0; ++i0; }
            }
            if (i0 != lengths.v0 || i1 != 0) ThrowException.InvalidOperationException("not enough items");
            return array;
        }
        public static Array ToArray<T>(this IEnumerable<T> source, int[] lengths)
        {
            var index = EnumeratorReset(lengths);
            if (index == null) ThrowException.ArgumentOutOfRangeException("lengths");
            Array array = Array.CreateInstance(typeof(T), lengths);
            foreach (var element in source)
            {
                array.SetValue(element, index);
                if (!index.EnumeratorMoveNext(lengths)) break;
            }
            return array;
        }

        public static T GetValue<T>(this T[,] array, Int2 index) { return array[index.v0, index.v1]; }
        public static T GetValue<T>(this T[, ,] array, Int3 index) { return array[index.v0, index.v1, index.v2]; }
        public static void SetValue<T>(this T[,] array, T item, Int2 index) { array[index.v0, index.v1] = item; }  //Array.SetValueの引数の順番に従った
        public static void SetValue<T>(this T[, ,] array, T item, Int3 index) { array[index.v0, index.v1, index.v2] = item; }  //Array.SetValueの引数の順番に従った

        public static T[] Let<T>(this T[] array, Func<T, T> selector)
        {
            int l = array.Length;
            for (int i = 0; i < l; i++)
                array[i] = selector(array[i]);
            return array;
        }
        public static T[,] Let<T>(this T[,] array, Func<T, T> selector)
        {
            var l = array.Lengths();
            for (int i0 = 0; i0 < l.v0; i0++)
                for (int i1 = 0; i1 < l.v1; i1++)
                    array[i0, i1] = selector(array[i0, i1]);
            return array;
        }
        public static T[, ,] Let<T>(this T[, ,] array, Func<T, T> selector)
        {
            Int3 l = array.Lengths();
            for (int i0 = 0; i0 < l.v0; i0++)
                for (int i1 = 0; i1 < l.v1; i1++)
                    for (int i2 = 0; i2 < l.v2; i2++)
                        array[i0, i1, i2] = selector(array[i0, i1, i2]);
            return array;
        }

        public static T[] Let<T>(this T[] array, Func<T, int, T> selector)
        {
            int l = array.Length;
            for (int i = 0; i < l; i++)
                array[i] = selector(array[i], i);
            return array;
        }
        public static T[,] Let<T>(this T[,] array, Func<T, int, int, T> selector)
        {
            Int2 l = array.Lengths();
            for (int i0 = 0; i0 < l.v0; i0++)
                for (int i1 = 0; i1 < l.v1; i1++)
                    array[i0, i1] = selector(array[i0, i1], i0, i1);
            return array;
        }
        public static T[, ,] Let<T>(this T[, ,] array, Func<T, int, int, int, T> selector)
        {
            Int3 l = array.Lengths();
            for (int i0 = 0; i0 < l.v0; i0++)
                for (int i1 = 0; i1 < l.v1; i1++)
                    for (int i2 = 0; i2 < l.v2; i2++)
                        array[i0, i1, i2] = selector(array[i0, i1, i2], i0, i1, i2);
            return array;
        }

        public static void Let<T>(this T[] array, T value)
        {
            for (int i = array.Length; --i >= 0; )
                array[i] = value;
        }
        public static void Let<T>(this T[,] array, T value)
        {
            for (int i = array.GetLength(0); --i >= 0; )
                for (int j = array.GetLength(1); --j >= 0; )
                    array[i, j] = value;
        }
        public static void Let<T>(this T[, ,] array, T value)
        {
            for (int i = array.GetLength(0); --i >= 0; )
                for (int j = array.GetLength(1); --j >= 0; )
                    for (int k = array.GetLength(2); --k >= 0; )
                        array[i, j, k] = value;
        }
        public static void Let<T>(this T[, , ,] array, T value)
        {
            for (int i = array.GetLength(0); --i >= 0; )
                for (int j = array.GetLength(1); --j >= 0; )
                    for (int k = array.GetLength(2); --k >= 0; )
                        for (int l = array.GetLength(3); --l >= 0; )
                            array[i, j, k, l] = value;
        }
        public static T[] SubArray<T>(this T[] array, int start)
        {
            T[] data = new T[array.Length - start];
            Array.Copy(array, start, data, 0, data.Length);
            return data;
        }
        public static T[] SubArray<T>(this T[] array, int start, int count)
        {
            T[] data = new T[count];
            Array.Copy(array, start, data, 0, count);
            return data;
        }

        public static T[][] ToJaggedArray<T>(this T[,] array)
        {
            var jagged = new T[array.GetLength(0)][];
            for (int i = 0; i < jagged.Length; i++)
            {
                var row = new T[array.GetLength(1)];
                for (int j = 0; j < row.Length; j++)
                    row[j] = array[i, j];
                jagged[i] = row;
            }
            return jagged;
        }

        public static IEnumerable<TResult> Zip<TSource0, TSource1, TResult>(TSource0[] array0, TSource1[] array1, Func<TSource0, TSource1, TResult> selector)
        {
            var l = array0.Length;
            if (l != array1.Length) ThrowException.SizeMismatch();
            for (int i = 0; i < l; i++)
                yield return selector(array0[i], array1[i]);
        }
        public static IEnumerable<TResult> Zip<TSource0, TSource1, TResult>(TSource0[,] array0, TSource1[,] array1, Func<TSource0, TSource1, TResult> selector)
        {
            var l = array0.Lengths();
            if (l != array1.Lengths()) ThrowException.SizeMismatch();
            for (int i0 = 0; i0 < l.v0; i0++)
                for (int i1 = 0; i1 < l.v1; i1++)
                    yield return selector(array0[i0, i1], array1[i0, i1]);
        }
        public static IEnumerable<TResult> Zip<TSource0, TSource1, TResult>(TSource0[, ,] array0, TSource1[, ,] array1, Func<TSource0, TSource1, TResult> selector)
        {
            var l = array0.Lengths();
            if (l != array1.Lengths()) ThrowException.SizeMismatch();
            for (int i0 = 0; i0 < l.v0; i0++)
                for (int i1 = 0; i1 < l.v1; i1++)
                    for (int i2 = 0; i2 < l.v2; i2++)
                        yield return selector(array0[i0, i1, i2], array1[i0, i1, i2]);
        }
        #endregion

        #region List
        public static bool AddOrDiscard<T>(this List<T> list, T item)
        {
            if (list.Contains(item)) return false;
            list.Add(item); return true;
        }
        public static bool AddOrOverwrite<T>(this List<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index != -1) { list[index] = item; return false; }
            list.Add(item); return true;
        }
        #endregion

        #region IList
        // IList Index
        public static int MinIndex<T>(this IList<T> list, int index, int length) where T : IComparable<T>
        {
            int j;
            for (int i = j = index; ++i < index + length; )
                if (list[j].CompareTo(list[i]) > 0) j = i;
            return j;
        }
        public static int MaxIndex<T>(this IList<T> list, int index, int length) where T : IComparable<T>
        {
            int j;
            for (int i = j = index; ++i < index + length; )
                if (list[j].CompareTo(list[i]) < 0) j = i;
            return j;
        }
        public static Int2 MinMaxIndex<T>(this IList<T> list, int index, int length) where T : IComparable<T>
        {
            int j0, j1;
            for (int i = j0 = j1 = index; ++i < index + length; )
                if (list[j0].CompareTo(list[i]) > 0) j0 = i;
                else if (list[j1].CompareTo(list[i]) < 0) j1 = i;
            return new Int2(j0, j1);
        }
        public static int MinIndex<T>(this IList<T> list) where T : IComparable<T> { return MinIndex(list, 0, list.Count); }
        public static int MaxIndex<T>(this IList<T> list) where T : IComparable<T> { return MaxIndex(list, 0, list.Count); }
        public static Int2 MinMaxIndex<T>(this IList<T> list) where T : IComparable<T> { return MinMaxIndex(list, 0, list.Count); }

        public static int MinLastIndex<T>(this IList<T> list, int index, int length) where T : IComparable<T>
        {
            int j;
            for (int i = j = index + length - 1; --i >= index; )
                if (list[j].CompareTo(list[i]) > 0) j = i;
            return j;
        }
        public static int MaxLastIndex<T>(this IList<T> list, int index, int length) where T : IComparable<T>
        {
            int j;
            for (int i = j = index + length - 1; --i >= index; )
                if (list[j].CompareTo(list[i]) < 0) j = i;
            return j;
        }
        public static Int2 MinMaxLastIndex<T>(this IList<T> list, int index, int length) where T : IComparable<T>
        {
            int j0, j1;
            for (int i = j0 = j1 = index + length - 1; --i >= index; )
                if (list[j0].CompareTo(list[i]) > 0) j0 = i;
                else if (list[j1].CompareTo(list[i]) < 0) j1 = i;
            return new Int2(j0, j1);
        }
        public static int MinLastIndex<T>(this IList<T> list) where T : IComparable<T> { return MinLastIndex(list, 0, list.Count); }
        public static int MaxLastIndex<T>(this IList<T> list) where T : IComparable<T> { return MaxLastIndex(list, 0, list.Count); }
        public static Int2 MinMaxLastIndex<T>(this IList<T> list) where T : IComparable<T> { return MinMaxLastIndex(list, 0, list.Count); }

        public static int FindIndex<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = 0; i < list.Count; i++)
                if (match(list[i])) return i;
            return -1;
        }
        public static int FindIndexLast<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = list.Count; --i >= 0; )
                if (match(list[i])) return i;
            return -1;
        }
        public static int[] FindAllIndex<T>(this IList<T> list, Predicate<T> match)
        {
            var L = new List<int>();
            for (int i = 0; i < list.Count; i++)
                if (match(list[i])) L.Add(i);
            return L.ToArray();
        }

        // IList Others
        public static void Let<T>(this IList<T> list, T item)
        {
            for (int i = list.Count; --i >= 0; )
                list[i] = item;
        }
        public static T[] Sub<T>(this IList<T> list, int start)
        {
            T[] data = new T[list.Count - start];
            for (int i = 0; i < data.Length; i++)
                data[i] = list[i + start];
            return data;
        }
        public static T[] Sub<T>(this IList<T> list, int start, int count)
        {
            T[] data = new T[count];
            for (int i = 0; i < count; i++)
                data[i] = list[i + start];
            return data;
        }
        public static bool AllBackward<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = list.Count; --i >= 0; )
                if (!match(list[i])) return false;
            return true;
        }
        public static bool AnyBackward<T>(this IList<T> list, Predicate<T> match)
        {
            for (int i = list.Count; --i >= 0; )
                if (match(list[i])) return false;
            return true;
        }

        public static int BinarySearch<T>(this IList<T> list, T item) where T : IComparable<T>
        {
            int i0 = 0;
            int i1 = list.Count;
            while (true)
            {
                if (i0 == i1) return ~i0;
                int ii = (i0 + i1) / 2;
                int c = item.CompareTo(list[ii]);
                if (c == 0) return ii;
                if (c < 0) i1 = ii; else i0 = ii + 1;
            }
        }
        public static int BinarySearch<T>(this IList<T> list, T item, IComparer<T> comparer)
        {
            int i0 = 0;
            int i1 = list.Count;
            while (true)
            {
                if (i0 == i1) return ~i0;
                int ii = (i0 + i1) / 2;
                int c = comparer.Compare(item, list[ii]);
                if (c == 0) return ii;
                if (c < 0) i1 = ii; else i0 = ii + 1;
            }
        }

        public static T First<T>(this IList<T> list)
        {
            return list[0];
        }
        public static T Last<T>(this IList<T> list)
        {
            return list[list.Count - 1];
        }
        public static T Pop<T>(this IList<T> list)
        {
            T item = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return item;
        }

        public static int LetDistinctSorted<T>(this IList<T> list)
        {
            var comparer = EqualityComparer<T>.Default;
            int i = 0;
            for (int j = 1; j < list.Count; j++)
            {
                if (!comparer.Equals(list[i], list[j]))
                    list[++i] = list[j];
            }
            i++;
            int overlap = list.Count - i;
            for (int j = list.Count; --j >= i; )
                list.RemoveAt(j);
            return overlap;
        }

        public static void Overwrite<TSource>(this IList<TSource> list, Func<TSource, TSource> selector)
        {
            for (int i = 0; i < list.Count; i++)
                list[i] = selector(list[i]);
        }
        public static void Overwrite<TSource>(this IList<TSource> list, Func<TSource, int, TSource> selector)
        {
            for (int i = 0; i < list.Count; i++)
                list[i] = selector(list[i], i);
        }

        public static T[] Concatenate<T>(this IList<T> list, IList<T> other)
        {
            var array = new T[list.Count + other.Count];
            list.CopyTo(array, 0);
            other.CopyTo(array, list.Count);
            return array;
        }
        public static T[] Concatenate<T>(this IList<T> list, T item)
        {
            var array = new T[list.Count + 1];
            list.CopyTo(array, 0);
            array[list.Count] = item;
            return array;
        }
        public static T[] Concatenate<T>(T item, IList<T> list)
        {
            var array = new T[list.Count + 1];
            array[0] = item;
            list.CopyTo(array, 1);
            return array;
        }
        #endregion

        #region ICollection, SortedSet, IDictionary, ISet
        // ICollection
        public static void AddNew<T>(this ICollection<T> source) where T : new()
        {
            source.Add(new T());
        }
        public static void AddNew<T>(this ICollection<T> source, int count) where T : new()
        {
            for (int i = count; --i >= 0; )
                source.Add(new T());
        }

        // SortedSet
        public static T First<T>(this SortedSet<T> set)
        {
            return set.Min;
        }
        public static T Last<T>(this SortedSet<T> set)
        {
            return set.Max;
        }
        public static T Pop<T>(this SortedSet<T> set)
        {
            var item = set.Max;
            set.Remove(item);
            return item;
        }

        // IDictionary
        public static KeyValuePair<TKey, TValue> Pop<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var item = dictionary.Last();
            dictionary.Remove(item.Key);
            return item;
        }
        public static bool AddOrDiscard<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, value); return true;
        }
        public static bool AddOrOverwrite<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key)) { dictionary[key] = value; return false; }
            dictionary.Add(key, value); return true;
        }
        public static TValue GetItemOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.Keys.Contains(key) ? dictionary[key] : default(TValue);
        }
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultvalue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultvalue;
        }
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }

        // ISet
        public static bool AddOrDiscard<T>(this ISet<T> set, T item)
        {
            return set.Add(item);
        }
        public static bool AddOrOverwrite<T>(this ISet<T> set, T item)
        {
            if (set.Contains(item)) { set.Remove(item); set.Add(item); return false; }
            else { set.Add(item); return true; }
        }

        // Dictionary
        // SortedDictionary
        // SortedList
        #endregion

        #region Sorting
        public static T[] LetReverse<T>(this T[] array) { Array.Reverse(array); return array; }
        public static T[] LetSort<T>(this T[] array) where T : IComparable<T> { Array.Sort(array); return array; }
        public static T[] LetSort<T>(this T[] array, Comparison<T> compare) { Array.Sort(array, compare); return array; }
        public static T[] LetSort<T>(this T[] array, Func<T, int> selector) { return array.LetSort((x, y) => selector(x).CompareTo(selector(y))); }
        public static T[] LetSort<T>(this T[] array, Func<T, double> selector) { return array.LetSort((x, y) => selector(x).CompareTo(selector(y))); }

        public static List<T> LetReverse<T>(this List<T> list) { list.Reverse(); return list; }
        public static List<T> LetSort<T>(this List<T> list) where T : IComparable<T> { list.Sort(); return list; }
        public static List<T> LetSort<T>(this List<T> list, Comparison<T> compare) { list.Sort(compare); return list; }
        public static List<T> LetSort<T>(this List<T> list, Func<T, int> selector) { return list.LetSort((x, y) => selector(x).CompareTo(selector(y))); }
        public static List<T> LetSort<T>(this List<T> list, Func<T, double> selector) { return list.LetSort((x, y) => selector(x).CompareTo(selector(y))); }

        public static int[] SortIndex<T>(this IList<T> list, Comparison<T> comparison)
        {
            int[] index = new int[list.Count];
            for (int i = index.Length; --i >= 0; )
                index[i] = i;
            Array.Sort(index, (x, y) => comparison(list[x], list[y]));
            return index;
        }
        public static int[] SortIndex<T>(this IList<T> list) where T : IComparable<T> { return SortIndex(list, (x, y) => x.CompareTo(y)); }
        public static int[] SortIndex<T>(this IList<T> list, Func<T, int> selector) { return SortIndex(list, (x, y) => selector(x).CompareTo(selector(y))); }
        public static int[] SortIndex<T>(this IList<T> list, Func<T, double> selector) { return SortIndex(list, (x, y) => selector(x).CompareTo(selector(y))); }

        public static int[] IndexToRank(IList<int> index)
        {
            int[] Rank = new int[index.Count];
            for (int i = index.Count; --i >= 0; )
                Rank[index[i]] = i;
            return Rank;
        }

        public static T[] LetSortAsIndex<T>(this T[] list, IList<int> index) { return (T[])((IList<T>)list).LetSortAsIndex(index); }
        public static List<T> LetSortAsIndex<T>(this List<T> list, IList<int> index) { return (List<T>)((IList<T>)list).LetSortAsIndex(index); }
        public static IList<T> LetSortAsIndex<T>(this IList<T> list, IList<int> index)
        {
            for (int i = index.Count; --i >= 0; )
            {
                if (index[i] < 0) continue;
                T item = list[i];
                for (int j = i; ; )  // listの中の空いている添字番号
                {
                    int jj = index[j]; index[j] = ~index[j];
                    if (jj == i) { list[j] = item; break; }
                    list[j] = list[jj];
                    j = jj;
                }
            }
            for (int i = index.Count; --i >= 0; )
                index[i] = ~index[i];
            return list;
        }
        public static T[] OrderByIndex<T>(this IList<T> list, IList<int> index)
        {
            var result = new T[index.Count];
            for (int i = result.Length; --i >= 0; )
                result[i] = list[index[i]];
            return result;
        }

        public static T[] LetSortAsRank<T>(this T[] list, IList<int> rank) { return (T[])((IList<T>)list).LetSortAsRank(rank); }
        public static List<T> LetSortAsRank<T>(this List<T> list, IList<int> rank) { return (List<T>)((IList<T>)list).LetSortAsRank(rank); }
        public static IList<T> LetSortAsRank<T>(this IList<T> list, IList<int> rank)
        {
            for (int i = rank.Count; --i >= 0; )
            {
                if (rank[i] < 0) continue;
                T item = list[i];
                for (int j = i; ; )  // listの中の玉突きの玉の添字番号
                {
                    int jj = rank[j]; rank[j] = ~rank[j];
                    if (jj == i) { list[jj] = item; break; }
                    { T temp = list[jj]; list[jj] = item; item = temp; }
                    j = jj;
                }
            }
            for (int i = rank.Count; --i >= 0; )
                rank[i] = ~rank[i];
            return list;
        }
        public static T[] OrderByRank<T>(this IList<T> list, IList<int> rank)
        {
            var result = new T[list.Count];
            for (int i = result.Length; --i >= 0; )
                result[rank[i]] = list[i];
            return result;
        }

        public static int Compare<T>(IList<T> left, IList<T> right) where T : IComparable<T>
        {
            if (left == null) if (right == null) return 0; else return -1; else if (right == null) return 1;
            if (left.Count != right.Count) return left.Count - right.Count;
            for (int i = 0; i < left.Count; i++)
            {
                int c = left[i].CompareTo(right[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        public static int Compare<T>(T[] left, T[] right) where T : IComparable<T>
        {
            if (left == null) if (right == null) return 0; else return -1; else if (right == null) return 1;
            if (left.Length != right.Length) return left.Length - right.Length;
            for (int i = 0; i < left.Length; i++)
            {
                int c = left[i].CompareTo(right[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        public static int Compare<T>(T[][] left, T[][] right) where T : IComparable<T>
        {
            if (left == null) if (right == null) return 0; else return -1; else if (right == null) return 1;
            if (left.Length != right.Length) return left.Length - right.Length;
            for (int i = 0; i < left.Length; i++)
            {
                int c = Compare(left[i], right[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        public static int Compare<T>(T[][][] left, T[][][] right) where T : IComparable<T>
        {
            if (left == null) if (right == null) return 0; else return -1; else if (right == null) return 1;
            if (left.Length != right.Length) return left.Length - right.Length;
            for (int i = 0; i < left.Length; i++)
            {
                int c = Compare(left[i], right[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        public static int Compare<T>(T[][][][] left, T[][][][] right) where T : IComparable<T>
        {
            if (left == null) if (right == null) return 0; else return -1; else if (right == null) return 1;
            if (left.Length != right.Length) return left.Length - right.Length;
            for (int i = 0; i < left.Length; i++)
            {
                int c = Compare(left[i], right[i]);
                if (c != 0) return c;
            }
            return 0;
        }
        #endregion

        #region TimeSpan
        public static TimeSpan Multiply(this TimeSpan left, int right) { return TimeSpan.FromTicks(left.Ticks * right); }
        public static TimeSpan Multiply(this TimeSpan left, double right) { return TimeSpan.FromTicks((long)(left.Ticks * right)); }
        public static TimeSpan Divide(this TimeSpan left, int right) { return TimeSpan.FromTicks(left.Ticks / right); }
        public static TimeSpan Divide(this TimeSpan left, double right) { return TimeSpan.FromTicks((long)(left.Ticks / right)); }
        public static TimeSpan Remainder(this TimeSpan left, int right) { return TimeSpan.FromTicks(left.Ticks % right); }
        public static TimeSpan Remainder(this TimeSpan left, double right) { return TimeSpan.FromTicks((long)(left.Ticks % right)); }
        public static TimeSpan Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            TimeSpan r = new TimeSpan();
            foreach (var element in source) checked { r += selector(element); }
            return r;
        }
        public static TimeSpan Average<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            TimeSpan r = new TimeSpan();
            int count = 0;
            foreach (var element in source) { checked { r += selector(element); } count++; }
            return r.Divide(count);
        }
        #endregion

        #region string
        public static int TryParse(this string str, int defaultvalue)
        {
            int value;
            return int.TryParse(str, out value) ? value : defaultvalue;
        }
        public static double TryParse(this string str, double defaultvalue)
        {
            double value;
            return double.TryParse(str, out value) ? value : defaultvalue;
        }

        public static string Join(this IEnumerable<string> source, string separator, string terminator = "")
        {
            var buffer = new StringBuilder();
            foreach (var element in source)
            {
                buffer.Append(element);
                buffer.Append(separator);
            }
            if (buffer.Length > 0)
                buffer.Remove(buffer.Length - separator.Length, separator.Length);
            buffer.Append(terminator);
            return buffer.ToString();
        }

        public static string[] Split(this string str, char separator) { return str.Split(new char[] { separator }); }
        public static string[] Split(this string str, string separator) { return str.Split(new string[] { separator }, StringSplitOptions.None); }

        public static string[] SplitLine(this string str) { return str.Split(Gb.NewLineCodes, StringSplitOptions.None); }
        public static string[] SplitTab(this string line) { return line.Split('\t'); }
        public static string[] SplitCsv(this string line)
        {
            if (line == null) return null;
            var converted = new List<char>();
            bool quotation = false;
            bool escape = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = i < line.Length ? line[i] : ',';
                if (escape)
                {
                    if (c != '\"') { quotation = false; escape = false; }
                }
                if (!quotation)
                {
                    if (c == '\"') { quotation = true; continue; }
                    if (c == ',') c = '\t';
                }
                else
                {
                    if (c == '\"')
                    {
                        if (!escape) { escape = true; continue; }  // 1個目
                        else { escape = false; }                   // 2個目
                    }
                }
                converted.Add(c);
            }
            return new string(converted.ToArray()).SplitTab();
        }
        public static string JoinTab(this IEnumerable<string> items) { return items.Join("\t"); }
        public static string JoinSpace(this IEnumerable<string> items) { return items.Join(" "); }
        public static string JoinComma(this IEnumerable<string> items) { return items.Join(","); }
        public static string JoinCommaSpace(this IEnumerable<string> items) { return items.Join(", "); }

        public static string Replace(this string str, string[,] replacelist)
        {
            for (int i = 0; i < replacelist.GetLength(0); i++)
                str = str.Replace(replacelist[i, 0], replacelist[i, 1]);
            return str;
        }

        public static void Write(this StringBuilder sb, string value) { sb.Append(value); }
        public static void Write(this StringBuilder sb, bool value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, char value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, char[] buffer) { sb.Append(buffer, 0, buffer.Length); }
        public static void Write(this StringBuilder sb, char[] buffer, int index, int count) { sb.Append(buffer, index, count); }
        public static void Write(this StringBuilder sb, decimal value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, double value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, float value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, int value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, long value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, object value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, string format, object arg0) { sb.Append(string.Format(format, arg0)); }
        public static void Write(this StringBuilder sb, string format, object arg0, object arg1) { sb.Append(string.Format(format, arg0, arg1)); }
        public static void Write(this StringBuilder sb, string format, object arg0, object arg1, object arg2) { sb.Append(string.Format(format, arg0, arg1, arg2)); }
        public static void Write(this StringBuilder sb, string format, params object[] arg) { sb.Append(string.Format(format, arg)); }
        public static void Write(this StringBuilder sb, uint value) { sb.Append(value.ToString()); }
        public static void Write(this StringBuilder sb, ulong value) { sb.Append(value.ToString()); }

        public static void WriteLine(this StringBuilder sb) { sb.AppendLine(); }
        public static void WriteLine(this StringBuilder sb, string value) { sb.AppendLine(value); }
        public static void WriteLine(this StringBuilder sb, bool value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, char value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, char[] buffer) { sb.Append(buffer); sb.AppendLine(); }
        public static void WriteLine(this StringBuilder sb, char[] buffer, int index, int count) { sb.Append(buffer, index, count); sb.AppendLine(); }
        public static void WriteLine(this StringBuilder sb, decimal value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, double value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, float value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, int value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, long value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, object value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, string format, object arg0) { sb.AppendLine(string.Format(format, arg0)); }
        public static void WriteLine(this StringBuilder sb, string format, object arg0, object arg1) { sb.AppendLine(string.Format(format, arg0, arg1)); }
        public static void WriteLine(this StringBuilder sb, string format, object arg0, object arg1, object arg2) { sb.AppendLine(string.Format(format, arg0, arg1, arg2)); }
        public static void WriteLine(this StringBuilder sb, string format, params object[] arg) { sb.AppendLine(string.Format(format, arg)); }
        public static void WriteLine(this StringBuilder sb, uint value) { sb.AppendLine(value.ToString()); }
        public static void WriteLine(this StringBuilder sb, ulong value) { sb.AppendLine(value.ToString()); }
        #endregion

        #region other classes
        // Path
        public static string[] GetFiles(string path) { return GetFiles(path, SearchOption.TopDirectoryOnly); }
        public static string[] GetFiles(string path, SearchOption option)
        {
            string dirname = Path.GetDirectoryName(path);
            string filename = Path.GetFileName(path);
            if (filename.Length == 0) filename = "*";
            string[] paths = Directory.GetFiles(dirname, filename, option);
            return paths;
        }

        public static void Write(this Stream stream, byte[] array)
        {
            stream.Write(array, 0, array.Length);
        }
        public static void Write(this Stream stream, IEnumerable<byte> source)
        {
            var array = source.ToArray();
            stream.Write(array, 0, array.Length);
        }
        public static int Read(this FileStream filestream, byte[] array)
        {
            return filestream.Read(array, 0, array.Length);
        }

        // Stream
        public static bool EndOfStream(this Stream stream)
        {
            return stream.Position < stream.Length;
        }

        // Average
        public static Double2 Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Double2> selector)
        {
            Double2 r = default(Double2);
            int count = 0;
            foreach (var element in source) { checked { r += selector(element); } count++; }
            return r / count;
        }
        public static Double3 Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Double3> selector)
        {
            Double3 r = default(Double3);
            int count = 0;
            foreach (var element in source) { checked { r += selector(element); } count++; }
            return r / count;
        }
        #endregion
    }
    #endregion

    #region Gb
    public static partial class Gb
    {
        public static readonly string NewLine = Environment.NewLine;
        public static readonly string[] NewLineCodes = { "\r\n", "\n", "\r" };
        public static readonly Encoding CodeUTF8 = Encoding.GetEncoding("utf-8");
        public static readonly Encoding CodeSJIS = Encoding.GetEncoding("shift_jis");
        public static readonly Encoding CodeEUC = Encoding.GetEncoding("euc-jp");
        public static readonly Encoding CodeJIS = Encoding.GetEncoding("iso-2022-jp");

        public static IEnumerable<string> EnumerateFileLines(string path, Encoding encoding)
        {
            using (var file = new StreamReader(path, encoding))
            {
                while (!file.EndOfStream)
                    yield return file.ReadLine();
            }
        }
        public static IEnumerable<string> EnumerateFileLines(string path)
        {
            return EnumerateFileLines(path, DetectEncoding(path));
        }
        public static Encoding DetectEncoding(string path)
        {
            var names = new string[] { "utf-8", "shift_jis", "euc-jp", "iso-2022-jp" };
            foreach (var name in names)
            {
                var encoding = Encoding.GetEncoding(name);
                bool sw = true;
                using (var reader = new StreamReader(path, encoding))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line.IndexOf('\ufffd') != -1) { sw = false; break; }
                    }
                    if (!sw) continue;
                    return encoding;
                }
            }
            //Console.Error.WriteLine("cannot detect proper encoding.");
            return null;
        }

        public static T FromString<T>(string str)
        {
            return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(str);
        }

        public static int[] ExtractNumbers(string str)
        {
            var list = new List<int>();
            string[] items = str.Split(',');
            foreach (string s in items)
            {
                if (s.IndexOf('-') < 0) { list.Add(int.Parse(s)); continue; }
                string[] t = s.Split('-');
                for (int i = int.Parse(t[0]), j = int.Parse(t[1]); i <= j; ++i)
                    list.Add(i);
            }
            list.Sort();
            return list.ToArray();
        }

    }

    #endregion

    #region StopWatch
    public class StopWatch
    {
        DateTime RegisteredTime;
        TimeSpan _Duration;
        bool _Running;
        public StopWatch() { Restart(); }

        public TimeSpan Duration { get { return _Running ? _Duration + (DateTime.Now - RegisteredTime) : _Duration; } }
        public bool Running { get { return _Running; } }

        public void Restart() { Reset(); Start(); }
        public void Reset() { _Running = false; _Duration = TimeSpan.Zero; }
        public void Start() { RegisteredTime = DateTime.Now; _Running = true; }
        public TimeSpan Stop() { _Running = false; return _Duration += DateTime.Now - RegisteredTime; }
        public override string ToString() { return Duration.ToString(); }
    }
    #endregion

    #region RandomMT
    // MersenneTwister.dSFMT2.1
    public class RandomMT
    {
        const int N = 624, M = 397;
        const uint UPPER_MASK = 0x80000000u;
        const uint LOWER_MASK = 0x7fffffffu;
        const uint MATRIX_A = 0x9908b0dfu;

        uint BufferIndex;
        uint[] Buffer = new uint[N];

        public RandomMT() { Init((uint)DateTime.Now.ToBinary()); }
        public RandomMT(uint seed) { Init(seed); }
        public RandomMT(int seed) { Init((uint)seed); }

        public void Init(uint seed)
        {
            Buffer[0] = seed;
            for (int i = 1; i < N; i++)
                Buffer[i] = 1812433253u * (Buffer[i - 1] ^ (Buffer[i - 1] >> 30)) + (uint)i;
            BufferIndex = N;
        }
        public void Init(uint[] key)
        {
            if (key == null) ThrowException.ArgumentNullException("key");
            Init(19650218u);
            uint i = 1, j = 0;
            for (int k = Math.Max(N, key.Length); k > 0; k--)
            {
                Buffer[i] = (Buffer[i] ^ ((Buffer[i - 1] ^ (Buffer[i - 1] >> 30)) * 1664525u)) + key[j] + j; // non linear
                if (++i >= N) { i = 1; Buffer[0] = Buffer[N - 1]; }
                if (++j >= key.Length) j = 0;
            }
            for (int k = N - 1; k > 0; k--)
            {
                Buffer[i] = (Buffer[i] ^ ((Buffer[i - 1] ^ (Buffer[i - 1] >> 30)) * 1566083941u)) - i; // non linear
                if (++i >= N) { i = 1; Buffer[0] = Buffer[N - 1]; }
            }
            Buffer[0] = 0x80000000u; // MSB is 1; assuring non-zero initial array
        }

        // 32-bit uint [0, 0xffffffff]
        public uint UInt32()
        {
            if (BufferIndex >= N) { BufferIndex = 0; UInt32_(); } // generate N words at one time            
            uint y = Buffer[BufferIndex++];
            // tempering
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680u;
            y ^= (y << 15) & 0xefc60000u;
            y ^= (y >> 18);
            return y;
        }
        void UInt32_()
        {
            for (int kk = 0, k1 = 1, kM = M; kk < N; kk++, k1++, kM++)
            {
                if (k1 == N) k1 = 0;
                if (kM == N) kM = 0;
                uint y = (Buffer[kk] & UPPER_MASK) | (Buffer[k1] & LOWER_MASK);
                Buffer[kk] = Buffer[kM] ^ (y >> 1) ^ ((y & 1u) * MATRIX_A);
            }
        }
        // 31-bit int [-0x80000000, 0x7fffffff]
        public int Int32() { return (int)this.UInt32(); }
        // 31-bit int [0, 0x7fffffff]
        public int Int31() { return (int)(this.UInt32() >> 1); }

        public uint UInt(uint value) { return this.UInt32() % value; }
        public int Int(int value) { return (int)UInt((uint)value); }

        public uint UIntExact(uint value)
        {
            while (true)
            {
                uint y = this.UInt32();
                if (y < (1LU << 32) / value * value) return y % value;
            }
        }
        public int IntExact(int value) { return (int)UIntExact((uint)value); }

        public double Double() { return Double32CO(); }
        // 32-bit double [0, 1]
        public double Double32CC() { return this.UInt32() * (1.0 / ((1L << 32) - 1)); }
        // 32-bit double [0, 1)
        public double Double32CO() { return this.UInt32() * (1.0 / (1L << 32)); }
        // 32-bit double (0, 1]
        public double Double32OC() { return (this.UInt32() + 1.0) * (1.0 / (1L << 32)); }
        // 32-bit double (0, 1)
        public double Double32OO() { return (this.UInt32() + 0.5) * (1.0 / (1L << 32)); }
        // 53-bit double [0, 1]
        public double Double53CC() { return ((this.UInt32() >> 5) * (double)(1 << 26) + (this.UInt32() >> 6)) * (1.0 / ((1L << 53) - 1)); }
        // 53-bit double [0, 1)
        public double Double53CO() { return ((this.UInt32() >> 5) * (double)(1 << 26) + (this.UInt32() >> 6)) * (1.0 / (1L << 53)); }
        // 53-bit double (0, 1]
        public double Double53OC() { return ((this.UInt32() >> 5) * (double)(1 << 26) + (this.UInt32() >> 6) + 1.0) * (1.0 / (1L << 53)); }
        // 52-bit double (0, 1)
        public double Double52OO() { return ((this.UInt32() >> 6) * (double)(1 << 26) + (this.UInt32() >> 6) + 0.5) * (1.0 / (1L << 52)); }

        public double Gaussian() { return Gaussian32(); }
        double BufferGaussian32 = double.PositiveInfinity;
        public double Gaussian32()
        {
            if (BufferGaussian32 != double.PositiveInfinity) { double x = BufferGaussian32; BufferGaussian32 = double.PositiveInfinity; return x; }
            double v1, v2, r;
            do
            {
                v1 = this.Double32OO() - 0.5;
                v2 = this.Double32OO() - 0.5;
                r = v1 * v1 + v2 * v2;
            } while (r >= 0.25 || r == 0);
            double f = Math.Sqrt(-2 * Math.Log(r * 4) / r);
            BufferGaussian32 = v2 * f;
            return v1 * f;
        }
        double BufferGaussian52 = double.PositiveInfinity;
        public double Gaussian52()
        {
            if (BufferGaussian52 != double.PositiveInfinity) { double x = BufferGaussian52; BufferGaussian52 = double.PositiveInfinity; return x; }
            double v1, v2, r;
            do
            {
                v1 = this.Double52OO() - 0.5;
                v2 = this.Double52OO() - 0.5;
                r = v1 * v1 + v2 * v2;
            } while (r >= 0.25 || r == 0);
            double f = Math.Sqrt(-2 * Math.Log(r * 4) / r);
            BufferGaussian52 = v2 * f;
            return v1 * f;
        }
        public int Categorical(double[] distribution)
        {
            double r = Double();
            double c = 0.0;
            for (int i = 0; i < distribution.Length - 1; i++)
            {
                c += distribution[i];
                if (r < c) return i;
            }
            return distribution.Length - 1;
        }
    }
    #endregion

    #region Comparer classes
    public class EqualityComparerArray<T> : IEqualityComparer<T[]>
        where T : IComparable<T>
    {
        public EqualityComparerArray() { }
        #region IEqualityComparer<T[]> メンバー
        public bool Equals(T[] x, T[] y) { return Ex.Compare(x, y) == 0; }
        public int GetHashCode(T[] obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
        #endregion
    }
    public class EqualityComparerArray2<T> : IEqualityComparer<T[][]>
        where T : IComparable<T>
    {
        public EqualityComparerArray2() { }
        #region IEqualityComparer<T[][]> メンバー
        public bool Equals(T[][] x, T[][] y) { return Ex.Compare(x, y) == 0; }
        public int GetHashCode(T[][] obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
        #endregion
    }
    public class EqualityComparerArray3<T> : IEqualityComparer<T[][][]>
        where T : IComparable<T>
    {
        public EqualityComparerArray3() { }
        #region IEqualityComparer<T[][][]> メンバー
        public bool Equals(T[][][] x, T[][][] y) { return Ex.Compare(x, y) == 0; }
        public int GetHashCode(T[][][] obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
        #endregion
    }
    public class EqualityComparerArray4<T> : IEqualityComparer<T[][][][]>
        where T : IComparable<T>
    {
        public EqualityComparerArray4() { }
        #region IEqualityComparer<T[][][][]> メンバー
        public bool Equals(T[][][][] x, T[][][][] y) { return Ex.Compare(x, y) == 0; }
        public int GetHashCode(T[][][][] obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
        #endregion
    }
    public class EqualityComparerIList<T> : IEqualityComparer<IList<T>>
        where T : IComparable<T>
    {
        public EqualityComparerIList() { }
        #region IEqualityComparer<IList<T>> メンバー
        public bool Equals(IList<T> x, IList<T> y) { return Ex.Compare(x, y) == 0; }
        public int GetHashCode(IList<T> obj) { int a = 0; foreach (var item in obj) a += item.GetHashCode() * 3; return a; }
        #endregion
    }

    public class ComparerArray<T> : IComparer<T[]>
        where T : IComparable<T>
    {
        public ComparerArray() { }
        #region IComparer<T[]> メンバー
        public int Compare(T[] x, T[] y) { return Ex.Compare(x, y); }
        #endregion
    }
    public class ComparerArray2<T> : IComparer<T[][]>
        where T : IComparable<T>
    {
        public ComparerArray2() { }
        #region IComparer<T[][]> メンバー
        public int Compare(T[][] x, T[][] y) { return Ex.Compare(x, y); }
        #endregion
    }
    public class ComparerArray3<T> : IComparer<T[][][]>
        where T : IComparable<T>
    {
        public ComparerArray3() { }
        #region IComparer<T[][][]> メンバー
        public int Compare(T[][][] x, T[][][] y) { return Ex.Compare(x, y); }
        #endregion
    }
    public class ComparerArray4<T> : IComparer<T[][][][]>
        where T : IComparable<T>
    {
        public ComparerArray4() { }
        #region IComparer<T[][][]> メンバー
        public int Compare(T[][][][] x, T[][][][] y) { return Ex.Compare(x, y); }
        #endregion
    }
    public class ComparerIList<T> : IComparer<IList<T>>
        where T : IComparable<T>
    {
        public ComparerIList() { }
        #region IComparer<IList<T>> メンバー
        public int Compare(IList<T> x, IList<T> y) { return Ex.Compare(x, y); }
        #endregion
    }
    public class ComparerComparison<T> : IComparer<T>
    {
        Comparison<T> comparer;
        public ComparerComparison(Comparison<T> comparison) { this.comparer = comparison; }
        #region IComparer<T> メンバ
        public int Compare(T x, T y) { return comparer(x, y); }
        #endregion
    }
    public class ComparerReverse<T> : IComparer<T>
    {
        IComparer<T> comparer;
        public ComparerReverse() { this.comparer = Comparer<T>.Default; }
        public ComparerReverse(IComparer<T> comparer) { this.comparer = comparer; }
        public ComparerReverse(Comparison<T> comparer) { this.comparer = new ComparerComparison<T>(comparer); }
        #region IComparer<T> メンバ
        public int Compare(T x, T y) { return comparer.Compare(y, x); }
        #endregion
    }
    #endregion

    #region SortedList
    public class SortedList<T> : List<T>
    {
        protected readonly IComparer<T> _Comparer;

        public SortedList()
            : base()
        {
            this._Comparer = Comparer<T>.Default;
        }
        public SortedList(IComparer<T> comparer)
            : base()
        {
            this._Comparer = comparer;
        }
        public SortedList(int capacity)
            : base(capacity)
        {
            this._Comparer = Comparer<T>.Default;
        }
        public SortedList(IComparer<T> comparer, int capacity)
            : base(capacity)
        {
            this._Comparer = comparer;
        }

        public IComparer<T> Comparer
        {
            get { return _Comparer; }
        }

        public bool AddOrDiscard(T item)
        {
            int index = IndexOf(item);
            if (index >= 0) return false;
            else { Insert(~index, item); return true; }
        }
        public bool AddOrOverwrite(T item)
        {
            int index = IndexOf(item);
            if (index >= 0) { this[index] = item; return false; }
            else { Insert(~index, item); return true; }
        }
        public T Pop()
        {
            T item = this[Count - 1];
            RemoveAt(Count - 1);
            return item;
        }

        public new int IndexOf(T item) { return BinarySearch(item); }
        public new void Add(T item)
        {
            int index = BinarySearch(item);
            if (index >= 0) throw new ArgumentException();
            Insert(~index, item);
        }
        public new bool Contains(T item) { return BinarySearch(item) >= 0; }
    }
    #endregion

    #region ListedList
    public class ListedList<T> : IList<T>
    {
        protected const int FixedSize = 1024, FixedHalfSize = FixedSize / 2;
        protected List<List<T>> _Items = new List<List<T>>();
        protected int _Count = 0;

        public ListedList()
        {
        }
        public ListedList(IEnumerable<T> collections)
        {
            foreach (var item in collections) Add(item);
        }

        protected Int2 DecomposeIndex(int index)
        {
            int j = index;
            for (int i = 0; i < _Items.Count; i++)
            {
                if (j < _Items[i].Count) return new Int2(i, j);
                j -= _Items[i].Count;
            }
            ThrowException.ArgumentOutOfRangeException("index");
            return default(Int2);
        }
        protected int ComposeIndex(Int2 indexes)
        {
            int index = indexes.Y;
            for (int i = indexes.X; --i >= 0; )
                index += _Items[i].Count;
            return index;
        }
        protected Int2 _IndexOf(T item)
        {
            for (int i = 0; i < _Items.Count; ++i)
            {
                List<T> items = _Items[i];
                int j = _Items[i].IndexOf(item);
                if (j >= 0) return new Int2(i, j);
            }
            return ~new Int2(0, 0);
        }
        protected Int2 _BinarySearch(T item) { return _BinarySearch(item, Comparer<T>.Default); }
        protected Int2 _BinarySearch(T item, IComparer<T> comparer)
        {
            int i0 = 0, i1 = _Items.Count;
            while (i0 < i1)
            {
                int i = (i0 + i1) / 2;
                int c = comparer.Compare(item, _Items[i][0]);
                if (c == 0) return new Int2(i, 0);
                if (c < 0) i1 = i; else i0 = i + 1;
            }
            if (i0 == 0) return ~new Int2(0, 0);
            int ii = i0 - 1;
            List<T> items = _Items[ii];
            int j0 = 0, j1 = items.Count;
            while (j0 < j1)
            {
                int j = (j0 + j1) / 2;
                int c = comparer.Compare(item, items[j]);
                if (c == 0) return new Int2(ii, j);
                if (c < 0) j1 = j; else j0 = j + 1;
            }
            return ~new Int2(ii, j0);
        }

        protected void _Insert(Int2 indexes, T item)
        {
            if (indexes.X == _Items.Count) _Items.Add(new List<T>(FixedSize));
            if (_Items[indexes.X].Count == FixedSize)
            {
                _Items.Insert(indexes.X + 1, new List<T>(FixedSize));
                _Items[indexes.X + 1].AddRange(_Items[indexes.X].GetRange(FixedHalfSize, FixedHalfSize));
                _Items[indexes.X].RemoveRange(FixedHalfSize, FixedHalfSize);
                if (indexes.Y >= FixedHalfSize) indexes -= new Int2(-1, FixedHalfSize);
            }
            _Items[indexes.X].Insert(indexes.Y, item);
            _Count++;
        }
        protected void _RemoveAt(Int2 indexes)
        {
            _Count--;
            _Items[indexes.X].RemoveAt(indexes.Y);
            if (_Items[indexes.X].Count == 0) _Items.RemoveAt(indexes.X);
        }

        public int RemoveAll(Predicate<T> match)
        {
            int count = 0;
            _Items.RemoveAll(items =>
            {
                count += items.RemoveAll(match);
                return items.Count == 0;
            });
            _Count -= count;
            return count;
        }
        public void CopyTo(T[] array)
        {
            int index = 0;
            foreach (List<T> items in _Items) { items.CopyTo(array, index); index += items.Count; }
        }
        public IEnumerable<T> Reverse()
        {
            for (int i = _Items.Count; --i >= 0; )
            {
                List<T> items = _Items[i];
                for (int j = items.Count; --j >= 0; )
                    yield return items[j];
            }
        }
        public override string ToString()
        {
            return string.Format("Count = {0}", _Count);
        }

        public T Pop()
        {
            _Count--;
            List<T> items = _Items[_Items.Count - 1];
            T item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            if (items.Count == 0) _Items.RemoveAt(_Items.Count - 1);
            return item;
        }

        public void Sort()
        {
            var newlist = new ListedList<T>();
            foreach (var list in _Items) { list.Sort(); list.Reverse(); }
            var head = _Items.Select(list => list.Count - 1).ToList();
            var index = Enumerable.Range(0, _Items.Count).ToList();
            var comparer = new ComparerComparison<int>((y, x) => Comparer<T>.Default.Compare(_Items[x][head[x]], _Items[y][head[y]]));
            index.Sort(comparer);
            while (_Items.Count > 0)
            {
                int most = index.Pop();
                newlist.Add(_Items[most][head[most]]);
                if (--head[most] >= 0)
                {
                    int insert = ~index.BinarySearch(most, comparer);
                    if (insert < 0) insert = ~insert;
                    index.Insert(insert, most);
                }
                else
                {
                    for (int i = index.Count; --i >= 0; )
                        if (index[i] > most) index[i]--;
                    head.RemoveAt(most);
                    _Items.RemoveAt(most);
                }
            }
            _Items = newlist._Items;
        }

        public int LetDistinct()
        {
            var comparer = EqualityComparer<T>.Default;
            int overlap = 0;
            for (int i = _Items.Count; --i >= 0; )
            {
                var list = _Items[i];
                for (int j = list.Count; --j > 0; )
                {
                    if (comparer.Equals(list[j], list[j - 1])) { list.RemoveAt(j); overlap++; }
                }
                if (i > 0)
                {
                    if (comparer.Equals(list[0], _Items[i - 1].Last())) { list.RemoveAt(0); overlap++; }
                }
                if (list.Count == 0) _Items.RemoveAt(i);
            }
            _Count -= overlap;
            return overlap;
        }

        #region IList<T> メンバ
        public int IndexOf(T item)
        {
            Int2 indices = _IndexOf(item);
            if (indices.X >= 0) return ComposeIndex(indices);
            else return ~ComposeIndex(~indices);
        }
        public void Insert(int index, T item) { _Insert(DecomposeIndex(index), item); }
        public void RemoveAt(int index) { _RemoveAt(DecomposeIndex(index)); }
        public T this[int index]
        {
            get { Int2 indices = DecomposeIndex(index); return _Items[indices.X][indices.Y]; }
            set { Int2 indices = DecomposeIndex(index); _Items[indices.X][indices.Y] = value; }
        }
        #endregion
        #region ICollection<T> メンバ
        public void Add(T item)
        {
            if (_Items.Count == 0 || _Items[_Items.Count - 1].Count == FixedSize) _Items.Add(new List<T>(FixedSize));
            _Items[_Items.Count - 1].Add(item);
            _Count++;
        }
        public void Clear()
        {
            _Count = 0;
            _Items.Clear();
        }
        public bool Contains(T item)
        {
            return _IndexOf(item).X >= 0;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            int index = arrayIndex;
            foreach (var items in _Items)
            {
                items.CopyTo(array, index);
                index += items.Count;
            }
        }
        public int Count
        {
            get { return _Count; }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(T item)
        {
            Int2 indices = _IndexOf(item);
            if (indices.X >= 0) { _RemoveAt(indices); return true; }
            else { return false; }
        }
        #endregion
        #region IEnumerable<T> メンバ
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _Items.Count; ++i)
            {
                List<T> items = _Items[i];
                for (int j = 0; j < items.Count; ++j)
                    yield return items[j];
            }
        }
        #endregion
        #region IEnumerable メンバ
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        #endregion
    }
    #endregion

    #region SortedListedList
    public class SortedListedList<T> : ListedList<T>
    {
        protected readonly IComparer<T> _Comparer;

        public SortedListedList()
            : base()
        {
            this._Comparer = Comparer<T>.Default;
        }
        public SortedListedList(IEnumerable<T> collections)
        {
            foreach (var item in collections) Add(item);
        }
        public SortedListedList(IComparer<T> comparer)
            : base()
        {
            this._Comparer = comparer;
        }

        public IComparer<T> Comparer
        {
            get { return _Comparer; }
        }

        protected new Int2 _BinarySearch(T item) { return _BinarySearch(item, _Comparer); }

        public int BinarySearch(T item) { return ComposeIndex(_BinarySearch(item)); }
        public new bool Add(T item)
        {
            Int2 indices = _BinarySearch(item);
            if (indices.X >= 0) { return false; }
            else { _Insert(~indices, item); return true; }
        }
        public new void Insert(int index, T item) { throw new NotImplementedException(); }

        public bool AddOrDiscard(T item)
        {
            Int2 indices = _BinarySearch(item);
            if (indices.X >= 0) return false;
            else { _Insert(~indices, item); return true; }
        }
        public bool AddOrOverwrite(T item)
        {
            Int2 indices = _BinarySearch(item);
            if (indices.X >= 0) { _Items[indices.X][indices.Y] = item; return false; }
            else { _Insert(~indices, item); return true; }
        }
        public T FindOrDefault(T item)
        {
            Int2 index = _BinarySearch(item);
            return index.X >= 0 ? _Items[index.X][index.Y] : default(T);
        }

        public new bool Contains(T item)
        {
            return _BinarySearch(item).X >= 0;
        }
    }
    #endregion

    #region WaveFile
    public static class WaveFile
    {
        static readonly char[] chunkRiff = { 'R', 'I', 'F', 'F' };
        static readonly char[] chunkType = { 'W', 'A', 'V', 'E' };
        static readonly char[] chunkFrmt = { 'f', 'm', 't', ' ' };
        static readonly char[] chunkData = { 'd', 'a', 't', 'a' };

        public static void Save(string filename, double[][] data, int samplesPerSecond, int bitsPerSample = 16)
        {
            try
            {
                using (var writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    int channels = data.Length;
                    int dataLength = data[0].Length;

                    int padding = 1;                           // File padding
                    int bytesPerSample = (bitsPerSample / 8) * channels;  // Bytes per sample.
                    int averageBytesPerSecond = bytesPerSample * samplesPerSecond;
                    int chunkDataLength = (bitsPerSample / 8) * dataLength * channels;
                    int chunkFrmtLength = 16;                    // Format chunk length.
                    int chunkRiffLength = chunkDataLength + 36;  // File length, minus first 8 bytes of RIFF description.

                    writer.Write(chunkRiff);                     //4 bytes
                    writer.Write(chunkRiffLength);               //4 bytes
                    {
                        writer.Write(chunkType);                     //4 bytes

                        writer.Write(chunkFrmt);                     //4 bytes
                        writer.Write(chunkFrmtLength);               //4 bytes
                        {
                            writer.Write((short)padding);                //2 bytes
                            writer.Write((short)channels);               //2 bytes
                            writer.Write(samplesPerSecond);              //4 bytes
                            writer.Write(averageBytesPerSecond);         //4 bytes
                            writer.Write((short)bytesPerSample);         //2 bytes
                            writer.Write((short)bitsPerSample);          //2 bytes
                        }
                        writer.Write(chunkData);                     //4 bytes
                        writer.Write(chunkDataLength);               //4 bytes
                        switch (bitsPerSample)
                        {
                            case 8: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) writer.Write((Byte)Mt.MinMax(data[j][i] * 0x80 + 0x80, 0, 0xff)); break;
                            case 16: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) writer.Write((Int16)Mt.MinMax(data[j][i] * 0x8000, -0x8000, 0x7fff)); break;
                            case 32: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) writer.Write((Int32)Mt.MinMax(data[j][i] * 0x80000000L, -0x80000000L, 0x7fffffffL)); break;
                            default: throw new Exception("WaveFormat.Save: unknown BitsPerSample");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        public static Tuple<double[][], int> Load(string filename)
        {
            try
            {
                using (var reader = new BinaryReader(File.Open(filename, FileMode.Open)))
                {
                    if (Ex.Compare(reader.ReadChars(4), chunkRiff) != 0) throw new Exception("WaveFormat.Load: not RIFF chunk");
                    reader.ReadInt32();  //chunkRiffLength
                    if (Ex.Compare(reader.ReadChars(4), chunkType) != 0) throw new Exception("WaveFormat.Load: not WAVE chunk");

                    int bitsPerSample = 0;
                    int samplesPerSecond = 0;
                    int channels = 0;
                    while (true)
                    {
                        char[] chunkName = reader.ReadChars(4);
                        int chunkLength = reader.ReadInt32();
                        if (Ex.Compare(chunkName, chunkFrmt) == 0)
                        {
                            reader.ReadInt16();  //shPad                 //2 bytes
                            channels = reader.ReadInt16();               //2 bytes
                            samplesPerSecond = reader.ReadInt32();       //4 bytes
                            reader.ReadInt32();  //averageBytesPerSecond //4 bytes
                            reader.ReadInt16();  //shBytesPerSample      //2 bytes
                            bitsPerSample = reader.ReadInt16();          //2 bytes
                            if (chunkLength > 16) reader.ReadBytes(chunkLength - 16);  //unknown data
                        }
                        else if (Ex.Compare(chunkName, chunkData) == 0)
                        {
                            int dataLength = chunkLength / channels / (bitsPerSample / 8);
                            var data = New.Array(channels, i => new double[dataLength]);
                            switch (bitsPerSample)
                            {
                                case 8: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) data[j][i] = (reader.ReadByte() - 0x80) / (double)0x80; break;
                                case 16: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) data[j][i] = reader.ReadInt16() / (double)0x8000; break;
                                case 32: for (int i = 0; i < dataLength; ++i) for (int j = 0; j < channels; ++j) data[j][i] = reader.ReadInt32() / (double)0x80000000L; break;
                                default: throw new Exception("WaveFormat.Load: unknown BitsPerSample");
                            }
                            return Tuple.Create(data, samplesPerSecond);
                        }
                        else
                        {
                            reader.ReadBytes(chunkLength);  //unknown chunk
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return null;
        }
    }
    #endregion

    #region Unsafe array calculations
    public unsafe static partial class Us
    {
        public static double Sum(double* p, int n, Func<double, double> f) { double a = 0; for (int i = n; --i >= 0; ) a += f(p[i]); return a; }
        public static double Sum(double* p, double* q, int n, Func<double, double, double> f) { double a = 0; for (int i = n; --i >= 0; ) a += f(p[i], q[i]); return a; }
        public static double Sum(double* p, int n) { double a = 0; for (int i = n; --i >= 0; ) a += p[i]; return a; }
        public static double SumAbs(double* p, int n) { double a = 0; for (int i = n; --i >= 0; ) a += Math.Abs(p[i]); return a; }
        public static double SumSq(double* p, int n) { double a = 0; for (int i = n; --i >= 0; ) a += p[i] * p[i]; return a; }
        public static double SumPow(double* p, int n, double nu) { double a = 0; for (int i = n; --i >= 0; ) a += Math.Pow(Math.Abs(p[i]), nu); return a; }
        public static double SumMul(double* p, double* q, int n) { double a = 0; for (int i = n; --i >= 0; ) a += p[i] * q[i]; return a; }
        public static double SumAbsSub(double* p, double* q, int n) { double a = 0; for (int i = n; --i >= 0; ) a += Math.Abs(p[i] - q[i]); return a; }
        public static double SumSqSub(double* p, double* q, int n) { double a = 0; for (int i = n; --i >= 0; ) a += Mt.Sq(p[i] - q[i]); return a; }
        public static double SumPowSub(double* p, double* q, int n, double nu) { double a = 0; for (int i = n; --i >= 0; ) a += Math.Pow(Math.Abs(p[i] - q[i]), nu); return a; }
        public static double Min(double* p, int n, Func<double, double> f) { double a = 0; for (int i = 0; i < n; i++) { var b = f(p[i]); if (i == 0 || a > b) a = b; } return a; }
        public static double Max(double* p, int n, Func<double, double> f) { double a = 0; for (int i = 0; i < n; i++) { var b = f(p[i]); if (i == 0 || a < b) a = b; } return a; }
        public static double Min(double* p, double* q, int n, Func<double, double, double> f) { double a = 0; for (int i = 0; i < n; i++) { var b = f(p[i], q[i]); if (i == 0 || a > b) a = b; } return a; }
        public static double Max(double* p, double* q, int n, Func<double, double, double> f) { double a = 0; for (int i = 0; i < n; i++) { var b = f(p[i], q[i]); if (i == 0 || a < b) a = b; } return a; }
        public static double Min(double* p, int n) { double a = 0; for (int i = 0; i < n; i++) { var b = p[i]; if (i == 0 || a > b) a = b; } return a; }
        public static double Max(double* p, int n) { double a = 0; for (int i = 0; i < n; i++) { var b = p[i]; if (i == 0 || a < b) a = b; } return a; }
        public static double MinAbs(double* p, int n) { double a = 0; for (int i = 0; i < n; i++) { var b = Math.Abs(p[i]); if (i == 0 || a > b) a = b; } return a; }
        public static double MaxAbs(double* p, int n) { double a = 0; for (int i = 0; i < n; i++) { var b = Math.Abs(p[i]); if (i == 0 || a < b) a = b; } return a; }
        public static double MinSq(double* p, int n) { double a = 0; for (int i = 0; i < n; i++) { var b = Mt.Sq(p[i]); if (i == 0 || a > b) a = b; } return a; }
        public static double MaxSq(double* p, int n) { double a = 0; for (int i = 0; i < n; i++) { var b = Mt.Sq(p[i]); if (i == 0 || a < b) a = b; } return a; }
        public static double MinPow(double* p, int n, double nu) { double a = 0; for (int i = 0; i < n; i++) { var b = Math.Pow(Math.Abs(p[i]), nu); if (i == 0 || a > b) a = b; } return a; }
        public static double MaxPow(double* p, int n, double nu) { double a = 0; for (int i = 0; i < n; i++) { var b = Math.Pow(Math.Abs(p[i]), nu); if (i == 0 || a < b) a = b; } return a; }

        public static void Let(double* r, double* p, int n) { for (int i = n; --i >= 0; ) r[i] = p[i]; }
        public static void Neg(double* r, double* p, int n) { for (int i = n; --i >= 0; ) r[i] = -p[i]; }
        public static void Add(double* r, double* p, double* q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] + q[i]; }
        public static void Sub(double* r, double* p, double* q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] - q[i]; }
        public static void Mul(double* r, double* p, double* q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] * q[i]; }
        public static void Div(double* r, double* p, double* q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] / q[i]; }
        public static void Mod(double* r, double* p, double* q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] % q[i]; }
        public static void Add(double* r, double* p, double q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] + q; }
        public static void Sub(double* r, double* p, double q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] - q; }
        public static void Mul(double* r, double* p, double q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] * q; }
        public static void Div(double* r, double* p, double q, int n) { Mul(r, p, 1 / q, n); }
        public static void Mod(double* r, double* p, double q, int n) { for (int i = n; --i >= 0; ) r[i] = p[i] % q; }
        public static void Sub(double* r, double p, double* q, int n) { for (int i = n; --i >= 0; ) r[i] = p - q[i]; }
        public static void Div(double* r, double p, double* q, int n) { for (int i = n; --i >= 0; ) r[i] = p / q[i]; }
        public static void Mod(double* r, double p, double* q, int n) { for (int i = n; --i >= 0; ) r[i] = p % q[i]; }

        public static void Operate(double* r, double* p, int n, Func<double, double> f) { for (int i = n; --i >= 0; ) r[i] = f(p[i]); }
        public static void Operate(double* r, double* p, double* q, int n, Func<double, double, double> f) { for (int i = n; --i >= 0; ) r[i] = f(p[i], q[i]); }

        public static void LetAddMul(double* r, double* p, double v, int n)
        {
            int m = n & ~3;
            for (int i = n; --i >= m; )
                r[i] += p[i] * v;
            for (int i = m; (i -= 4) >= 0; )
            {
                *(r + i + 3) += *(p + i + 3) * v;
                *(r + i + 2) += *(p + i + 2) * v;
                *(r + i + 1) += *(p + i + 1) * v;
                *(r + i + 0) += *(p + i + 0) * v;
            }
        }
        public static void LetMulAdd(double* r, double v, double* p, int n)
        {
            for (int i = n; --i >= 0; )
                r[i] = r[i] * v + p[i];
        }
    }
    #endregion

    public static partial class Mt
    {
        #region Miscellaneous functions
        public const double DoubleEpsilon = 2.2250738585072013830902327173324e-308;  // 2^-1022
        public const double DoubleEps = 2.2204460492503130808472633361816e-16;  // 2^-52
        public const double DoubleFpMin = 1.0020841800044863889980540256751e-292;  // 2^-1022 / 2^-52 = 2^-970
        public const double PI2 = Math.PI * 2;
        public const double Ln2 = 0.69314718055994530941723212145818; // Math.Log(2);

        public static bool IsNanOrInfinity(this double value)
        {
            return double.IsInfinity(value) || double.IsNaN(value);
        }
        public static bool IsNanOrInfinity(this double[] array)
        {
            for (int i = array.Length; --i >= 0; )
                if (array[i].IsNanOrInfinity()) return true;
            return false;
        }
        public static bool IsTooSmall(double x, double y) { return x + y == y; }

        public static void Swap<T>(ref T val1, ref T val2) { T z = val1; val1 = val2; val2 = z; }

        public static T MinMax<T>(T value, T valmin, T valmax) where T : IComparable<T> { return value.CompareTo(valmin) < 0 ? valmin : (value.CompareTo(valmax) > 0 ? valmax : value); }
        public static void LetMin<T>(ref T value, T valmin) where T : IComparable<T> { if (value.CompareTo(valmin) > 0) value = valmin; }
        public static void LetMax<T>(ref T value, T valmax) where T : IComparable<T> { if (value.CompareTo(valmax) < 0) value = valmax; }
        public static void LetMinMax<T>(ref T value, T valmin, T valmax) where T : IComparable<T> { if (value.CompareTo(valmin) < 0) value = valmin; else if (value.CompareTo(valmax) > 0) value = valmax; }
        public static bool IfLetMin<T>(ref T value, T valmin) where T : IComparable<T> { if (value.CompareTo(valmin) > 0) { value = valmin; return true; } return false; }
        public static bool IfLetMax<T>(ref T value, T valmax) where T : IComparable<T> { if (value.CompareTo(valmax) < 0) { value = valmax; return true; } return false; }
        public static int MinMaxC(int value, int valmin, int valmax) { return value < valmin ? valmin : value >= valmax ? valmax - 1 : value; }
        public static void LetMinMaxC(ref int value, int valmin, int valmax) { if (value < valmin) value = valmin; else if (value >= valmax) value = valmax - 1; }

        public static Int2 DivRem(int left, int right) { int rem, div = Math.DivRem(left, right, out rem); return new Int2(rem, div); }
        public static int AlignUp(int value, int size) { return (value + (size - 1)) - (value + (size - 1)) % size; }
        public static int AlignDown(int value, int size) { return value - value % size; }
        public static int AlignUpX(int value, int size) { return (value + (size - 1)) & (~(size - 1)); }
        public static int AlignDownX(int value, int size) { return value & (~(size - 1)); }
        public static int RoundOff(double value) { return (int)Math.Floor(value + 0.5); }
        public static double RoundOff(double value, int digit) { double k = Math.Pow(10, digit); return Math.Floor(value * k + 0.5) / k; }
        public static Int2 RoundOff(Double2 value) { return new Int2(Mt.RoundOff(value.X), Mt.RoundOff(value.Y)); }
        public static Int3 RoundOff(Double3 value) { return new Int3(Mt.RoundOff(value.X), Mt.RoundOff(value.Y), Mt.RoundOff(value.Z)); }

        public static int Div_(int left, int right) { return (left - ((left % right) + right) % right) / right; }  //right>0:下方向商   right<0:上方向商
        public static int Mod_(int left, int right) { return ((left % right) + right) % right; }        //right>0:下方向剰余 right<0:上方向剰余        
        //  5/ 3= 1	Div_( 5, 3)= 1
        // -5/ 3=-1	Div_(-5, 3)=-2
        //  5/-3=-1	Div_( 5,-3)=-2
        // -5/-3= 1	Div_(-5,-3)= 1
        //  5% 3= 2	Mod_( 5, 3)= 2
        // -5% 3=-2	Mod_(-5, 3)= 1
        //  5%-3= 2	Mod_( 5,-3)=-1
        // -5%-3=-2	Mod_(-5,-3)=-2

        public static int Sq(int value) { return value * value; }
        public static double Sq(double value) { return value * value; }
        public static int Cube(int value) { return value * value * value; }
        public static double Cube(double value) { return value * value * value; }
        public static int LetAbs(ref int value) { if (value < 0) value *= -1; return value; }
        public static double LetAbs(ref double value) { if (value < 0) value *= -1; return value; }
        public static void LetOrder(ref int val1, ref int val2) { if (val2 < val1) Swap(ref val1, ref val2); }
        public static void LetOrder(ref double val1, ref double val2) { if (val2 < val1) Swap(ref val1, ref val2); }

        public static int DivCeil(int left, int right) { return (left + right - 1) / right; }
        public static long DivCeil(long left, int right) { return (left + right - 1) / right; }
        public static int DivRound(int left, int right) { return (left + (left >= 0 ? right / 2 : -right / 2)) / right; }
        public static long DivRound(long left, int right) { return (left + (left >= 0 ? right / 2 : -right / 2)) / right; }

        public static int Min(int val1, int val2, int val3) { return Math.Min(Math.Min(val1, val2), val3); }
        public static int Max(int val1, int val2, int val3) { return Math.Max(Math.Max(val1, val2), val3); }
        public static int Min(int val1, int val2, int val3, int val4) { return Math.Min(Math.Min(val1, val2), Math.Min(val3, val4)); }
        public static int Max(int val1, int val2, int val3, int val4) { return Math.Max(Math.Max(val1, val2), Math.Max(val3, val4)); }
        public static double Min(double val1, double val2, double val3) { return Math.Min(Math.Min(val1, val2), val3); }
        public static double Max(double val1, double val2, double val3) { return Math.Max(Math.Max(val1, val2), val3); }
        public static double Min(double val1, double val2, double val3, double val4) { return Math.Min(Math.Min(val1, val2), Math.Min(val3, val4)); }
        public static double Max(double val1, double val2, double val3, double val4) { return Math.Max(Math.Max(val1, val2), Math.Max(val3, val4)); }

        public static double SlightlyInferior(double value) { return value - Math.Max(1e-296, Math.Abs(value) * 1e-12); }
        public static double SlightlySuperior(double value) { return value + Math.Max(1e-296, Math.Abs(value) * 1e-12); }
        public static int Log2Floor(int value)
        {
            if (value <= 0) ThrowException.ArgumentOutOfRangeException("value");
            return Log2Floor((uint)value);
        }
        public static int Log2Floor(uint value)
        {
            if (value == 0) ThrowException.ArgumentOutOfRangeException("value");
            int i = 16;
            for (int j = 8; j > 0; j >>= 1)
                if (value < (1u << i)) i -= j; else i += j;
            return value < (1u << i) ? i - 1 : i;
        }
        public static int Log2Ceiling(int value)
        {
            if (value <= 0) ThrowException.ArgumentOutOfRangeException("value");
            return Log2Ceiling((uint)value);
        }
        public static int Log2Ceiling(uint value)
        {
            if (value == 0) ThrowException.ArgumentOutOfRangeException("value");
            if (value == 1) return 0;
            int i = 16;
            for (int j = 8; j > 0; j >>= 1)
                if (value <= (1u << i)) i -= j; else i += j;
            return value > (1u << i) ? i + 1 : i;
        }

        public static double WLog(double weight, double value)
        {
            if (value == 0 && weight == 0) return 0;
            return weight * Math.Log(value);
        }
        public static double Logistic(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }
        public static double Atanh(double value)
        {
            return (Math.Abs(value) >= 1 ? (value > 0 ? double.PositiveInfinity : double.NegativeInfinity) : 0.5 * Math.Log((1 + value) / (1 - value)));
        }
        public static double Atanh_(double value)
        {
            return (Math.Abs(value) >= 1 ? (value > 0 ? 18.7149738751185 : -18.7149738751185) : 0.5 * Math.Log((1 + value) / (1 - value)));
        }
        public static double Tanh(double value)
        {
            if (value == double.PositiveInfinity) return 1;
            if (value == double.NegativeInfinity) return -1;
            double y = Math.Exp(value * 2);
            return (y - 1) / (y + 1);
        }
        public static double Tanh_(double value)
        {
            if (Math.Abs(value) >= 18.7149738751185) return value > 0 ? 1 - 1e-16 : -1 + 1e-16;
            double y = Math.Exp(value + value);
            return (y - 1) / (y + 1);
        }
        public static double Acos_(double value)
        {
            return value <= -1 ? Math.PI : value >= 1 ? 0.0 : Math.Acos(value);
        }
        public static double Asin_(double value)
        {
            return value <= -1 ? -0.5 * Math.PI : value >= 1 ? 0.5 * Math.PI : Math.Asin(value);
        }
        public static double Atan1(double y, double x)
        {
            double theta = Math.Atan2(y, x);
            if (theta <= -0.5 * Math.PI) return theta + Math.PI;
            if (theta > 0.5 * Math.PI) return theta - Math.PI;
            return theta;
        }
        #endregion

        #region Basic calculations
        #region Int32
        public static double Average(int count, Func<int, int> function) { return (double)Sum(count, function) / count; }
        public static int Sum(int count, Func<int, int> function)
        {
            var a = default(int);
            for (int i = 0; i < count; i++) a += function(i);
            return a;
        }
        public static double GeometricalAverage(int count, Func<int, int> function) { return Math.Pow((double)Sum(count, function), 1.0 / count); }
        public static int Product(int count, Func<int, int> function)
        {
            int a = 1;
            for (int i = 0; i < count; i++) checked { a *= function(i); }
            return a;
        }
        #endregion

        #region Double
        public static double Average(int count, Func<int, double> function) { return Sum(count, function) / count; }
        public static double Sum(int count, Func<int, double> function)
        {
            var a = default(double);
            for (int i = 0; i < count; i++) a += function(i);
            return a;
        }
        public static double GeometricalAverage(int count, Func<int, double> function) { return Math.Pow(Sum(count, function), 1.0 / count); }
        public static double Product(int count, Func<int, double> function)
        {
            double a = 1;
            for (int i = 0; i < count; i++) a *= function(i);
            return a;
        }
        #endregion

        #region Double[]
        public static double[] Average(int count, Func<int, double[]> function) { return Sum(count, function).LetDiv(count); }
        public static double[] Sum(int count, Func<int, double[]> function)
        {
            var a = (count == 0) ? default(double[]) : function(0).Pos();
            for (int i = 1; i < count; i++) a.LetAdd(function(i));
            return a;
        }
        #endregion

        #region Double[,]
        public static double[,] Average(int count, Func<int, double[,]> function) { return Sum(count, function).LetDiv(count); }
        public static double[,] Sum(int count, Func<int, double[,]> function)
        {
            var a = (count == 0) ? default(double[,]) : function(0).Pos();
            for (int i = 1; i < count; i++) a.LetAdd(function(i));
            return a;
        }
        #endregion

        #region Variance
        public static double StandardErrorMean<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { return source.Select(element => selector(element)).StandardErrorMean(); }
        public static double StandardErrorMean(this IEnumerable<double> source)
        {
            var r = source._Variance();
            if (r.v0 < 2) ThrowException.InvalidOperationException("count < 2");
            return Math.Sqrt(r.v1 / (r.v0 - 1) / r.v0);
        }
        public static double StandardDeviation<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { return source.Select(element => selector(element)).StandardDeviation(); }
        public static double StandardDeviation(this IEnumerable<double> source) { return Math.Sqrt(source.Variance()); }
        public static double Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { return source.Select(element => selector(element)).Variance(); }
        public static double Variance(this IEnumerable<double> source)
        {
            var r = source._Variance();
            if (r.v0 < 2) ThrowException.InvalidOperationException("count < 2");
            return r.v1 / (r.v0 - 1);
        }
        public static double VariancePopulation<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector) { return source.Select(element => selector(element)).VariancePopulation(); }
        public static double VariancePopulation(this IEnumerable<double> source)
        {
            var r = source._Variance();
            if (r.v0 < 1) ThrowException.InvalidOperationException("count < 1");
            return r.v1 / r.v0;
        }
        static V2<int, double> _Variance(this IEnumerable<double> source)
        {
            int count = 0;
            double moment1 = 0;
            double moment2 = 0;
            foreach (var element in source)
            {
                count++;
                checked
                {
                    moment1 += element;
                    moment2 += element * element;
                }
            }
            return New.V2(count, count == 0 ? 0 : Math.Max(0, moment2 - moment1 * moment1 / count));
        }
        #endregion

        #region Median
        public static double Median(this IList<double> data) { return Quantile(data, 0.5); }
        public static double MedianSorted(this IList<double> data) { return QuantileSorted(data, 0.5); }
        public static double Quantile(this IList<double> data, double quantile)
        {
            var index = data.SortIndex();
            return _QuantileSorted(index.Length, i => data[index[i]], quantile);
        }
        public static double QuantileSorted(this IList<double> data, double quantile)
        {
            return _QuantileSorted(data.Count, i => data[i], quantile);
        }
        static double _QuantileSorted(int count, Func<int, double> function, double quantile)
        {
            if (count == 0) ThrowException.ArgumentException("count");
            if (function == null) ThrowException.ArgumentException("function");
            if (quantile < 0 || quantile > 1) ThrowException.ArgumentException("quantile");
            double r = (count - 1) * quantile;
            int index = (int)r;
            r -= index;
            return r == 0 ? function(index) : function(index) * (1 - r) + function(index + 1) * r;
        }
        #endregion

        #region Cumurative
        public static IEnumerable<int> Cumurative(this IEnumerable<int> source)
        {
            var a = default(int);
            return source.Select(element => checked(a += element));
        }
        public static IEnumerable<int> CumurativeZero(this IEnumerable<int> source)
        {
            var a = default(int);
            return source.Select(element => { var s = a; checked { a += element; } return s; });
        }
        public static IEnumerable<double> Cumurative(this IEnumerable<double> source)
        {
            var a = default(double);
            return source.Select(element => checked(a += element));
        }
        public static IEnumerable<double> CumurativeZero(this IEnumerable<double> source)
        {
            var a = default(double);
            return source.Select(element => { var s = a; checked { a += element; } return s; });
        }
        #endregion
        #endregion

        #region Integer functions
        static List<uint> PrimeNumbersUInt = new List<uint>() { 2, 3 };
        static uint PrimeNumbersUIntExamined = 3;
        static bool FindNextPrimeNumberUInt()
        {
            var v = PrimeNumbersUIntExamined;
            while (v != uint.MaxValue)
            {
                v += 2;
                var limit = (uint)Math.Sqrt(v);
                for (int i = 1; ; i++)
                {
                    var prime = PrimeNumbersUInt[i];
                    if (prime <= limit)
                    {
                        if (v % prime != 0) continue;
                        break;
                    }
                    PrimeNumbersUInt.Add(v);
                    PrimeNumbersUIntExamined = v;
                    return true;
                }
            }
            PrimeNumbersUIntExamined = v;
            return false;
        }

        static Tuple<int, int>[] FactorInteger_(int value)
        {
            lock (PrimeNumbersUInt)
            {
                var list = new List<Int2>();
                uint v = (uint)value;
                if (value == 0) { list.Add(new Int2(0, 1)); v = 1; }
                if (value < 0) { list.Add(new Int2(-1, 1)); v = (uint)-value; }
                for (int i = 0; v != 1; i++)
                {
                    if (PrimeNumbersUInt.Count <= i) FindNextPrimeNumberUInt();
                    var prime = PrimeNumbersUInt[i];
                    int count = 0;
                    while (v % prime == 0) { v /= prime; count++; }
                    if (count > 0) { list.Add(new Int2((int)prime, count)); continue; }
                    if (v < prime * prime) { list.Add(new Int2((int)v, 1)); break; }
                }
                return list.Select(p => Tuple.Create(p.v0, p.v1)).ToArray();
            }
        }
        public static Func<int, Tuple<int, int>[]> FactorInteger = New.Cache<int, Tuple<int, int>[]>(FactorInteger_);
        static int[] FactorIntegerExpanded_(int value)
        {
            return FactorInteger(value).SelectMany(p=> Enumerable.Repeat(p.Item1, p.Item2)).ToArray();
        }
        public static Func<int, int[]> FactorIntegerExpanded = New.Cache<int, int[]>(FactorIntegerExpanded_);

        #endregion

        #region Special functions
        #region gamma functions
        readonly static double[] LogGammaCoefficients = { 57.1562356658629235, -59.5979603554754912, 14.1360979747417471, -0.491913816097620199, .339946499848118887e-4, .465236289270485756e-4, -.983744753048795646e-4, .158088703224912494e-3, -.210264441724104883e-3, .217439618115212643e-3, -.164318106536763890e-3, .844182239838527433e-4, -.261908384015814087e-4, .368991826595316234e-5 };
        public static double LogGamma(double value)
        {
            if (value <= 0) return double.NaN;
            double ser = 0.999999999999997092;
            for (int j = 0; j < LogGammaCoefficients.Length; j++) ser += LogGammaCoefficients[j] / (j + 1 + value);
            double tmp = value + 5.24218750000000000;
            return (value + 0.5) * Math.Log(tmp) - tmp + Math.Log(2.5066282746310005 * ser / value);
        }
        public static double Gamma(double value) { return Math.Exp(LogGamma(value)); }

        // 階乗
        // 22! まではdoubleで正確に表現可能
        // 170! まではdoubleで近似的に表現可能
        static double[] FactorialBuffer;
        static void Factorial_()
        {
            FactorialBuffer = new double[171];
            double f = 1;
            FactorialBuffer[0] = f;
            for (int i = 1; i < FactorialBuffer.Length; i++)
            {
                f *= i;
                FactorialBuffer[i] = f;
            }
        }
        public static double Factorial(int value)
        {
            if (FactorialBuffer == null) Factorial_();
            if (value < 0 || value >= FactorialBuffer.Length) ThrowException.ArgumentOutOfRangeException("value");
            return FactorialBuffer[value];
        }

        static double[] LogFactorialBuffer;
        static void LogFactorial_()
        {
            LogFactorialBuffer = new double[2000];
            double f = 0;
            for (int i = 2; i < LogFactorialBuffer.Length; i++)
            {
                f += Math.Log(i);
                LogFactorialBuffer[i] = i <= 22 ? Math.Log(Factorial(i)) : f;
            }
        }
        public static double LogFactorial(int value)
        {
            if (value < 0) ThrowException.ArgumentOutOfRangeException("value");
            if (LogFactorialBuffer == null) LogFactorial_();
            if (value < LogFactorialBuffer.Length) return LogFactorialBuffer[value];
            return Mt.LogGamma(value + 1.0);
        }

        // same to Pochhammer function
        public static double RisingFactorial(int value, int count)
        {
            if (count == 0) return 1;
            if (count == 1) return value;
            int s, v0, v1;
            if (value > 0)
            {
                s = 1;
                v0 = value + count - 1;
                v1 = value - 1;
            }
            else
            {
                s = 1 - 2 * (count & 1);
                v0 = -value;
                v1 = -value - count;
            }
            if (v0 < 0) return double.NaN;
            if (v1 < 0) return 0;
            return s * (v1 < 171 && v0 < 171 ? Factorial(v0) / Factorial(v1) : Math.Exp(LogFactorial(v0) - LogFactorial(v1)));
        }
        // FactorialPowerと同じ
        public static double FallingFactorial(int value, int count)
        {
            if (count == 0) return 1;
            if (count == 1) return value;
            int s, v0, v1;
            if (value >= count)
            {
                s = 1;
                v0 = value;
                v1 = value - count;
            }
            else
            {
                s = 1 - 2 * (count & 1);
                v0 = -value + count - 1;
                v1 = -value - 1;
            }
            if (v0 < 0) return double.NaN;
            if (v1 < 0) return 0;
            return s * (v1 < 171 && v0 < 171 ? Factorial(v0) / Factorial(v1) : Math.Exp(LogFactorial(v0) - LogFactorial(v1)));
        }

        public static double LogBinomial(int left, int right)
        {
            if (left < 0) ThrowException.ArgumentOutOfRangeException("left");
            int val2 = left - right;
            if (right < 0 || val2 < 0) return double.NegativeInfinity;
            if (right == 0 || val2 == 0) return 0.0;
            return Mt.LogFactorial(left) - (Mt.LogFactorial(right) + Mt.LogFactorial(val2));
        }
        public static double Binomial(int left, int right)
        {
            if (left < 0) ThrowException.ArgumentOutOfRangeException("left");
            int val2 = left - right;
            if (right < 0 || val2 < 0) return 0.0;
            if (right == 0 || val2 == 0) return 1.0;
            return Math.Round(Math.Exp(Mt.LogFactorial(left) - (Mt.LogFactorial(right) + Mt.LogFactorial(val2))), MidpointRounding.AwayFromZero);
        }

        public static double LogMultinomial(IEnumerable<int> source)
        {
            int total = 0;
            double sum = 0;
            foreach (int element in source)
            {
                if (element < 0) ThrowException.ArgumentOutOfRangeException("element");
                total += element;
                sum += Mt.LogFactorial(element);
            }
            return Mt.LogFactorial(total) - sum;
        }
        public static double Multinomial(IEnumerable<int> table)
        {
            return Math.Round(Math.Exp(LogMultinomial(table)), MidpointRounding.AwayFromZero);
        }

        // beta function
        public static double LogBeta(double value0, double value1)
        {
            return Mt.LogGamma(value0) + Mt.LogGamma(value1) - Mt.LogGamma(value0 + value1);
        }
        public static double Beta(double value0, double value1)
        {
            return Math.Exp(LogBeta(value0, value1));
        }
        #endregion

        #region incomplete gamma functions
        readonly static double[] Gauleg18y = new double[] {
            0.0021695375159141994, 0.011413521097787704, 0.027972308950302116, 0.051727015600492421,
            0.082502225484340941, 0.12007019910960293, 0.16415283300752470, 0.21442376986779355,
            0.27051082840644336, 0.33199876341447887, 0.39843234186401943, 0.46931971407375483,
            0.54413605556657973, 0.62232745288031077, 0.70331500465597174, 0.78649910768313447,
            0.87126389619061517, 0.95698180152629142
        };
        readonly static double[] Gauleg18w = new double[] {
            0.0055657196642445571, 0.012915947284065419, 0.020181515297735382, 0.027298621498568734,
            0.034213810770299537, 0.040875750923643261, 0.047235083490265582, 0.053244713977759692,
            0.058860144245324798, 0.064039797355015485, 0.068745323835736408, 0.072941885005653087,
            0.076598410645870640, 0.079687828912071670, 0.082187266704339706, 0.084078218979661945,
            0.085346685739338721, 0.085983275670394821
        };
        static double gammpapprox(double value, double gamma, bool psig)
        {
            double a1 = gamma - 1, lna1 = Math.Log(a1), sqrta1 = Math.Sqrt(a1);
            double xu = (value > a1) ?
                Math.Max(a1 + 11.5 * sqrta1, value + 6 * sqrta1) :
                Math.Max(0, Math.Min(a1 - 7.5 * sqrta1, value - 5 * sqrta1));
            double sum = 0;
            for (int j = 0; j < Gauleg18y.Length; j++)
            {
                double t = value + (xu - value) * Gauleg18y[j];
                sum += Gauleg18w[j] * Math.Exp(a1 - t + a1 * (Math.Log(t) - lna1));
            }
            double ans = sum * (xu - value) * Math.Exp(a1 * (lna1 - 1) - Mt.LogGamma(gamma));
            return psig ? (ans > 0 ? 1 : 0) - ans : (ans >= 0 ? 0 : 1) + ans;
        }
        static double gser(double value, double gamma)
        {
            double sum = 1 / gamma;
            double del = sum;
            for (int i = 1; ; i++)
            {
                del *= value / (gamma + i);
                sum += del;
                if (Math.Abs(del) < Math.Abs(sum) * Mt.DoubleEps) break;
            }
            return sum * Math.Exp(gamma * Math.Log(value) - value - Mt.LogGamma(gamma));
        }
        static double gcf(double value, double gamma)
        {
            double c = double.PositiveInfinity;
            double d = 1 / (value - gamma + 1);
            double h = d;
            for (int n = 1; ; n++)
            {
                double an = n * (gamma - n);
                double bn = (value - gamma + 1) + n * 2;
                c = bn + an / c; if (Math.Abs(c) < DoubleFpMin) c = DoubleFpMin;
                d = bn + an * d; if (Math.Abs(d) < DoubleFpMin) d = DoubleFpMin;
                if (c == d) break;
                h *= c / d;
                d = 1.0 / d;
            }
            return h * Math.Exp(gamma * Math.Log(value) - value - Mt.LogGamma(gamma));
        }
        // integrate [0, value] t^(gamma-1) e^-t dt / Gamma(gamma)
        public static double IncompleteGammaLower(double value, double gamma)
        {
            if (gamma <= 0) return double.NaN;
            if (value == double.PositiveInfinity) return 1;
            if (value <= 0) return 0;
            if (gamma >= 100) return gammpapprox(value, gamma, true);
            return (value < gamma + 1) ? gser(value, gamma) : 1 - gcf(value, gamma);
        }
        // integrate [value, inf) t^(gamma-1) e^-t dt / Gamma(gamma)
        public static double IncompleteGammaUpper(double value, double gamma)
        {
            if (gamma <= 0) return double.NaN;
            if (value == double.PositiveInfinity) return 0;
            if (value <= 0) return 1;
            if (gamma >= 100.0) return gammpapprox(value, gamma, false);
            return (value < gamma + 1) ? 1 - gser(value, gamma) : gcf(value, gamma);
        }
        static double InverseIncompleteGamma(double value, double gamma)
        {
            if (gamma <= 0) return double.NaN;
            if (value >= 1) return Math.Max(100, gamma + 100 * Math.Sqrt(gamma));
            if (value <= 0) return 0;

            const double eps = 1e-8;
            double x, t, lna1 = 0, afac = 0, a1 = gamma - 1;
            double gln = Mt.LogGamma(gamma);
            if (gamma > 1)
            {
                lna1 = Math.Log(a1);
                afac = Math.Exp(a1 * (lna1 - 1) - gln);
                t = Math.Sqrt(-2 * Math.Log((value < 0.5) ? value : 1 - value));
                x = (2.30753 + t * 0.27061) / (1 + t * (0.99229 + t * 0.04481)) - t;
                if (value < 0.5) x = -x;
                x = Math.Max(1.0e-3, gamma * Math.Pow(1 - 1 / (9 * gamma) - x / (3 * Math.Sqrt(gamma)), 3));
            }
            else
            {
                t = 1 - gamma * (0.253 + gamma * 0.12);
                x = (value < t) ?
                    Math.Pow(value / t, 1 / gamma) :
                    1 - Math.Log(1 - (value - t) / (1 - t));
            }
            for (int j = 0; j < 12; j++)
            {
                if (x <= 0) return 0;
                double err = Mt.IncompleteGammaLower(x, gamma) - value;
                t = (gamma > 1) ?
                    afac * Math.Exp(-(x - a1) + a1 * (Math.Log(x) - lna1)) :
                    Math.Exp(-x + a1 * Math.Log(x) - gln);
                double u = err / t;
                x -= (t = u / (1 - 0.5 * Math.Min(1, u * (a1 / x - 1))));
                if (x <= 0) x = 0.5 * (x + t);
                if (Math.Abs(t) < eps * x) break;
            }
            return x;
        }
        public static double Erf(double value)
        {
            return Mt.IncompleteGammaLower(value * value, 0.5) * Math.Sign(value);
        }

        // normal distribution
        const double Sqrt2PIinv = 0.39894228040143267793994605993438;  // 1 / Math.Sqrt(2 * Math.PI);
        const double Sqrt2inv = 0.70710678118654752440084436210485;  // 1 / Math.Sqrt(2);
        static public double StandardNormalDistribution(double value)
        {
            return Math.Exp(value * value * -0.5) * Sqrt2PIinv;
        }
        static public double StandardNormalDistributionLower(double value)
        {
            return (1 + Mt.Erf(value * Sqrt2inv)) / 2;
        }
        static public double StandardNormalDistributionUpper(double value)
        {
            return (1 - Mt.Erf(value * Sqrt2inv)) / 2;
        }
        static public double NormalDistribution(double value, double mean, double variance)
        {
            if (variance <= 0) ThrowException.ArgumentException("variance");
            return Math.Exp(Mt.Sq(value - mean) / variance * -0.5) / Math.Sqrt(Mt.PI2 * variance);
        }

        // chi-square distribution
        public static double ChiSquareDistribution(double value, double freedom)
        {
            if (freedom < 0) ThrowException.ArgumentException("freedom");
            if (value < 0) return 0;
            if (value == 0)
            {
                if (freedom < 2) return double.PositiveInfinity;
                if (freedom == 2) return 0.5;
                return 0;
            }
            double f = freedom / 2;
            return Math.Exp(-0.5 * value + (f - 1) * Math.Log(value) - f * Mt.Ln2 - Mt.LogGamma(f));
        }
        public static double ChiSquareDistributionLower(double value, double freedom)
        {
            return Mt.IncompleteGammaLower(value / 2, freedom / 2);
        }
        public static double ChiSquareDistributionUpper(double value, double freedom)
        {
            return Mt.IncompleteGammaUpper(value / 2, freedom / 2);
        }
        #endregion

        #region incomplete beta functions
        static double AbsMaxFPMIN(double value)
        {
            return Math.Abs(value) < DoubleFpMin ? DoubleFpMin : value;
        }
        static double betacf(double a, double b, double x)
        {
            double qab = a + b;
            double qap = a + 1;
            double qam = a - 1;
            double c = 1;
            double d = 1 / AbsMaxFPMIN(1 - qab * x / qap);
            double h = d;
            for (int m = 1; m < 10000; m++)
            {
                int m2 = 2 * m;
                double aa = m * (b - m) * x / ((qam + m2) * (a + m2));
                d = 1 / AbsMaxFPMIN(1 + aa * d);
                c = AbsMaxFPMIN(1 + aa / c);
                h *= d * c;
                aa = -(a + m) * (qab + m) * x / ((a + m2) * (qap + m2));
                d = 1 / AbsMaxFPMIN(1 + aa * d);
                c = AbsMaxFPMIN(1 + aa / c);
                h *= d * c;
                if (Math.Abs(d * c - 1) <= DoubleEps) break;
            }
            return h;
        }

        static double betaiapprox(double a, double b, double x)
        {
            double xu;
            double a1 = a - 1;
            double b1 = b - 1;
            double mu = a / (a + b);
            double lnmu = Math.Log(mu);
            double lnmuc = Math.Log(1 - mu);
            double t = Math.Sqrt(a * b / (Mt.Sq(a + b) * (a + b + 1)));
            if (x > a / (a + b))
            {
                if (x >= 1) return 1;
                xu = Math.Min(1.0, Math.Max(mu + 10 * t, x + 5 * t));
            }
            else
            {
                if (x <= 0) return 0;
                xu = Math.Max(0.0, Math.Min(mu - 10 * t, x - 5 * t));
            }
            double sum = 0;
            for (int j = 0; j < 18; j++)
            {
                t = x + (xu - x) * Gauleg18y[j];
                sum += Gauleg18w[j] * Math.Exp(a1 * (Math.Log(t) - lnmu) + b1 * (Math.Log(1 - t) - lnmuc));
            }
            double ans = sum * (xu - x) * Math.Exp(a1 * lnmu - Mt.LogGamma(a) + b1 * lnmuc - Mt.LogGamma(b) + Mt.LogGamma(a + b));
            return ans > 0.0 ? 1.0 - ans : -ans;
        }
        public static double IncompleteBeta(double value, double param1, double param2)
        {
            if (value < 0 || value > 1) ThrowException.ArgumentOutOfRangeException("value");
            if (param1 <= 0) ThrowException.ArgumentOutOfRangeException("param1");
            if (param2 <= 0) ThrowException.ArgumentOutOfRangeException("param2");
            if (value == 0 || value == 1) return value;
            if (param1 > 3000 && param2 > 3000) return betaiapprox(param1, param2, value);
            double bt = Math.Exp(
                Mt.LogGamma(param1 + param2) - Mt.LogGamma(param1) - Mt.LogGamma(param2)
                + param1 * Math.Log(value) + param2 * Math.Log(1 - value));
            return (value < (param1 + 1) / (param1 + param2 + 2)) ?
                bt * betacf(param1, param2, value) / param1 :
                1 - bt * betacf(param2, param1, 1 - value) / param2;
        }

        // Student t distribution
        static public double StudentTDistribution(double value, double freedom)
        {
            double n2 = (freedom + 1) / 2;
            return Mt.Gamma(n2) / (Mt.Gamma(freedom / 2) * Math.Sqrt(Math.PI * freedom)) * Math.Pow(1 + value * value / freedom, -n2);
        }
        // ∫∞~-t and t~∞
        static public double StudentTDistributionBilateral(double value, double freedom)
        {
            return Mt.IncompleteBeta(freedom / (freedom + value * value), freedom / 2, 0.5);
        }
        // ∫t~∞
        static public double StudentTDistributionUpper(double value, double freedom)
        {
            double a = Mt.StudentTDistributionBilateral(value, freedom) / 2;
            return value < 0.0 ? 1.0 - a : a;
        }
        // ∫-∞~t
        static public double StudentTDistributionLower(double value, double freedom)
        {
            double a = Mt.StudentTDistributionBilateral(value, freedom) / 2;
            return value < 0 ? a : 1 - a;
        }

        // F distribution
        // ∫f~∞, Q(F|v1,v2) <0.01 で有意
        static public double FDistributionUpper(double value, double freedom1, double freedom2)
        {
            return Mt.IncompleteBeta(freedom2 / (freedom2 + freedom1 * value), freedom2 / 2, freedom1 / 2);
        }
        // ∫-∞~f = ∫0~f
        static public double FDistributionLower(double value, double freedom1, double freedom2)
        {
            return 1.0 - Mt.FDistributionUpper(value, freedom1, freedom2);
        }
        #endregion

        #region elliptic functions
        public static double EllipticTheta3(double phase, double radius)
        {
            if (radius < 0.0 || radius >= 1.0) ThrowException.ArgumentOutOfRangeException("radius");
            if (radius == 0.0) return 1.0;
            int digit = (int)Math.Ceiling(Math.Sqrt(Math.Log(DoubleEps) / Math.Log(radius)));
            double a = 0.0;
            for (int d = 1; d <= digit; d++)
                a += Math.Cos(2.0 * d * phase) * Math.Pow(radius, d * d);
            return 1.0 + 2.0 * a;
        }
        #endregion
        #endregion

        #region Linear functions
        #region fixed size
        public static double Norm1(this Double2 array) { return Math.Abs(array.X) + Math.Abs(array.Y); }
        public static double Norm1(this Double3 array) { return Math.Abs(array.X) + Math.Abs(array.Y) + Math.Abs(array.Z); }
        public static double SqNorm2(this Double2 array) { return array.X * array.X + array.Y * array.Y; }
        public static double SqNorm2(this Double3 array) { return array.X * array.X + array.Y * array.Y + array.Z * array.Z; }
        public static double Norm2(this Double2 array) { return Math.Sqrt(array.SqNorm2()); }
        public static double Norm2(this Double3 array) { return Math.Sqrt(array.SqNorm2()); }
        public static double Inner(Double2 array0, Double2 array1) { return array0.X * array1.X + array0.Y * array1.Y; }
        public static double Inner(Double3 array0, Double3 array1) { return array0.X * array1.X + array0.Y * array1.Y + array0.Z * array1.Z; }
        public static Double2 NormalizeNorm2(Double2 array) { return array / Norm2(array); }
        public static Double3 NormalizeNorm2(Double3 array) { return array / Norm2(array); }
        public static Double3 Outer(Double3 array0, Double3 array1)
        {
            return new Double3(
                array0.Y * array1.Z - array0.Z * array1.Y,
                array0.Z * array1.X - array0.X * array1.Z,
                array0.X * array1.Y - array0.Y * array1.X
            );
        }
        #endregion

        #region double[]
        static void SizeCheck0<TSource0, TSource1>(TSource0[] array0, TSource1[] array1)
        {
            if (array0.Length != array1.Length) ThrowException.SizeMismatch();
        }
        static TSource[] Zero<TSource>(this TSource[] array) { return new TSource[array.Length]; }
        static TSource0[] Zero<TSource0, TSource1>(TSource0[] array0, TSource1[] array1) { SizeCheck0(array0, array1); return Zero(array0); }

        public static double Sum(this double[] array, Func<double, double> selector) { unsafe { fixed (double* p = array) return Us.Sum(p, array.Length, selector); } }
        public static double Sum(this double[] array) { unsafe { fixed (double* p = array) return Us.Sum(p, array.Length); } }
        public static double Average(this double[] array, Func<double, double> selector) { return Sum(array, selector) / array.Length; }
        public static double Average(this double[] array) { return Sum(array) / array.Length; }
        public static double Norm(this double[] array, double nu) { unsafe { fixed (double* p = array) return Math.Pow(Us.SumPow(p, array.Length, nu), 1 / nu); } }
        public static double Norm1(this double[] array) { unsafe { fixed (double* p = array) return Us.SumAbs(p, array.Length); } }
        public static double Norm2(this double[] array) { return Math.Sqrt(SqNorm2(array)); }
        public static double SqNorm2(this double[] array) { unsafe { fixed (double* p = array) return Us.SumSq(p, array.Length); } }
        public static double Min(this double[] array) { unsafe { fixed (double* p = array) return Us.Min(p, array.Length); } }
        public static double Max(this double[] array) { unsafe { fixed (double* p = array) return Us.Max(p, array.Length); } }
        public static double MaxAbs(this double[] array) { unsafe { fixed (double* p = array) return Us.MaxAbs(p, array.Length); } }
        public static double MaxSq(this double[] array) { unsafe { fixed (double* p = array) return Us.MaxSq(p, array.Length); } }
        public static double Inner(double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) return Us.SumMul(p, q, array0.Length); } }
        public static double NormSub(double[] array0, double[] array1, double nu) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) return Us.SumPowSub(p, q, array0.Length, nu); } }
        public static double Norm1Sub(double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) return Us.SumAbsSub(p, q, array0.Length); } }
        public static double Norm2Sub(double[] array0, double[] array1) { return Math.Sqrt(SqNorm2Sub(array0, array1)); }
        public static double SqNorm2Sub(double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) return Us.SumSqSub(p, q, array0.Length); } }

        public static double[] LetNeg(this double[] array) { unsafe { fixed (double* p = array) Us.Neg(p, p, array.Length); } return array; }
        public static double[] Let(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Let(p, q, array0.Length); } return array0; }
        public static double[] LetAdd(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Add(p, p, q, array0.Length); } return array0; }
        public static double[] LetSub(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Sub(p, p, q, array0.Length); } return array0; }
        public static double[] LetMul(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Mul(p, p, q, array0.Length); } return array0; }
        public static double[] LetDiv(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Div(p, p, q, array0.Length); } return array0; }
        public static double[] LetMod(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Mod(p, p, q, array0.Length); } return array0; }
        public static double[] LetSubr(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Sub(p, q, p, array0.Length); } return array0; }
        public static double[] LetDivr(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Div(p, q, p, array0.Length); } return array0; }
        public static double[] LetModr(this double[] array0, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Mod(p, q, p, array0.Length); } return array0; }
        public static double[] LetAdd(this double[] array, double scalar) { if (scalar != 0) unsafe { fixed (double* p = array) Us.Add(p, p, scalar, array.Length); } return array; }
        public static double[] LetSub(this double[] array, double scalar) { if (scalar != 0) unsafe { fixed (double* p = array) Us.Sub(p, p, scalar, array.Length); } return array; }
        public static double[] LetMul(this double[] array, double scalar) { if (scalar != 1) unsafe { fixed (double* p = array) Us.Mul(p, p, scalar, array.Length); } return array; }
        public static double[] LetDiv(this double[] array, double scalar) { if (scalar != 1) unsafe { fixed (double* p = array) Us.Div(p, p, scalar, array.Length); } return array; }
        public static double[] LetMod(this double[] array, double scalar) { unsafe { fixed (double* p = array) Us.Mod(p, p, scalar, array.Length); } return array; }
        public static double[] LetSubr(this double[] array, double scalar) { unsafe { fixed (double* p = array) Us.Sub(p, scalar, p, array.Length); } return array; }
        public static double[] LetDivr(this double[] array, double scalar) { unsafe { fixed (double* p = array) Us.Div(p, scalar, p, array.Length); } return array; }
        public static double[] LetModr(this double[] array, double scalar) { unsafe { fixed (double* p = array) Us.Mod(p, scalar, p, array.Length); } return array; }
        public static double[] Pos(this double[] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Let(r, p, result.Length); } return result; }
        public static double[] Neg(this double[] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Neg(r, p, result.Length); } return result; }
        public static double[] Add(double[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Add(r, p, q, result.Length); } return result; }
        public static double[] Sub(double[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Sub(r, p, q, result.Length); } return result; }
        public static double[] Mul(double[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Mul(r, p, q, result.Length); } return result; }
        public static double[] Div(double[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Div(r, p, q, result.Length); } return result; }
        public static double[] Mod(double[] array0, double[] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Mod(r, p, q, result.Length); } return result; }
        public static double[] Add(double[] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static double[] Sub(double[] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Sub(r, p, scalar, result.Length); } return result; }
        public static double[] Mul(double[] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static double[] Div(double[] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Div(r, p, scalar, result.Length); } return result; }
        public static double[] Mod(double[] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Mod(r, p, scalar, result.Length); } return result; }
        public static double[] Add(double scalar, double[] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static double[] Sub(double scalar, double[] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Sub(r, scalar, p, result.Length); } return result; }
        public static double[] Mul(double scalar, double[] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static double[] Div(double scalar, double[] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Div(r, scalar, p, result.Length); } return result; }
        public static double[] Mod(double scalar, double[] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Mod(r, scalar, p, result.Length); } return result; }

        public static double[] LetAddMul(this double[] array0, double[] array1, double scalar) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.LetAddMul(p, q, scalar, array0.Length); } return array0; }
        public static double[] LetMulAdd(this double[] array0, double scalar, double[] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.LetMulAdd(p, scalar, q, array0.Length); } return array0; }
        public static double[] AddMul(double[] array0, double[] array1, double scalar) { SizeCheck0(array0, array1); var result = array0.CloneX(); unsafe { fixed (double* r = result, p = array1) Us.LetAddMul(r, p, scalar, result.Length); } return result; }
        public static double[] LetNormalizeSum(this double[] data) { data.LetDiv(data.Sum()); return data; }
        public static double[] LetNormalizeNorm1(this double[] data) { data.LetDiv(data.Norm1()); return data; }
        public static double[] LetNormalizeNorm2(this double[] data) { data.LetDiv(data.Norm2()); return data; }
        #endregion

        #region double[,]
        static void SizeCheck0<TSource0, TSource1>(TSource0[,] array0, TSource1[,] array1)
        {
            if (array0.GetLength(0) != array1.GetLength(0) || array0.GetLength(1) != array1.GetLength(1)) ThrowException.SizeMismatch();
        }
        static TSource[,] Zero<TSource>(this TSource[,] array) { return new TSource[array.GetLength(0), array.GetLength(1)]; }
        static TSource0[,] Zero<TSource0, TSource1>(TSource0[,] array0, TSource1[,] array1) { SizeCheck0(array0, array1); return Zero(array0); }

        public static double Sum(this double[,] array, Func<double, double> selector) { unsafe { fixed (double* p = array) return Us.Sum(p, array.Length, selector); } }
        public static double Sum(this double[,] array) { unsafe { fixed (double* p = array) return Us.Sum(p, array.Length); } }
        public static double Average(this double[,] array, Func<double, double> selector) { return Sum(array, selector) / array.Length; }
        public static double Average(this double[,] array) { return Sum(array) / array.Length; }
        public static double Norm(this double[,] array, double nu) { unsafe { fixed (double* p = array) return Math.Pow(Us.SumPow(p, array.Length, nu), 1 / nu); } }
        public static double Norm1(this double[,] array) { unsafe { fixed (double* p = array) return Us.SumAbs(p, array.Length); } }
        public static double Norm2(this double[,] array) { return Math.Sqrt(SqNorm2(array)); }
        public static double SqNorm2(this double[,] array) { unsafe { fixed (double* p = array) return Us.SumSq(p, array.Length); } }
        public static double Min(this double[,] array) { unsafe { fixed (double* p = array) return Us.Min(p, array.Length); } }
        public static double Max(this double[,] array) { unsafe { fixed (double* p = array) return Us.Max(p, array.Length); } }
        public static double MaxAbs(this double[,] array) { unsafe { fixed (double* p = array) return Us.MaxAbs(p, array.Length); } }
        public static double MaxSq(this double[,] array) { unsafe { fixed (double* p = array) return Us.MaxSq(p, array.Length); } }
        public static double Inner(double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) return Us.SumMul(p, q, array0.Length); } }
        public static double NormSub(double[,] array0, double[,] array1, double nu) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) return Us.SumPowSub(p, q, array0.Length, nu); } }
        public static double Norm1Sub(double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) return Us.SumAbsSub(p, q, array0.Length); } }
        public static double Norm2Sub(double[,] array0, double[,] array1) { return Math.Sqrt(SqNorm2Sub(array0, array1)); }
        public static double SqNorm2Sub(double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) return Us.SumSqSub(p, q, array0.Length); } }

        public static double[,] LetNeg(this double[,] array) { unsafe { fixed (double* p = array) Us.Neg(p, p, array.Length); } return array; }
        public static double[,] Let(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Let(p, q, array0.Length); } return array0; }
        public static double[,] LetAdd(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Add(p, p, q, array0.Length); } return array0; }
        public static double[,] LetSub(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Sub(p, p, q, array0.Length); } return array0; }
        public static double[,] LetMul(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Mul(p, p, q, array0.Length); } return array0; }
        public static double[,] LetDiv(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Div(p, p, q, array0.Length); } return array0; }
        public static double[,] LetMod(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Mod(p, p, q, array0.Length); } return array0; }
        public static double[,] LetSubr(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Sub(p, q, p, array0.Length); } return array0; }
        public static double[,] LetDivr(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Div(p, q, p, array0.Length); } return array0; }
        public static double[,] LetModr(this double[,] array0, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.Mod(p, q, p, array0.Length); } return array0; }
        public static double[,] LetAdd(this double[,] array, double scalar) { if (scalar != 0) unsafe { fixed (double* p = array) Us.Add(p, p, scalar, array.Length); } return array; }
        public static double[,] LetSub(this double[,] array, double scalar) { if (scalar != 0) unsafe { fixed (double* p = array) Us.Sub(p, p, scalar, array.Length); } return array; }
        public static double[,] LetMul(this double[,] array, double scalar) { if (scalar != 1) unsafe { fixed (double* p = array) Us.Mul(p, p, scalar, array.Length); } return array; }
        public static double[,] LetDiv(this double[,] array, double scalar) { if (scalar != 1) unsafe { fixed (double* p = array) Us.Div(p, p, scalar, array.Length); } return array; }
        public static double[,] LetMod(this double[,] array, double scalar) { unsafe { fixed (double* p = array) Us.Mod(p, p, scalar, array.Length); } return array; }
        public static double[,] LetSubr(this double[,] array, double scalar) { unsafe { fixed (double* p = array) Us.Sub(p, scalar, p, array.Length); } return array; }
        public static double[,] LetDivr(this double[,] array, double scalar) { unsafe { fixed (double* p = array) Us.Div(p, scalar, p, array.Length); } return array; }
        public static double[,] LetModr(this double[,] array, double scalar) { unsafe { fixed (double* p = array) Us.Mod(p, scalar, p, array.Length); } return array; }
        public static double[,] Pos(this double[,] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Let(r, p, result.Length); } return result; }
        public static double[,] Neg(this double[,] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Neg(r, p, result.Length); } return result; }
        public static double[,] Add(double[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Add(r, p, q, result.Length); } return result; }
        public static double[,] Sub(double[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Sub(r, p, q, result.Length); } return result; }
        public static double[,] Mul(double[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Mul(r, p, q, result.Length); } return result; }
        public static double[,] Div(double[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Div(r, p, q, result.Length); } return result; }
        public static double[,] Mod(double[,] array0, double[,] array1) { var result = Zero(array0, array1); unsafe { fixed (double* r = result, p = array0, q = array1) Us.Mod(r, p, q, result.Length); } return result; }
        public static double[,] Add(double[,] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static double[,] Sub(double[,] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Sub(r, p, scalar, result.Length); } return result; }
        public static double[,] Mul(double[,] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static double[,] Div(double[,] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Div(r, p, scalar, result.Length); } return result; }
        public static double[,] Mod(double[,] array, double scalar) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Mod(r, p, scalar, result.Length); } return result; }
        public static double[,] Add(double scalar, double[,] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Add(r, p, scalar, result.Length); } return result; }
        public static double[,] Sub(double scalar, double[,] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Sub(r, scalar, p, result.Length); } return result; }
        public static double[,] Mul(double scalar, double[,] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Mul(r, p, scalar, result.Length); } return result; }
        public static double[,] Div(double scalar, double[,] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Div(r, scalar, p, result.Length); } return result; }
        public static double[,] Mod(double scalar, double[,] array) { var result = Zero(array); unsafe { fixed (double* r = result, p = array) Us.Mod(r, scalar, p, result.Length); } return result; }

        public static double[,] LetAddMul(this double[,] array0, double[,] array1, double scalar) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.LetAddMul(p, q, scalar, array0.Length); } return array0; }
        public static double[,] LetMulAdd(this double[,] array0, double scalar, double[,] array1) { SizeCheck0(array0, array1); unsafe { fixed (double* p = array0, q = array1) Us.LetMulAdd(p, scalar, q, array0.Length); } return array0; }
        public static double[,] AddMul(double[,] array0, double[,] array1, double scalar) { SizeCheck0(array0, array1); var result = array0.CloneX(); unsafe { fixed (double* r = result, p = array1) Us.LetAddMul(r, p, scalar, result.Length); } return result; }
        #endregion

        #region double matrix
        public static double Inner(double[] vector, double[,] matrix) { return Inner(vector, Multiply(matrix, vector)); }
        public static double Inner(double[] vector0, double[,] matrix, double[] vector1) { return Inner(vector0, Multiply(matrix, vector1)); }
        public static double[,] Multiply(double[] vector) { return Multiply(vector, vector); }
        public unsafe static double[,] Multiply(double[] vector0, double[] vector1)
        {
            int n = vector1.Length;
            var result = new double[vector0.Length, n];
            fixed (double* r = result, p = vector0, q = vector1)
                for (int i = vector0.Length; --i >= 0; )
                    Us.Mul(&r[n * i], q, p[i], n);
            return result;
        }
        public unsafe static double[] Multiply(double[,] matrix, double[] vector)
        {
            int n = vector.Length;
            if (matrix.GetLength(1) != n) ThrowException.SizeMismatch();
            var result = new double[matrix.GetLength(0)];
            fixed (double* r = result, p = matrix, q = vector)
                for (int i = result.Length; --i >= 0; )
                    r[i] = Us.SumMul(&p[n * i], q, n);
            return result;
        }
        public unsafe static double[] Multiply(double[] vector, double[,] matrix)
        {
            int n = matrix.GetLength(1);
            if (matrix.GetLength(0) != vector.Length) ThrowException.SizeMismatch();
            var result = new double[n];
            fixed (double* r = result, p = matrix, q = vector)
                for (int i = vector.Length; --i >= 0; )
                    Us.LetAddMul(r, &p[n * i], q[i], n);
            return result;
        }
        public unsafe static double[,] Multiply(double[,] matrix0, double[,] matrix1)
        {
            int o = matrix0.GetLength(1);
            if (o != matrix1.GetLength(0)) ThrowException.SizeMismatch();
            int n = matrix0.GetLength(0);
            int m = matrix1.GetLength(1);
            var result = new double[n, m];
            fixed (double* r = result, p = matrix0, q = matrix1)
                for (int i = n; --i >= 0; )
                {
                    double* ri = &r[m * i];
                    double* pi = &p[o * i];
                    for (int j = o; --j >= 0; )
                        Us.LetAddMul(ri, &q[m * j], pi[j], m);
                }
            return result;
        }

        public static double Tr(this double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1)) ThrowException.SizeMismatch();
            double a = 0;
            for (int i = matrix.GetLength(0); --i >= 0; ) a += matrix[i, i];
            return a;
        }
        public static double TrMultiply(double[,] matrix0, double[,] matrix1)
        {
            if (matrix0.GetLength(1) != matrix1.GetLength(0) || matrix0.GetLength(0) != matrix1.GetLength(1)) ThrowException.SizeMismatch();
            double a = 0;
            for (int i = matrix0.GetLength(0); --i >= 0; )
                for (int j = matrix0.GetLength(1); --j >= 0; )
                    a += matrix0[i, j] * matrix1[j, i];
            return a;
        }
        public static double TrMultiply(double[,] matrix0, double[,] matrix1, double[,] matrix2)
        {
            if (matrix0.GetLength(1) != matrix1.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(0) || matrix0.GetLength(0) != matrix2.GetLength(1)) ThrowException.SizeMismatch();
            double a = 0;
            for (int i = matrix1.GetLength(0); --i >= 0; )
                for (int j = matrix1.GetLength(1); --j >= 0; )
                {
                    double b = 0;
                    for (int k = matrix0.GetLength(0); --k >= 0; )
                        b += matrix0[k, i] * matrix2[j, k];
                    a += b * matrix1[i, j];
                }
            return a;
        }

        public static double[,] UnitMatrix(int size) { return DiagonalMatrix(size, 1); }
        public static double[,] DiagonalMatrix(int size, double value)
        {
            var result = new double[size, size];
            for (int i = size; --i >= 0; ) result[i, i] = value;
            return result;
        }
        public static double[,] DiagonalMatrix(double[] values)
        {
            var result = new double[values.Length, values.Length];
            for (int i = values.Length; --i >= 0; ) result[i, i] = values[i];
            return result;
        }
        public static void LetSymmetrical(this double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1)) ThrowException.SizeMismatch();
            for (int i = matrix.GetLength(0); --i >= 0; )
                for (int j = i; --j >= 0; )
                {
                    double a = (matrix[i, j] + matrix[j, i]) * 0.5;
                    matrix[i, j] = a;
                    matrix[j, i] = a;
                }
        }
        public static double[,] T(this double[,] matrix)
        {
            double[,] result = new double[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = result.GetLength(0); --i >= 0; )
                for (int j = result.GetLength(1); --j >= 0; )
                    result[i, j] = matrix[j, i];
            return result;
        }

        // matrix inverse and determinant

        static double Det1by1(double[,] matrix)
        {
            return matrix[0, 0];
        }
        static double[,] Inverse1by1(double[,] matrix)
        {
            double det = matrix[0, 0];
            if (det == 0) ThrowException.WriteLine("Inverse: singular matrix");
            return new double[1, 1]{
                {1.0 / det}
            };
        }
        static double Det2by2(double[,] matrix)
        {
            double a = matrix[0, 0], b = matrix[0, 1];
            double c = matrix[1, 0], d = matrix[1, 1];
            return a * d - b * c;
        }
        static double[,] Inverse2by2(double[,] matrix)
        {
            double a = matrix[0, 0], b = matrix[0, 1];
            double c = matrix[1, 0], d = matrix[1, 1];
            double det = a * d - b * c;
            if (det == 0) ThrowException.WriteLine("Inverse: singular matrix");
            return new double[2, 2]{
                {d / det, -b / det},
                {-c / det, a / det}
            };
        }
        static double Det3by3(double[,] matrix)
        {
            double a = matrix[0, 0], b = matrix[0, 1], c = matrix[0, 2];
            double d = matrix[1, 0], e = matrix[1, 1], f = matrix[1, 2];
            double g = matrix[2, 0], h = matrix[2, 1], i = matrix[2, 2];
            return a * (e * i - f * h) + b * (f * g - d * i) + c * (d * h - e * g);
        }
        static double[,] Inverse3by3(double[,] matrix)
        {
            double a = matrix[0, 0], b = matrix[0, 1], c = matrix[0, 2];
            double d = matrix[1, 0], e = matrix[1, 1], f = matrix[1, 2];
            double g = matrix[2, 0], h = matrix[2, 1], i = matrix[2, 2];
            double eifh = e * i - f * h, fgdi = f * g - d * i, dheg = d * h - e * g;
            double det = a * eifh + b * fgdi + c * dheg;
            if (det == 0) ThrowException.WriteLine("Inverse: singular matrix");
            return new double[3, 3]{
                { eifh / det, (c * h - b * i) / det, (b * f - c * e) / det},
                { fgdi / det, (a * i - c * g) / det, (c * d - a * f) / det},
                { dheg / det, (b * g - a * h) / det, (a * e - b * d) / det}
            };
        }
        static int[] LUDecomposition(double[][] matrix)
        {
            int n = matrix.GetLength(0);
            int[] pivot = new int[n];
            double[] vv = new double[n];
            for (int i = 0; i < n; i++)
            {
                double max = Ex.Range(n).Max(j => Math.Abs(matrix[i][j]));
                if (max == 0.0) ThrowException.WriteLine("LUDecomposition: singular matrix");
                vv[i] = 1.0 / max;
            }

            for (int k = 0; k < n; k++)
            {
                int p = k + Ex.FromTo(k, n).Select(i => vv[i] * Math.Abs(matrix[i][k])).MaxIndex();
                if (p != k)
                {
                    Mt.Swap(ref matrix[p], ref matrix[k]);
                    vv[p] = vv[k];
                }
                pivot[k] = p;
                var mk = matrix[k];
                if (mk[k] == 0.0) { ThrowException.WriteLine("LUDecomposition: singular matrix"); mk[k] = 1e-40; }
                for (int i = k; ++i < n; )  //順不同並列可
                {
                    var mi = matrix[i];
                    double temp = mi[k] /= mk[k];
                    for (int j = k; ++j < n; )
                        mi[j] -= temp * mk[j];
                }
            }
            return pivot;
        }
        public static double[,] Inverse(this double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1)) ThrowException.SizeMismatch();
            if (n == 1) return Inverse1by1(matrix);
            if (n == 2) return Inverse2by2(matrix);
            if (n == 3) return Inverse3by3(matrix);

            double[][] LU = New.Array(n, i => New.Array(n, j => matrix[j, i]));
            int[] pivot = LUDecomposition(LU);
            int[] index = New.Array(n, i => i);
            for (int i = 0; i < n; i++)
                Mt.Swap(ref index[i], ref index[pivot[i]]);

            double[,] result = new double[n, n];
            double[] vec = new double[n];
            for (int i = n; --i >= 0; )  //順不同並列可
            {
                vec.Clear();
                vec[i] = 1.0;
                for (int j = i; ++j < n; )
                {
                    double sum = 0;
                    for (int k = i; k < j; k++)
                        sum -= LU[j][k] * vec[k];
                    vec[j] = sum;
                }
                for (int j = n; --j >= 0; )
                {
                    double sum = vec[j];
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

        public static double Det(this double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n == 1) return Det1by1(matrix);
            if (n == 2) return Det2by2(matrix);
            if (n == 3) return Det3by3(matrix);

            double[][] L = New.Array(n, i => New.Array(n, j => matrix[i, j]));
            int[] pivot = LUDecomposition(L);
            int swap = Ex.Range(n).Count(i => pivot[i] != i);
            double sign = swap % 2 == 0 ? 1.0 : -1.0;
            return sign * Mt.Product(n, i => L[i][i]);
        }

        public static double[,] PseudoInverse(this double[,] matrix)
        {
            var transpose = matrix.T();
            if (matrix.GetLength(0) >= matrix.GetLength(1))
                return Mt.Multiply(Mt.Multiply(transpose, matrix).Inverse(), transpose);
            else
                return Mt.Multiply(transpose, Mt.Multiply(matrix, transpose).Inverse());
        }

        // Inverse of symmetric positive definite matrix
        static double[,] CholeskyDecomposition(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] L = new double[n, n];
            //対角と下三角部分を計算
            for (int j = 0; j < n; j++)
            {
                for (int i = j; i < n; i++)
                {
                    double sum = matrix[j, i];
                    for (int k = j; --k >= 0; )
                        sum -= L[j, k] * L[i, k];
                    if (j == i && sum <= 0.0) ThrowException.WriteLine("CholeskyDecomposition: not a positive definite matrix");
                    L[i, j] = (j == i) ? Math.Sqrt(sum) : sum / L[j, j];
                }
            }
            return L;
        }
        public static double[,] InverseSymmetricPositiveDefinite(this double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1)) ThrowException.SizeMismatch();
            double[,] L = CholeskyDecomposition(matrix);
            //対角と上三角部分を計算
            for (int j = 0; j < n; j++)
            {
                L[j, j] = 1.0 / L[j, j];
                for (int i = j; --i >= 0; )
                {
                    double sum = 0.0;
                    for (int k = j; --k >= i; )
                        sum -= L[j, k] * L[i, k];
                    L[i, j] = sum * L[j, j];
                }
            }
            for (int j = n; --j >= 0; )
            {
                for (int i = 0; i <= j; i++)
                {
                    double sum = L[i, j];
                    for (int k = j; ++k < n; )
                        sum -= L[k, j] * L[i, k];
                    L[i, j] = sum * L[j, j];
                }
            }
            //下三角部分を上三角部分からコピー
            for (int i = n; --i >= 0; )
                for (int j = i; --j >= 0; )
                    L[i, j] = L[j, i];
            return L;
        }


        //Sqrt(x*x + y*y) をoverflow, underflowに気をつけて計算
        static double pythag(double x, double y)
        {
            double z = Math.Sqrt(x * x + y * y);
            if (!double.IsPositiveInfinity(z)) return z;
            return Math.Abs(x) < Math.Abs(y) ?
                Math.Sqrt(1 + Mt.Sq(x / y)) * Math.Abs(y) :
                Math.Sqrt(1 + Mt.Sq(y / x)) * Math.Abs(x);
        }
        static void _QR(double[] vector0, double[] vector1, double c, double s)
        {
            for (int i = vector0.Length; --i >= 0; )
            {
                double x = vector0[i];
                double y = vector1[i];
                vector0[i] = x * c + y * s;
                vector1[i] = y * c - x * s;
            }
        }

        //M: symmetric matrix
        //Mを転置させた計算の方が速いため改変
        static double[] Householder(double[][] M, double[] D)
        {
            int n = M.Length;
            double[] E = new double[n];
            for (int i = n; --i >= 2; )
            {
                double[] Mi = new double[i];
                for (int l = i; --l >= 0; ) Mi[l] = M[l][i];
                double scale = Mi.Sum(x => Math.Abs(x));
                if (scale == 0) continue;
                for (int l = i; --l >= 0; ) Mi[l] /= scale;

                double Di;
                {
                    double f = Mi.Sum(x => x * x);
                    double g = Math.Sqrt(f);
                    if (Mi[i - 1] < 0) g *= -1;
                    E[i] = -g * scale;
                    D[i] = Di = f + g * Mi[i - 1];
                    Mi[i - 1] += g;
                }

                double hh = 0;
                for (int j = 0; j < i; j++)
                {
                    M[i][j] = Mi[j] / Di;
                    {
                        double a = 0;
                        for (int l = i; --l > j; ) a += M[j][l] * Mi[l];
                        for (int l = j + 1; --l >= 0; ) a += M[l][j] * Mi[l];
                        E[j] = a / Di;
                    }
                    hh += E[j] * Mi[j];
                }
                hh /= 2 * Di;
                for (int j = 0; j < i; j++)
                {
                    E[j] -= Mi[j] * hh;
                    for (int k = j + 1; --k >= 0; ) M[k][j] -= Mi[j] * E[k] + Mi[k] * E[j];
                }
                for (int l = i; --l >= 0; ) M[l][i] = Mi[l];
            }
            E[1] = M[0][1];

            for (int i = 0; i < n; i++)
            {
                if (D[i] != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        double a = 0;
                        for (int l = i; --l >= 0; ) a += M[j][l] * M[l][i];
                        for (int l = i; --l >= 0; ) M[j][l] -= M[i][l] * a;
                    }
                }
                D[i] = M[i][i];
                M[i][i] = 1;
                for (int l = i; --l >= 0; ) M[i][l] = 0;
                for (int l = i; --l >= 0; ) M[l][i] = 0;
            }
            return E;
        }
        static void QLMethod(double[][] M, double[] D, double[] E)
        {
            int N = M.Length;
            for (int l = 0; l < N; l++)
            {
                while (true)
                {
                    int m;
                    for (m = l; m < N - 1; m++)
                        if (IsTooSmall(E[m], Math.Abs(D[m]) + Math.Abs(D[m + 1])) || Math.Abs(E[m]) < Mt.DoubleEpsilon) break;
                    if (m == l) break;

                    double g = (D[l + 1] - D[l]) / E[l] * 0.5;
                    g += g * Math.Sqrt(1 + 1 / (g * g));
                    g = E[l] / g + D[m] - D[l];
                    double s = 1, c = 1, p = 0;
                    int i;
                    for (i = m; --i >= l; )
                    {
                        double b = E[i] * c;
                        {
                            double f = E[i] * s;
                            double py = pythag(f, g);
                            E[i + 1] = py;
                            if (py == 0) break;
                            s = f / py;
                            c = g / py;
                        }
                        {
                            double q = D[i + 1] - p;
                            double r = (D[i] - q) * s + b * c * 2;
                            p = r * s;
                            D[i + 1] = q + p;
                            g = r * c - b;
                        }
                        _QR(M[i], M[i + 1], c, -s);
                    }
                    if (i < l) E[l] = g;
                    D[i + 1] -= p;
                    E[m] = 0;
                }
            }
        }
        //matrix: symmetric matrix
        //matrix => V * diag[D] * V^T
        //固有値の配列と固有ベクトルの配列を返す
        public static Tuple<double[], double[][]> EigenValueDecomposition(double[,] matrix)
        {
            int N = matrix.GetLength(0);
            if (N != matrix.GetLength(1)) ThrowException.SizeMismatch();
            var D = new double[N];
            var V = New.Array(N, i => New.Array(N, j => matrix[i, j]));

            double[] E = Householder(V, D);
            for (int i = 0; i < N - 1; i++) E[i] = E[i + 1];
            E[N - 1] = 0;
            QLMethod(V, D, E);

            int[] order = D.SortIndex().LetReverse();
            D.LetSortAsIndex(order);
            V.LetSortAsIndex(order);
            for (int i = N; --i >= 0; )
                if (V[i].Count(a => a < 0) > N / 2) Mt.LetMul(V[i], -1);
            return Tuple.Create(D, V);
        }
        //matrix => V * diag[D] * V^T
        //V, Dを返す
        public static Tuple<double[,], double[]> EigenValueDecompositionM(double[,] matrix)
        {
            var T = EigenValueDecomposition(matrix);
            return Tuple.Create(
                New.Array(T.Item2[0].Length, T.Item2.Length, (i, j) => T.Item2[j][i]),
                T.Item1
            );
        }

        //matrix -> U diag[W] V^T
        //U, V: 縦ベクトルの配列を返す
        public static Tuple<double[][], double[], double[][]> SingularValueDecomposition(double[,] matrix)
        {
            int XN = matrix.GetLength(1);
            int YN = matrix.GetLength(0);
            int ZN = Math.Min(YN, XN);
            var U = New.Array(XN, i => New.Array(YN, j => matrix[j, i]));
            var V = New.Array(XN, i => new double[XN]);
            var W = new double[XN];
            var R = new double[XN];
            double anorm = 0;
            for (int i = 0; i < XN; i++)
            {
                var I = Ex.FromTo(i, YN);
                if (i < YN)
                {
                    double scale = I.Sum(k => Math.Abs(U[i][k]));
                    if (scale != 0.0)
                    {
                        I.ForEach(k => { U[i][k] /= scale; });
                        double s = I.Sum(k => Mt.Sq(U[i][k]));
                        double f = U[i][i];
                        double g = Math.Sqrt(s);
                        if (f > 0) g *= -1;
                        double h = f * g - s;
                        U[i][i] = f - g;
                        for (int j = i; ++j < XN; )
                        {
                            double coef = 0;
                            for (int k = i; k < YN; k++) coef += U[j][k] * U[i][k];
                            coef /= h;
                            for (int k = i; k < YN; k++) U[j][k] += coef * U[i][k];
                        }
                        I.ForEach(k => { U[i][k] *= scale; });
                        W[i] = scale * g;
                    }
                }
                int l = i + 1;
                var L = Ex.FromTo(l, XN);
                if (i < YN && i < XN - 1)
                {
                    double scale = L.Sum(k => Math.Abs(U[k][i]));
                    if (scale != 0.0)
                    {
                        L.ForEach(k => { U[k][i] /= scale; });
                        double s = L.Sum(k => Mt.Sq(U[k][i]));
                        double f = U[l][i];
                        double g = -Math.Sign(f) * Math.Sqrt(s);
                        double h = f * g - s;
                        U[l][i] = f - g;
                        L.ForEach(k => { R[k] = U[k][i] / h; });
                        for (int j = l; j < YN; j++)
                        {
                            double coef = 0;
                            for (int k = l; k < XN; k++) coef += U[k][j] * U[k][i];
                            for (int k = l; k < XN; k++) U[k][j] += coef * R[k];
                        }
                        L.ForEach(k => { U[k][i] *= scale; });
                        R[l] = scale * g;
                    }
                }
                Mt.LetMax(ref anorm, Math.Abs(W[i]) + Math.Abs(R[i]));
                if (i > YN) R[i] = 0.0;
            }

            for (int i = XN; --i >= 0; )
            {
                if (i < XN - 1)
                {
                    int l = i + 1;
                    var L = Ex.FromTo(l, XN);
                    double g = R[l];
                    if (g != 0.0)
                    {
                        if (U[l][i] != 0.0)
                        {
                            double coef = 1 / U[l][i] / g;
                            L.ForEach(k => { V[i][k] = U[k][i] * coef; });
                        }
                        for (int j = l; j < XN; j++)
                        {
                            double coef = 0;
                            for (int k = l; k < XN; k++) coef += V[j][k] * U[k][i];
                            for (int k = l; k < XN; k++) V[j][k] += coef * V[i][k];
                        }
                    }
                    L.ForEach(k => { V[i][k] = 0.0; });
                    L.ForEach(k => { V[k][i] = 0.0; });
                }
                V[i][i] = 1.0;
            }

            for (int i = ZN; --i >= 0; )
            {
                int l = i + 1;
                var I = Ex.FromTo(i, YN);
                Ex.FromTo(l, XN).ForEach(k => { U[k][i] = 0.0; });
                double g = W[i];
                if (g != 0.0)
                {
                    g = 1 / g;
                    for (int j = l; j < XN; j++)
                    {
                        double coef = 0;
                        for (int k = l; k < YN; k++) coef += U[j][k] * U[i][k];
                        coef = coef / U[i][i] * g;
                        for (int k = i; k < YN; k++) U[j][k] += coef * U[i][k];  //
                    }
                    I.ForEach(k => { U[i][k] *= g; });
                }
                else
                    I.ForEach(k => { U[i][k] = 0.0; });
                U[i][i] += 1;
            }

            for (int k = XN; --k >= 0; )
            {
                for (int iterations = 30; ; iterations--)
                {
                    int l;
                    for (l = k; l > 0; l--)
                    {  // R[0]==0;
                        if (IsTooSmall(R[l], anorm)) break;
                        if (IsTooSmall(W[l - 1], anorm))
                        {
                            double c = 0, s = 1;
                            for (int i = l; i <= k; i++)
                            {
                                double f = s * R[i];
                                R[i] *= c;
                                if (IsTooSmall(f, anorm)) break;
                                double g = W[i];
                                double py = pythag(f, g);
                                W[i] = py;
                                c = g / py;
                                s = -f / py;
                                _QR(U[l - 1], U[i], c, s);
                            }
                            break;
                        }
                    }

                    if (l == k)
                    {
                        if (W[k] < 0) { W[k] *= -1; Mt.LetMul(V[k], -1); }
                        break;
                    }
                    if (iterations == 0) { ThrowException.WriteLine("SingularValueDecomposition: too many iterations"); break; }

                    {
                        double f = 0.0;
                        double x = W[l];
                        {
                            double y = W[k - 1];
                            double z = W[k];
                            double g = R[k - 1];
                            double h = R[k];
                            f = ((y - z) * (y + z) + (g - h) * (g + h)) / (2 * h * y);
                            double py = f * (1 + Math.Sqrt(1 + 1 / (f * f)));
                            f = ((x - z) * (x + z) + h * (y / py - h)) / x;
                        }
                        double c = 1, s = 1;
                        for (int j = l; j < k; j++)
                        {
                            int i = j + 1;
                            double g = R[i] * c;
                            double h = R[i] * s;
                            {
                                double py = pythag(f, h);
                                R[j] = py;
                                c = f / py;
                                s = h / py;
                            }
                            f = x * c + g * s;
                            g = g * c - x * s;

                            double y = W[i] * c;
                            h = W[i] * s;
                            _QR(V[j], V[i], c, s);
                            {
                                double py = pythag(f, h);
                                W[j] = py;
                                if (py != 0.0)
                                {
                                    c = f / py;
                                    s = h / py;
                                }
                            }
                            f = g * c + y * s;
                            x = y * c - g * s;
                            _QR(U[j], U[i], c, s);
                        }
                        R[l] = 0;
                        R[k] = f;
                        W[k] = x;
                    }
                }
            }
            {
                var order = W.SortIndex().LetReverse();
                W.LetSortAsIndex(order);
                U.LetSortAsIndex(order);
                V.LetSortAsIndex(order);

                for (int k = 0; k < XN; k++)
                {
                    int s = U[k].Count(a => a < 0) + V[k].Count(a => a < 0);
                    if (s > (YN + XN) / 2)
                    {
                        Mt.LetMul(U[k], -1);
                        Mt.LetMul(V[k], -1);
                    }
                }
            }
            return Tuple.Create(U, W, V);
        }
        //matrix -> U diag[W] V^T
        //U, W, Vを返す
        public static Tuple<double[,], double[], double[,]> SingularValueDecompositionM(double[,] matrix)
        {
            var T = SingularValueDecomposition(matrix);
            return Tuple.Create(
                New.Array(T.Item1[0].Length, T.Item1.Length, (i, j) => T.Item1[j][i]),
                T.Item2,
                New.Array(T.Item3.Length, T.Item3.Length, (i, j) => T.Item3[j][i])
            );
        }

        // Rotation Matrix
        public static Double2 Rotate(Double2 vector, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new Double2(cos * vector.X - sin * vector.Y, sin * vector.X + cos * vector.Y);
        }
        // 右手座標系ならvectorをaxisを軸にして右ねじの回転方向に回転させる．axisの長さ=1
        public static Double3 Rotate(Double3 vector, Double3 axis, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double cos1 = 1 - cos;
            double x = axis.X;
            double y = axis.Y;
            double z = axis.Z;
            return new Double3(
                (cos1 * x * x + cos) * vector.X + (cos1 * x * y - sin * z) * vector.Y + (cos1 * z * x + sin * y) * vector.Z,
                (cos1 * x * y + sin * z) * vector.X + (cos1 * y * y + cos) * vector.Y + (cos1 * y * z - sin * x) * vector.Z,
                (cos1 * z * x - sin * y) * vector.X + (cos1 * y * z + sin * x) * vector.Y + (cos1 * z * z + cos) * vector.Z
            );
        }
        public static double[,] RotationMatrix(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double[,] matrix = new double[2, 2]{
                { cos, -sin },
                { sin, cos }
            };
            return matrix;
        }
        public static double[,] RotationMatrix(Double3 axis, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double cos1 = 1 - cos;
            double x = axis.X;
            double y = axis.Y;
            double z = axis.Z;
            double[,] matrix = new double[3, 3] {
                { cos1 * x * x + cos, cos1 * x * y - sin * z, cos1 * z * x + sin * y },
                { cos1 * x * y + sin * z, cos1 * y * y + cos, cos1 * y * z - sin * x },
                { cos1 * z * x - sin * y, cos1 * y * z + sin * x, cos1 * z * z + cos },
            };
            return matrix;
        }
        #endregion
        #endregion
    }
}
