using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int m=7;
            int p=6;
            int[,] matrixA =new int[m,p];
            int[] data1 = new int[m];
            int[] data2 = new int[p];
            int[,] index = new int[m, p];

            Console.WriteLine("二つの配列を入力してください。　例:A,T,G,C,A");
            Console.WriteLine("一つ目の配列は？");
            string line1=Console.ReadLine();
            Console.WriteLine("二つ目の配列は？");
            string line2 = Console.ReadLine();

            string[] line1str = line1.Split(',');
            string[] line2str = line2.Split(',');

            data1[1] = 0;
            data2[1] = 0;

            for (int i = 0; i < m-1; i++)
            {
                if (line1str[i] == "A") data1[i+1] = 1;
                else if (line1str[i] == "T") data1[i+1] = 2;
                else if (line1str[i] == "G") data1[i+1] = 3;
                else if (line1str[i] == "C") data1[i+1] = 4;
            }
            for(int i=0;i<p-1;i++)
            {
                if (line2str[i] == "A") data2[i+1] = 1;
                else if (line2str[i] == "T") data2[i+1] = 2;
                else if (line2str[i] == "G") data2[i+1] = 3;
                else if (line2str[i] == "C") data2[i+1] = 4;

            }


            int c=0;
            int scoredif=-1;
            int scoresame=2;
           int d=2;
            matrixA[0, 0] = c;
            for (int i = 1; i < m; i++)
            {   
                for (int j = 1; j < p; i++)
                {
                    matrixA[0, j] = matrixA[0, j - 1]+scoredif*d;
                    matrixA[i, 0] = matrixA[i - 1, 0]+scoredif*d;
                    
                    if (data1[i] == data2[j]) matrixA[i, j] = matrixA[i - 1, j - 1] + scoresame;
                     else if (data1[i] != data2[j]) matrixA[i, j] = matrixA[i - 1, j - 1] + scoredif;


                    if (data1[i] == data2[j]) matrixA[i, j] = matrixA[i - 1, j - 1] + scoresame;
                     else if (data1[i] != data2[j]) matrixA[i, j] = matrixA[i - 1, j - 1] + scoredif;



                    if (matrixA[i,j]>matrixA[i-1])
                }
            }


        }
    }
}
