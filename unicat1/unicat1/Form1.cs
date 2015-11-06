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
        //string myname = "rinapon";
        string myname = Form3.username;

        Graphics g;
        Graphics g2;

        //画像を変数に格納する
        public Image back = Image.FromFile(@"素材/back.png");
        Image road = Image.FromFile(@"素材/road.png");
        Image fish = Image.FromFile(@"素材/fish.png");
        Image fish2 = Image.FromFile(@"素材/fish2.png");
        Image fish3 = Image.FromFile(@"素材/fish3.png");
        Image catu = Image.FromFile(@"素材/cat.png");
        Image catr = Image.FromFile(@"素材/catr.png");
        Image catl = Image.FromFile(@"素材/catl.png");
        Image catd = Image.FromFile(@"素材/catd.png");
        Image command1 = Image.FromFile(@"素材/up.png");
        Image command2 = Image.FromFile(@"素材/left.png");
        Image command3 = Image.FromFile(@"素材/right.png");
        Image command4 = Image.FromFile(@"素材/catch.png");
        Image commandpanel = Image.FromFile(@"素材/commandpanel.png");
        Image commandpanel2 = Image.FromFile(@"素材/commandpanel2.png");
        Image loop1 = Image.FromFile(@"素材/1.png");
        Image loop2 = Image.FromFile(@"素材/2.png");
        Image pic_if1 = Image.FromFile(@"素材/if1.png");
        Image pic_if2 = Image.FromFile(@"素材/if2.png");
        Image pic_if3 = Image.FromFile(@"素材/if3.png");
        Image cat;

        int[,] nowboard;    //現在選択されている盤面のデータ
        int xmax = 10, ymax = 10;   //盤面のサイズ       
        int catposx, catposy;
        int footcount;
        int fishcount;
        int fish2count;
        int fish3count;
        int totalscore;

        int orderone_count;
        int power = 100;
        int mainpiccount = 0;
        int onepiccount = 0;
        int twopiccount = 0;
        int mosimopiccount1 = 0;
        int mosimopiccount2 = 0;
        int mosimopiccount3 = 0;


        //0=上、1=右、2=下、3=左
        int catdirection = 0;

        int[] movecount;
        int selectBox = 0;
        List<int> movelist = new List<int>();  //メインボックスの命令リスト
        List<int> onelist = new List<int>();    //１ボックスの命令リスト
        List<int> twolist = new List<int>();    //２ボックスの命令リスト
        List<int> mosimolist1 = new List<int>();    //２ボックスの命令リスト
        List<int> mosimolist2 = new List<int>();
        List<int> mosimolist3 = new List<int>();
        List<Runking> scorelist = new List<Runking>(); //ランキングのリスト
        int nowstage = 0;
        int stagenum = 0;
        int Score = 0;

        public List<int[,]> boardlist = new List<int[,]>();
        PictureBox[] mainpicarray = new PictureBox[12];
        PictureBox[] onepicarray;
        PictureBox[] twopicarray = new PictureBox[6];
        PictureBox[] mosimopicarray1 = new PictureBox[6];
        PictureBox[] mosimopicarray2 = new PictureBox[6];
        PictureBox[] mosimopicarray3 = new PictureBox[6];

        SoundPlayer Hoge = new SoundPlayer(@"素材/BGM.wav");

        //盤面情報をCSVファイルから読み込み
        string[] files1 = System.IO.Directory.GetFiles("boardmatrix/", "*.csv");
        string[] files2 = System.IO.Directory.GetFiles("boardmatrix2/", "*.csv");


        public class Runking
        {
            public string Stage;
            public string FirstPerson;
            public double First;
            public string SecondPerson;
            public double Second;
            public string ThirdPerson;
            public double Third;
            public Runking(string s, string t, double x, string u, double y, string v, double z)
            {
                Stage = s;
                FirstPerson = t;
                First = x;
                SecondPerson = u;
                Second = y;
                ThirdPerson = v;
                Third = z;
            }
        }

        public Form1()
        {
            InitializeComponent();
            //ピクチャーボックス配列に各ピクチャーボックスを格納
            mainpicarray = new PictureBox[] { main1, main2, main3, main4, main5, main6, main7, main8, main9, main10, main11, main12 };
            onepicarray = new PictureBox[] { one1, one2, one3, one4, one5, one6 };
            twopicarray = new PictureBox[] { two1, two2, two3, two4, two5, two6 };
            mosimopicarray1 = new PictureBox[] { mosimo11, mosimo12, mosimo13, mosimo14, mosimo15, mosimo16 };
            mosimopicarray2 = new PictureBox[] { mosimo21, mosimo22, mosimo23, mosimo24, mosimo25, mosimo26 };
            mosimopicarray3 = new PictureBox[] { mosimo31, mosimo32, mosimo33, mosimo34, mosimo35, mosimo36 };


            foreach (var i in mainpicarray) i.Click += new EventHandler(main_Click);
            foreach (var i in onepicarray) i.Click += new EventHandler(one_Click);
            foreach (var i in twopicarray) i.Click += new EventHandler(two_Click);
            foreach (var i in mosimopicarray1) i.Click += new EventHandler(mosimo1_Click);
            foreach (var i in mosimopicarray2) i.Click += new EventHandler(mosimo2_Click);
            foreach (var i in mosimopicarray3) i.Click += new EventHandler(mosimo3_Click);


            SelectedBoxChanged();
            RankRead();

            radioButton2.Checked = true;
            radio_off.Checked = true;
            //音楽流す
            if (radioButton1.Checked == true)
            {
                SoundPlayer Hoge = new SoundPlayer(@"素材/BGM.wav");
                Hoge.PlayLooping();
            }
            movecount = new int[12];

            cat = catu;

            RandomMT rand = new RandomMT();

            //コンボボックスにステージ名を自動で追加
            for (int i = 0; i < files1.Length; i++)
            {
                var stagename = Path.GetFileName(files1[i]);
                stagename = stagename.Replace(".csv", "");
                comboBox1.Items.Add(stagename);

            }

            mosimocmb1.Items.Add("もしも、 前 に壁が なかったら");
            mosimocmb1.Items.Add("もしも、 左 に壁が なかったら");
            mosimocmb1.Items.Add("もしも、 右 に壁が なかったら");
            mosimocmb1.Items.Add("もしも、 前 に壁が あったら");
            mosimocmb1.Items.Add("もしも、 左 に壁が あったら");
            mosimocmb1.Items.Add("もしも、 右 に壁が あったら");
            mosimocmb2.Items.Add("もしも、 前 に壁が なかったら");
            mosimocmb2.Items.Add("もしも、 左 に壁が なかったら");
            mosimocmb2.Items.Add("もしも、 右 に壁が なかったら");
            mosimocmb2.Items.Add("もしも、 前 に壁が あったら");
            mosimocmb2.Items.Add("もしも、 左 に壁が あったら");
            mosimocmb2.Items.Add("もしも、 右 に壁が あったら");
            mosimocmb3.Items.Add("もしも、 前 に壁が なかったら");
            mosimocmb3.Items.Add("もしも、 左 に壁が なかったら");
            mosimocmb3.Items.Add("もしも、 右 に壁が なかったら");
            mosimocmb3.Items.Add("もしも、 前 に壁が あったら");
            mosimocmb3.Items.Add("もしも、 左 に壁が あったら");
            mosimocmb3.Items.Add("もしも、 右 に壁が あったら");



            //命令パネルの背景を設置
            foreach (var n in mainpicarray) n.Image = commandpanel;
            foreach (var n in onepicarray) n.Image = commandpanel2;
            foreach (var n in twopicarray) n.Image = commandpanel2;
            foreach (var n in mosimopicarray1) n.Image = commandpanel2;
            foreach (var n in mosimopicarray2) n.Image = commandpanel2;
            foreach (var n in mosimopicarray3) n.Image = commandpanel2;


            //命令ボタンの背景を設置
            gobutton.BackgroundImage = Image.FromFile(@"素材/up.png");
            turnleft_button.BackgroundImage = Image.FromFile(@"素材/left.png");
            turnright_button.BackgroundImage = Image.FromFile(@"素材/right.png");
            catchfish_button.BackgroundImage = Image.FromFile(@"素材/catch.png");
            one_button.BackgroundImage = Image.FromFile(@"素材/1.png");
            two_button.BackgroundImage = Image.FromFile(@"素材/2.png");
            if_button1.BackgroundImage = Image.FromFile(@"素材/if1.png");
            if_button2.BackgroundImage = Image.FromFile(@"素材/if2.png");
            if_button3.BackgroundImage = Image.FromFile(@"素材/if3.png");

            gobutton.Paint += new PaintEventHandler(button_Paint);
            turnleft_button.Paint += new PaintEventHandler(button_Paint);
            turnright_button.Paint += new PaintEventHandler(button_Paint);
            catchfish_button.Paint += new PaintEventHandler(button_Paint);
            one_button.Paint += new PaintEventHandler(button_Paint);
            two_button.Paint += new PaintEventHandler(button_Paint);
            if_button1.Paint += new PaintEventHandler(button_Paint);
            if_button2.Paint += new PaintEventHandler(button_Paint);
            if_button3.Paint += new PaintEventHandler(button_Paint);

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Bitmap power_canvas = new Bitmap(power_pictureBox.Width, power_pictureBox.Height);

            //ImageオブジェクトのGraphicsオブジェクトを作成する
            g = Graphics.FromImage(canvas);
            g2 = Graphics.FromImage(power_canvas);

            comboBox1.SelectedIndex = 0;    //コンボボックスの初期値
            selectBox = 0;
            mosimocmb1.SelectedIndex = 0;
            mosimocmb2.SelectedIndex = 0;
            mosimocmb3.SelectedIndex = 0;


            //盤面情報をCSVファイルから読み込み、boardlistに格納(要素は二次元配列)
            foreach (var n in files1)
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
            power_pictureBox.Image = power_canvas;
            pictureBox26.Image = fish;
            //画像の大きさをPictureBoxに合わせる
            pictureBox26.SizeMode = PictureBoxSizeMode.StretchImage;
            powerPaint();
            foodlabel.Text = checkfood().ToString();
            foodlabel.Refresh();

        }

        private void button_Paint(object sender, PaintEventArgs e)
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
            //harapekocount.Text = harapekoscore.Text = 0.ToString();

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
            foodlabel.Text = checkfood().ToString();
            foodlabel.Refresh();
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
                    mosimopicarray1[mosimopiccount1].Image = orderimage;
                    mosimolist1.Add(ordernum);
                    mosimopiccount1 += 1;
                }
                catch { }
            }
            else if (selectBox == 4)
            {
                try
                {
                    mosimopicarray2[mosimopiccount2].Image = orderimage;
                    mosimolist2.Add(ordernum);
                    mosimopiccount2 += 1;
                }
                catch { }
            }
            else if (selectBox == 5)
            {
                try
                {
                    mosimopicarray3[mosimopiccount3].Image = orderimage;
                    mosimolist3.Add(ordernum);
                    mosimopiccount3 += 1;
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
            orderclick(6, pic_if1);
        }
        private void if_button2_Click(object sender, EventArgs e)
        {
            orderclick(7, pic_if2);

        }
        private void if_button3_Click(object sender, EventArgs e)
        {
            orderclick(8, pic_if3);

        }
        private void mosimoaction(int cmbnum, List<int> mlist)
        {
            //List<int[,]> temp_boardlist = new List<int[,]>();

            int xmove = 0, ymove = 0;
            if (cmbnum == 0)
            {        //catdirection 0=上、1=右、2=下、3=左
                //方向と端にいるかどうかで移動の変化量を決める
                if (catdirection == 0) ymove = -1;
                if (catdirection == 2) ymove = 1;
                if (catdirection == 1) xmove = 1;
                if (catdirection == 3) xmove = -1;
                try
                {
                    if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] != 2)//移動した先が壁じゃない
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            listcheck(mlist, j);
                            totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                            if (totalscore <= -100) break;
                        }
                    }
                    else { Thread.Sleep(200); }

                }
                catch
                {

                }
            }
            else if (cmbnum == 1)
            {
                //方向と端にいるかどうかで移動の変化量を決める
                if (catdirection == 0) xmove = -1;
                if (catdirection == 2) xmove = 1;
                if (catdirection == 1) ymove = -1;
                if (catdirection == 3) ymove = 1;

                try
                {
                    if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] != 2)//移動した先が壁じゃない
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            listcheck(mlist, j);
                            //totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                            //if (totalscore <= -100) break;
                        }

                    }
                    else { Thread.Sleep(200); }

                }
                catch
                {

                }
            }
            else if (cmbnum == 2)
            {
                //方向と端にいるかどうかで移動の変化量を決める
                if (catdirection == 0) xmove = 1;
                if (catdirection == 2) xmove = -1;
                if (catdirection == 1) ymove = 1;
                if (catdirection == 3) ymove = -1;
                try
                {
                    if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] != 2)//移動した先が壁じゃない
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            listcheck(mlist, j);
                            //totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                            //if (totalscore <= -100) break;
                        }

                    }
                    else { Thread.Sleep(200); }

                }
                catch
                {

                }
            }
            else if (cmbnum == 3)
            {
                //方向と端にいるかどうかで移動の変化量を決める
                if (catdirection == 0) ymove = -1;
                if (catdirection == 2) ymove = 1;
                if (catdirection == 1) xmove = 1;
                if (catdirection == 3) xmove = -1;
                try
                {
                    if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] == 2)//移動した先が壁
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            listcheck(mlist, j);
                            //totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                            //if (totalscore <= -100) break;
                        }

                    }
                    else { Thread.Sleep(200); }

                }
                catch
                {
                    for (int j = 0; j < mlist.Count; j++)
                    {
                        listcheck(mlist, j);
                        //totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                        //if (totalscore <= -100) break;
                    }
                }
            }
            else if (cmbnum == 4)
            {
                //方向と端にいるかどうかで移動の変化量を決める
                if (catdirection == 0) xmove = -1;
                if (catdirection == 2) xmove = 1;
                if (catdirection == 1) ymove = -1;
                if (catdirection == 3) ymove = 1;

                try
                {
                    if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] == 2)//移動した先が壁
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            listcheck(mlist, j);
                            //totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                            //if (totalscore <= -100) break;
                        }

                    }
                    else { Thread.Sleep(200); }

                }
                catch
                {
                    for (int j = 0; j < mlist.Count; j++)
                    {
                        listcheck(mlist, j);
                        //totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                        //if (totalscore <= -100) break;
                    }
                }
            }
            else if (cmbnum == 5)
            {
                //方向と端にいるかどうかで移動の変化量を決める
                if (catdirection == 0) xmove = 1;
                if (catdirection == 2) xmove = -1;
                if (catdirection == 1) ymove = 1;
                if (catdirection == 3) ymove = -1;
                try
                {
                    if (boardlist[comboBox1.SelectedIndex][catposx + xmove, catposy + ymove] == 2)//移動した先が壁
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            listcheck(mlist, j);
                            //totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                            //if (totalscore <= -100) break;
                        }

                    }
                    else { Thread.Sleep(200); }

                }
                catch
                {
                    for (int j = 0; j < mlist.Count; j++)
                    {
                        listcheck(mlist, j);
                        //totalscore = fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5;
                        //if (totalscore <= -100) break;
                    }
                }
            }
        }

        private void listcheck(List<int> list, int index)
        {
            if (checkfood() != 0 && 0 < power)
            {
                usePower(list, index);//体力消費
                powerPaint();//体力描画
                orderFlash(list, index);//使用中の命令を赤くする
                if (list[index] == 0)
                {
                    catmove(catdirection);
                }
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
                    for (int k = 0; k < onelist.Count; k++)
                    {
                        listcheck(onelist, k);
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
                    mosimoaction(mosimocmb1.SelectedIndex, mosimolist1);
                }
                else if (list[index] == 7)
                {
                    mosimoaction(mosimocmb2.SelectedIndex, mosimolist2);
                }
                else if (list[index] == 8)
                {
                    mosimoaction(mosimocmb3.SelectedIndex, mosimolist3);
                }

                foodlabel.Text = checkfood().ToString();
                foodlabel.Refresh();
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

        //private bool check_inf()
        //{
        //    int one_c = 0;
        //    for (int i = 0; i < onelist.Count; i++)
        //    {
        //        if (onelist[i] == 0 || onelist[i] == 1 || onelist[i] == 2 || onelist[i] == 3)
        //        {
        //            one_c++;
        //        }
        //        else { break; }
        //    }
        //    int two_c = 0;
        //    for (int i = 0; i < twolist.Count; i++)
        //    {
        //        if (twolist[i] == 0 || twolist[i] == 1 || twolist[i] == 2 || twolist[i] == 3)
        //        {
        //            two_c++;
        //        }
        //        else { break; }
        //    }
        //    if (one_c != 0 || two_c != 0) return true;
        //    else return false;
        //}

        private void movebutton_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            //movelistに格納された番号にしたがって命令を実行
            for (int i = orderone_count; i < movelist.Count; i++)
            {
                listcheck(movelist, i);
            }
            sw.Stop();

            int foodcount = checkfood();
            foodlabel.Text = checkfood().ToString();
            foodlabel.Refresh();
            for (int i = 0; i < nowboard.GetLength(0); i++) //盤面の食べ物の数を数える
            {
                for (int j = 0; j < nowboard.GetLength(1); j++)
                {
                    if (nowboard[i, j] == 4 || nowboard[i, j] == 5 || nowboard[i, j] == 6) foodcount++;
                }

            }

            if (power <= 0)
            {
                MessageBox.Show("力尽きてしまいました", "残念！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                makeboard(boardlist[comboBox1.SelectedIndex]);
                scorereset();
            }
            else if (foodcount == 0) //全部食べられていたらクリア
            {
                //if (movelist.Count <= double.Parse(label11.Text))
                //{
                //    Form3 form3 = new Form3();
                //    form3.Show();
                //}
                myscore.Text = (6 * movelist.Count + onelist.Count + twolist.Count + mosimolist1.Count + mosimolist2.Count + mosimolist3.Count).ToString();
                if (radio_off.Checked == true)
                {
                    RankUpdate(comboBox1.Text, 6 * movelist.Count + onelist.Count + twolist.Count + mosimolist1.Count + mosimolist2.Count + mosimolist3.Count);
                }
                DialogResult result = MessageBox.Show("次に進みますか？", "ステージクリア！", MessageBoxButtons.YesNo, MessageBoxIcon.Information);



                try
                {
                    if (result == DialogResult.Yes)
                    {
                        makeboard(boardlist[comboBox1.SelectedIndex + 1]);
                        comboBox1.SelectedIndex++;
                        if (radio_off.Checked == true)
                        {
                            RankDisp();
                            myscore.Text = "";
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("全ステージクリア！", "おめでとう！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                MessageBox.Show("プログラムを見直してみよう！", "残念！", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                tabPage1.BackColor = DefaultBackColor;
                tabPage2.BackColor = DefaultBackColor;
                tabPage3.BackColor = DefaultBackColor;
                mosimocmb1.Enabled = false;
                mosimocmb2.Enabled = false;
                mosimocmb3.Enabled = false;

            }
            if (selectBox == 1)
            {
                main_Box.BackColor = DefaultBackColor;
                one_Box.BackColor = Color.Yellow;
                two_Box.BackColor = DefaultBackColor;
                tabPage1.BackColor = DefaultBackColor;
                tabPage2.BackColor = DefaultBackColor;
                tabPage3.BackColor = DefaultBackColor;
                mosimocmb1.Enabled = false;
                mosimocmb2.Enabled = false;
                mosimocmb3.Enabled = false;


            }
            if (selectBox == 2)
            {
                main_Box.BackColor = DefaultBackColor;
                one_Box.BackColor = DefaultBackColor;
                two_Box.BackColor = Color.Yellow;
                tabPage1.BackColor = DefaultBackColor;
                tabPage2.BackColor = DefaultBackColor;
                tabPage3.BackColor = DefaultBackColor;
                mosimocmb1.Enabled = false;
                mosimocmb2.Enabled = false;
                mosimocmb3.Enabled = false;


            }
            if (selectBox == 3)
            {
                main_Box.BackColor = DefaultBackColor;
                one_Box.BackColor = DefaultBackColor;
                two_Box.BackColor = DefaultBackColor;
                tabPage1.BackColor = Color.Yellow;
                tabPage2.BackColor = Color.Yellow;
                tabPage3.BackColor = Color.Yellow;
                mosimocmb1.Enabled = true;
                mosimocmb2.Enabled = false;
                mosimocmb3.Enabled = false;

            }
            if (selectBox == 4)
            {
                main_Box.BackColor = DefaultBackColor;
                one_Box.BackColor = DefaultBackColor;
                two_Box.BackColor = DefaultBackColor;
                tabPage1.BackColor = Color.Yellow;
                tabPage2.BackColor = Color.Yellow;
                tabPage3.BackColor = Color.Yellow;
                mosimocmb1.Enabled = false;
                mosimocmb2.Enabled = true;
                mosimocmb3.Enabled = false;

            }
            if (selectBox == 5)
            {
                main_Box.BackColor = DefaultBackColor;
                one_Box.BackColor = DefaultBackColor;
                two_Box.BackColor = DefaultBackColor;
                tabPage1.BackColor = Color.Yellow;
                tabPage2.BackColor = Color.Yellow;
                tabPage3.BackColor = Color.Yellow;
                mosimocmb1.Enabled = false;
                mosimocmb2.Enabled = false;
                mosimocmb3.Enabled = true;

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
                foodlabel.Text = fishcount.ToString();
                power = 100;
                powerPaint();
                //fish100score.Text = (fishcount * Score).ToString();
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
                float a = (float)Math.Sqrt(Math.Pow(pictureBox1.Width / (2 * xmax), 2) + Math.Pow(pictureBox1.Height / (2 * ymax), 2));//傾けたい角度
                //新しい座標位置を計算する
                float x = catposx * pictureBox1.Width / xmax + (pictureBox1.Width / (2 * xmax));   //中心のｘ座標
                float y = catposy * pictureBox1.Height / ymax + (pictureBox1.Height / (2 * ymax)); //中心のｙ座標
                float r = (float)Math.Sqrt(Math.Pow(pictureBox1.Width / xmax, 2) + Math.Pow(pictureBox1.Height / ymax, 2)); //中心のｙ座標
                float xx = x - (pictureBox1.Width / xmax) * (float)Math.Cos(d) / 2;
                float yy = y - (pictureBox1.Height / ymax) * (float)Math.Sin(d) / 2;
                float x1 = xx + a * (float)Math.Cos(i / (180 / Math.PI));
                float y1 = yy + a * (float)Math.Sin(i / (180 / Math.PI));
                float x2 = xx - a * (float)Math.Sin(i / (180 / Math.PI));
                float y2 = yy + a * (float)Math.Cos(i / (180 / Math.PI));
                //PointF配列を作成
                PointF[] destinationPoints = { new PointF(xx, yy), new PointF(x1, y1), new PointF(x2, y2) };
                //画像を表示
                g.DrawImage(road, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                g.DrawImage(cat, destinationPoints);
                pictureBox1.Refresh();
                Thread.Sleep(10);
            }
            g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
            pictureBox1.Refresh();
            Thread.Sleep(10);

        }


        //スコア初期化
        private void scorereset()
        {
            orderone_count = 0;
            fishcount = 0;
            fish2count = 0;
            fish3count = 0;
            footcount = 0;
            foodlabel.Text = checkfood().ToString();
            power = 100;
            powerPaint();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            makeboard(boardlist[comboBox1.SelectedIndex]);
            catdirection = 0;
            catd_change();
            myscore.Text = "";
        }

        private void orderreset_button_Click(object sender, EventArgs e)
        {
            movelist.Clear();
            onelist.Clear();
            twolist.Clear();
            mosimolist1.Clear();
            mosimolist2.Clear();
            mosimolist3.Clear();


            mainpiccount = 0;
            onepiccount = 0;
            twopiccount = 0;
            mosimopiccount1 = 0;
            mosimopiccount2 = 0;
            mosimopiccount3 = 0;


            for (int i = 0; i < 12; i++)
            {
                mainpicarray[i].Image = commandpanel;
            }

            for (int i = 0; i < 6; i++)
            {
                onepicarray[i].Image = commandpanel2;
                twopicarray[i].Image = commandpanel2;
                mosimopicarray1[i].Image = commandpanel2;
                mosimopicarray2[i].Image = commandpanel2;
                mosimopicarray3[i].Image = commandpanel2;

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
                if (mosimopiccount1 < 0) mosimopiccount1 = 0;
                else if (mosimopiccount1 > 0)
                {
                    mosimopicarray1[mosimopiccount1 - 1].Image = commandpanel2;
                    mosimopiccount1--;
                }
                if (mosimolist1.Count > 0)
                {
                    mosimolist1.RemoveAt(mosimolist1.Count - 1);
                }
            }
            if (selectBox == 4)
            {
                if (mosimopiccount2 < 0) mosimopiccount2 = 0;
                else if (mosimopiccount2 > 0)
                {
                    mosimopicarray2[mosimopiccount2 - 1].Image = commandpanel2;
                    mosimopiccount2--;
                }
                if (mosimolist2.Count > 0)
                {
                    mosimolist2.RemoveAt(mosimolist2.Count - 1);
                }
            }
            if (selectBox == 5)
            {
                if (mosimopiccount3 < 0) mosimopiccount3 = 0;
                else if (mosimopiccount3 > 0)
                {
                    mosimopicarray3[mosimopiccount3 - 1].Image = commandpanel2;
                    mosimopiccount3--;
                }
                if (mosimolist3.Count > 0)
                {
                    mosimolist3.RemoveAt(mosimolist3.Count - 1);
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

        bool flag;
        private void makeStage_button_Click(object sender, EventArgs e)
        {
            flag = true;
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) Hoge.PlayLooping();
            else Hoge.Stop();
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
            if (list == mosimolist1)
            {
                tabControl1.SelectedIndex = 0;
                mosimopicarray1[index].BackColor = Color.Red;
                mosimopicarray1[index].Refresh();
            }
            if (list == mosimolist2)
            {
                tabControl1.SelectedIndex = 1;
                mosimopicarray2[index].BackColor = Color.Red;
                mosimopicarray2[index].Refresh();
            }
            if (list == mosimolist3)
            {
                tabControl1.SelectedIndex = 2;
                mosimopicarray3[index].BackColor = Color.Red;
                mosimopicarray3[index].Refresh();
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
                onepicarray[index].BackColor = one_Box.BackColor;
                onepicarray[index].Refresh();
            }
            if (list == twolist)
            {
                twopicarray[index].BackColor = two_Box.BackColor;
                twopicarray[index].Refresh();
            }
            if (list == mosimolist1)
            {
                mosimopicarray1[index].BackColor = tabPage1.BackColor;
                mosimopicarray1[index].Refresh();
            }
            if (list == mosimolist2)
            {
                mosimopicarray2[index].BackColor = tabPage1.BackColor;
                mosimopicarray2[index].Refresh();
            }
            if (list == mosimolist3)
            {
                mosimopicarray3[index].BackColor = tabPage1.BackColor;
                mosimopicarray3[index].Refresh();
            }
        }

        private void powerPaint()//体力の描画
        {
            int x = power_pictureBox.Width / 20;
            int y = power_pictureBox.Height;
            int s = power / 5;
            Brush br = Brushes.LightGreen; ;
            if (s <= 5) br = Brushes.Red;
            else if (5 < s && s <= 10) br = Brushes.Yellow;
            else if (10 < s && s <= 20) br = Brushes.GreenYellow;

            g2.FillRectangle(Brushes.Black, 0, 0, power_pictureBox.Width, power_pictureBox.Height);
            for (int i = 0; i < s; i++)
            {
                g2.FillRectangle(br, i * x, 0, x - 2, y - 2);
            }
            power_pictureBox.Refresh();
        }

        private void usePower(List<int> list, int index)//体力の消費
        {
            if (list == movelist) power -= 10;
            else if (list == onelist) power -= 5;
            else if (list == twolist) power -= 5;
            else if (list == mosimolist1) power -= 5;
            else if (list == mosimolist2) power -= 5;
            else if (list == mosimolist3) power -= 5;

        }

        private void RankRead() //ランキング読み込み表示
        {
            StreamReader sr = new StreamReader("runking.csv", Encoding.GetEncoding("Shift_JIS")); //ランキング読み込み
            string Line = "";
            while (Line != null)
            {
                Line = sr.ReadLine();
                if (Line == null) break;
                string[] t = Line.Split(',');
                scorelist.Add(new Runking(t[0], t[1], double.Parse(t[2]), t[3], double.Parse(t[4]), t[5], double.Parse(t[6])));
                stagenum++;
            }
            sr.Close();
            //int now = 0;
            for (int i = 0; i < stagenum; i++)
            {
                if (scorelist[i].Stage == comboBox1.Text) //現ステージはリストの何番目か
                {
                    nowstage = i;
                    break;
                }
            }
            firstscore.Text = scorelist[nowstage].First.ToString();
            firstname.Text = scorelist[nowstage].FirstPerson;
            secondscore.Text = scorelist[nowstage].Second.ToString();
            secondname.Text = scorelist[nowstage].SecondPerson;
            thirdscore.Text = scorelist[nowstage].Third.ToString();
            thirdname.Text = scorelist[nowstage].ThirdPerson;
        }

        private void RankDisp() //ランキング読み込み表示
        {
            for (int i = 0; i < stagenum; i++)
            {
                if (scorelist[i].Stage == comboBox1.Text) //現ステージはリストの何番目か
                {
                    nowstage = i;
                    break;
                }
            }
            firstscore.Text = scorelist[nowstage].First.ToString();
            firstname.Text = scorelist[nowstage].FirstPerson;
            secondscore.Text = scorelist[nowstage].Second.ToString();
            secondname.Text = scorelist[nowstage].SecondPerson;
            thirdscore.Text = scorelist[nowstage].Third.ToString();
            thirdname.Text = scorelist[nowstage].ThirdPerson;
        }

        private void RankUpdate(string stagename, double myscore)
        {
            if (double.Parse(firstscore.Text) >= myscore) //今回1位!!
            {
                if ((myname != firstname.Text) || (myscore < double.Parse(firstscore.Text)))
                {
                    thirdscore.Text = secondscore.Text; //3に2を格下げ
                    thirdname.Text = secondname.Text;
                    scorelist[nowstage].Third = double.Parse(secondscore.Text);
                    scorelist[nowstage].ThirdPerson = secondname.Text;

                    secondscore.Text = firstscore.Text; //2に1を格下げ
                    secondname.Text = firstname.Text;
                    scorelist[nowstage].Second = double.Parse(firstscore.Text);
                    scorelist[nowstage].SecondPerson = firstname.Text;

                    firstscore.Text = myscore.ToString(); //1に自分スコアを入れる
                    firstname.Text = myname;
                    scorelist[nowstage].First = myscore;
                    scorelist[nowstage].FirstPerson = myname;
                }
            }

            else if (double.Parse(secondscore.Text) >= myscore) //今回2位!!
            {
                if ((myname != secondname.Text) || (myscore < double.Parse(secondscore.Text)))
                {
                    thirdscore.Text = secondscore.Text; //3に2を格下げ
                    thirdname.Text = secondname.Text;
                    scorelist[nowstage].Third = double.Parse(secondscore.Text);
                    scorelist[nowstage].ThirdPerson = secondname.Text;

                    secondscore.Text = myscore.ToString(); //2に自分のスコアを入れる
                    secondname.Text = myname;
                    scorelist[nowstage].Second = myscore;
                    scorelist[nowstage].SecondPerson = myname;
                }
            }
            else if (double.Parse(thirdscore.Text) >= myscore) //今回3位!!
            {
                if ((myname != thirdname.Text) || (myscore < double.Parse(thirdscore.Text)))
                {
                    thirdscore.Text = myscore.ToString(); //3に自分スコアを入れる
                    thirdname.Text = myname;
                    scorelist[nowstage].Third = myscore;
                    scorelist[nowstage].ThirdPerson = myname;
                }
            }
            //csvに書き出し
            System.IO.StreamWriter sw = new System.IO.StreamWriter("runking.csv", false, System.Text.Encoding.GetEncoding("shift_jis"));
            for (int i = 0; i < stagenum; i++)
            {
                sw.Write(scorelist[i].Stage + ",");
                sw.Write(scorelist[i].FirstPerson + ",");
                sw.Write(scorelist[i].First.ToString() + ",");
                sw.Write(scorelist[i].SecondPerson + ",");
                sw.Write(scorelist[i].Second.ToString() + ",");
                sw.Write(scorelist[i].ThirdPerson + ",");
                sw.Write(scorelist[i].Third.ToString());
                sw.WriteLine();
            }
            sw.Close();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            makeboard(boardlist[comboBox1.SelectedIndex]);
            footcount = 0;
            fishcount = 0;
            fish2count = 0;
            fish3count = 0;
            catdirection = 0;
            //harapekocount.Text = harapekoscore.Text = 0.ToString();
            if (radio_off.Checked == true) RankDisp();
        }


        private void button_orderone_Click(object sender, EventArgs e)
        {
            if (movelist.Count != 0)
            {
                listcheck(movelist, orderone_count);
                if (orderone_count == movelist.Count - 1)
                {
                    if (checkfood() == 0)
                    {
                        DialogResult result = MessageBox.Show("次に進みますか？", "ステージクリア！", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        try
                        {
                            if (result == DialogResult.Yes)
                            {
                                makeboard(boardlist[comboBox1.SelectedIndex + 1]);
                                comboBox1.SelectedIndex++;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("全ステージクリア！", "おめでとう！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("プログラムを見直してみよう！", "残念！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        makeboard(boardlist[comboBox1.SelectedIndex]);
                        scorereset();
                    }
                }
                else orderone_count++;
            }
            else
            {
                MessageBox.Show("プログラムを見直してみよう！", "残念！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                makeboard(boardlist[comboBox1.SelectedIndex]);
                scorereset();

            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            orderclick(0, command1);

        }

        private void main_Click(object sender, EventArgs e)
        {
            selectBox = 0;
            SelectedBoxChanged();
        }
        private void one_Click(object sender, EventArgs e)
        {
            selectBox = 1;
            SelectedBoxChanged();
        }
        private void two_Click(object sender, EventArgs e)
        {
            selectBox = 2;
            SelectedBoxChanged();
        }
        private void mosimo1_Click(object sender, EventArgs e)
        {
            selectBox = 3;
            SelectedBoxChanged();
        }
        private void mosimo2_Click(object sender, EventArgs e)
        {
            selectBox = 4;
            SelectedBoxChanged();
        }
        private void mosimo3_Click(object sender, EventArgs e)
        {
            selectBox = 5;
            SelectedBoxChanged();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectBox = tabControl1.SelectedIndex + 3;
            SelectedBoxChanged();
        }

        private void load()
        {
            comboBox1.Items.Clear();
            boardlist.Clear();
            files2 = System.IO.Directory.GetFiles("boardmatrix2/", "*.csv");
            if (radio_on.Checked == true)
            {
                firstscore.Text = "";
                secondscore.Text = "";
                thirdscore.Text = "";
                label12.Text = "";
                label13.Text = "";
                label14.Text = "";
                firstname.Text = "";
                secondname.Text = "";
                thirdname.Text = "";
                myscore.Text = "";
                //コンボボックスにステージ名を自動で追加
                for (int i = 0; i < files2.Length; i++)
                {
                    var stagename = Path.GetFileName(files2[i]);
                    stagename = stagename.Replace(".csv", "");
                    comboBox1.Items.Add(stagename);

                }
                //盤面情報をCSVファイルから読み込み、boardlistに格納(要素は二次元配列)
                foreach (var n2 in files2)
                {
                    using (StreamReader sr = new StreamReader(n2, Encoding.GetEncoding(932)))
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
                comboBox1.SelectedIndex = 0;
                makeboard(boardlist[comboBox1.SelectedIndex]);
            }
            else
            {
                label12.Text = "1位";
                label13.Text = "2位";
                label14.Text = "3位";
                RankDisp();

                //コンボボックスにステージ名を自動で追加
                for (int i = 0; i < files1.Length; i++)
                {
                    var stagename = Path.GetFileName(files1[i]);
                    stagename = stagename.Replace(".csv", "");
                    comboBox1.Items.Add(stagename);

                }
                //盤面情報をCSVファイルから読み込み、boardlistに格納(要素は二次元配列)
                foreach (var n1 in files1)
                {
                    using (StreamReader sr = new StreamReader(n1, Encoding.GetEncoding(932)))
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
                comboBox1.SelectedIndex = 0;
                makeboard(boardlist[comboBox1.SelectedIndex]);
            }

        }
        private void radio_on_CheckedChanged(object sender, EventArgs e)
        {
            load();
        }

        private void radio_off_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void thirdname_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (flag == true)
            {
                load();
                flag = false;
            }

        }

    }
}
