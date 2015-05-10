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
        //Image catharapeko = Image.FromFile(@"../../素材/catharapeko.png");
        Image fish = Image.FromFile(@"../../素材/fish.png");
        Image fish2 = Image.FromFile(@"../../素材/fish2.png");
        Image fish3 = Image.FromFile(@"../../素材/fish3.png");
        Image catu = Image.FromFile(@"../../素材/cat.png");
        Image catr = Image.FromFile(@"../../素材/catr.png");
        Image catl = Image.FromFile(@"../../素材/catl.png");
        Image catd = Image.FromFile(@"../../素材/catd.png");
       

        Image cat;

        int xmax = 10, ymax = 10;

        int catposx;
        int catposy;
    //    int footcount;
        int fishcount;
        int fish2count;
        int fish3count;

        int maincount = 0;
        int onecount = 0;
        int twocount = 0;

        //0=上、1=右、2=下、3=左
        int catdirction = 0;

        //int upcount = 0;
        //int rightcount = 0;
        //int leftcount = 0;
        //int catchcount = 0;

        int[] movecount;
        List<int> movelist = new List<int>();
        List<int> onelist = new List<int>();
        List<int> twolist = new List<int>();

        int Score=0;

        List<int[,]> boardlist = new List<int[,]>();
        PictureBox[] mainpicarray = new PictureBox[12];
        PictureBox[] onepicarray = new PictureBox[6];
        PictureBox[] twopicarray = new PictureBox[6];

        ////画像ファイルを読み込んで、Imageオブジェクトを作成する
        System.Drawing.Image command1 = System.Drawing.Image.FromFile(@"../../素材/up.png");
        System.Drawing.Image command2 = System.Drawing.Image.FromFile(@"../../素材/left.png");
        System.Drawing.Image command3 = System.Drawing.Image.FromFile(@"../../素材/right.png");
        System.Drawing.Image command4 = System.Drawing.Image.FromFile(@"../../素材/catch.png");
        System.Drawing.Image commandpanel = System.Drawing.Image.FromFile(@"../../素材/commandpanel.png");
        System.Drawing.Image commandpanel2 = System.Drawing.Image.FromFile(@"../../素材/commandpanel2.png");
        System.Drawing.Image loop1 = System.Drawing.Image.FromFile(@"../../素材/1.png");
        System.Drawing.Image loop2 = System.Drawing.Image.FromFile(@"../../素材/2.png");


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
            comboBox1.Items.Add("stage1");
            comboBox1.Items.Add("stage2");
            comboBox1.Items.Add("stage3");
            comboBox1.Items.Add("stage4");
            comboBox1.Items.Add("stage5");
            comboBox1.Items.Add("stage6");
            comboBox1.Items.Add("stage7");
            comboBox1.Items.Add("stage8");
            comboBox1.Items.Add("stage9");
            comboBox1.Items.Add("stage10");
            comboBox1.Items.Add("stage11");
            comboBox1.Items.Add("stage12");

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
            comboBox2.SelectedIndex = 0;

            //盤面情報をCSVファイルから読み込み
            //string[] files = System.IO.Directory.GetFiles("../../boardmatrix/", "*.csv");
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
            int xmove = 0, ymove = 0;
            if (direction == 0)
            {
                if (catposy != 0) ymove = -1;

            }
            if (direction == 2)
            {
                if (catposy !=ymax-1 ) ymove = 1;

            }
            if (direction == 1)
            {
                if (catposx != xmax-1) xmove = 1;

            }
            if (direction == 3)
            {
                if (catposx != 0) xmove = -1;

            }

            if(boardlist[comboBox1.SelectedIndex][catposx+xmove,catposy+ymove]!=2)
            {
            for (int i = 0; i <= pictureBox1.Width / xmax; i = i + 1)
            {
                if (i < pictureBox1.Width / xmax - 5) i = i + 4;
                g.DrawImage(road, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax + xmove * i, catposy * pictureBox1.Height / ymax + ymove * i, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

                pictureBox1.Refresh();
                Thread.Sleep(1);
            }
            catposx += xmove;
            catposy += ymove;
        }
        }

        private void catd_change()
        {
            if (catdirction == 0)
            {
                cat = catu;
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
            }
            else if (catdirction == 1)
            {
                cat = catr;
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

            }
            else if (catdirction == 2)
            {
                cat = catd;
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

            }
            else if (catdirction == 3)
            {
                cat = catl;
                g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);

            }
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
        }

        //二次元配列にしたがって盤面を描画
        private void makeboard(int[,] boardmat)
        {
            xmax = boardmat.GetLength(0);
            ymax = boardmat.Length / boardmat.GetLength(0);
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
                        g.DrawImage(cat, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));
                    }//猫だよ
                    else if (boardmat[i, j] == 4) g.DrawImage(fish, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚dayo
                    else if (boardmat[i, j] == 5) g.DrawImage(fish2, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚2dayo
                    else if (boardmat[i, j] == 6) g.DrawImage(fish3, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚3dayo

                }

            }
            pictureBox1.Refresh();
        }

        //引数に命令番号と表示画像をとって命令情報を管理するメソッド
        private void orderclick(int ordernum,Image orderimage)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                try
                {
                    mainpicarray[maincount].Image = orderimage;
                    movelist.Add(ordernum);
                    maincount += 1;
                }
                catch { }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                try
                {
                    onepicarray[onecount].Image = orderimage;
                    onelist.Add(ordernum);
                    onecount += 1;
                }
                catch { }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                try
                {
                    twopicarray[twocount].Image = orderimage;
                    twolist.Add(ordernum);
                    twocount += 1;
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
                try
                {
                    mainpicarray[maincount].Image = loop1;
                    movelist.AddRange(onelist);
                    maincount += 1;
                }
                catch { }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                try
                {
                    onepicarray[onecount].Image = loop1;
                    onelist.AddRange(onelist);
                    onecount += 1;
                }
                catch { }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                try
                {
                    twopicarray[twocount].Image = loop1;
                    twolist.AddRange(onelist);
                    twocount += 1;
                }
                catch { }
            }


        }

        private void two_button_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                try
                {
                    mainpicarray[maincount].Image = loop2;
                    movelist.AddRange(twolist);
                    maincount += 1;
                }
                catch { }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                try
                {
                    onepicarray[onecount].Image = loop2;
                    onelist.AddRange(twolist);
                    onecount += 1;
                }
                catch { }
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                try
                {
                    twopicarray[twocount].Image = loop2;
                    twolist.AddRange(twolist);
                    twocount += 1;
                }
                catch { }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //movelistに格納された番号にしたがって命令を実行
            for (int i = 0; i < movelist.Count; i++)
            {
                if (movelist[i] == 0)
                {
                    catmove(catdirction);
                }

                else if (movelist[i] == 1)
                {
                    if (catdirction == 0)
                    {
                        catdirction = 3;
                    }
                    else
                    {
                        catdirction -= 1;
                    }

                    catd_change();
                    //catmove(1);
                }

                else if (movelist[i] == 2)
                {
                    if (catdirction == 0)
                    {
                        catdirction = 1;
                    }
                    else if (catdirction == 3) catdirction = 0;
                    else
                    {

                        catdirction += 1;
                    }

                    catd_change();
                    //catmove(3);
                }

      
                else if (movelist[i] == 3)
                {
                    catchfish(catposx, catposy);
                }

                label21.Text = movelist.Count.ToString();
                label23.Text = (-movelist.Count * 5).ToString();

            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //スコアの実装
        private void catchfish(int catposx, int catposy)
        {
            //魚の位置確認
            int[,] fishtemp = boardlist[comboBox1.SelectedIndex];
            int fishposx=5, fishposy=5;
            int fish2posx = 5, fish2posy = 5;
            int fish3posx = 5, fish3posy = 5;

            for (int i = 0; i < xmax; i++)
            {
                for (int j = 0; j < ymax; j++)
                {
                    if (fishtemp[i, j] == 4)
                    {
                        fishposx = i;
                        fishposy = j;
                        break;
                    }
                    if (fishtemp[i, j] == 5)
                    {
                        fish2posx = i;
                        fish2posy = j;
                        break;
                    }
                    if (fishtemp[i, j] == 6)
                    {
                        fish3posx = i;
                        fish3posy = j;
                        break;
                    }
                }
            }
          
            if (catposx == fishposx && catposy == fishposy)
            {
                Score = 100;
                fishcount += 1;
                label13.Text = fishcount.ToString();
                label16.Text = (fishcount*100).ToString();
            }
            if (catposx == fish2posx && catposy == fish2posy)
            {
                Score = 300;
                fish2count += 1;
                label14.Text = fish2count.ToString();
                label17.Text = (fish2count * 100).ToString();
            }
            if (catposx == fish3posx && catposy == fish3posy)
            {
                Score = 500;
                fish3count += 1;
                label15.Text = fish3count.ToString();
                label18.Text = (fish3count * 100).ToString();
            }



            label19.Text = (fishcount * 100 + fish2count * 300 + fish3count * 500-movelist.Count*5).ToString();
 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            makeboard(boardlist[comboBox1.SelectedIndex]);
            catdirction = 0;
            catd_change();
        }

        private void orderreset_button_Click(object sender, EventArgs e)
        {
            movelist.Clear();
            onelist.Clear();
            twolist.Clear();

            maincount = 0;
            onecount = 0;
            twocount = 0;

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

 
    }
}
