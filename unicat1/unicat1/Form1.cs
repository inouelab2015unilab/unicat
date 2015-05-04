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
using System.IO;

namespace unicat1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Image back = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\back.png");
        Image road = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\road.png");
        Image fish = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\fish.png");
        Image cat = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\cat.png");
        int xmax = 6, ymax = 6;


        int catposx;
        int catposy;
        List<int[,]> boardlist = new List<int[,]>();
        int buttoncount=0;
        PictureBox[] picarray = new PictureBox[12];
        ////画像ファイルを読み込んで、Imageオブジェクトを作成する
        System.Drawing.Image command1 = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\up.png");
        System.Drawing.Image command2 = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\left.png");
        System.Drawing.Image command3 = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\right.png");
        System.Drawing.Image command4 = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\catch.png");
        System.Drawing.Image commandpanel = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\commandpanel.png");

        public Form1()
        {
            InitializeComponent();
            RandomMT rand = new RandomMT();
            comboBox1.Items.Add("stage1");
            comboBox1.Items.Add("stage2");

            comboBox1.SelectedIndex = 0;

           
               picarray[0] = pictureBox2;
               picarray[1] = pictureBox3;
               picarray[2] = pictureBox4;
               picarray[3] = pictureBox5;
               picarray[4] = pictureBox6;
               picarray[5] = pictureBox7;
               picarray[6] = pictureBox8;
               picarray[7] = pictureBox9;
               picarray[8] = pictureBox10;
               picarray[9] = pictureBox11;
               picarray[10] = pictureBox12;
               picarray[11] = pictureBox13;

               for (int i = 0; i < 12; i++)
               {
                   picarray[i].Image = commandpanel;
               }


                   button3.BackgroundImage = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\up.png");
            button4.BackgroundImage = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\left.png");
            button5.BackgroundImage = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\right.png");
            button6.BackgroundImage = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\catch.png");

            button3.Paint += new PaintEventHandler(button3_Paint);
            button4.Paint += new PaintEventHandler(button4_Paint);
            button5.Paint += new PaintEventHandler(button5_Paint);
            button6.Paint += new PaintEventHandler(button6_Paint);



            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
           
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            g = Graphics.FromImage(canvas);
           
           
            //画像ファイルを読み込んで、Imageオブジェクトとして取得する
            //Image img = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\paneru.png");


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
            comboBox1.SelectedIndex = 0;
            string[] files = System.IO.Directory.GetFiles("../../boardmatrix/", "*.csv");
            foreach (var n in files)
            {
                using (StreamReader sr = new StreamReader(n, Encoding.GetEncoding(932)))
                {
                    int[,] temp = new int[6, 6];


                    int index = 0;
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        string[] a = s.Split(',');
                        for (int i = 0; i < a.GetLength(0); i++)
                        {
                            temp[i, index] = int.Parse(a[i]);

                        }
                        index++;
                    }
                    boardlist.Add(temp);

                }
            }

         
            catposx = 3;
            catposy = 3;
            //お魚
            g.DrawImage(fish, 1 * fish.Width, 1 * fish.Height, fish.Width, fish.Height);
            //猫
            g.DrawImage(cat, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);

            pictureBox1.Refresh();

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

        private void button3_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            //ボタンの背景画像をボタンの大きさに合わせて描画
            e.Graphics.DrawImage(btn.BackgroundImage, btn.ClientRectangle);

        }

        private void button4_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            //ボタンの背景画像をボタンの大きさに合わせて描画
            e.Graphics.DrawImage(btn.BackgroundImage, btn.ClientRectangle);

        }

        private void button5_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            //ボタンの背景画像をボタンの大きさに合わせて描画
            e.Graphics.DrawImage(btn.BackgroundImage, btn.ClientRectangle);

        }

        private void button6_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            //ボタンの背景画像をボタンの大きさに合わせて描画
            e.Graphics.DrawImage(btn.BackgroundImage, btn.ClientRectangle);

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

        private void button2_Click(object sender, EventArgs e)
        {
            
            makeboard(boardlist[comboBox1.SelectedIndex]);
        }

        private void makeboard(int[,] boardmat)
    {
        for (int i = 0; i < xmax; i++)
        {
            for (int j = 0; j < ymax; j++)
            {
                if (boardmat[i, j] == 1) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);               //道
                else if (boardmat[i, j] == 2) g.DrawImage(back, i * back.Width, j * back.Height, back.Width, back.Height);          //背景
                else if (boardmat[i, j] == 3)  
                {
                    catposx = i;
                    catposy = j;
                    g.DrawImage(cat, i * cat.Width, j * cat.Height, cat.Width, cat.Height);
                }//猫だよ
                else if (boardmat[i, j] == 4) g.DrawImage(fish, i * fish.Width, j * fish.Height, fish.Width, fish.Height);          //魚dayo
            }
        }
        pictureBox1.Refresh();
    }

        private void button3_Click(object sender, EventArgs e)
        {

            picarray[buttoncount].Image = command1;
            buttoncount += 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            picarray[buttoncount].Image = command2;
            buttoncount += 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            picarray[buttoncount].Image = command3;
            buttoncount += 1;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            picarray[buttoncount].Image = command4;
            buttoncount += 1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            

        }



    }
}
