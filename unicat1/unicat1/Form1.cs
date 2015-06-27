﻿using System;
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

        //画像を変数に格納する
        Image back = Image.FromFile(@"../../素材/back.png");
        Image road = Image.FromFile(@"../../素材/road.png");
        Image fish = Image.FromFile(@"../../素材/fish.png");
        Image fish2 = Image.FromFile(@"../../素材/fish2.png");
        Image fish3 = Image.FromFile(@"../../素材/fish3.png");
        Image catu = Image.FromFile(@"../../素材/cat.png");
        Image catr = Image.FromFile(@"../../素材/catr.png");
        Image catl = Image.FromFile(@"../../素材/catl.png");
        Image catd = Image.FromFile(@"../../素材/catd.png");
        Image command1 = Image.FromFile(@"../../素材/up.png");
        Image command2 = Image.FromFile(@"../../素材/left.png");
        Image command3 = Image.FromFile(@"../../素材/right.png");
        Image command4 = Image.FromFile(@"../../素材/catch.png");
        Image commandpanel = Image.FromFile(@"../../素材/commandpanel.png");
        Image commandpanel2 = Image.FromFile(@"../../素材/commandpanel2.png");
        Image loop1 = Image.FromFile(@"../../素材/1.png");
        Image loop2 = Image.FromFile(@"../../素材/2.png");
        Image cat;

        int[,] nowboard;
        int xmax = 10, ymax = 10;

        int catposx;
        int catposy;
        int footcount;
        int fishcount;
        int fish2count;
        int fish3count;

        int mainpiccount = 0;
        int onepiccount = 0;
        int twopiccount = 0;
        int totalscore = 0;

        //0=上、1=右、2=下、3=左
        int catdirection = 0;

        int[] movecount;
        List<int> movelist = new List<int>() ;  //メインボックスの命令リスト
        List<int> onelist = new List<int>();    //１ボックスの命令リスト
        List<int> twolist = new List<int>();    //２ボックスの命令リスト

        int Score=0;

        List<int[,]> boardlist = new List<int[,]>();
        PictureBox[] mainpicarray = new PictureBox[12];
        PictureBox[] onepicarray = new PictureBox[6];
        PictureBox[] twopicarray = new PictureBox[6];
     

        //盤面情報をCSVファイルから読み込み
        string[] files = System.IO.Directory.GetFiles("../../boardmatrix/", "*.csv");
        
        public Form1()
        {
            InitializeComponent();
            
            //音楽流す
            SoundPlayer Hoge = new SoundPlayer(@"../../素材/BGM.wav");
            Hoge.PlayLooping();

            movecount = new int[12];

            cat = catu;
            
            RandomMT rand = new RandomMT();

            //コンボボックスにステージ名を自動で追加
            for (int i = 0; i < files.Length; i++)
            {
                var stagename = Path.GetFileName(files[i]);
                stagename = stagename.Replace(".csv", "");
                comboBox1.Items.Add(stagename);

            }

            comboBox2.Items.Add("メイン");
            comboBox2.Items.Add("one");
            comboBox2.Items.Add("two");

            mainpicarray[0] = main1;
            mainpicarray[1] = main2;
            mainpicarray[2] = main3;
            mainpicarray[3] = main4;
            mainpicarray[4] = main5;
            mainpicarray[5] = main6;
            mainpicarray[6] = main7;
            mainpicarray[7] = main8;
            mainpicarray[8] = main9;
            mainpicarray[9] = main10;
            mainpicarray[10] = main11;
            mainpicarray[11] = main12;
            onepicarray[0] = one1;
            onepicarray[1] = one2;
            onepicarray[2] = one3;
            onepicarray[3] = one4;
            onepicarray[4] = one5;
            onepicarray[5] = one6;
            twopicarray[5] = two6;
            twopicarray[4] = two5;
            twopicarray[3] = two4;
            twopicarray[2] = two3;
            twopicarray[1] = two2;
            twopicarray[0] = two1;

            for (int i = 0; i < 12; i++)
            {
                mainpicarray[i].Image = commandpanel;
            }

            for (int i = 0; i < 6; i++)
            {
                onepicarray[i].Image = commandpanel2;
                twopicarray[i].Image = commandpanel2;
            }

            gobutton.BackgroundImage = Image.FromFile(@"../../素材/up.png");
            turnleft_button.BackgroundImage = Image.FromFile(@"../../素材/left.png");
            turnright_button.BackgroundImage = Image.FromFile(@"../../素材/right.png");
            catchfish_button.BackgroundImage = Image.FromFile(@"../../素材/catch.png");
            one_button.BackgroundImage = Image.FromFile(@"../../素材/1.png");
            two_button.BackgroundImage = Image.FromFile(@"../../素材/2.png");

            gobutton.Paint += new PaintEventHandler(button3_Paint);
            turnleft_button.Paint += new PaintEventHandler(button4_Paint);
            turnright_button.Paint += new PaintEventHandler(button5_Paint);
            catchfish_button.Paint += new PaintEventHandler(button6_Paint);
            one_button.Paint += new PaintEventHandler(button7_Paint);
            two_button.Paint += new PaintEventHandler(button8_Paint);

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            //ImageオブジェクトのGraphicsオブジェクトを作成する
            g = Graphics.FromImage(canvas);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            //盤面情報をCSVファイルから読み込み
            foreach (var n in files)
            {
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
            //pictureBox2.Image = catharapeko;
            pictureBox26.Image = fish;
            pictureBox27.Image = fish2;
            pictureBox28.Image = fish3;
            //画像の大きさをPictureBoxに合わせる
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox26.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox27.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox28.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //ネコがある方向に一つ進む
        public void catmove(int direction)
        {

            footcount++;
            int xmove = 0, ymove = 0;
            //方向と端にいるかどうかで移動の変化量を決める
            if (direction == 0)
            {
                if (catposy != 0) ymove = -1;

            }
            if (direction == 2)
            {
                if (catposy != ymax - 1) ymove = 1;

            }
            if (direction == 1)
            {
                if (catposx != xmax - 1) xmove = 1;

            }
            if (direction == 3)
            {
                if (catposx != 0) xmove = -1;

            }


            if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] != 2)//移動した先に壁がなければ
            {
                for (int i = 0; i <= pictureBox1.Width / xmax; i = i + 1)
                {
                    if (i < pictureBox1.Width / xmax - 5) i = i + 4;

                    if (nowboard[catposx, catposy] == 4)
                    {
                        g.DrawImage(fish, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                    }
                    else if (nowboard[catposx, catposy] == 5)
                    {
                        g.DrawImage(fish2, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                    }
                    else if (nowboard[catposx, catposy] == 6)
                    {
                        g.DrawImage(fish3, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                    }
                    else
                    {
                        g.DrawImage(road, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                    }

                    g.DrawImage(cat, catposx * pictureBox1.Width / xmax + xmove * i, catposy * pictureBox1.Height / ymax + ymove * i, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

                    pictureBox1.Refresh();
                    Thread.Sleep(1);
                }
                catposx += xmove;
                catposy += ymove;
                harapekocount.Text = footcount.ToString();
                harapekoscore.Text = (-footcount * 5).ToString();
                totalscorelabel.Text = (100+fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5).ToString();
                totalscorelabel.Refresh();
                harapekocount.Refresh();
                harapekoscore.Refresh();
                Thread.Sleep(100);

            }
        }

        private void catd_change()  //ネコの方向転換をする(イラストの変更)
        {
            footcount++;

            if (catdirection == 0)
            {
                cat = catu;
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
            }
            else if (catdirection == 1)
            {
                cat = catr;
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

            }
            else if (catdirection == 2)
            {
                cat = catd;
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

            }
            else if (catdirection == 3)
            {
                cat = catl;
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

            }
            harapekocount.Text = footcount.ToString();
            harapekoscore.Text = (-footcount * 5).ToString();          
            totalscorelabel.Text = (100+fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5).ToString();
            totalscorelabel.Refresh();
            harapekocount.Refresh();
            harapekoscore.Refresh();          
            pictureBox1.Refresh();
            Thread.Sleep(200);
        }

        //矢印キーでネコ移動
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //左キーが押されているか調べる
            if ((keyData & Keys.KeyCode) == Keys.Up)
            {
                catmove(0);
                return true;
            }
            if ((keyData & Keys.KeyCode) == Keys.Down)
            {
                catmove(2);

                return true;
            } if ((keyData & Keys.KeyCode) == Keys.Right)
            {
                catmove(1);

                return true;
            } if ((keyData & Keys.KeyCode) == Keys.Left)
            {
                catmove(3);

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

        private void button7_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            //ボタンの背景画像をボタンの大きさに合わせて描画
            e.Graphics.DrawImage(btn.BackgroundImage, btn.ClientRectangle);

        }
        private void button8_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            //ボタンの背景画像をボタンの大きさに合わせて描画
            e.Graphics.DrawImage(btn.BackgroundImage, btn.ClientRectangle);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            makeboard(boardlist[comboBox1.SelectedIndex]);
            footcount = 0;
            fishcount = 0;
            fish2count = 0;
            fish3count = 0;
            harapekocount.Text = harapekoscore.Text = 0.ToString();

        }

        //二次元配列にしたがって盤面を描画
        private void makeboard(int[,] boardmat)
        {
            xmax = boardmat.GetLength(0);
            ymax = boardmat.Length / boardmat.GetLength(0);
            nowboard = new int[xmax, ymax];

            for (int i = 0; i < boardmat.GetLength(0); i++)
            {
                for (int j = 0; j < boardmat.GetLength(1); j++)
                {
                    nowboard[i,j] = boardmat[i, j];
                }
            }

                g.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width, pictureBox1.Height);
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
                        //猫の画像，向きの初期化
                        cat = catu;
                        catdirection = 0;
                        g.DrawImage(cat, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));
                    }//猫だよ
                    else if (boardmat[i, j] == 4) g.DrawImage(fish, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚dayo
                    else if (boardmat[i, j] == 5) g.DrawImage(fish2, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚2dayo
                    else if (boardmat[i, j] == 6) g.DrawImage(fish3, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚3dayo

                }

            }
            scorereset();
            pictureBox1.Refresh();
        }

        //引数に命令番号と表示画像をとって命令情報を管理するメソッド
        private void orderclick(int ordernum,Image orderimage)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                try
                {
                    mainpicarray[mainpiccount].Image = orderimage;
                    movelist.Add(ordernum);
                    mainpiccount += 1;
                }
                catch { }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                try
                {
                    if (orderimage != loop1 || orderimage != loop2)
                    {
                        onepicarray[onepiccount].Image = orderimage;
                        onelist.Add(ordernum);
                        onepiccount += 1;
                    }
                }
                catch { }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                try
                {
                    if (orderimage != loop1 || orderimage != loop2)
                    {
                        twopicarray[twopiccount].Image = orderimage;
                        twolist.Add(ordernum);
                        twopiccount += 1;
                    }
                }
                catch { }
            }

        }

        private void go_button_Click(object sender, EventArgs e)
        {
            orderclick(0,command1);
        }

        private void turnleft_button_Click(object sender, EventArgs e)
        {
            orderclick(1,command2);
        }

        private void turnright_button_Click(object sender, EventArgs e)
        {
            orderclick(2,command3);
        }

        private void catchfish_button_Click_1(object sender, EventArgs e)
        {
            orderclick(3,command4);
        }

        private void one_button_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                orderclick(4, loop1);
            }
        }

        private void two_button_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                orderclick(5, loop2);
            }      
        }

        private void listcheck(List<int> list,int index)
        {
            if (list[index] == 0)
            {
                catmove(catdirection);
            }

            else if (list[index] == 1)
            {
                if (catdirection == 0)
                {
                    catdirection = 3;
                }
                else
                {
                    catdirection -= 1;
                }

                catd_change();
               
            }

            else if (list[index] == 2)
            {
                if (catdirection == 0)
                {
                    catdirection = 1;
                }
                else if (catdirection == 3) catdirection = 0;
                else
                {

                    catdirection += 1;
                }

                catd_change();
              
            }


            else if (list[index] == 3)
            {
                catchfish(catposx, catposy);
            }

        }

        private void movebutton_Click_1(object sender, EventArgs e)
        {

            //movelistに格納された番号にしたがって命令を実行
            for (int i = 0; i < movelist.Count; i++)
            {
                listcheck(movelist, i);

                if (movelist[i] == 4)
                {
                    for (int j = 0; j < onelist.Count; j++)
                    {
                        listcheck(onelist, j);

                    }
                }

                if (movelist[i] == 5)
                {
                    for (int j = 0; j < twolist.Count; j++)
                    {
                        listcheck(twolist, j);

                    }
                }

            }

            int totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;

            if (totalscore <= -100)
            {
                MessageBox.Show("死亡", "オーイ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else totalscorelabel.Text = (100 + totalscore).ToString();


            int foodcount = 0;
            for (int i = 0; i < nowboard.GetLength(0); i++)
            {
                for (int j = 0; j < nowboard.GetLength(1); j++)
                {
                    if (nowboard[i, j] == 4 || nowboard[i, j] == 5 || nowboard[i, j] == 6) foodcount++;
                }

            }

            if (foodcount == 0)
            {
                totalscorelabel.Text = (100 +totalscore).ToString();
                totalscorelabel.Refresh();
                MessageBox.Show("オメ", "おおっ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    makeboard(boardlist[comboBox1.SelectedIndex + 1]);
                    comboBox1.SelectedIndex++;
                }
                catch
                {
                    MessageBox.Show("全クリ", "ムム...?", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                MessageBox.Show("残念", "あーあ。。。", MessageBoxButtons.OK, MessageBoxIcon.Error);
                makeboard(boardlist[comboBox1.SelectedIndex]);
                scorereset();
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                label4.BackColor = Color.Yellow;
                label2.BackColor = DefaultBackColor;
                label3.BackColor = DefaultBackColor;

            }
            if (comboBox2.SelectedIndex == 1)
            {
                label2.BackColor = Color.Yellow;
                label4.BackColor = DefaultBackColor;
                label3.BackColor = DefaultBackColor;

            }
            if (comboBox2.SelectedIndex == 2)
            {
                label3.BackColor = Color.Yellow;
                label2.BackColor = DefaultBackColor;
                label4.BackColor = DefaultBackColor;

            }
        }

        //スコアの実装
        private void catchfish(int catposx, int catposy)
        {
            //魚の位置確認
            int[,] fishtemp = nowboard;
         
            if (nowboard[catposx,catposy]==4)
            {
                nowboard[catposx, catposy] = 1;
                Score = 100;
                fishcount += 1;
                fish100count.Text = fishcount.ToString();
                fish100score.Text = (fishcount * Score).ToString();
            }
            if (nowboard[catposx,catposy]==5)
            {
                nowboard[catposx, catposy] = 1;
                Score = 300;
                fish2count += 1;
                fish300count.Text = fish2count.ToString();
                fish300score.Text = (fish2count * Score).ToString();
            }
            if (nowboard[catposx,catposy]==6)
            {
                nowboard[catposx, catposy] = 1;
                Score = 500;
                fish3count += 1;
                fish500count.Text = fish3count.ToString();
                fish500score.Text = (fish3count * Score).ToString();
            }

        }


        //スコア初期化
        private void scorereset()
        {
            fishcount = 0;
            fish2count=0;
            fish3count=0;
            footcount = 0;
            fish100count.Text = fishcount.ToString();
            fish100score.Text = (fishcount * Score).ToString();
            fish300count.Text = fishcount.ToString();
            fish300score.Text = (fishcount * Score).ToString();
            fish500count.Text = fishcount.ToString();
            fish500score.Text = (fishcount * Score).ToString();

            harapekocount.Text = footcount.ToString();
            harapekoscore.Text = (-footcount * 5).ToString();
            totalscorelabel.Text = (100+fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5).ToString();
            totalscorelabel.Refresh();

        }


        private void button9_Click(object sender, EventArgs e)
        {
            makeboard(boardlist[comboBox1.SelectedIndex]);
            catdirection = 0;
            catd_change();
        }

        private void orderreset_button_Click(object sender, EventArgs e)
        {
            movelist.Clear();
            onelist.Clear();
            twolist.Clear();

            mainpiccount = 0;
            onepiccount = 0;
            twopiccount = 0;

            for (int i = 0; i < 12; i++)
            {
                mainpicarray[i].Image = commandpanel;
            }

            for (int i = 0; i < 6; i++)
            {
                onepicarray[i].Image = commandpanel2;
                twopicarray[i].Image = commandpanel2;
            }

        }


        //命令を一つ消す
        private void undo_button_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                if (mainpiccount < 0) mainpiccount = 0;
                else if (mainpiccount > 0)
                {
                    mainpicarray[mainpiccount - 1].Image = commandpanel;
                    mainpiccount--;
                }
                if (movelist.Count > 0)
                {                                                                               //スレッド分けたいです
                    movelist.Remove(movelist[movelist.Count - 1]);
                }

            }

            if (comboBox2.SelectedIndex == 1)
            {
                if (onepiccount < 0) onepiccount = 0;
                else if (onepiccount > 0)
                {
                    onepicarray[onepiccount - 1].Image = commandpanel2;
                    onepiccount--;
                }
                if (onelist.Count > 0)
                {
                    onelist.Remove(onelist[onelist.Count - 1]);
                }
            }

            if (comboBox2.SelectedIndex == 2)
            {
                if (twopiccount < 0) twopiccount = 0;
                else if (twopiccount > 0)
                {
                    twopicarray[twopiccount - 1].Image = commandpanel2;
                    twopiccount--;
                }
                if (twolist.Count > 0)
                {
                    twolist.Remove(twolist[twolist.Count - 1]);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 2;
        }

 
    }
}
