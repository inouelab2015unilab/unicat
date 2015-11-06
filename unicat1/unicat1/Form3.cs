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
    public partial class Form3 : Form
    {
        static public string username = "";
        public Form3()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_name.Text == string.Empty)
            {
                MessageBox.Show("名前を入力してね");
            }
            else
            {
                username = textBox_name.Text;
                DialogResult result = MessageBox.Show(username + "さんでよろしいですか？", "質問", MessageBoxButtons.OKCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.OK)
                {
                    Form1 f = new Form1();
                    //フォームを表示する
                    f.Show();
                }

            }

        }
    }
}
