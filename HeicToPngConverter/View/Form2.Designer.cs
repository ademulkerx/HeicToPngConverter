namespace HeicToPngConverter.View
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
            components = new System.ComponentModel.Container();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            panel1 = new Panel();
            label5 = new Label();
            btn1 = new Button();
            btn2 = new Button();
            panel2 = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(209, 9);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(3, 7);
            label1.Name = "label1";
            label1.Size = new Size(58, 23);
            label1.TabIndex = 1;
            label1.Text = "000";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(110, 7);
            label2.Name = "label2";
            label2.Size = new Size(57, 23);
            label2.TabIndex = 2;
            label2.Text = "000";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(67, 7);
            label3.Name = "label3";
            label3.Size = new Size(37, 23);
            label3.TabIndex = 3;
            label3.Text = "X";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(166, 4);
            label4.Name = "label4";
            label4.Size = new Size(37, 23);
            label4.TabIndex = 4;
            label4.Text = "=";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(22, 56);
            panel1.Name = "panel1";
            panel1.Size = new Size(319, 40);
            panel1.TabIndex = 5;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(22, 18);
            label5.Name = "label5";
            label5.Size = new Size(319, 43);
            label5.TabIndex = 6;
            label5.Text = "Lütfen işlem sonucunu girin...";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btn1
            // 
            btn1.BackColor = Color.FromArgb(192, 255, 192);
            btn1.Location = new Point(22, 102);
            btn1.Name = "btn1";
            btn1.Size = new Size(319, 23);
            btn1.TabIndex = 7;
            btn1.Text = "Kontrol Et";
            btn1.UseVisualStyleBackColor = false;
            btn1.Click += button1_Click;
            // 
            // btn2
            // 
            btn2.BackColor = Color.FromArgb(255, 192, 192);
            btn2.Location = new Point(240, 353);
            btn2.Name = "btn2";
            btn2.Size = new Size(105, 39);
            btn2.TabIndex = 8;
            btn2.Text = "Dönüşümü\r\nBaşlat";
            btn2.UseVisualStyleBackColor = false;
            btn2.Click += btn2_Click;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(btn1);
            panel2.Location = new Point(111, 117);
            panel2.Name = "panel2";
            panel2.Size = new Size(363, 226);
            panel2.TabIndex = 9;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 500;
            timer2.Tick += timer2_Tick;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 461);
            Controls.Add(btn2);
            Controls.Add(panel2);
            Cursor = Cursors.Default;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Human Detector";
            TopMost = true;
            Load += Form2_Load;
            MouseMove += Form2_MouseMove;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private Label label5;
        private Button btn1;
        private Button btn2;
        private Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}