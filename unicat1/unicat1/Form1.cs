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
using System.Media;

namespace unicat1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Image back = Image.FromFile(@"../../素材/back.png");
        Image road = Image.FromFile(@"../../素材/road.png");
        Image fish = Image.FromFile(@"../../素材/fish.png");
        Image cat = Image.FromFile(@"../../素材/cat.png");
        int xmax = 10, ymax = 10;

        int catposx;
        int catposy;
       
        int buttoncount=0;
        int upcount = 0;
        int rightcount = 0;
        int leftcount = 0;
        int catchcount = 0;
        int[] movecount;

        List<int[,]> boardlist = new List<int[,]>();
        PictureBox[] picarray = new PictureBox[12];

        ////画像ファイルを読み込んで、Imageオブジェクトを作成する
        System.Drawing.Image command1 = System.Drawing.Image.FromFile(@"../../素材/up.png");
        System.Drawing.Image command2 = System.Drawing.Image.FromFile(@"../../素材/left.png");
        System.Drawing.Image command3 = System.Drawing.Image.FromFile(@"../../素材/right.png");
        System.Drawing.Image command4 = System.Drawing.Image.FromFile(@"../../素材/catch.png");
        System.Drawing.Image commandpanel = System.Drawing.Image.FromFile(@"../../素材/commandpanel.png");

        public Form1()
        {
            InitializeComponent();


             movecount=new int[100];
            SoundPlayer Hoge = new SoundPlayer(@"../../素材/BGM.wav");
            Hoge.PlayLooping();

            RandomMT rand = new RandomMT();
            comboBox1.Items.Add("stage1");
            comboBox1.Items.Add("stage2");
           
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

            button3.BackgroundImage = Image.FromFile(@"../../素材/up.png");
            button4.BackgroundImage = Image.FromFile(@"../../素材/left.png");
            button5.BackgroundImage = Image.FromFile(@"../../素材/right.png");
            button6.BackgroundImage = Image.FromFile(@"../../素材/catch.png");

            button3.Paint += new PaintEventHandler(button3_Paint);
            button4.Paint += new PaintEventHandler(button4_Paint);
            button5.Paint += new PaintEventHandler(button5_Paint);
            button6.Paint += new PaintEventHandler(button6_Paint);



            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
           
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            g = Graphics.FromImage(canvas);            

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

            //盤面情報をCSVファイルから読み込み
            string[] files = System.IO.Directory.GetFiles("../../boardmatrix/", "*.csv");
            foreach (var n in files)
            {
                //using (StreamReader sr = new StreamReader(n, Encoding.GetEncoding(932)))
                //{
                //    int index = 0;
                //    int[,] temp = new int[xmax, ymax];

                //    while (!sr.EndOfStream)
                //    {
                //        string s = sr.ReadLine();
                //        string[] a = s.Split(',');
                //        for (int i = 0; i < a.GetLength(0); i++)
                //        {
                //            temp[i, index] = int.Parse(a[i]);

                //        }
                //        index++;
                //    }
                //    boardlist.Add(temp);

                //}

                using (StreamReader sr = new StreamReader(n, Encoding.GetEncoding(932)))
                {
                    List<string> templist = new List<string>();

                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        templist.Add(s);
                     }
                    xmax = templist.Count;
                    string[] a = templist[0].Split(',');
                    ymax = a.Length;
                    int[,] temp = new int[xmax, ymax];

                    for (int y = 0; y < ymax; y++)
                    {
                        string[] arraytemp = templist[y].Split(',');
                        for (int x = 0; x < xmax; x++)
                        {
                            temp[x, y] = int.Parse(arraytemp[x]);
                        }
                    }
                    boardlist.Add(temp);
                }

            }

            makeboard(boardlist[comboBox1.SelectedIndex]);

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

            for (int i = 0; i <= pictureBox1.Width / xmax; i = i + 5)
            {
                //g.DrawImage(road, catposx * cat.Width, catposy * cat.Height, cat.Width, cat.Height);
                //g.DrawImage(cat, catposx * cat.Width + xmove*i, catposy * cat.Height + ymove*i, cat.Width, cat.Height);
                g.DrawImage(road, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax + xmove * i, catposy * pictureBox1.Height / ymax + ymove * i, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

                pictureBox1.Refresh();
                Thread.Sleep(1);
            }
            catposx +=xmove;
            catposy += ymove;
        }

        //矢印キーでネコ移動
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
            //for (int i = 0; i < xmax; i++)
            //{
            //    for (int j = 0; j < ymax; j++)
            //    {
            //        if (boardmat[i, j] == 1) g.DrawImage(road, i * road.Width, j * road.Height, road.Width, road.Height);               //道
            //        else if (boardmat[i, j] == 2) g.DrawImage(back, i * back.Width, j * back.Height, back.Width, back.Height);          //背景
            //        else if (boardmat[i, j] == 3)
            //        {
            //            catposx = i;
            //            catposy = j;
            //            g.DrawImage(cat, i * cat.Width, j * cat.Height, cat.Width, cat.Height);
            //        }//猫だよ
            //        else if (boardmat[i, j] == 4) g.DrawImage(fish, i * fish.Width, j * fish.Height, fish.Width, fish.Height);          //魚dayo
            //    }
            //}
            //pictureBox1.Refresh();

            xmax = boardmat.GetLength(0);
            ymax = boardmat.Length / boardmat.GetLength(0);
            g.FillRectangle(Brushes.White,0,0,pictureBox1.Width,pictureBox1.Height);
            for (int i = 0; i < boardmat.GetLength(0); i++)
            {
                for (int j = 0; j < (boardmat.Length / boardmat.GetLength(0)); j++)
                {
                    if (boardmat[i, j] == 1) g.DrawImage(road, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));               //道
                    else if (boardmat[i, j] == 2 || boardmat[i, j] == 0) g.DrawImage(back, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //背景
                    else if (boardmat[i, j] == 3)
                    {
                        catposx = i;
                        catposy = j;
                        g.DrawImage(cat, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));
                    }//猫だよ
                    else if (boardmat[i, j] == 4) g.DrawImage(fish, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚dayo
                }
            }
            pictureBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            picarray[buttoncount].Image = command1;
            movecount[buttoncount] = 0;
            buttoncount += 1;
            upcount += 1;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            picarray[buttoncount].Image = command2;
            movecount[buttoncount] = 1;
            buttoncount += 1;
            leftcount += 1;
            movecount[buttoncount] = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            picarray[buttoncount].Image = command3;
            movecount[buttoncount] = 2;
            buttoncount += 1;
            rightcount += 1;
            movecount[buttoncount] = 2;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            picarray[buttoncount].Image = command4;
            movecount[buttoncount] = 3;
            buttoncount += 1;
            catchcount += 1;
            movecount[buttoncount] = 3;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < buttoncount; i++)
            {
                if (movecount[i] == 0)
                {
                    catmove("up");
                }

                else if (movecount[i] == 1)
                {
                    catmove("left");
                }

                else if (movecount[i] == 2)
                {
                    catmove("right");
                }
                else if (movecount[i] == 3)
                {
                    //catmove("catch");
                }
            }

        }



    }
}
