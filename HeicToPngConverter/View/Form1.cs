using HeicToPngConverter.View;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeicToPngConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> heicFilePaths = new List<string>();

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "HEIC Files|*.heic",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                flowLayoutPanelImages.Controls.Clear();
                heicFilePaths.Clear();

                foreach (string file in openFileDialog.FileNames)
                {
                    heicFilePaths.Add(file);

                    try
                    {
                        // MagickImage ile resmi aç ve küçük önizleme oluþtur
                        using (var image = new MagickImage(file))
                        {
                            image.Resize(new MagickGeometry(100, 100) { IgnoreAspectRatio = false });

                            using (var ms = new MemoryStream())
                            {
                                image.Format = MagickFormat.Png;
                                image.Write(ms);
                                ms.Position = 0;

                                // Panel oluþtur
                                Panel panel = new Panel
                                {
                                    Width = 110,
                                    Height = 130,
                                    Margin = new Padding(5)
                                };

                                // PictureBox oluþtur
                                PictureBox pb = new PictureBox
                                {
                                    Width = 100,
                                    Height = 100,
                                    SizeMode = PictureBoxSizeMode.Zoom,
                                    Image = Image.FromStream(ms),
                                    Location = new Point(5, 0)
                                };

                                // Label oluþtur ve dosya adýný ata
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
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Resim yüklenirken hata: {ex.Message}");
                    }
                }
            }
        }


        private async void btnConvert_Click(object sender, EventArgs e)
        {
            using (Form2 form2 = new Form2())
            {
                // Form2'yi modal olarak aç ve sonucu al
                DialogResult result = form2.ShowDialog();

                // Sonucu kontrol et
                if (result == DialogResult.OK)
                {
                    using FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                    if (folderDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string outputFolder = folderDialog.SelectedPath;

                    progressBar.Value = 0;
                    progressBar.Maximum = heicFilePaths.Count;
                    statusLabel.Text = "Dönüþtürme iþlemi baþladý...";
                    btnConvert.Enabled = false;

                    // IProgress örneði oluþturma
                    IProgress<int> progress = new Progress<int>(increment =>
                    {
                        // UI thread'de progress bar'ý güncelle
                        progressBar.Value = Math.Min(progressBar.Maximum, progressBar.Value + increment);
                    });

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
                        await semaphore.WaitAsync();

                        var task = Task.Run(() =>
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
                                semaphore.Release();
                                // Ýlerlemeyi bildir
                                progress.Report(1);

                            }
                        });

                        tasks.Add(task);
                    }

                    await Task.WhenAll(tasks);


                    btnConvert.Enabled = true;


                    if (errorFiles.Count > 0)
                    {
                        bool originalTopMost = this.TopMost;
                        this.TopMost = false;  // Formu geçici olarak her zaman üstte yap
                        string errorMessage = "Bazý dosyalar dönüþtürülemedi:\n" + string.Join("\n", errorFiles);
                        statusLabel.Text = "Dönüþtürme iþlemi bitti...";
                        MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.TopMost = originalTopMost;  // Önceki TopMost durumuna geri dön
                    }
                    else
                    {
                        statusLabel.Text = "Dönüþtürme iþlemi bitti...";
                        MessageBox.Show("Tüm dosyalar baþarýyla dönüþtürüldü.", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {

            try
            {
                // Kullanýcýya onay mesajý göster
                DialogResult result = MessageBox.Show(
                    "Uygulamayý kapatmak istediðinizden emin misiniz?",
                    "Çýkýþ Onayý",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                // Eðer kullanýcý "Evet" derse, uygulama kapatýlýr
                if (result == DialogResult.Yes)
                {
                    // Uygulamayý kapat
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                // Hata yakalama ve kullanýcýya bilgi verme
                MessageBox.Show("Bir hata oluþtu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
