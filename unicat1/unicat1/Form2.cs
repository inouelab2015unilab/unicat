using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace unicat1
{
    public partial class Form2 : Form
    {
        Graphics g;

        //画像を変数に格納する
        static Image back = Image.FromFile(@"../../素材/back.png");
        static Image road = Image.FromFile(@"../../素材/road.png");
        static Image fish = Image.FromFile(@"../../素材/fish.png");
        static Image fish2 = Image.FromFile(@"../../素材/fish2.png");
        static Image fish3 = Image.FromFile(@"../../素材/fish3.png");
        Image catu = Image.FromFile(@"../../素材/cat.png");
        Image catr = Image.FromFile(@"../../素材/catr.png");
        Image catl = Image.FromFile(@"../../素材/catl.png");
        static Image catd = Image.FromFile(@"../../素材/catd.png");
        Image cat;
        Image[] imageset = { back, road, back, catd, fish,fish2,fish3};
        static int boardsize=5;
        int cellsize;
        int cellnumber=0;
        static int[,] stage = new int[boardsize, boardsize];
        
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
            for (int i = 5; i <= 10; i++)
            {
                comboBox1.Items.Add(i+"×"+i);
            }
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "新しいステージ" + (System.IO.Directory.GetFiles("../../boardmatrix/", "*.csv").Count()-8);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width, pictureBox1.Height);
            boardsize = comboBox1.SelectedIndex + 5;
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
                        // 出力用のファイルを開く
                        string filepas = "../../boardmatrix/" + textBox1.Text + ".csv";
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

                    Application.Restart();
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

    }
}
