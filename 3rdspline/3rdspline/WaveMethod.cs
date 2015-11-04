using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using System.IO;
using InoueLab;
using System.Numerics;

namespace Wave
{
    #region FFTクラス
    public static class FFT
    {
        //↓データ数2の乗数
        #region FFT_HT ver_Inoue
        public static void FFT_HT(double[] inputRe, double[] inputIm, out double[] outputRe, out double[] outputIm, int bitSize)
        {
            Complex[] input = new Complex[inputRe.Length];
            Complex[] output = new Complex[inputRe.Length];

            //Complexは実部、虚部を格納する
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = new Complex(inputRe[i], inputIm[i]);
            }

            //先生のＦＦＴ
            output = Nm.FastFourierTransform(input);

            outputRe = new double[output.Length];
            outputIm = new double[output.Length];

            for (int i = 0; i < input.Length; i++)
            {
                outputRe[i] = output[i].Real;
                outputIm[i] = output[i].Imaginary;
            }


        }
        #endregion

        #region IFFT  ver_normal
        public static void IFFT(double[] inputRe, double[] inputIm, out double[] outputRe, out double[] outputIm, int bitSize)
        {
            int dataSize = 1 << bitSize;
            outputRe = new double[dataSize];
            outputIm = new double[dataSize];

            for (int i = 0; i < dataSize; i++)
            {
                inputIm[i] = -inputIm[i];
            }
            FFT_HT(inputRe, inputIm, out outputRe, out outputIm, bitSize);
            for (int i = 0; i < dataSize; i++)
            {
                outputRe[i] /= (double)dataSize;
                outputIm[i] /= (double)(-dataSize);
            }
        }
        #endregion

        #region IFFT  ver_Complex
        //public static void IFFT(Complex[] input,out Complex[] output,int bitSize)
        //{
        //    int dataSize = 1 << bitSize;
        //    output = new Complex[dataSize];

        //    for (int i = 0; i < dataSize; i++)
        //    {
        //        input[i] = new Complex(input[i].Real, -input[i].Imaginary);
        //    }
        //    output = Nm.FastFourierTransform(input, true);
        //    for (int i = 0; i < dataSize; i++)
        //    {
        //        output[i] = new Complex(output[i].Real / (double)dataSize, -output[i].Imaginary / (double)dataSize);
        //    }
        //}
        #endregion

        //窓関数
        //----------------------------------------------

        #region どの窓関数をかけるかを指定する列挙型(type(整数)を指定するタイプのメソッド用)
        public enum DataWindowType
        {
            Box, Hanning, Hamming, Blackman, Parzen, Welch, NormalDistribution = -1
        }
        #endregion

        #region type(整数)でどの窓関数を使うか指定するGetDataWindowメソッド
        public static double[] GetDataWindow(DataWindowType type, int n)
        {
            double[] Table = new double[n];
            Table = new double[n];

            double h = 2 * Math.PI / (n - 1);
            switch (type)
            {
                //typeの値で決めている
                case DataWindowType.Box:
                    for (int i = n; --i >= 0; ) Table[i] = 1;
                    break;
                case DataWindowType.Hanning:
                    for (int i = n; --i >= 0; ) Table[i] = 0.50 - 0.50 * Math.Cos(h * i);
                    break;
                case DataWindowType.Hamming:
                    for (int i = n; --i >= 0; ) Table[i] = 0.54 - 0.46 * Math.Cos(h * i);
                    break;
                case DataWindowType.Blackman:
                    for (int i = n; --i >= 0; ) Table[i] = 0.42 - 0.50 * Math.Cos(h * i) + 0.08 * Math.Cos(2 * h * i);
                    break;
                case DataWindowType.Parzen:
                    for (int i = n; --i >= 0; ) Table[i] = 1.0 - Math.Abs((i * 2 - (n - 1)) / (double)(n + 1));
                    break;
                case DataWindowType.Welch:
                    for (int i = n; --i >= 0; ) Table[i] = 1.0 - Mt.Sq((i * 2 - (n - 1)) / (double)(n + 1));
                    break;
            }
            //double c = Math.Sqrt(n / Table.Sum(x => Sq(x)));
            //for (int i = n; --i >= 0; ) Table[i] *= c;
            return Table;
        }
        #endregion

        #region  type(整数)で何の窓関数をかけるか指定できるWindowingメソッド(ver_Complex)
        //public static void Windowing(Complex[] Data, DataWindowType type)
        //{
        //    double[] window = GetDataWindow(type, Data.Length);
        //    for (int i = Data.Length; --i >= 0; ) Data[i] *= window[i];
        //}
        #endregion

        //----------------------------------------------

        #region  type(整数)で何の窓関数をかけるか指定できるWindowingメソッド(ver_double)
        public static void Windowing(double[] Data, DataWindowType type)
        {
            double[] window = GetDataWindow(type, Data.Length);

            for (int i = Data.Length; --i >= 0; ) Data[i] *= window[i];

        }

        #endregion


        #region  type(整数)で窓関数を指定し実際に割るReverseWindowingメソッド(ver_double)
        public static void ReverseWindowing1(double[] Data, DataWindowType type)
        {
            double[] window = GetDataWindow(type, Data.Length);
            for (int i = Data.Length; --i >= 0; ) Data[i] /= window[i];
        }

