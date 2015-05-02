using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InoueLab;
using System.Threading;

namespace unicat1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Image cat = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\cat.png");
        Image back = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\back.png");
        Image road = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\road.png");
        Image fish = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\fish.png");
        int catposx ;
        int catposy ;
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
            g = Graphics.FromImage(canvas);

            //画像ファイルを読み込んで、Imageオブジェクトとして取得する
            //Image img = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\paneru.png");
            int xmax = 6, ymax = 6;

           

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

            catposx = 5;
            catposy = 5;
            //猫
            g.DrawImage(cat, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);

            //Graphicsオブジェクトのリソースを解放する
            //g.Dispose();
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
            g.DrawImage(road, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
            catposx = catposx - 1;
            g.DrawImage(cat, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
            pictureBox1.Refresh();
        }

        private void up_Click(object sender, EventArgs e)
        {
            g.DrawImage(road, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
            catposy = catposy - 1;
                g.DrawImage(cat, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
                pictureBox1.Refresh();
                //Thread.Sleep(500);
            
        }

        private void down_Click(object sender, EventArgs e)
        {
            g.DrawImage(road, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
            catposy = catposy + 1;
            g.DrawImage(cat, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
            pictureBox1.Refresh();
        }

        private void right_Click(object sender, EventArgs e)
        {
            g.DrawImage(road, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
            catposx = catposx + 1;
            g.DrawImage(cat, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
            pictureBox1.Refresh();
        }

    }
}
