using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace unicat1
{
    public partial class Form2 : Form
    {
        Graphics g;

        //画像を変数に格納する
        static Image back = Image.FromFile(@"素材/back.png");
        static Image road = Image.FromFile(@"素材/road.png");
        static Image fish = Image.FromFile(@"素材/fish.png");
        static Image fish2 = Image.FromFile(@"素材/fish2.png");
        static Image fish3 = Image.FromFile(@"素材/fish3.png");
        Image catu = Image.FromFile(@"素材/cat.png");
        Image catr = Image.FromFile(@"素材/catr.png");
        Image catl = Image.FromFile(@"素材/catl.png");
        static Image catd = Image.FromFile(@"素材/catd.png");
        public List<int[,]> boardlist = new List<int[,]>();
        Image cat;
        Image[] imageset = { back, road, back, catd, fish, fish2, fish3 };
        static int boardsize = 5;
        int cellsize;
        int cellnumber = 0;
        static int[,] stage = new int[boardsize, boardsize];
        string nowfile;
        //盤面情報をCSVファイルから読み込み
        string[] files = System.IO.Directory.GetFiles("boardmatrix2/", "*.csv");

        public Form2()
        {
            InitializeComponent();

            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(canvas);
            pictureBox1.Image = canvas;
            pictureBox_back.Image = back;
            pictureBox_back.SizeMode = PictureBoxSizeMode.StretchImage;//サイズ合わせ
            pictureBox_road.Image = road;
            pictureBox_road.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_cat.Image = catd;
            pictureBox_cat.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_fish1.Image = fish;
            pictureBox_fish1.SizeMode = PictureBoxSizeMode.StretchImage;

            loadData();

            listBox1.SelectedIndex = 0;
            makeboard(stage);


            for (int i = 5; i <= 10; i++)
            {
                comboBox1.Items.Add(i + "×" + i);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width, pictureBox1.Height);
            nowfile = listBox1.Items[listBox1.SelectedIndex].ToString();
            string filepath = "boardmatrix2/" + nowfile + ".csv";
            using (StreamReader sr = new StreamReader(filepath, Encoding.GetEncoding(932)))
            {
                List<string> templist = new List<string>();
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    templist.Add(s);
                }
                boardsize = templist.Count;
                for (int y = 0; y < boardsize; y++)
                {
                    string[] arraytemp = templist[y].Split(',');
                    for (int x = 0; x < boardsize; x++)
                    {
                        stage[x, y] = int.Parse(arraytemp[x]);
                    }
                }
            }
            cellsize = pictureBox1.Width / boardsize;
            cellnumber = 2;
            stage = new int[boardsize, boardsize];
            for (int i = 0; i < boardsize; i++)
            {
                for (int j = 0; j < boardsize; j++)
                {
                    g.DrawImage(back, i * cellsize, j * cellsize, cellsize, cellsize);
                    stage[i, j] = cellnumber;
                }
            }
            pictureBox1.Refresh();
            selected_pictureBox.Image = back;
            pictureBox_back.Refresh();
        }

        private void pictureBox_back_Click(object sender, EventArgs e)
        {
            cellnumber = 2;
            selected_pictureBox.Image = back;
            pictureBox_back.Refresh();
        }

        private void pictureBox_road_Click(object sender, EventArgs e)
        {
            cellnumber = 1;
            selected_pictureBox.Image = road;
            pictureBox_back.Refresh();
        }

        private void pictureBox_cat_Click(object sender, EventArgs e)
        {
            cellnumber = 3;
            selected_pictureBox.Image = catd;
            pictureBox_back.Refresh();
        }

        private void pictureBox_fish1_Click(object sender, EventArgs e)
        {
            cellnumber = 4;
            selected_pictureBox.Image = fish;
            pictureBox_back.Refresh();
        }

        private void pictureBox_fish2_Click(object sender, EventArgs e)
        {
            cellnumber = 5;
            selected_pictureBox.Image = fish2;
            pictureBox_back.Refresh();
        }

        private void pictureBox_fish3_Click(object sender, EventArgs e)
        {
            cellnumber = 6;
            selected_pictureBox.Image = fish3;
            pictureBox_back.Refresh();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                stage = boardlist[listBox1.SelectedIndex];
                boardsize = stage.GetLength(0);
                cellsize = pictureBox1.Width / boardsize;
                Point p = this.pictureBox1.PointToScreen(new Point(0, 0));
                int x = e.X / cellsize;
                int y = e.Y / cellsize;
                if (cellnumber != 3)
                {
                    g.DrawImage(imageset[cellnumber], x * cellsize, y * cellsize, cellsize, cellsize);
                    stage[x, y] = cellnumber;
                }
                else
                {
                    for (int i = 0; i < stage.GetLength(0); i++)
                    {
                        for (int j = 0; j < stage.GetLength(1); j++)
                        {
                            if (stage[i, j] == 3)
                            {
                                g.DrawImage(road, i * cellsize, j * cellsize, cellsize, cellsize);
                                stage[i, j] = 1;
                                break;
                            }
                        }
                    }
                    g.DrawImage(imageset[cellnumber], x * cellsize, y * cellsize, cellsize, cellsize);
                    stage[x, y] = cellnumber;
                }
                pictureBox1.Refresh();
            }
            catch { }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //メッセージボックスを表示する
            DialogResult result = MessageBox.Show("ステージを保存しますか？",
                "質問",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.OK)
            {
                if (check() == true)
                {
                    try
                    {
                        File.Move("boardmatrix2/" + nowfile + ".csv", "boardmatrix2/" + textBox1.Text + ".csv");
                        // 出力用のファイルを開く
                        string filepas = "boardmatrix2/" + textBox1.Text + ".csv";
                        using (var sw = new System.IO.StreamWriter(@filepas, false))
                        {
                            for (int j = 0; j < stage.GetLength(1); j++)
                            {
                                for (int i = 0; i < stage.GetLength(0); i++)
                                {
                                    if (i != stage.GetLength(0) - 1) sw.Write(stage[i, j] + ",");
                                    else sw.WriteLine(stage[i, j]);
                                }
                            }
                        }
                    }
                    catch { }

                    string[] files = System.IO.Directory.GetFiles("boardmatrix2/", "*.csv");
                    listBox1.Items.Clear();
                    foreach (var i in files)
                    {
                        string temp = i.Replace(".csv", "");
                        temp = temp.Replace("boardmatrix2/", "");
                        listBox1.Items.Add(temp);
                    }

                    //Application.Restart();
                }
                else
                {
                    MessageBox.Show("ネコを１匹、魚を１匹以上セットしてください", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (result == DialogResult.Cancel) { }
        }

        private bool check()
        {
            int c = 0;
            int f = 0;
            for (int j = 0; j < stage.GetLength(1); j++)
            {
                for (int i = 0; i < stage.GetLength(0); i++)
                {
                    if (stage[i, j] == 3) c++;
                    if (stage[i, j] == 4) f++;

                }
            }
            if (0 < c && 0 < f) return true;
            else return false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file = listBox1.Items[listBox1.SelectedIndex].ToString();
            stage = boardlist[listBox1.SelectedIndex];
            makeboard(stage);
            textBox1.Text = file;
        }

        private void loadData()
        {
            string[] files = System.IO.Directory.GetFiles("boardmatrix2/", "*.csv");
            listBox1.Items.Clear();
            boardlist.Clear();

            foreach (var pas in files)
            {
                string tempname = pas.Replace(".csv", "");
                tempname = tempname.Replace("boardmatrix2/", "");
                listBox1.Items.Add(tempname);
                using (StreamReader sr = new StreamReader(pas, Encoding.GetEncoding(932)))
                {
                    List<string> templist = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        templist.Add(s);
                    }
                    boardsize = templist.Count;
                    int[,] temp = new int[boardsize, boardsize];
                    for (int y = 0; y < boardsize; y++)
                    {
                        string[] arraytemp = templist[y].Split(',');
                        for (int x = 0; x < boardsize; x++)
                        {
                            temp[x, y] = int.Parse(arraytemp[x]);
                        }
                    }
                    boardlist.Add(temp);
                }
            }
            listBox1.SelectedIndex = 0;


        }

        private void makeboard(int[,] boardmat)
        {
            boardsize = boardmat.GetLength(0);
            boardsize = boardmat.Length / boardmat.GetLength(0);
            stage = new int[boardsize, boardsize];

            for (int i = 0; i < boardmat.GetLength(0); i++)
            {
                for (int j = 0; j < boardmat.GetLength(1); j++)
                {
                    stage[i, j] = boardmat[i, j];
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
                        g.DrawImage(catu, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));
                    }//猫だよ
                    else if (boardmat[i, j] == 4) g.DrawImage(fish, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚dayo
                    else if (boardmat[i, j] == 5) g.DrawImage(fish2, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚2dayo
                    else if (boardmat[i, j] == 6) g.DrawImage(fish3, i * pictureBox1.Width / boardmat.GetLength(0), j * pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)), pictureBox1.Width / boardmat.GetLength(0), pictureBox1.Height / (boardmat.Length / boardmat.GetLength(0)));          //魚3dayo

                }

            }
            pictureBox1.Refresh();
        }

        private void make_button_Click(object sender, EventArgs e)
        {
            boardsize = comboBox1.SelectedIndex + 5;
            stage = new int[boardsize, boardsize];
            string stagename = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                // 出力用のファイルを開く
                string filepas = "boardmatrix2/" + stagename + ".csv";
                using (var sw = new System.IO.StreamWriter(@filepas, false))
                {
                    for (int j = 0; j < stage.GetLength(1); j++)
                    {
                        for (int i = 0; i < stage.GetLength(0); i++)
                        {
                            if (i != stage.GetLength(0) - 1) sw.Write(2 + ",");
                            else sw.WriteLine(2);
                        }
                    }
                }
            }
            catch { }
            loadData();
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            string deletefile = listBox1.Items[listBox1.SelectedIndex].ToString();
            string filepath = "boardmatrix2/" + deletefile + ".csv";

            //メッセージボックスを表示する
            DialogResult result = MessageBox.Show("本当にこのステージを削除しますか？",
                "質問",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.OK)
            {
                System.IO.File.Delete(filepath);
                string[] file = System.IO.Directory.GetFiles("boardmatrix2/", "*.csv");
                listBox1.Items.Clear();
                foreach (var i in file)
                {
                    string temp = i.Replace(".csv", "");
                    temp = temp.Replace("boardmatrix2/", "");
                    listBox1.Items.Add(temp);
                }
                listBox1.SelectedIndex = 0;
                MessageBox.Show("削除しました");
            }
            loadData();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Restart();
        }
    }
}
