namespace unicat1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.make_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox_back = new System.Windows.Forms.PictureBox();
            this.pictureBox_road = new System.Windows.Forms.PictureBox();
            this.pictureBox_cat = new System.Windows.Forms.PictureBox();
            this.pictureBox_fish1 = new System.Windows.Forms.PictureBox();
            this.selected_pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.delete_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_back)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_road)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_fish1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selected_pictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(230, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(89, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.make_button);
            this.groupBox1.Location = new System.Drawing.Point(21, 336);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 70);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // make_button
            // 
            this.make_button.Location = new System.Drawing.Point(6, 38);
            this.make_button.Name = "make_button";
            this.make_button.Size = new System.Drawing.Size(89, 25);
            this.make_button.TabIndex = 16;
            this.make_button.Text = "新規作成";
            this.make_button.UseVisualStyleBackColor = true;
            this.make_button.Click += new System.EventHandler(this.make_button_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(642, 383);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 29);
            this.button2.TabIndex = 4;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox_back
            // 
            this.pictureBox_back.Location = new System.Drawing.Point(642, 182);
            this.pictureBox_back.Name = "pictureBox_back";
            this.pictureBox_back.Size = new System.Drawing.Size(60, 60);
            this.pictureBox_back.TabIndex = 5;
            this.pictureBox_back.TabStop = false;
            this.pictureBox_back.Click += new System.EventHandler(this.pictureBox_back_Click);
            // 
            // pictureBox_road
            // 
            this.pictureBox_road.Location = new System.Drawing.Point(714, 182);
            this.pictureBox_road.Name = "pictureBox_road";
            this.pictureBox_road.Size = new System.Drawing.Size(60, 60);
            this.pictureBox_road.TabIndex = 6;
            this.pictureBox_road.TabStop = false;
            this.pictureBox_road.Click += new System.EventHandler(this.pictureBox_road_Click);
            // 
            // pictureBox_cat
            // 
            this.pictureBox_cat.Location = new System.Drawing.Point(642, 249);
            this.pictureBox_cat.Name = "pictureBox_cat";
            this.pictureBox_cat.Size = new System.Drawing.Size(60, 60);
            this.pictureBox_cat.TabIndex = 7;
            this.pictureBox_cat.TabStop = false;
            this.pictureBox_cat.Click += new System.EventHandler(this.pictureBox_cat_Click);
            // 
            // pictureBox_fish1
            // 
            this.pictureBox_fish1.Location = new System.Drawing.Point(714, 249);
            this.pictureBox_fish1.Name = "pictureBox_fish1";
            this.pictureBox_fish1.Size = new System.Drawing.Size(60, 60);
            this.pictureBox_fish1.TabIndex = 8;
            this.pictureBox_fish1.TabStop = false;
            this.pictureBox_fish1.Click += new System.EventHandler(this.pictureBox_fish1_Click);
            // 
            // selected_pictureBox
            // 
            this.selected_pictureBox.Location = new System.Drawing.Point(10, 19);
            this.selected_pictureBox.Name = "selected_pictureBox";
            this.selected_pictureBox.Size = new System.Drawing.Size(60, 60);
            this.selected_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.selected_pictureBox.TabIndex = 11;
            this.selected_pictureBox.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.selected_pictureBox);
            this.groupBox2.Location = new System.Drawing.Point(666, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(78, 86);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "選択中";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(642, 360);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 19);
            this.textBox1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(640, 346);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "ステージ名";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(21, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(194, 324);
            this.listBox1.TabIndex = 15;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // delete_button
            // 
            this.delete_button.Location = new System.Drawing.Point(133, 373);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(82, 25);
            this.delete_button.TabIndex = 17;
            this.delete_button.Text = "削除";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(792, 421);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox_fish1);
            this.Controls.Add(this.pictureBox_cat);
            this.Controls.Add(this.pictureBox_road);
            this.Controls.Add(this.pictureBox_back);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ステージ編集";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_back)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_road)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_fish1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selected_pictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox_back;
        private System.Windows.Forms.PictureBox pictureBox_road;
        private System.Windows.Forms.PictureBox pictureBox_cat;
        private System.Windows.Forms.PictureBox pictureBox_fish1;
        private System.Windows.Forms.PictureBox selected_pictureBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button make_button;
        private System.Windows.Forms.Button delete_button;
    }
}