using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3rdspline
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 15; //行列の大きさ
            double[,] A = new double[m - 1, m - 1];//基の行列
            double[,] Inv_A = new double[m - 1, m - 1];//逆行列
            double[] a = new double[m - 2];
            double[] b = new double[m - 2];
            double[] c = new double[m - 1];
            double[] d = new double[m - 2];
            double[] B = new double[m - 1];
            double[] x = new double[14] { 0.9, 1.3, 1.9, 2.1, 2.6, 3.0, 3.9, 4.4, 4.7, 5.0, 6.0, 7.0, 8.0, 9.2 };
            double[] fx = new double[14] { 1.3, 1.5, 1.85, 2.1, 2.6, 2.7, 2.4, 2.15, 2.05, 2.1, 2.25, 2.3, 2.25, 1.95 };
            double[] h = new double[13];

            //h[]の計算
            for (int i = 0; i < m - 2; i++)
            {
                h[i] = x[i + 1] - x[i];
                if (i == m - 1) h[i] = 1;
                //   Console.WriteLine("{0}",h[i]);
            }

            //Aの計算
            for (int i = 0; i < m - 1; i++)
            {
                for (int j = 0; j < m - 1; j++)
                {
                    if (i == 0 && j == 0)
                        A[i, j] = 1;

                    else if (i == m - 2 && j == m - 2)
                        A[i, j] = 1;

                    else if (i == 0 && j != 0)
                        A[i, j] = 0;

                    else if (i == m - 2 && j != m - 2)
                        A[i, j] = 0;

                    else if (j == i - 1)
                        A[i, j] = h[j];

                    else if (i == j && i % 2 == 1 && i < 12)
                        A[i, j] = 2 * (h[j] + h[j - 1]);

                    else if (i == j && i % 2 == 0 && i < 12)
                        A[i, j] = 2 * (h[j] - h[j - 1]);

                    else if (j == i + 1 && i < 12)
                        A[i, j] = h[i];

                    else
                        A[i, j] = 0;
                    // Console.WriteLine("Aの中身は{0}です", A[i, j]);
                }

            }

           // Aの出力
            //for (int i = 0; i < m - 1; i++)
            //{
            //    for (int j = 0; j < m - 1; j++)
            //    {
            //        Console.Write("{0:f2}\t", A[i, j]);
            //    }
            //}

            //Bの計算
            for (int i = 0; i < m - 1; i++)
            {
                if (i == 0 || i == m - 1)
                    B[i] = 0;
                else if (i < m - 2)
                    B[i] = (3 / h[i]) * (fx[i + 1] - fx[i]) - (3 / h[i - 1]) * (fx[i] - fx[i - 1]);
                //  Console.Write("{0:f2}\t", B[i]);

            }

            Hakidashi(A, Inv_A, m - 1);//逆行列算出

            for (int i = 0; i < m - 1; i++)
            {
                for (int j = 0; j < m - 1; j++)
                {
                    c[i] += Inv_A[i, j] * B[j];

                }
            }
            for (int i = 0; i < m - 1; i++)
            {
            //    Console.Write("{0:f2}\t", c[i]);
            }

            Console.WriteLine("基の行列を以下に記します");
            // 基の行列を出力
            //for (int i = 0; i < m-1; i++)
            //{
            //    for (int j = 0; j < m-1; j++)
            //    {
            //        Console.Write("{0:f2}\t", A[i, j]);
            //    }
            //    Console.WriteLine("");
            //}
            //Console.WriteLine("");

            // Console.WriteLine("逆行列の算出結果を以下に記します");

            // 逆行列を出力
            //for (int i = 0; i < m - 1; i++)
            //{
            //    for (int j = 0; j < m - 1; j++)
            //    {
            //        Console.Write("{0:f2}\t", Inv_A[i, j]);
            //    }
            //    Console.WriteLine("");
            //}


        }
        //Mainroop終わり

        /// <summary>
        /// 掃き出し法で逆行列を求める
        /// </summary>
        /// <param name="a"></param>基の行列
        /// <param name="Inv_a"></param>逆行列
        /// <param name="n"></param>行列(n*n)
        static void Hakidashi(double[,] a, double[,] Inv_a, int n)
        {
            //単位行列を作る
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Inv_a[j, i] = (i == j) ? 1.0 : 0.0;
                }
            }

            //掃き出し法
            for (int i = 0; i < n; i++)
            {
                double buf = 1 / a[i, i];
                for (int j = 0; j < n; j++)
                {
                    a[j, i] *= buf;
                    Inv_a[j, i] *= buf;
                }
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        buf = a[i, j];
                        for (int k = 0; k < n; k++)
                        {
                            a[k, j] -= a[k, i] * buf;
                            Inv_a[k, j] -= Inv_a[k, i] * buf;
                        }
                    }
                }
            }

        }






    }

}
