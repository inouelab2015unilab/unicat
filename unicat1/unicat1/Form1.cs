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

        //画像を変数に格納する
        public Image back = Image.FromFile(@"../../素材/back.png");
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
        Image pic_if = Image.FromFile(@"../../素材/if.png");
        Image cat;

        int[,] nowboard;    //現在選択されている盤面のデータ
        int xmax = 10, ymax = 10;   //盤面のサイズ       
        int catposx, catposy;
        int footcount;
        int fishcount;
        int fish2count;
        int fish3count;
        int totalscore;

        int mainpiccount = 0;
        int onepiccount = 0;
        int twopiccount = 0;
        int mosimopiccount = 0;

        //0=上、1=右、2=下、3=左
        int catdirection = 0;

        int[] movecount;
        int selectBox = 0;
        List<int> movelist = new List<int>();  //メインボックスの命令リスト
        List<int> onelist = new List<int>();    //１ボックスの命令リスト
        List<int> twolist = new List<int>();    //２ボックスの命令リスト
        List<int> mosimolist = new List<int>();    //２ボックスの命令リスト

        int Score = 0;

        public List<int[,]> boardlist = new List<int[,]>();
        PictureBox[] mainpicarray = new PictureBox[12];
        PictureBox[] onepicarray;
        PictureBox[] twopicarray = new PictureBox[6];
        PictureBox[] mosimopicarray = new PictureBox[6];
        SoundPlayer Hoge = new SoundPlayer(@"../../素材/BGM.wav");

        //盤面情報をCSVファイルから読み込み
        string[] files = System.IO.Directory.GetFiles("../../boardmatrix/", "*.csv");

        public Form1()
        {
            InitializeComponent();

            radioButton2.Checked = true;
            radioButton_main.Checked = true;
            //音楽流す
            if (radioButton1.Checked == true)
            {
                SoundPlayer Hoge = new SoundPlayer(@"../../素材/BGM.wav");
                Hoge.PlayLooping();
            }
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

            comboBox3.Items.Add("もしも前に壁がなかったら");
            comboBox3.Items.Add("もしも左に壁がなかったら");
            comboBox3.Items.Add("もしも右に壁がなかったら");

            //ピクチャーボックス配列に各ピクチャーボックスを格納
            mainpicarray = new PictureBox[] { main1, main2, main3, main4, main5, main6, main7, main8, main9, main10, main11, main12 };
            onepicarray = new PictureBox[] { one1, one2, one3, one4, one5, one6 };
            twopicarray = new PictureBox[] { two1, two2, two3, two4, two5, two6 };
            mosimopicarray = new PictureBox[] { mosimo1, mosimo2, mosimo3, mosimo4, mosimo5, mosimo6 };


            //命令パネルの背景を設置
            foreach (var n in mainpicarray) n.Image = commandpanel;
            foreach (var n in onepicarray) n.Image = commandpanel2;
            foreach (var n in twopicarray) n.Image = commandpanel2;
            foreach (var n in mosimopicarray) n.Image = commandpanel2;

            //命令ボタンの背景を設置
            gobutton.BackgroundImage = Image.FromFile(@"../../素材/up.png");
            turnleft_button.BackgroundImage = Image.FromFile(@"../../素材/left.png");
            turnright_button.BackgroundImage = Image.FromFile(@"../../素材/right.png");
            catchfish_button.BackgroundImage = Image.FromFile(@"../../素材/catch.png");
            one_button.BackgroundImage = Image.FromFile(@"../../素材/1.png");
            two_button.BackgroundImage = Image.FromFile(@"../../素材/2.png");
            if_button.BackgroundImage = Image.FromFile(@"../../素材/if.png");

            gobutton.Paint += new PaintEventHandler(button3_Paint);
            turnleft_button.Paint += new PaintEventHandler(button4_Paint);
            turnright_button.Paint += new PaintEventHandler(button5_Paint);
            catchfish_button.Paint += new PaintEventHandler(button6_Paint);
            one_button.Paint += new PaintEventHandler(button7_Paint);
            two_button.Paint += new PaintEventHandler(button8_Paint);
            if_button.Paint += new PaintEventHandler(button8_Paint);

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            //ImageオブジェクトのGraphicsオブジェクトを作成する
            g = Graphics.FromImage(canvas);

            comboBox1.SelectedIndex = 0;    //コンボボックスの初期値
            selectBox = 0;
            comboBox3.SelectedIndex = 0;

            //盤面情報をCSVファイルから読み込み、boardlistに格納(要素は二次元配列)
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
            pictureBox26.Image = fish;
            //pictureBox27.Image = fish2;
            //pictureBox28.Image = fish3;
            //画像の大きさをPictureBoxに合わせる
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox26.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox27.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox28.SizeMode = PictureBoxSizeMode.StretchImage;

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
            catdirection = 0;
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
                    nowboard[i, j] = boardmat[i, j];
                }
            }

            g.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width, pictureBox1.Height);

            //道＝１、壁＝２、猫＝３、魚１＝４、魚２＝５、魚３＝６
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
        private void orderclick(int ordernum, Image orderimage)
        {
            if (selectBox == 0)
            {
                try
                {
                    mainpicarray[mainpiccount].Image = orderimage;
                    movelist.Add(ordernum);
                    mainpiccount += 1;
                }
                catch { }
            }
            else if (selectBox == 1)
            {
                try
                {
                    onepicarray[onepiccount].Image = orderimage;
                    onelist.Add(ordernum);
                    onepiccount += 1;
                }
                catch { }
            }
            else if (selectBox == 2)
            {
                try
                {
                    twopicarray[twopiccount].Image = orderimage;
                    twolist.Add(ordernum);
                    twopiccount += 1;
                }
                catch { }
            }
            else if (selectBox == 3)
            {
                try
                {
                    mosimopicarray[mosimopiccount].Image = orderimage;
                    mosimolist.Add(ordernum);
                    mosimopiccount += 1;
                }
                catch { }
            }

        }

        private void go_button_Click(object sender, EventArgs e)
        {
            orderclick(0, command1);
        }

        private void turnleft_button_Click(object sender, EventArgs e)
        {
            orderclick(1, command2);
        }

        private void turnright_button_Click(object sender, EventArgs e)
        {
            orderclick(2, command3);
        }

        private void catchfish_button_Click_1(object sender, EventArgs e)
        {
            orderclick(3, command4);
        }

        private void one_button_Click(object sender, EventArgs e)
        {
            orderclick(4, loop1);
        }

        private void two_button_Click(object sender, EventArgs e)
        {
            orderclick(5, loop2);
        }
        private void if_button_Click(object sender, EventArgs e)
        {
            orderclick(6, pic_if);
        }

        private void listcheck(List<int> list, int index)
        {
            if (checkfood() != 0)
            {
                orderFlash(list,index);
                if (list[index] == 0) catmove(catdirection);

                else if (list[index] == 1)
                {
                    if (catdirection == 0) catdirection = 3;
                    else catdirection -= 1;

                    catd_change();
                }

                else if (list[index] == 2)
                {
                    if (catdirection == 3) catdirection = 0;
                    else catdirection += 1;

                    catd_change();
                }

                else if (list[index] == 3)
                {
                    catchfish(catposx, catposy);
                }
                else if (list[index] == 4)
                {
                    for (int j = 0; j < onelist.Count; j++)
                    {
                        listcheck(onelist, j);
                        totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                        if (totalscore <= -100) break;
                    }
                }

                else if (list[index] == 5)
                {
                    for (int j = 0; j < twolist.Count; j++)
                    {
                        listcheck(twolist, j);
                        totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                        if (totalscore <= -100) break;
                    }
                }
                else if (list[index] == 6)
                {
                    int xmove = 0, ymove = 0;
                    if (comboBox3.SelectedIndex == 0)
                    {        //catdirection 0=上、1=右、2=下、3=左
                        //方向と端にいるかどうかで移動の変化量を決める
                        if (catdirection == 0 && catposy != 0) ymove = -1;
                        if (catdirection == 2 && catposy != ymax - 1) ymove = 1;
                        if (catdirection == 1 && catposx != xmax - 1) xmove = 1;
                        if (catdirection == 3 && catposx != 0) xmove = -1;
                        if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] != 2)//移動した先に壁ならば
                        {
                            for (int j = 0; j < mosimolist.Count; j++)
                            {
                                listcheck(mosimolist, j);
                                totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                                if (totalscore <= -100) break;
                            }

                        }
                    }

                    if (comboBox3.SelectedIndex == 1)
                    {
                        //方向と端にいるかどうかで移動の変化量を決める
                        if (catdirection == 0 && catposy != 0) xmove = -1;
                        if (catdirection == 2 && catposy != ymax - 1) xmove = 1;
                        if (catdirection == 1 && catposx != xmax - 1) ymove = -1;
                        if (catdirection == 3 && catposx != 0) ymove = 1;
                        if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] != 2)//移動した先に壁ならば
                        {
                            for (int j = 0; j < mosimolist.Count; j++)
                            {
                                listcheck(mosimolist, j);
                                totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                                if (totalscore <= -100) break;
                            }

                        }
                    }
                    if (comboBox3.SelectedIndex == 2)
                    {
                        //方向と端にいるかどうかで移動の変化量を決める
                        if (catdirection == 0 && catposy != 0) xmove = 1;
                        if (catdirection == 2 && catposy != ymax - 1) xmove = -1;
                        if (catdirection == 1 && catposx != xmax - 1) ymove = 1;
                        if (catdirection == 3 && catposx != 0) ymove = -1;
                        if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] != 2)//移動した先に壁ならば
                        {
                            for (int j = 0; j < mosimolist.Count; j++)
                            {
                                listcheck(mosimolist, j);
                                totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                                if (totalscore <= -100) break;
                            }

                        }
                    }
                    //for (int j = 0; j < mosimolist.Count; j++)
                    //{
                    //    listcheck(mosimolist, j);
                    //    totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                    //    if (totalscore <= -100) break;
                    //}
                }
                orderFlashBack(list, index);
            }
        }

        public int checkfood()
        {
            int foodcount = 0;
            for (int i = 0; i < nowboard.GetLength(0); i++) //盤面の食べ物の数を数える
            {
                for (int j = 0; j < nowboard.GetLength(1); j++)
                {
                    if (nowboard[i, j] == 4 || nowboard[i, j] == 5 || nowboard[i, j] == 6) foodcount++;
                }

            }
            return foodcount;

        }

        private void movebutton_Click_1(object sender, EventArgs e)
        {

            //movelistに格納された番号にしたがって命令を実行
            for (int i = 0; i < movelist.Count; i++)
            {
                listcheck(movelist, i);
                totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                if (totalscore <= -100) break;
            }

            if (totalscore <= -100)
            {
                MessageBox.Show("死亡", "オーイ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                totalscorelabel.Text = (100 + totalscore).ToString();
                totalscorelabel.Refresh();
            }

            int foodcount = checkfood();
            for (int i = 0; i < nowboard.GetLength(0); i++) //盤面の食べ物の数を数える
            {
                for (int j = 0; j < nowboard.GetLength(1); j++)
                {
                    if (nowboard[i, j] == 4 || nowboard[i, j] == 5 || nowboard[i, j] == 6) foodcount++;
                }

            }

            if (foodcount == 0) //全部食べられていたらクリア
            {
                totalscorelabel.Text = (100 + totalscore).ToString();
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

        private void SelectedBoxChanged()
        {
            if (selectBox == 0)
            {
                main_Box.BackColor = Color.Yellow;
                one_Box.BackColor = DefaultBackColor;
                two_Box.BackColor = DefaultBackColor;
                if_Box.BackColor = DefaultBackColor;
            }
            if (selectBox == 1)
            {
                main_Box.BackColor = DefaultBackColor;
                one_Box.BackColor = Color.Yellow;
                two_Box.BackColor = DefaultBackColor;
                if_Box.BackColor = DefaultBackColor;

            }
            if (selectBox == 2)
            {
                main_Box.BackColor = DefaultBackColor;
                one_Box.BackColor = DefaultBackColor;
                two_Box.BackColor = Color.Yellow;
                if_Box.BackColor = DefaultBackColor;

            }
            if (selectBox == 3)
            {
                main_Box.BackColor = DefaultBackColor;
                one_Box.BackColor = DefaultBackColor;
                two_Box.BackColor = DefaultBackColor;
                if_Box.BackColor = Color.Yellow;

            }
        }

        //スコアの実装
        private void catchfish(int catposx, int catposy)
        {
            //魚の位置確認
            int[,] fishtemp = nowboard;

            if (nowboard[catposx, catposy] == 4)
            {
                nowboard[catposx, catposy] = 1;
                Score = 100;
                fishcount += 1;
                fish100count.Text = fishcount.ToString();
                fish100score.Text = (fishcount * Score).ToString();
            }
            if (nowboard[catposx, catposy] == 5)
            {
                nowboard[catposx, catposy] = 1;
                Score = 300;
                fish2count += 1;
                //fish300count.Text = fish2count.ToString();
                //fish300score.Text = (fish2count * Score).ToString();
            }
            if (nowboard[catposx, catposy] == 6)
            {
                nowboard[catposx, catposy] = 1;
                Score = 500;
                fish3count += 1;
                //fish500count.Text = fish3count.ToString();
                //fish500score.Text = (fish3count * Score).ToString();
            }
            cat_turn();
        }

        public void cat_turn()
        {
            for (int i = 0; i <= 360; i = i + 20)
            {
                double d = (i + 45) / (180 / Math.PI);//傾けたい角度
                float a = (float)Math.Sqrt(Math.Pow(pictureBox1.Width / (2*xmax), 2) + Math.Pow(pictureBox1.Height / (2*ymax), 2));//傾けたい角度
                //新しい座標位置を計算する
                float x = catposx * pictureBox1.Width / xmax + (pictureBox1.Width / (2 * xmax));   //中心のｘ座標
                float y = catposy * pictureBox1.Height / ymax + (pictureBox1.Height / (2 * ymax)); //中心のｙ座標
                float r = (float)Math.Sqrt(Math.Pow(pictureBox1.Width / xmax, 2) + Math.Pow(pictureBox1.Height / ymax, 2)); //中心のｙ座標
                float xx = x - (pictureBox1.Width / xmax) * (float)Math.Cos(d) / 2;
                float yy = y - (pictureBox1.Height / ymax) * (float)Math.Sin(d) / 2;
                float x1 = xx + a * (float)Math.Cos(i / (180 / Math.PI)) ;
                float y1 = yy + a * (float)Math.Sin(i / (180 / Math.PI)) ;
                float x2 = xx - a * (float)Math.Sin(i / (180 / Math.PI)) ;
                float y2 = yy + a * (float)Math.Cos(i / (180 / Math.PI)) ;
                //PointF配列を作成
                PointF[] destinationPoints = { new PointF(xx, yy), new PointF(x1, y1), new PointF(x2, y2) };
                //画像を表示
                g.DrawImage(road, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);                
                g.DrawImage(cat, destinationPoints);
                pictureBox1.Refresh();
                Thread.Sleep(10);
            }
            g.DrawImage(cat, catposx * pictureBox1.Width / xmax , catposy * pictureBox1.Height / ymax , pictureBox1.Width / xmax, pictureBox1.Height / ymax);
            pictureBox1.Refresh();
            Thread.Sleep(10);

        }


        //スコア初期化
        private void scorereset()
        {
            fishcount = 0;
            fish2count = 0;
            fish3count = 0;
            footcount = 0;
            fish100count.Text = fishcount.ToString();
            fish100score.Text = (fishcount * Score).ToString();
            //fish300count.Text = fishcount.ToString();
            //fish300score.Text = (fishcount * Score).ToString();
            //fish500count.Text = fishcount.ToString();
            //fish500score.Text = (fishcount * Score).ToString();

            harapekocount.Text = footcount.ToString();
            harapekoscore.Text = (-footcount * 5).ToString();
            totalscorelabel.Text = (100 + fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5).ToString();
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
            mosimolist.Clear();

            mainpiccount = 0;
            onepiccount = 0;
            twopiccount = 0;
            mosimopiccount = 0;

            for (int i = 0; i < 12; i++)
            {
                mainpicarray[i].Image = commandpanel;
            }

            for (int i = 0; i < 6; i++)
            {
                onepicarray[i].Image = commandpanel2;
                twopicarray[i].Image = commandpanel2;
                mosimopicarray[i].Image = commandpanel2;
            }

        }


        //命令を一つ消す
        private void undo_button_Click(object sender, EventArgs e)
        {
            if (selectBox == 0)
            {
                if (mainpiccount < 0) mainpiccount = 0;
                else if (mainpiccount > 0)
                {
                    mainpicarray[mainpiccount - 1].Image = commandpanel;
                    mainpiccount--;
                }
                if (movelist.Count > 0)
                {
                    movelist.RemoveAt(movelist.Count - 1);
                }

            }

            if (selectBox == 1)
            {
                if (onepiccount < 0) onepiccount = 0;
                else if (onepiccount > 0)
                {
                    onepicarray[onepiccount - 1].Image = commandpanel2;
                    onepiccount--;
                }
                if (onelist.Count > 0)
                {
                    onelist.RemoveAt(onelist.Count - 1);
                }
            }

            if (selectBox == 2)
            {
                if (twopiccount < 0) twopiccount = 0;
                else if (twopiccount > 0)
                {
                    twopicarray[twopiccount - 1].Image = commandpanel2;
                    twopiccount--;
                }
                if (twolist.Count > 0)
                {
                    twolist.RemoveAt(twolist.Count - 1);
                }
            }
            if (selectBox == 3)
            {
                if (mosimopiccount < 0) mosimopiccount = 0;
                else if (mosimopiccount > 0)
                {
                    mosimopicarray[mosimopiccount - 1].Image = commandpanel2;
                    mosimopiccount--;
                }
                if (mosimolist.Count > 0)
                {
                    mosimolist.RemoveAt(mosimolist.Count - 1);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            selectBox = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            selectBox = 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            selectBox = 2;
        }

        private void makeStage_button_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) Hoge.PlayLooping();
            else Hoge.Stop();
        }

        private void radioButton_main_CheckedChanged(object sender, EventArgs e)
        {
            SelectedBoxChanged();
            if (radioButton_1.Checked == true) selectBox = 1;
            if (radioButton_2.Checked == true) selectBox = 2;
            if (radioButton_mosimo.Checked == true) selectBox = 3;
        }

        private void radioButton_1_CheckedChanged(object sender, EventArgs e)
        {
            SelectedBoxChanged();
            if (radioButton_main.Checked == true) selectBox = 0;
            if (radioButton_1.Checked == true) selectBox = 1;
            if (radioButton_2.Checked == true) selectBox = 2;
            if (radioButton_mosimo.Checked == true) selectBox = 3;

        }

        private void radioButton_2_CheckedChanged(object sender, EventArgs e)
        {
            SelectedBoxChanged();
            if (radioButton_main.Checked == true) selectBox = 0;
            if (radioButton_1.Checked == true) selectBox = 1;
            if (radioButton_2.Checked == true) selectBox = 2;
            if (radioButton_mosimo.Checked == true) selectBox = 3;

        }

        private void radioButton_mosimo_CheckedChanged(object sender, EventArgs e)
        {
            SelectedBoxChanged();
            if (radioButton_main.Checked == true) selectBox = 0;
            if (radioButton_1.Checked == true) selectBox = 1;
            if (radioButton_2.Checked == true) selectBox = 2;
            if (radioButton_mosimo.Checked == true) selectBox = 3;
        }

        private void orderFlash(List<int> list, int index)//実行している命令の縁を赤くする
        {
            if (list == movelist)
            {
                mainpicarray[index].BackColor = Color.Red;
                mainpicarray[index].Refresh();
            }
            if (list == onelist)
            {
                onepicarray[index].BackColor = Color.Red;
                onepicarray[index].Refresh();
            }
            if (list == twolist)
            {
                twopicarray[index].BackColor = Color.Red;
                twopicarray[index].Refresh();
            }
            if (list == mosimolist)
            {
                mosimopicarray[index].BackColor = Color.Red;
                mosimopicarray[index].Refresh();
            }
        }
        private void orderFlashBack(List<int> list, int index)//実行している命令の縁を赤くする
        {
            if (list == movelist)
            {
                mainpicarray[index].BackColor = main_Box.BackColor;
                mainpicarray[index].Refresh();
            }
            if (list == onelist)
            {
                onepicarray[index].BackColor =one_Box.BackColor;
                onepicarray[index].Refresh();
            }
            if (list == twolist)
            {
                twopicarray[index].BackColor = two_Box.BackColor;
                twopicarray[index].Refresh();
            }
            if (list == mosimolist)
            {
                mosimopicarray[index].BackColor = if_Box.BackColor;
                mosimopicarray[index].Refresh();
            }
        }

    }
}
