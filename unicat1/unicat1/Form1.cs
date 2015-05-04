using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using InoueLab;

namespace unicat1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Image back = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\back.png");
        Image road = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\road.png");
        Image fish = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\fish.png");
        Image cat = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\cat.png");



        int catposx;
        int catposy;

        public Form1()
        {
            InitializeComponent();
            RandomMT rand = new RandomMT();
            comboBox1.Items.Add("stage select");
            comboBox1.Items.Add("stage1");
            comboBox1.Items.Add("stage2");
            comboBox1.SelectedIndex = 0;

            ////画像ファイルを読み込んで、Imageオブジェクトを作成する
            System.Drawing.Image command1 = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\up.png");
            System.Drawing.Image command2 = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\left.png");
            System.Drawing.Image command3 = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\right.png");
            System.Drawing.Image command4 = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\catch.png");
            System.Drawing.Image commandpanel = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\commandpanel.png");
            ////画像を表示する
            pictureBox3.Image = commandpanel;
            pictureBox4.Image =command1;
            pictureBox5.Image = command2;
            pictureBox6.Image = command3;
            pictureBox7.Image = command4;
           

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
           
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            g = Graphics.FromImage(canvas);
           
           
            //画像ファイルを読み込んで、Imageオブジェクトとして取得する
            //Image img = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\paneru.png");
            int xmax = 6, ymax = 6;

            //ランダム
            //for (int i = 0; i < xmax ; i++)
            //{
            //    for (int j = 0; j < ymax; j++)
            //    {
            //        int a = rand.Int(10);

            //        if (a < 6) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
            //        else g.DrawImage(back, i * back.Width, j * back.Height, back.Width, back.Height);
            //    }
            //}
     

            if (comboBox1.SelectedIndex==0)
            {
                //stage1
                for (int i = 0; i < xmax; i++)
                {
                    for (int j = 0; j < ymax; j++)
                    {
                        if (j == 0) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                        else if (i == 5) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                        else g.DrawImage(back, i * back.Width, j * back.Height, back.Width, back.Height);
                    }
                }

            }

            else if (comboBox1.SelectedIndex == 2)
           // stage2
                for (int i = 0; i < xmax; i++)
                {
                    for (int j = 0; j < ymax; j++)
                    {
                        if (j == 0 && i <= 2) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                        else if (i == 2 && j == 1) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                        else if (j == 2 && i == 2 || j == 2 && i == 3) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                        else if (j == 3 && i == 3 || j == 3 && i == 4 || j == 3 && i == 5) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                        else if (j == 4 && i == 5) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                        else if (j == 5 && i == 5) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);
                        else g.DrawImage(back, i * back.Width, j * back.Height, back.Width, back.Height);
                    }
                }

            catposx = 3;
            catposy = 3;
            //お魚
            g.DrawImage(fish, 1 * fish.Width, 1 * fish.Height, fish.Width, fish.Height);
            //猫
            g.DrawImage(cat, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);


            //Graphicsオブジェクトのリソースを解放する
            //g.Dispose();

            //PictureBox1に表示する
            pictureBox1.Image = canvas;  

        }
      
        //ネコがある方向に一つ進む
        public void catmove(string direction)
        {
            int xmove = 0, ymove = 0;
            if (direction == "up")
            {
                xmove = 0; ymove = -1;
            }
            if (direction == "down")
            {
                xmove = 0; ymove = 1;

            }
            if (direction == "right")
            {
                xmove = 1; ymove = 0;

            }
            if (direction == "left")
            {
                xmove = -1; ymove = 0;

            }

            for (int i = 0; i <= 100; i = i + 5)
            {

                g.DrawImage(road, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
                g.DrawImage(cat, catposx * cat.Width + xmove*i, catposy * cat.Height + ymove*i, cat.Width, cat.Height);
                pictureBox1.Refresh();
                Thread.Sleep(1);
            }
            catposx +=xmove;
            catposy += ymove;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //左キーが押されているか調べる
            if ((keyData & Keys.KeyCode) == Keys.Up)
            {
                catmove("up");
                return true;
            }
            if ((keyData & Keys.KeyCode) == Keys.Down)
            {
                catmove("down");

                return true;
            } if ((keyData & Keys.KeyCode) == Keys.Right)
            {
                catmove("right");

                return true;
            } if ((keyData & Keys.KeyCode) == Keys.Left)
            {
                catmove("left");

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void left_Click(object sender, EventArgs e)
        {
            catmove("left");
        }

        private void up_Click(object sender, EventArgs e)
        {
            catmove("up");

        }

        private void down_Click(object sender, EventArgs e)
        {
            catmove("down");
        }

        private void right_Click(object sender, EventArgs e)
        {
            catmove("right");
        }


    }
}
