using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wave
{
    #region Wavデータのクラス(読み込み用)
    public class WaveData1
    {
        public tagSWaveFileHeader wfh = new tagSWaveFileHeader();  // WAVEファイルのヘッダ構造体
        public tagChank chank = new tagChank();          // チャンク構造体
        public tagWaveFormatPcm wfp = new tagWaveFormatPcm();    // formatの構造体
        public tagChank dataChank = new tagChank();
        public long sizeOfData;         // 実際のサイズ
        public long posOfData;          // 入力WAVの data 開始位置
        public int start;               // 開始秒
        public int end;                 // 終了秒


        #region コンストラクタ
        public WaveData1()
        {
            this.init();
            this.wfh.init();
            this.chank.init();
        }
        #endregion

        public void init()
        {
            sizeOfData = -1;            // 'fmt ' or 'data'
        }


        #region ヘッダを格納するtagSWaveFileHeader
        public class tagSWaveFileHeader
        {
            public byte[] hdrRiff;          // 'RIFF'
            public uint sizeOfFile;         // ファイルサイズ - 8
            public byte[] hdrWave;          // 'WAVE'

            public void init()
            {
                hdrRiff = new byte[4];      // 'RIFF'
                hdrWave = new byte[4];      // 'WAVE'
            }
        }
        #endregion

        #region チャンクを格納するtagChank
        public class tagChank
        {
            public byte[] hdrFmtData;       // 'fmt ' or 'data'
            public uint sizeOfFmtData;      // sizeof(PCMWAVEFORMAT) or
            //        Waveデーターサイズ
            public void init()
            {
                hdrFmtData = new byte[4];   // 'fmt ' or 'data'
            }
        }
        #endregion

        #region フォーマットを格納するtagWaveFormatPcm
        public class tagWaveFormatPcm
        {
            public ushort formatTag;        // WAVE_FORMAT_PCM
            public ushort channels;         // number of channels
            public uint samplesPerSec;      // sampling rate
            public uint bytesPerSec;        // samplesPerSec * channels//       * (bitsPerSample/8)
            public ushort blockAlign;       // block align
            public ushort bitsPerSample;    // bits per sampling
        }
        #endregion

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
    #endregion

    #region Wavデータのクラス(書き込み用)
    public class WaveData2 : WaveData1
    {
        public WaveData2()
        {
            datainit();
        }
        public void datainit()
        {
            wfh = new tagSWaveFileHeader();
            wfh.hdrRiff = new byte[4];      // 'RIFF'
            wfh.hdrRiff = Encoding.UTF8.GetBytes("RIFF");
            wfh.hdrWave = new byte[4];      // 'WAVE'
            wfh.hdrWave = Encoding.UTF8.GetBytes("WAVE");

            chank = new tagChank();
            chank.hdrFmtData = new byte[4];
            chank.hdrFmtData = Encoding.UTF8.GetBytes("fmt ");
            chank.sizeOfFmtData = 16;

            wfp = new tagWaveFormatPcm();
            wfp.formatTag = 1;              // WAVE_FORMAT_PCM
            wfp.channels = 1;      // number of channels
            wfp.samplesPerSec = 44100;      // sampling rate
            wfp.bytesPerSec = wfp.samplesPerSec * wfp.channels * 2;
            wfp.blockAlign = (ushort)(wfp.channels * 2);  // block align
            wfp.bitsPerSample = 16;         // bits per sampling

            dataChank = new tagChank();
            dataChank.hdrFmtData = new byte[4];
            dataChank.hdrFmtData = Encoding.UTF8.GetBytes("data");

            sizeOfData = -1;                // 'fmt ' or 'data'
        }

    }
    #endregion
}
