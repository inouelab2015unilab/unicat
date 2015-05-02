using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace unicat1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ////画像ファイルを読み込んで、Imageオブジェクトを作成する
            //System.Drawing.Image img = System.Drawing.Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\paneru.png");

            ////画像を表示する
            //pictureBox1.Image = img;

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            //画像ファイルを読み込んで、Imageオブジェクトとして取得する
            Image img = Image.FromFile(@"\\SERVERFILE1\Common\ユニラブ\ユニラブ2015\素材\paneru.png");
            //画像をcanvasの座標(20, 10)の位置に描画する
            g.DrawImage(img, 100, 10, img.Width, img.Height);
            //Imageオブジェクトのリソースを解放する
            img.Dispose();

            //Graphicsオブジェクトのリソースを解放する
            g.Dispose();
            //PictureBox1に表示する
            pictureBox1.Image = canvas;
        }
    }
}
