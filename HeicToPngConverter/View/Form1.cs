using HeicToPngConverter.View;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace HeicToPngConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> heicFilePaths = new List<string>();

        private async void btnSelectFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "HEIC Files|*.heic",
                Multiselect = true
            };

            // Kullan�c� dosya se�ip OK'e t�klad���nda i�lemlere ba�la
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Dosya se�ildiyse pictureBox'� g�r�n�r yap ve butonu devre d��� b�rak
                pictureBox1.Visible = true;
                btnSelectFiles.Enabled = false;
                btnConvert.Enabled = false;

                await methotSelectFilef(openFileDialog);

                pictureBox1.Visible = false;
                btnSelectFiles.Enabled = true;
                btnConvert.Enabled = true;
            }
        }

        public async Task methotSelectFilef(OpenFileDialog openFileDialog)
        {
            // Se�im i�lemi tamamland��� i�in flowLayoutPanel'i ve ilgili listeleri temizle
            flowLayoutPanelImages.Controls.Clear();
            heicFilePaths.Clear();

            // Arka planda dosya i�leme i�lemini ba�lat
            await Task.Run(() =>
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    heicFilePaths.Add(file);
                    try
                    {
                        using (var image = new MagickImage(file))
                        {
                            image.Resize(new MagickGeometry(100, 100) { IgnoreAspectRatio = false });

                            using (var ms = new MemoryStream())
                            {
                                image.Format = MagickFormat.Png;
                                image.Write(ms);
                                ms.Position = 0;

                                // UI g�ncellemesi ana i� par�ac���nda yap�l�r
                                this.Invoke((MethodInvoker)delegate
                                {
                                    Panel panel = new Panel
                                    {
                                        Width = 110,
                                        Height = 130,
                                        Margin = new Padding(5)
                                    };

                                    PictureBox pb = new PictureBox
                                    {
                                        Width = 100,
                                        Height = 100,
                                        SizeMode = PictureBoxSizeMode.Zoom,
                                        Image = Image.FromStream(ms),
                                        Location = new Point(5, 0)
                                    };

                                    Label lbl = new Label
                                    {
                                        Text = Path.GetFileName(file),
                                        AutoSize = false,
                                        TextAlign = ContentAlignment.MiddleCenter,
                                        Width = 100,
                                        Height = 30,
                                        Location = new Point(5, 100)
                                    };

                                    panel.Controls.Add(pb);
                                    panel.Controls.Add(lbl);
                                    flowLayoutPanelImages.Controls.Add(panel);
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show($"Resim y�klenirken hata: {ex.Message}");
                        });
                    }
                }
            });
        }


        private async void btnConvert_Click(object sender, EventArgs e)
        {
            using (Form2 form2 = new Form2())
            {
                // Form2'yi modal olarak a� ve sonucu al
                DialogResult result = form2.ShowDialog();

                await methotConvert(result);

            }
        }

        public async Task methotConvert(DialogResult dialogResult)
        {
            // Sonucu kontrol et
            if (dialogResult == DialogResult.OK)
            {

                using FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                if (folderDialog.ShowDialog() != DialogResult.OK)
                    return;


                await Task.Run(() =>
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        btnSelectFiles.Enabled = false;
                        btnConvert.Enabled = false;
                        pictureBox1.Visible = true;

                        progressBar.Value = 0;
                        progressBar.Maximum = heicFilePaths.Count;
                        statusLabel.Text = "D�n��t�rme i�lemi ba�lad�...";
                        btnConvert.Enabled = false;
                    });
                    

                    string outputFolder = folderDialog.SelectedPath;



                    int maxDegreeOfParallelism = Environment.ProcessorCount;
                    SemaphoreSlim semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
                    List<Task> tasks = new List<Task>();
                    List<string> errorFiles = new List<string>();
                    int successCount = 0;

                    bool renameFiles = chkRename.Checked;
                    string baseName = txtBaseName.Text.Trim();
                    int firstNumber = (int)numericUpDown1.Value;
                    int fileIndex = firstNumber - 1;


                    foreach (var file in heicFilePaths)
                    {
                        try
                        {
                            string fileName;
                            if (renameFiles && !string.IsNullOrEmpty(baseName))
                            {
                                int currentNumber = Interlocked.Increment(ref fileIndex);
                                fileName = $"{baseName}_{currentNumber}.png";
                            }
                            else
                            {
                                fileName = Path.GetFileNameWithoutExtension(file) + ".png";
                            }

                            string pngFilePath = Path.Combine(outputFolder, fileName);

                            using (MagickImage image = new MagickImage(file))
                            {
                                image.Format = MagickFormat.Png;
                                image.Write(pngFilePath);
                            }

                            Interlocked.Increment(ref successCount);
                        }
                        catch (Exception ex)
                        {
                            lock (errorFiles)
                            {
                                errorFiles.Add($"{file}: {ex.Message}");
                            }
                        }
                        finally
                        {

                            this.Invoke((MethodInvoker)delegate
                            {
                                progressBar.Value = Math.Min(progressBar.Maximum, progressBar.Value + 1);
                            });
                        }
                    }





                    if (errorFiles.Count > 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            bool originalTopMost = this.TopMost;
                            this.TopMost = false;  // Formu ge�ici olarak her zaman �stte yap
                            string errorMessage = "Baz� dosyalar d�n��t�r�lemedi:\n" + string.Join("\n", errorFiles);
                            statusLabel.Text = "D�n��t�rme i�lemi bitti...";
                            MessageBox.Show(this,errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.TopMost = originalTopMost;  // �nceki TopMost durumuna geri d�n
                        });
                        
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            statusLabel.Text = "D�n��t�rme i�lemi bitti...";
                            MessageBox.Show(this,"T�m dosyalar ba�ar�yla d�n��t�r�ld�.", "Ba�ar�l�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                        
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        pictureBox1.Visible = false;
                        btnSelectFiles.Enabled = true;
                        btnConvert.Enabled = true;
                    });
                   

                });
                    
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {

            try
            {
                // Kullan�c�ya onay mesaj� g�ster
                DialogResult result = MessageBox.Show(
                    "Uygulamay� kapatmak istedi�inizden emin misiniz?",
                    "��k�� Onay�",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                // E�er kullan�c� "Evet" derse, uygulama kapat�l�r
                if (result == DialogResult.Yes)
                {
                    // Uygulamay� kapat
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                // Hata yakalama ve kullan�c�ya bilgi verme
                MessageBox.Show("Bir hata olu�tu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string VersionControl()
        {
            string path = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\VersionControl.txt";
            string content = File.ReadAllText(path);
            string[] parts = content.Split('-');

            int Up_Number = int.Parse(parts[0]);
            int Low_Number = int.Parse(parts[1]);
            int Rev_Number = int.Parse(parts[2]);
            string result = $"{Up_Number}.{Low_Number}.{Rev_Number}";
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LblVersion.Text = $"Heic To Png Convert V{VersionControl()}";
        }
    }
}