        #endregion

        #region  type(整数)で窓関数を指定し実際に割るReverseWindowingメソッド(ver_double)
        public static void ReverseWindowing2(double[][] Data, DataWindowType type, int num)
        {
            double[] window = GetDataWindow(type, Data[0].Length);
            for (int i = Data[0].Length; --i >= 0; ) Data[num][i] /= window[i];

        }

        #endregion


        #region  どの窓関数をかけるかを指定する列挙型(直接窓関数の名前を指定するタイプのメソッド用)
        public enum WindowFunc
        {
            Hamming, Hanning, Blackman, Rectangular

        }
        #endregion

        #region dataと窓関数の種類を引数とするWindowingメソッド
        public static double[] Windowing(double[] data, WindowFunc windowFunc)
        {
            int size = data.Length;
            double[] windata = new double[size];

            for (int i = 0; i < size; i++)
            {
                double winValue = 0;
                // 各々の窓関数(ハニング窓以外二次元非対応)
                if (windowFunc == WindowFunc.Hamming)
                {
                    winValue = 0.54 - 0.46 * Math.Cos(2 * Math.PI * i / (size - 1));
                }
                else if (WindowFunc.Hanning == windowFunc)
                {
                    winValue = 0.5 * (1 + Math.Cos(2 * Math.PI * i / (size - 1)));
                }
                else if (WindowFunc.Blackman == windowFunc)
                {
                    winValue =
                        0.42 - 0.5 * Math.Cos(2 * Math.PI * i / (size - 1))
                        + 0.08 * Math.Cos(4 * Math.PI * i / (size - 1));
                }
                else if (WindowFunc.Rectangular == windowFunc)
                {
                    winValue = 1.0;
                }
                else
                {
                    winValue = 1.0;
                }
                // 窓関数を掛け算
                windata[i] = data[i] * winValue;
            }
            return windata;
        }
        #endregion



    }
    #endregion
    //Wavデータに関するメソッド
    sealed public class WaveMethod
    {

        public static void BinAdd(List<short> bin, double[] resultData)
        {
            for (int i = 0; i < resultData.Length; i++)
            {
                bin.Add((short)resultData[i]); //左
                bin.Add((short)resultData[i]); //右
            }
        }

        #region 対称の音源データがモノラルかステレオかを文字列で返すMonoStereo
        public static string MonoStereo(WaveData1 wav)
        {
            string MonoStereo = null;
            if (wav.wfp.channels == 1) MonoStereo = "Mono";
            else MonoStereo = "Stereo";
            return MonoStereo;
        }
        #endregion

        #region 入力のWAVEヘッダを読み込むwavHdrRead
        public static long wavHdrRead(string FName, WaveData1 wav)
        {
            long fPos = -1;

            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(FName));
                int fileSize = (int)br.BaseStream.Length;
                // RIFF
                wav.wfh.hdrRiff = br.ReadBytes(wav.wfh.hdrRiff.Length);
                // string型文字列へ変換
                string bufString = Encoding.UTF8.GetString(wav.wfh.hdrRiff);

                // ファイルサイズ
                wav.wfh.sizeOfFile = br.ReadUInt32();

                // WAVE
                wav.wfh.hdrWave = br.ReadBytes(wav.wfh.hdrWave.Length);
                // string型文字列へ変換
                bufString = Encoding.UTF8.GetString(wav.wfh.hdrWave);

                // chank読み込み
                while (true)
                {
                    wav.chank.hdrFmtData = br.ReadBytes(wav.chank.hdrFmtData.Length);
                    // string型文字列へ変換
                    bufString = Encoding.UTF8.GetString(wav.chank.hdrFmtData);

                    wav.chank.sizeOfFmtData = br.ReadUInt32();

                    if (bufString == "fmt ")
                    {
                        fPos = br.BaseStream.Position;

                        wav.wfp.formatTag = br.ReadUInt16();
                        wav.wfp.channels = br.ReadUInt16();
                        wav.wfp.samplesPerSec = br.ReadUInt32();
                        wav.wfp.bytesPerSec = br.ReadUInt32();
                        wav.wfp.blockAlign = br.ReadUInt16();
                        wav.wfp.bitsPerSample = br.ReadUInt16();

                        fPos += wav.chank.sizeOfFmtData;
                        br.BaseStream.Position = fPos;
                    }
                    else if (bufString == "data")
                    {
                        fPos = br.BaseStream.Position;
                        wav.sizeOfData = wav.chank.sizeOfFmtData;
                        break;
                    }
                    else
                    {
                        br.BaseStream.Position += wav.chank.sizeOfFmtData;
                    }
                }
                br.Close();

            }

            catch (EndOfStreamException)
            {
                //MessageBox.Show("ファイルの終端に達しました.", "エラー", MessageBoxButtons.OK);
                return fPos = -1;
            }
            catch (SystemException)
            {
                //MessageBox.Show("WavehdrReadで例外発生", "エラー", MessageBoxButtons.OK);
                return fPos = -1;

            }
            return fPos;
        }
        #endregion

        #region 配列からWaveFileを作るwavArrangementWrite
        public static bool wavArrangementWrite(string outFName, double[] wave, WaveData1 wav4)
        {
            List<short> aBin = new List<short>();
            for (int i = 0; i < wave.Count(); i++)
            {
                aBin.Add((short)wave[i]);
            }
            BinaryWriter bw = new BinaryWriter(File.Open(outFName, FileMode.Create));
            if (!TextwavHeaderWrite(bw, wav4))
            {
                return false;
            }
            for (int i = 0; i < aBin.Count; i++) bw.Write((Int16)aBin[i]);
            bw.Close();
            return true;
        }
        #endregion

        #region 配列からWaveFileを作るwavArrangementWrite(ステレオ)
        public static bool wavArrangementWrite(string outFName, double[] L,double[]R, WaveData1 wav4)
        {
            List<short> LBin = new List<short>();
            List<short> RBin = new List<short>();
            for (int i = 0; i < L.Count(); i++)
            {
                LBin.Add((short)L[i]);
                RBin.Add((short)R[i]);
            }
            BinaryWriter bw = new BinaryWriter(File.Open(outFName, FileMode.Create));
            if (!TextwavHeaderWrite(bw, wav4))
            {
                return false;
            }
            for (int i = 0; i < LBin.Count; i++)
            {
                bw.Write((Int16)LBin[i]);
                bw.Write((Int16)RBin[i]);
            } 
            bw.Close();
            return true;
        }
        #endregion

        #region TextFileから波形データを書き込むwavTextWrite
        public static bool wavTextWrite(string outFName, List<short> aBin, WaveData1 wav2)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(File.Open(outFName, FileMode.Create));
                if (!TextwavHeaderWrite(bw, wav2))
                {
                    //MessageBox.Show("wavHeaderWriteでエラー発生", "エラー", MessageBoxButtons.OK);
                    return false;
                }
                for (int i = 0; i < aBin.Count; i++) bw.Write((Int16)aBin[i]);
                bw.Close();
            }
            catch (EndOfStreamException)
            {
                //MessageBox.Show("ファイルの終端に達しました.", "エラー", MessageBoxButtons.OK);
            }
            catch (SystemException)
            {
                //MessageBox.Show(Ex.Message, "エラー", MessageBoxButtons.OK);
            }
            return true;

        }
        #endregion

        #region 出力のWAVファイルに波形を書き込むwavWriteメソッド
        public static bool wavWrite(string inFName, string outFName, ref WaveData1 wav)
        {
            try
            {
                if (wav.sizeOfData < 0)
                    return false;

                // サイズ調整
                wav.sizeOfData = (wav.end - wav.start) * wav.wfp.bytesPerSec;   //wav.end-wav.start+1になっていたがなぜ？

                BinaryWriter bw = new BinaryWriter(File.Open(outFName, FileMode.Create));
                //ヘッダ書き込み
                if (!wavHeaderWrite(bw, wav))
                {
                    //MessageBox.Show("wavHeaderWriteでエラー発生", "エラー", MessageBoxButtons.OK);
                    return false;
                }

                BinaryReader br = new BinaryReader(File.OpenRead(inFName));
                br.BaseStream.Position = wav.posOfData;
                br.BaseStream.Position += (wav.start * wav.wfp.bytesPerSec);

                for (long i = 0; i < wav.sizeOfData; i++)
                {
                    byte data = br.ReadByte();
                    bw.Write(data);
                }
                bw.Close();
            }
            catch (EndOfStreamException)
            {
                //MessageBox.Show("ファイルの終端に達しました.", "エラー", MessageBoxButtons.OK);
            }
            catch (SystemException)
            {
                //MessageBox.Show("Ex.Message", "エラー", MessageBoxButtons.OK);

            }
            return true;
        }
        #endregion

        #region ヘッダーを書き込むMonowavHeaderWriteメソッド(Monoファイル生成用)
        public static bool MonowavHeaderWrite(BinaryWriter bw, WaveData1 wav)
        {
            try
            {
                //WAVヘッダー
                bw.Write(wav.wfh.hdrRiff, 0, wav.wfh.hdrRiff.Length);
                wav.wfh.sizeOfFile = (uint)(wav.sizeOfData + 44 - 8);
                bw.Write(wav.wfh.sizeOfFile);
                bw.Write(wav.wfh.hdrWave, 0, wav.wfh.hdrWave.Length);


                //fmt
                byte[] hdrFmt = new byte[4];
                string szFmt = "fmt ";
                UInt32 chankLength = 16;

                hdrFmt = Encoding.UTF8.GetBytes(szFmt);
                bw.Write(hdrFmt, 0, hdrFmt.Length);
                bw.Write(chankLength);

                bw.Write(wav.wfp.formatTag);
                wav.wfp.channels = 1;  //1だとモノラル 2だとステレオ
                bw.Write(wav.wfp.channels);
                bw.Write(wav.wfp.samplesPerSec);
                wav.wfp.bytesPerSec >>= 1;
                bw.Write(wav.wfp.bytesPerSec);
                wav.wfp.blockAlign >>= 1;
                bw.Write(wav.wfp.blockAlign);
                bw.Write(wav.wfp.bitsPerSample);

                //data
                bw.Write(wav.chank.hdrFmtData, 0, wav.chank.hdrFmtData.Length);
                wav.chank.sizeOfFmtData >>= 1;
                bw.Write(wav.chank.sizeOfFmtData);

            }
            catch (SystemException Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            return true;
        }
        #endregion

        #region 出力のWAVファイルに波形を書き込むMonowavWriteメソッド
        public static bool MonowavWrite(string inFName, string outFName, WaveData1 wav, long dataPos)
        {
            try
            {
                if (wav.sizeOfData < 0) return false;

                BinaryWriter bw = new BinaryWriter(File.OpenWrite(outFName));


                if (!MonowavHeaderWrite(bw, wav))
                {
                    Console.WriteLine("wavHeaderWriteでエラー発生.");
                    return false;
                }

                BinaryReader br = new BinaryReader(File.OpenRead(inFName));
                br.BaseStream.Position = dataPos;

                for (long i = 0; i < wav.sizeOfData / (wav.wfp.blockAlign / 2); i++)
                {
                    if (wav.wfp.bitsPerSample == 8)
                    {                               // 8 bits/sampling
                        ushort L = (ushort)br.ReadByte();
                        ushort R = (ushort)br.ReadByte();
                        ushort mix = (ushort)((L + R) / (ushort)2);
                        bw.Write((byte)mix);
                    }
                    else
                    {                               //16 bits/sampling
                        uint L = (uint)br.ReadInt16();
                        uint R = (uint)br.ReadInt16();
                        uint mix = (uint)((L + R) / (uint)2);
                        bw.Write((ushort)mix);
                    }
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("ファイルの終端に達しました.");
            }
            catch (SystemException Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            return true;
        }
        #endregion

        #region 出力のWAVファイルにヘッダを書き込むwavHeaderWriteメソッド
        public static bool wavHeaderWrite(BinaryWriter bw, WaveData1 wav)
        {
            try
            {
                //WAVヘッダー 12 bytes
                bw.Write(wav.wfh.hdrRiff, 0, wav.wfh.hdrRiff.Length);
                wav.wfh.sizeOfFile = (uint)(wav.sizeOfData + 44 - 8);
                bw.Write(wav.wfh.sizeOfFile);
                bw.Write(wav.wfh.hdrWave, 0, wav.wfh.hdrWave.Length);

                //fmt 20 bytes
                byte[] hdrFmt = new byte[4];
                string szFmt = "fmt ";
                UInt32 chankLength = 16;

                hdrFmt = Encoding.UTF8.GetBytes(szFmt);
                bw.Write(hdrFmt, 0, hdrFmt.Length);
                bw.Write(chankLength);

                bw.Write(wav.wfp.formatTag);
                bw.Write(wav.wfp.channels);
                bw.Write(wav.wfp.samplesPerSec);
                bw.Write(wav.wfp.bytesPerSec);
                bw.Write(wav.wfp.blockAlign);
                bw.Write(wav.wfp.bitsPerSample);

                //data 8 bytes
                hdrFmt = new byte[4];
                szFmt = "data";
                hdrFmt = Encoding.UTF8.GetBytes(szFmt);
                bw.Write(hdrFmt, 0, hdrFmt.Length);
                bw.Write((uint)wav.sizeOfData);

            }
            catch (SystemException)
            {
                //MessageBox.Show("Ex.Message", "エラー", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        #endregion

        #region WavTextWriteメソッドで使うTextwavHeaderWriteメソッド
        private static bool TextwavHeaderWrite(BinaryWriter bw, WaveData1 wav)
        {
            try
            {
                //WAVヘッダー 12 bytes
                bw.Write(wav.wfh.hdrRiff, 0, wav.wfh.hdrRiff.Length);
                //wav.wfh.sizeOfFile = (uint)wav.sizeOfData / 8 - 8;
                wav.wfh.sizeOfFile = (uint)(wav.dataChank.sizeOfFmtData + 44 - 8);
                bw.Write(wav.wfh.sizeOfFile);
                bw.Write(wav.wfh.hdrWave, 0, wav.wfh.hdrWave.Length);


                //fmt 20 bytes
                bw.Write(wav.chank.hdrFmtData, 0, wav.chank.hdrFmtData.Length);
                bw.Write(wav.chank.sizeOfFmtData);

                bw.Write(wav.wfp.formatTag);
                bw.Write(wav.wfp.channels);
                bw.Write(wav.wfp.samplesPerSec);
                bw.Write(wav.wfp.bytesPerSec);
                bw.Write(wav.wfp.blockAlign);
                bw.Write(wav.wfp.bitsPerSample);

                //data 8 bytes
                bw.Write(wav.dataChank.hdrFmtData, 0, wav.dataChank.hdrFmtData.Length);
                bw.Write(wav.dataChank.sizeOfFmtData);
            }
            catch (SystemException)
            {
                //MessageBox.Show(Ex.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region ファイルを切り取る時開始位置と終了位置のチェックを行うcheckRangeメソッド
        public static bool checkRange(ref WaveData1 wav)
        {
            if (wav.start * wav.wfp.bytesPerSec > wav.sizeOfData)
            {
                // MessageBox.Show("開始位置がファイルの大きさを超えています.", "エラー", MessageBoxButtons.OK);

                return false;
            }

            if ((wav.end + 1) * wav.wfp.bytesPerSec > wav.sizeOfData)
            {
                //MessageBox.Show("終了位置がファイルの大きさを超えていたので終了をファイルの最後に調整しました", "エラー", MessageBoxButtons.OK);
                wav.end = (int)((wav.sizeOfData / wav.wfp.bytesPerSec) - 1);
            }
            return true;
        }
        #endregion

        #region 出力WAVEファイルの波形をうちこむdumpDataメソッド(モノラル)
        public static bool dumpData(string FName, long dataPos, WaveData1 wav, long dataLength, double[] data)
        {
            try
            {
                if (dataLength < 0)
                    return false;
                BinaryReader br = new BinaryReader(File.OpenRead(FName));
                br.BaseStream.Position = dataPos;

                for (long i = 0; i < dataLength / (wav.wfp.blockAlign); i++)
                {
                    if (wav.wfp.bitsPerSample == 8)
                    {                               // 8 bits/sampling
                        byte D = br.ReadByte();
                        data[i] = D;
                    }
                    else
                    {                               //16 bits/sampling
                        short D = br.ReadInt16();
                        data[i] = D;
                    }
                }
                br.Close();
            }
            catch (EndOfStreamException)
            {
                //MessageBox.Show("ファイルの終端に達しました", "お知らせ", MessageBoxButtons.OK);
                return false;
            }
            catch (SystemException)
            {
                //MessageBox.Show("dumpdataで例外発生", "エラー", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        #endregion

        #region 出力WAVEファイルの波形をうちこむdumpDataメソッド(ステレオ)
        public static bool StereodumpData(string FName, long dataPos, WaveData1 wav, long dataLength, double[] L,double[]R)
        {
            try
            {
                if (dataLength < 0)
                    return false;
                BinaryReader br = new BinaryReader(File.OpenRead(FName));
                br.BaseStream.Position = dataPos;
                int count = 0;
                for (long i = 0; i < dataLength ; i++)
                {
                    if (wav.wfp.bitsPerSample == 8)
                    {                               // 8 bits/sampling
                        if(i%4==0)
                        {
                            byte D = br.ReadByte();
                            L[count]=D;
                        }
                        else if(i%2==0)
                        {
                            byte D = br.ReadByte();
                            R[count]=D;
                            count++;
                        }
                        
                    }
                    else
                    {                               //16 bits/sampling
                        if (i % 4 == 0)
                        {
                            short D = br.ReadInt16();
                            L[count]=D;
                        }
                        else if(i%2==0)
                        {
                            short D = br.ReadInt16();
                            R[count]=D;
                            count++;
                        }
                    }
                }
                br.Close();
            }
            catch (EndOfStreamException)
            {
                //MessageBox.Show("ファイルの終端に達しました", "お知らせ", MessageBoxButtons.OK);
                return false;
            }
            catch (SystemException)
            {
                //MessageBox.Show("dumpdataで例外発生", "エラー", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        #endregion

        #region ファイルを読み込んでstring型のListに格納するReadFileメソッド
        public static List<string> ReadFile(string InFileName)
        {

            List<string> aText = new List<string>();
            try
            {
                string line;
                StreamReader sr = new StreamReader(InFileName);

                while ((line = sr.ReadLine()) != null) aText.Add(line);

                sr.Dispose();
            }
            catch (SystemException Ex)
            {
                Console.WriteLine(Ex.Message);
                aText = null;
            }
            return aText;
        }
        #endregion

        #region ReadFileで読み込んだstring型Listをshort型にするText2binメソッド
        public static List<short> Text2bin(List<string> aText)
        {
            List<short> aBin = new List<short>();
            string line;
            Int16 Lch = 0, Rch = 0;

            try
            {
                for (int i = 0; i < aText.Count; i++)
                {
                    line = (string)aText[i];
                    if (!GetLR(line, ref Lch, ref Rch)) return null;
                    aBin.Add(Lch);
                    aBin.Add(Rch);
                }
            }
            catch (SystemException)
            {
                //MessageBox.Show(Ex.Message, "エラー", MessageBoxButtons.OK);
                return aBin = null;
            }
            return aBin;
        }
        #endregion

        #region Text2binを使うときに数値を確認するGetLRメソッド
        public static bool GetLR(string text, ref Int16 Lch, ref Int16 Rch)
        {
            bool rVal = true;
            int left, right;

            try
            {
                char[] delimiter = { ',', '\t' };       // 区切り記号
                string[] split = text.Split(delimiter); // スプリット
                if (split.Length != 2)
                    return false;

                left = (int)Convert.ToDouble(split[0]);
                right = (int)Convert.ToDouble(split[1]);
                if (left < -32768 || left > 32767 || right < -32768 || right > 32767)
                {
                    //MessageBox.Show("入力データが不正です!!!", "エラー", MessageBoxButtons.OK);
                    return false;
                }

                Lch = (Int16)left;
                Rch = (Int16)right;
            }
            catch (SystemException)
            {
                //MessageBox.Show(Ex.Message, "エラー", MessageBoxButtons.OK);
                return rVal = false;
            }
            return rVal;
        }
        #endregion

        #region 波形をshort型リストに格納するにするListAdd
        public static void ListAdd(List<short> aBin, double[] Wave)
        {
            for (int i = 0; i < Wave.Length; i++)
            {
                aBin.Add((short)Wave[i]); //左
                aBin.Add((short)Wave[i]); //右
            }
        }
        #endregion

        #region 高速フーリエ変換
        //FFTの関数
        public static double[] FastFourierTransform(double[] inputRe, int bitSize)
        {
            int dataSize = 1 << bitSize;
            double[] inputIm = new double[dataSize];
            double[] outData = new double[dataSize/2];
            double[] outRe;
            double[] outIm;
   
            FFT.FFT_HT(inputRe, inputIm, out outRe, out outIm, bitSize);

            for (int i = 0; i < dataSize/2; i++)
            {
                outData[i] = Math.Sqrt(outRe[i] * outRe[i] + outIm[i] * outIm[i]);
            }

            return outData;
        }


        //FFTの実際の処理(未完成)
        public static void FFT_main(double[] inputRe, double[] inputIm, out double[] outputRe, out double[] outputIm, int bitSize)
        {
            int dataSize = 1 << bitSize;
            int[] reverseBitArray = bitScrollArray(dataSize);

            outputRe = new double[dataSize];
            outputIm = new double[dataSize];

            //バタフライ演算のための置き換え
            for (int i = 0; i < dataSize; i++)
            {
                outputRe[i] = inputRe[reverseBitArray[i]];
                outputIm[i] = inputIm[reverseBitArray[i]];
            }
            int butterflyDistance;
            int numType;
            int butterflySize;

            double wRe, wIm, uRe, uIm;

            int jp;
            double tempRe, tempIm;
            double tempWRe, tempWIm;

            //バタフライ演算
            for (int stage = 1; stage <= bitSize; stage++)
            {
                butterflyDistance = 1 << stage;
                numType = butterflyDistance >> 1;
                butterflySize = butterflyDistance >> 1;

                wRe = 1.0;
                wIm = 0.0;
                uRe = Math.Cos(Math.PI / butterflySize);
                uIm = -Math.Sin(Math.PI / butterflySize);

                for (int type = 0; type < numType; type++)
                {
                    for (int j = type; j < dataSize; j += butterflyDistance)
                    {
                        jp = j + butterflyDistance;
                        tempRe = outputRe[jp] * wRe - outputIm[jp] * wIm;
                        tempIm = outputRe[jp] * wIm + outputIm[jp] * wRe;
                        outputRe[jp] = outputRe[j] - tempRe;
                        outputIm[jp] = outputIm[j] - tempIm;
                        outputRe[j] += tempRe;
                        outputIm[j] += tempIm;
                    }
                    tempWRe = wRe * uRe - wIm * uIm;
                    tempWIm = wRe * uIm + wIm * uRe;
                    wRe = tempWRe;
                    wIm = tempWIm;
                }
            }

        }

        //ビットを左右反転した配列を返す(FFT用)
        private static int[] bitScrollArray(int arraySize)
        {
            int[] reBitArray = new int[arraySize];
            int arraySizeHarf = arraySize >> 1;
            reBitArray[0] = 0;
            for (int i = 1; i < arraySize; i <<= 1)
            {
                for (int j = 0; j < i; j++)
                {
                    reBitArray[j + i] = reBitArray[j] + arraySizeHarf;
                }
                arraySizeHarf >>= 1;
            }
            return reBitArray;
        }
        #endregion

        #region テンポ解析(簡易版)
        public static int analysisTenpo(Double[] data,int length)
        {
            int SampleOfFlame = 512;
            int NumOfFlame = length / SampleOfFlame;
            double[] Vol = new double[NumOfFlame];
            double[] D = new double[NumOfFlame];
            double[] a = new double[181];
            double[] b = new double[181];
            double[] r = new double[181];

            //フレームごとの音量を求める
            for (int i = 0; i < NumOfFlame; i++)
            {
                for (int j = 0; j < SampleOfFlame; j++)
                {
                    Vol[i] += data[i * SampleOfFlame + j] * data[i * SampleOfFlame + j];
                }
                Vol[i] = Math.Sqrt(Vol[i] / SampleOfFlame);
            }

            //隣り合うフレームの音量の増加を求める
            for (int i = 1; i < NumOfFlame; i++)
            {
                if (Vol[i] - Vol[i - 1] < 0) D[i] = 0;
                else D[i] = Vol[i] - Vol[i - 1];
            }

            //増加量の時間変化の周波数成分を求める
            for (int tempo = 60; tempo <= 240; tempo++)
            {
                for (int i = 0; i < NumOfFlame; i++)
                {
                    double win = han_window(i, NumOfFlame);
                    a[tempo - 60] += D[i] * Math.Cos(2.0 * Math.PI * tempo / 60 * i / (44100 / SampleOfFlame)) * win;
                    b[tempo - 60] += D[i] * Math.Sin(2.0 * Math.PI * tempo / 60 * i / (44100 / SampleOfFlame)) * win;
                }
                a[tempo-60] /= NumOfFlame;
                b[tempo-60] /= NumOfFlame;
                r[tempo - 60] = Math.Sqrt(a[tempo-60] * a[tempo-60] + b[tempo-60] * b[tempo-60]);
            }

            //周波数成分のピークを検出する
            double max = 0;
            int maxTempo=50;
            for (int tempo = 60; tempo <= 240; tempo++)
            {
                if (max <= r[tempo - 60])
                {
                    max = r[tempo - 60];
                    maxTempo = tempo;
                }
            }
            double theta = Math.Atan2(b[maxTempo - 60], a[maxTempo - 60]);
            if (theta < 0) theta += 2.0 * Math.PI;

            double start_time = theta / (2.0 * Math.PI * maxTempo / 60);

            return maxTempo;
        }

        public static double han_window(int i, int N)
        {
            return 0.5 - 0.5 * Math.Cos(2 * Math.PI * i / N);
        }
        #endregion

        #region 音声ファイル読み込み(モノラル)
        public static double[] ReadMonoWavFile(string fileName)
        {
            WaveData1 wav = new WaveData1();
            wav.posOfData = WaveMethod.wavHdrRead(fileName, wav);  //①
            long dataLength = (wav.sizeOfData / wav.wfp.blockAlign);　//②
            double[] Data = new double[dataLength];
            if (!WaveMethod.dumpData(fileName, wav.posOfData, wav, wav.sizeOfData, Data))　//③
            {
                return null;
            }
            return Data;
        }
        #endregion

        #region 音声ファイル読み込み(ステレオ)
        public static void ReadStereoWavFile(string fileName,out double[] L ,out double[] R)//
        {
            WaveData1 wav = new WaveData1();
            wav.posOfData = WaveMethod.wavHdrRead(fileName, wav);  //①
            long dataLength = (wav.sizeOfData / wav.wfp.blockAlign);　//②
            double[] dataL = new double[dataLength];
            double[] dataR = new double[dataLength];
            if (!WaveMethod.StereodumpData(fileName, wav.posOfData, wav, wav.sizeOfData, dataL,dataR))　//③
            {
                Console.WriteLine("エラー");
            }
            L = dataL;
            R = dataR;
            //Tuple<double[], double[]> data = Tuple.Create<double[], double[]>(dataL, dataR);
            //return data;
        }
        #endregion

        #region ハニング窓
        private static double HanningWindow(int i, int N)
        {
            return 0.5 - 0.5 * Math.Cos(2 * Math.PI * i / N);
        }
        #endregion

        #region フーリエ変換
        public static double[][] FastFourierTransform2(double[] data,int bitSize)
        {
            int dataSize = (int) Math.Pow(2,bitSize);
            double[] input = new double[dataSize];
            List<double[]> histFFT = new List<double[]>();
            for (int i = 0; i < 2 * (data.Length / dataSize - 1.0); i++)
            {
                for (int j = 0; j < dataSize; j++)
                {
                    input[j] = data[i * dataSize / 2 + j] * HanningWindow(j, dataSize);
                }
                double[] x = WaveMethod.FastFourierTransform(input, bitSize);
                histFFT.Add(x);
            }
            return histFFT.ToArray();
        }
        #endregion

        #region DCT変換
        static double[][] dct(double[][] f,int dimension)
        {
            int n = dimension;
            int createdataSize = f.Count();
            double[][] C = new double[createdataSize][];
            for(int i=0;i<createdataSize;i++)
            {
                C[i] = new double[n];
            }
            double cos;
            for (int k = 0; k < createdataSize; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    C[k][0] += f[k][i] / Math.Sqrt(n);
                }

                for (int i = 1; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {
                        cos = Math.Sqrt(2.0 / n) * Math.Cos((2 * j + 1) * i * Math.PI / 2.0 / n);
                        C[k][i] += f[k][j] * cos;
                    }
                }
            }
            return C;
        }
        #endregion

        #region Hz→Mel変換
        public static double Hz2Mel(double f)
        {
            return 1127.01048 * Math.Log(f / 700.0 + 1.0);
            //return 1127.01048 * Math.Log(f / 1400.0 + 1.0);
        }
        #endregion

        #region Mel→Hz変換
        public static double Mel2Hz(double m)
        {
            return 700.0 * (Math.Exp(m / 1127.01048) - 1.0);
            //return 1400.0 * (Math.Exp(m / 1127.01048) - 1.0);
        }
        #endregion

        #region MFCC
        public static double[][] MFCC(double[] wave,int bitSize,int samplef)
        {
            double[][] fft;
            const int melfilternum = 20;
            int nnum = (int)Math.Pow(2, bitSize) / 2;
            //プリエンファシスフィルタを掛けて波形の高域成分を強調する
            wave = preEmphasis(wave);
            //ハミング窓を掛けてフーリエ変換
            fft = FastFourierTransform2(wave, bitSize);
            //メルフィルターを構成して掛け合わせる
            double[][] mfcc = new double[fft.Count()][];
            for(int i=0;i<fft.Count();i++)
            {
                mfcc[i]=new double[melfilternum];
            }
            double[][] melfilter = MelFilterBank(samplef, bitSize);
            for (int i = 0; i < fft.Count();i++ )
            {
                for(int j=0;j<melfilternum;j++)
                {
                    for(int k=0;k<nnum;k++)
                    {
                        mfcc[i][j] += fft[i][k] * melfilter[j][k];
                    }
                    mfcc[i][j] = Math.Log10(mfcc[i][j]);
                }
            }
            //離散コサイン変換
            mfcc = dct(mfcc, melfilternum);
            return mfcc;

        }
        #endregion

        #region ΔMFCC
        public static double[][] deltaMFCC(double[][] MFCC)
        {
            //前後2フレーム,計5フレームのMFCCを用いて単回帰
            //MFCCの時間変化の回帰係数の値
            //メルケプストラム係数を目的変数 y、フレーム数（時間経過）を説明変数 x としたとき、回帰分析によって推定される関係式 y = ax + b の a の値（ x に対する y の変化の傾き）
            double[][] deltamfcc = new double[MFCC.Count()-4][];
            const int dimension = 12;
            for (int i = 0; i < MFCC.Count() - 4;i++ )
            {
                deltamfcc[i] = new double[dimension];
            }
            for (int k = 0; k < dimension; k++)
            {
                for (int i = 2; i < MFCC.Count() - 2; i++)
                {
                    double[] y = new double[5];
                    double[] x = { 0, 1, 2, 3, 4 };
                    double meanx=0.0;
                    double meany=0.0;
                    double numerator = 0.0;//分子
                    double denominator = 0.0;//分母
                    for (int j = 0; j < 5; j++)
                    {
                        y[j] = MFCC[i + j - 2][k];
                        meanx+=x[j];
                        meany+=y[j];
                    }
                    meanx/=5;
                    meany/=5;
                    for(int j=0;j<5;j++)
                    {
                        denominator += Math.Pow(meanx - x[j], 2);
                        numerator += (x[j]-meanx) * (y[j]-meany);
                    }
                    deltamfcc[i-2][k] = numerator / denominator;
                } 
            }
            return deltamfcc;
        }
        #endregion

        #region プリエンファシスフィルタ
        static double[] preEmphasis(double[] signal)
        {
            double[] wave = new double[signal.Count()];
            wave[0] = signal[0];
            for(int i=1;i<signal.Count();i++)
            {
                wave[i] = signal[i] - 0.97 * signal[i - 1];
            }
                return wave;
        }
        #endregion

        #region メルフィルタバンク
        static double[][] MelFilterBank(int samplef,int bitSize)
        {
            int melfilternum = 20;//メルフィルタバンクの数
            int nmax = (int)Math.Pow(2, bitSize) / 2;//周波数インデックスの最大数
            int fmax = samplef / 2;//ナイキスト周波数(Hz)
            double melmax = Hz2Mel(fmax);//ナイキスト周波数（mel）
            double df = samplef / (2 * nmax);//周波数インデックス1あたりのHz幅
            //メル尺度における各フィルタの中心周波数を求める
            double dmel = melmax / (melfilternum + 1);
            double[] melcenter = new double[melfilternum];
            for(int i=0;i<melfilternum;i++)
            {
                melcenter[i] = (i + 1) * dmel;
            }
            //各フィルタの中心周波数をHzに変換
            double[] fcenter = new double[melfilternum];
            for(int i=0;i<melfilternum;i++)
            {
                fcenter[i] = Mel2Hz(melcenter[i]);
            }
            //各フィルタの開始位置の周波数をHzに変換
            double[] fstart = new double[melfilternum];
            fstart[0] = 0;
            for(int i=1;i<melfilternum;i++)
            {
                fstart[i] = Mel2Hz(melcenter[i - 1]);
            }
            //各フィルタの終了位置の周波数をHzに変換
            double[] fend = new double[melfilternum];
            fend[19] = fmax;
            for(int i=0;i<melfilternum-1;i++)
            {
                fend[i] = Mel2Hz(melcenter[i + 1]);
            }
            //Hz領域での各インデックスの周波数を算出
            double[] findex = new double[nmax];
            for(int i=0;i<nmax;i++)
            {
                findex[i] = i * df;
            }
            //Mel領域のインデックス周波数をHz領域で最も近い位置に割り当てる
            double[] fstartindex = new double[melfilternum];
            double[] fcenterindex = new double[melfilternum];
            double[] fendindex = new double[melfilternum];
            for(int i=0;i<melfilternum;i++)
            {
                double[] startsub = new double[nmax];
                double[] centersub = new double[nmax];
                double[] endsub = new double[nmax];
                for(int j=0;j<nmax;j++)
                {
                    startsub[j] = Math.Abs(fstart[i] - findex[j]);
                    centersub[j] = Math.Abs(fcenter[i] - findex[j]);
                    endsub[j] = Math.Abs(fend[i] - findex[j]);
                }
                fstartindex[i] = startsub.MinIndex();
                fcenterindex[i] = centersub.MinIndex();
                fendindex[i] = endsub.MinIndex();
            }
            //メルフィルタバンクの構成
            double[][] melfilterbank = new double[melfilternum][];
            for(int i=0;i<melfilternum;i++)
            {
                melfilterbank[i] = new double[nmax];
            }
            for(int i=0;i<melfilternum;i++)
            {
                for(int j=0;j<nmax;j++)
                {
                    if (j < fstartindex[i])
                    {
                        melfilterbank[i][j] = 0;
                    }
                    else if(fstartindex[i]<=j)
                    {
                        melfilterbank[i][j] = (j - fstartindex[i]) * (1.0 / (fcenterindex[i] - fstartindex[i]));
                    }
                    else if(fcenterindex[i]<=j)
                    {
                        melfilterbank[i][j] = 1.0 - (j - fcenterindex[i]) * (1.0 / (fendindex[i] - fcenterindex[i]));
                    }
                    else if(fendindex[i]<=j)
                    {
                        melfilterbank[i][j] = 0;
                    }
                }
            }
            return melfilterbank;
        }
        #endregion
    }
}
