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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeComponent();
            //画像ファイルを読み込んで、Imageオブジェクトを作成する
            System.Drawing.Image img = System.Drawing.Image.FromFile(@"C\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\paneru.png");

            //画像を表示する
            pictureBox1.Image = img;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
