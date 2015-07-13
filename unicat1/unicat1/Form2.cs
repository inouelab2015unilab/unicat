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
        
        public Form2()
        {
            InitializeComponent();
            pictureBox_back.Image = back;
            pictureBox_back.SizeMode = PictureBoxSizeMode.StretchImage;//サイズ合わせ
            pictureBox_road.Image = road;
            pictureBox_road.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_cat.Image = catd;
            pictureBox_cat.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_fish1.Image = fish;
            pictureBox_fish1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_fish2.Image = fish2;
            pictureBox_fish2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_fish3.Image = fish3;
            pictureBox_fish3.SizeMode = PictureBoxSizeMode.StretchImage;
            for (int i = 5; i <= 10; i++)
            {
                comboBox1.Items.Add(i+"×"+i);
            }
            comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(canvas);
            g.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width, pictureBox1.Height);
            int boardsize = comboBox1.SelectedIndex + 5;
            int cellsize = pictureBox1.Width / boardsize;
            for (int i = 0; i < boardsize; i++)
            {
                for (int j = 0; j < boardsize; j++)
                {
                    g.DrawImage(back, i * cellsize, j * cellsize, cellsize, cellsize);
                }
            }

            g.Dispose();
            pictureBox1.Image = canvas;
            pictureBox1.Refresh();

        }


    }
}
