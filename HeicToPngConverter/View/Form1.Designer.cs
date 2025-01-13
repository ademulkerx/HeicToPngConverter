namespace HeicToPngConverter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnSelectFiles = new Button();
            btnConvert = new Button();
            statusLabel = new Label();
            progressBar = new ProgressBar();
            flowLayoutPanelImages = new FlowLayoutPanel();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            txtBaseName = new TextBox();
            chkRename = new CheckBox();
            panel2 = new Panel();
            btnClose = new DevExpress.XtraEditors.SimpleButton();
            LblVersion = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnSelectFiles
            // 
            btnSelectFiles.BackColor = Color.FromArgb(255, 192, 192);
            btnSelectFiles.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnSelectFiles.Location = new Point(578, 374);
            btnSelectFiles.Name = "btnSelectFiles";
            btnSelectFiles.Size = new Size(328, 37);
            btnSelectFiles.TabIndex = 0;
            btnSelectFiles.Text = "Dosya Seç";
            btnSelectFiles.UseVisualStyleBackColor = false;
            btnSelectFiles.Click += btnSelectFiles_Click;
            // 
            // btnConvert
            // 
            btnConvert.BackColor = Color.FromArgb(192, 255, 192);
            btnConvert.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnConvert.Location = new Point(578, 417);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(327, 37);
            btnConvert.TabIndex = 1;
            btnConvert.Text = "Dönüştür";
            btnConvert.UseVisualStyleBackColor = false;
            btnConvert.Click += btnConvert_Click;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            statusLabel.ForeColor = Color.White;
            statusLabel.Location = new Point(13, 445);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(101, 32);
            statusLabel.TabIndex = 2;
            statusLabel.Text = "Durum:";
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(0, 503);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(909, 23);
            progressBar.Step = 1;
            progressBar.TabIndex = 4;
            // 
            // flowLayoutPanelImages
            // 
            flowLayoutPanelImages.BackColor = Color.FromArgb(192, 192, 255);
            flowLayoutPanelImages.Dock = DockStyle.Top;
            flowLayoutPanelImages.Location = new Point(0, 32);
            flowLayoutPanelImages.Name = "flowLayoutPanelImages";
            flowLayoutPanelImages.Size = new Size(909, 336);
            flowLayoutPanelImages.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(numericUpDown1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtBaseName);
            panel1.Controls.Add(chkRename);
            panel1.Controls.Add(flowLayoutPanelImages);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(statusLabel);
            panel1.Controls.Add(btnSelectFiles);
            panel1.Controls.Add(progressBar);
            panel1.Controls.Add(btnConvert);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(911, 528);
            panel1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.loading_symbol;
            pictureBox1.Location = new Point(319, 74);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(271, 257);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            numericUpDown1.Location = new Point(310, 371);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(76, 29);
            numericUpDown1.TabIndex = 10;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(3, 373);
            label1.Name = "label1";
            label1.Size = new Size(158, 23);
            label1.TabIndex = 9;
            label1.Text = "Yeniden Adlandır";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtBaseName
            // 
            txtBaseName.Location = new Point(167, 373);
            txtBaseName.Name = "txtBaseName";
            txtBaseName.Size = new Size(139, 23);
            txtBaseName.TabIndex = 8;
            // 
            // chkRename
            // 
            chkRename.AutoSize = true;
            chkRename.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            chkRename.ForeColor = Color.White;
            chkRename.Location = new Point(393, 379);
            chkRename.Name = "chkRename";
            chkRename.Size = new Size(15, 14);
            chkRename.TabIndex = 7;
            chkRename.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btnClose);
            panel2.Controls.Add(LblVersion);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(909, 32);
            panel2.TabIndex = 6;
            // 
            // btnClose
            // 
            btnClose.ImageOptions.Image = Properties.Resources.close_32x32;
            btnClose.Location = new Point(872, 4);
            btnClose.Name = "btnClose";
            btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnClose.Size = new Size(38, 23);
            btnClose.TabIndex = 0;
            btnClose.Click += btnClose_Click;
            // 
            // LblVersion
            // 
            LblVersion.BorderStyle = BorderStyle.FixedSingle;
            LblVersion.Dock = DockStyle.Fill;
            LblVersion.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LblVersion.ForeColor = Color.White;
            LblVersion.Location = new Point(0, 0);
            LblVersion.Name = "LblVersion";
            LblVersion.Size = new Size(907, 30);
            LblVersion.TabIndex = 0;
            LblVersion.Text = "Heic To Png Convert V0.1";
            LblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.Logo_png;
            ClientSize = new Size(911, 528);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Heic To Png Convert V0.1";
            TopMost = true;
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnSelectFiles;
        private Button btnConvert;
        private Label statusLabel;
        private ProgressBar progressBar;
        private FlowLayoutPanel flowLayoutPanelImages;
        private Panel panel1;
        private Panel panel2;
        private Label LblVersion;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private Label label1;
        private TextBox txtBaseName;
        private CheckBox chkRename;
        private NumericUpDown numericUpDown1;
        private PictureBox pictureBox1;
    }
}
