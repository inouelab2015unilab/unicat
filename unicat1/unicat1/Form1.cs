using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unicat1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            InitializeComponent();
            //画像ファイルを読み込んで、Imageオブジェクトを作成する
            System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\Users\晴子\Documents\GitHub\unicat\unicat1\unicat1\bin\Debug\paneru.png");

            //画像を表示する
            pictureBox1.Image = img;
        }
    }
}
