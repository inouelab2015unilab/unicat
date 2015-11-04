using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using InoueLab;
using Wave;



namespace _3rdspline
{
    class Program
    {
        static void Main(string[] args)
        {

            //csvファイル読み込み
            StreamReader readcsv = new StreamReader(@"3rdspline.csv");
            string values = "";
            ArrayList arrText = new ArrayList();
            //一旦格納
            while (values != null)
            {
                values = readcsv.ReadLine();
                if (values != null)
                    arrText.Add(values);
            }
            readcsv.Close();
            ////最終的に格納したい配列に入れる準備
            int line = arrText.Count; //行の数えあげ
            string temp = (string)arrText[0];
            string[] temp2 = temp.Split(',');
            int col = temp2.Length; //列の数え上げ

            double[,] csv = new double[line, col]; //格納対象の配列
            int l = 0, cl = 0;
            foreach (string sOutput in arrText)
            {
                string[] temp_line = sOutput.Split(',');
                foreach (string value in temp_line)
                {
                    csv[l, cl] = Convert.ToDouble(value);
                    cl++;
                }
                cl = 0;
                l++;
            }
            int m = l + 1;
            //格納終了

            double[] x = new double[m - 1];
            double[] fx = new double[m - 1];

            ////ｘとｆｘに数値を代入
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        x[i] = csv[i, j];
                    }
                    else 
                    {
                        fx[i] = csv[i, j];
                    }
                }
            
            }


            //int m = 15; 行列の大きさ
            double[,] A = new double[m - 1, m - 1];//基の行列
            double[,] Inv_A = new double[m - 1, m - 1];//逆行列
           // double[] x = new double[14] { 0.9, 1.3, 1.9, 2.1, 2.6, 3.0, 3.9, 4.4, 4.7, 5.0, 6.0, 7.0, 8.0, 9.2 };
           // double[] fx = new double[14] { 1.3, 1.5, 1.85, 2.1, 2.6, 2.7, 2.4, 2.15, 2.05, 2.1, 2.25, 2.3, 2.25, 1.95 };
            double[] a = new double[m - 1];
            double[] b = new double[m - 1];
            double[] c = new double[m - 1];
            double[] d = new double[m - 1];
            double[] B = new double[m];
            double[] h = new double[m - 2];

            //h[]の計算
            for (int i = 0; i < m - 2; i++)
            {
                h[i] = x[i + 1] - x[i];
                //   Console.WriteLine("{0}", h[i]);
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

                    else if (i == j + 1 && i < m - 2)
                        A[i, j] = h[j];

                    else if (i < m - 2 && i == j)
                        A[i, j] = 2 * (h[j] + h[j - 1]);

                    else if (j == i + 1 && i < m - 2)
                        A[i, j] = h[i];
                }

            }

            //Aの出力
            for (int i = 0; i < m - 1; i++)
            {
                for (int j = 0; j < m - 1; j++)
                {
                    //   Console.Write("{0:f2}\t", A[i, j]);
                }
            }
            using (StreamWriter sw = new StreamWriter("A.csv"))
            {
                for (int i = 0; i < m - 1; i++)
                    for (int j = 0; j < m - 1; j++)
                    {
                        {
                            //  sw.WriteLine(A[i,j]);
                        }
                    }
            }

            //Bの計算
            for (int i = 0; i < m - 1; i++)
            {
                if (i == 0 || i == m - 2)
                    B[i] = 0;
                else if (i < m - 1)
                    B[i] = (3 / h[i]) * (fx[i + 1] - fx[i]) - (3 / h[i - 1]) * (fx[i] - fx[i - 1]);
                //   Console.Write("Bは{0:f2}\t", B[i]);

            }

            Inv_A = Mt.Inverse(A);
            // Hakidashi(A, Inv_A, m - 1);//逆行列算出
            // c = Mt.Multiply(Inv_A, B);
            for (int i = 0; i < m - 1; i++)
            {
                for (int j = 0; j < m - 1; j++)
                {
                    c[i] += Inv_A[i, j] * B[j];
                }
            }
            for (int i = 0; i < m - 2; i++)
            {
                Console.Write("{0:f2}\t", c[i]);
            }


            for (int i = 0; i < m - 2; i++)
            {
                b[i] = (1 / h[i]) * (fx[i + 1] - fx[i]) - (h[i] / 3) * (2 * c[i] + c[i + 1]);
                //    Console.Write("{0:f2}\t", b[i]);
            }

            for (int i = 0; i < m - 2; i++)
            {
                d[i] = (c[i + 1] - c[i]) / (3 * h[i]);
                //   Console.Write("{0:f2}\t", d[i]);
            }


            using (StreamWriter sw = new StreamWriter("spline.csv"))
            {
                for (int i = 0; i < m - 2; i++)
                {
                    sw.WriteLine(fx[i] + "," + b[i] + "," + c[i] + "," + d[i]);
                }
            }


            //    //      Console.WriteLine("逆行列の算出結果を以下に記します");

            //    // 逆行列を出力
            //    for (int i = 0; i < m - 1; i++)
            //    {
            //        for (int j = 0; j < m - 1; j++)
            //        {
            //            //      Console.Write("{0:f2}\t", Inv_A[i, j]);
            //        }
            //        Console.WriteLine("");
            //    }



            //音声波形の読み込み
            double[] wave = WaveMethod.ReadMonoWavFile(@"sina4.wav");


            for (int i = 0; i < wave.Length; i++)
            {
                wave[i] = wave[i] * 3;
            }

            WaveData2 wav = new WaveData2("Mono");
            wav.dataChank.sizeOfFmtData = (uint)(wave.Count()) * 3;
            WaveMethod.wavArrangementWrite("sample2.wav", wave, wav);

            using (StreamWriter sw = new StreamWriter("sin.csv"))
            {
                for (int i = 0; i < wave.Count(); i++)
                {
                    sw.WriteLine(i + "," + wave[i]);
                }
            }
                

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
                    Inv_a[j, i] *=
                        buf;
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



