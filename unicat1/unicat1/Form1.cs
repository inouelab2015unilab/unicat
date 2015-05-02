using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using InoueLab;

namespace unicat1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            RandomMT rand = new RandomMT();



            InitializeComponent();
            ////画像ファイルを読み込んで、Imageオブジェクトを作成する
            //System.Drawing.Image img = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\paneru.png");
            ////画像を表示する
            //pictureBox1.Image = img;

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            //画像ファイルを読み込んで、Imageオブジェクトとして取得する
            //Image img = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\paneru.png");
            int xmax = 6, ymax = 6;
            Image back = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\back.png");
            Image road = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\road.png");
            Image fish = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\fish.png");
            Image cat = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\cat.png");

            for (int i = 0; i < xmax ; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    int a = rand.Int(10);

                    if (a < 6) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                    else g.DrawImage(back, i * back.Width, j * back.Height, back.Width, back.Height);
                }
            }

            //お魚
            g.DrawImage(fish, 1 * fish.Width, 1 * fish.Height, fish.Width, fish.Height);
            //猫
            g.DrawImage(cat, 5 * cat.Width, 5 * cat.Height, cat.Width, cat.Height);

            //Graphicsオブジェクトのリソースを解放する
            g.Dispose();
            //PictureBox1に表示する
            pictureBox1.Image = canvas;
        }


        public void catmove()
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            catmove();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
