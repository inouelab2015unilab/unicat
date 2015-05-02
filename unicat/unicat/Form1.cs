using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unicat
{
    public partial class Form1 : Form
    {

          Bitmap bmp;
              Graphics g;
            //   RandomMT rand;
              int blockSize = 50;
              int blockWidth;
              int blockHeight;
              bool[,] wall;



        public Form1()
        {
            InitializeComponent();
       
            //width = this.pictureBox1.Width;//取得
            //height = this.pictureBox1.Height;//取得
            bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            blockWidth = pictureBox1.Width / blockSize;
            blockHeight = pictureBox1.Height / blockSize;
            wall = new bool[blockWidth, blockHeight];
            //pictureBoxに，bmpをはりつける
            this.pictureBox1.Image = bmp;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
