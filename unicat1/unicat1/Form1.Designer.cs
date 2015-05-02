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
            this.select = new System.Windows.Forms.ComboBox();
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
            this.button1.Location = new System.Drawing.Point(918, 110);
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
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(107, 73);
            this.left.TabIndex = 2;
            this.left.Text = "⇐";
            this.left.UseVisualStyleBackColor = true;
            this.left.Click += new System.EventHandler(this.button2_Click);
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(966, 367);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(106, 67);
            this.down.TabIndex = 3;
            this.down.Text = "⇓";
            this.down.UseVisualStyleBackColor = true;
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(1067, 290);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(113, 71);
            this.right.TabIndex = 4;
            this.right.Text = "⇒";
            this.right.UseVisualStyleBackColor = true;
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(966, 211);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(106, 71);
            this.up.TabIndex = 5;
            this.up.Text = "⇑";
            this.up.UseVisualStyleBackColor = true;
            // 
            // score
            // 
            this.score.FormattingEnabled = true;
            this.score.ItemHeight = 15;
            this.score.Location = new System.Drawing.Point(861, 500);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(282, 349);
            this.score.TabIndex = 6;
            this.score.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // select
            // 
            this.select.FormattingEnabled = true;
            this.select.Location = new System.Drawing.Point(918, 56);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(131, 23);
            this.select.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 953);
            this.Controls.Add(this.select);
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
        private System.Windows.Forms.ComboBox select;
    }
}

