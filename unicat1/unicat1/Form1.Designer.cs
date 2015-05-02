namespace unicat1
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.left = new System.Windows.Forms.Button();
            this.down = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.up = new System.Windows.Forms.Button();
            this.score = new System.Windows.Forms.ListBox();
            this.Catch = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(29, 31);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 750);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(917, 110);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 78);
            this.button1.TabIndex = 1;
            this.button1.Text = "ねこ移動";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // left
            // 
            this.left.Location = new System.Drawing.Point(861, 288);
            this.left.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(107, 72);
            this.left.TabIndex = 2;
            this.left.Text = "⇐";
            this.left.UseVisualStyleBackColor = true;
            this.left.Click += new System.EventHandler(this.button2_Click);
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(965, 368);
            this.down.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(107, 68);
            this.down.TabIndex = 3;
            this.down.Text = "⇓";
            this.down.UseVisualStyleBackColor = true;
            this.down.Click += new System.EventHandler(this.down_Click);
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(1067, 290);
            this.right.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(113, 71);
            this.right.TabIndex = 4;
            this.right.Text = "⇒";
            this.right.UseVisualStyleBackColor = true;
            this.right.Click += new System.EventHandler(this.right_Click);
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(965, 211);
            this.up.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(107, 71);
            this.up.TabIndex = 5;
            this.up.Text = "⇑";
            this.up.UseVisualStyleBackColor = true;
            this.up.Click += new System.EventHandler(this.up_Click);
            // 
            // score
            // 
            this.score.FormattingEnabled = true;
            this.score.ItemHeight = 15;
            this.score.Location = new System.Drawing.Point(879, 592);
            this.score.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(281, 349);
            this.score.TabIndex = 6;
            this.score.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Catch
            // 
            this.Catch.Location = new System.Drawing.Point(943, 492);
            this.Catch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Catch.Name = "Catch";
            this.Catch.Size = new System.Drawing.Size(163, 62);
            this.Catch.TabIndex = 8;
            this.Catch.Text = "キャッチ！";
            this.Catch.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(529, 826);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 23);
            this.comboBox1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 952);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Catch);
            this.Controls.Add(this.score);
            this.Controls.Add(this.up);
            this.Controls.Add(this.right);
            this.Controls.Add(this.down);
            this.Controls.Add(this.left);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.Button up;
        private System.Windows.Forms.ListBox score;
        private System.Windows.Forms.Button Catch;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

