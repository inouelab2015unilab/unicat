using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InoueLab;
using Wave;


namespace otozemi10
{
    class Program
    {
        static void Main(string[] args)
        {
         
            //double[] ex = new double[] { 5, 8 };
           //  var vector = New.Array(2, (i=>ex[i]));

            var matrix1 = New.Array(2, 2, 3.0);
            var matrix2 = New.Array(2, 2, 3.0);
            var matrix4 = Mt.DiagonalMatrix(2, 2.0);


            var matrix3 = Mt.Inverse(matrix4);
            var matrix5 = Mt.InverseSymmetricPositiveDefinite(matrix4);
            double[] wave = WaveMethod.ReadWavFile(@"write.wav");

            double[][] spectrum = WaveMethod.STPS_Hamming(wave, 10);
           // double[][] wave2 = WaveMethod.Inv_STFT_Hamming(spectrum, 10);


           // complex[][] spectrum1 = WaveMethod.STFT_Hanning(wave, 10);

            int check = 2;
            check++;
        }
    }
}
