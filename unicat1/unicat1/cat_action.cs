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
        //ネコがある方向に一つ進む
        public void catmove(int direction)
        {

            footcount++;
            int xmove = 0, ymove = 0;
            //方向と端にいるかどうかで移動の変化量を決める
            if (direction == 0 && catposy != 0) ymove = -1;
            if (direction == 2 && catposy != ymax - 1) ymove = 1;
            if (direction == 1 && catposx != xmax - 1) xmove = 1;
            if (direction == 3 && catposx != 0) xmove = -1;

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
                    //引数（画像、ｘ座標、y座標、width,height）

                    pictureBox1.Refresh();
                    Thread.Sleep(1);
                }
                catposx += xmove;
                catposy += ymove;
                harapekocount.Text = footcount.ToString();
                harapekoscore.Text = (-footcount * 5).ToString();
                totalscorelabel.Text = (100 + fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5).ToString();
                totalscorelabel.Refresh();
                harapekocount.Refresh();
                harapekoscore.Refresh();
                Thread.Sleep(100);
            }
            else
            {
                for (int i = 1; i <= 10; i++)
                {
                    g.DrawImage(road, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                    g.DrawImage(cat, catposx * pictureBox1.Width / xmax + xmove * i, catposy * pictureBox1.Height / ymax + ymove * i, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                    pictureBox1.Refresh();
                    Thread.Sleep(5);
                }
                for (int i = 1; i <= 10; i++)
                {
                    g.DrawImage(back, (catposx + xmove) * pictureBox1.Width / xmax, (catposy + ymove) * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                    g.DrawImage(cat, catposx * pictureBox1.Width / xmax, catposy * pictureBox1.Height / ymax, pictureBox1.Width / xmax, pictureBox1.Height / ymax);
                    pictureBox1.Refresh();
                    Thread.Sleep(5);
                }
                harapekocount.Text = footcount.ToString();
                harapekoscore.Text = (-footcount * 5).ToString();
                totalscorelabel.Text = (100 + fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5).ToString();
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
            totalscorelabel.Text = (100 + fishcount * 100 + fish2count * 300 + fish3count * 500 - footcount * 5).ToString();
            totalscorelabel.Refresh();
            harapekocount.Refresh();
            harapekoscore.Refresh();
            pictureBox1.Refresh();
            Thread.Sleep(200);
        }
    }
}
