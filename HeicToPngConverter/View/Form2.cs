using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace HeicToPngConverter.View
{
    public partial class Form2 : Form
    {
        Random rnd = new Random();
        int number1, number2;
        bool isAnswerCorrect = false; // Cevabın doğruluğunu takip için

        private const int checkInterval = 1;    // Timer interval in ms
        private const double fleeSpeed = 10;     // Kaçma hızı (piksel)
        private const int distanceThreshold = 100; // Buton kaçma mesafe eşiği

        public Form2()
        {
            InitializeComponent();

            // Olay bağlamaları
            this.Load += Form2_Load;
            timer1.Tick += Timer1_Tick;
            btn2.TabStop = false;

            // MouseMove olayı, eski mesafe tabanlı kontrol için
            // Bu örnekte Timer kullanacağımız için isteğe bağlı bırakabiliriz.
            this.MouseMove += Form2_MouseMove;
        }

        int uyarı = 0;
        private async void ShowBlackout()
        {
            using (BlackoutForm blackout = new BlackoutForm())
            {
                uyarı++;
                if (uyarı <= 2)
                {
                    int time = 150;
                    blackout.Show();                 // Siyah ekranı göster
                    await System.Threading.Tasks.Task.Delay(time); // 1 saniye bekle
                    blackout.Hide();                // 1 saniye sonra kapa
                    await System.Threading.Tasks.Task.Delay(time); // 1 saniye bekle

                    blackout.Show();                 // Siyah ekranı göster
                    await System.Threading.Tasks.Task.Delay(time); // 1 saniye bekle
                    blackout.Hide();                // 1 saniye sonra kapa
                    await System.Threading.Tasks.Task.Delay(time); // 1 saniye bekle

                    blackout.Show();                 // Siyah ekranı göster
                    await System.Threading.Tasks.Task.Delay(time); // 1 saniye bekle
                    blackout.Close();                // 1 saniye sonra kapa

                    MessageBox.Show(this,                      // Sahibi belirtmek için this eklendi
                                 "Yoruluyorum ama, Dur! ve sadece işlemi yap...",
                                 "Uyarı",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                }

                if (uyarı >= 3)
                {
                    int time = 3000;
                    blackout.Show();                 // Siyah ekranı göster
                    await System.Threading.Tasks.Task.Delay(time); // 1 saniye bekle
                    blackout.Hide();                // 1 saniye sonra kapa

                    blackout.Show();                 // Siyah ekranı göster
                    await System.Threading.Tasks.Task.Delay(150); // 1 saniye bekle
                    blackout.Hide();

                    blackout.Show();                 // Siyah ekranı göster
                    await System.Threading.Tasks.Task.Delay(150); // 1 saniye bekle
                    blackout.Hide();

                    blackout.Show();                 // Siyah ekranı göster
                    await System.Threading.Tasks.Task.Delay(time); // 1 saniye bekle
                    blackout.Close();

                    MessageBox.Show(this,                      // Sahibi belirtmek için this eklendi
                                 "Kusura bakma biraz dinlendim. 😎 😈 😈",
                                 "Uyarı",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);


                    uyarı = 0;
                }

            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 100 ile 999 arasında iki rastgele sayı üret
            number1 = rnd.Next(100, 1000);
            number2 = rnd.Next(100, 1000);

            // Label'lara bu sayıları ata
            label1.Text = number1.ToString();
            label2.Text = number2.ToString();

            // Timer ayarları
            timer1.Interval = checkInterval;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int userAnswer))
            {
                int correctAnswer = number1 * number2;
                if (userAnswer == correctAnswer)
                {
                    btn2.Enabled = true;
                    btn1.Enabled = false;
                    textBox1.Enabled = false;
                    isAnswerCorrect = true;
                    MessageBox.Show("Tebrikler, doğru cevap!");
                }
                else
                {
                    number1 = rnd.Next(100, 1000);
                    number2 = rnd.Next(100, 1000);
                    label1.Text = number1.ToString();
                    label2.Text = number2.ToString();

                    textBox1.Clear();
                    textBox1.Focus();

                    MessageBox.Show($"Yanlış cevap! Doğru cevap: {correctAnswer}");
                    isAnswerCorrect = false;
                }

               
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.");
            }
        }


        private bool hasWarnedCalculator = false;
        private DateTime? lastWarningTime = null;
        private readonly TimeSpan warningCooldown = TimeSpan.FromSeconds(5); // 5 saniyelik bekleme süresi

        private void CheckAndCloseCalculator()
        {
            bool calculatorFound = false;
            Process[] allProcesses = Process.GetProcesses();

            foreach (var proc in allProcesses)
            {
                try
                {
                    string processName = proc.ProcessName.ToLower();
                    if (processName.Contains("calc") || processName.Contains("calculator"))
                    {
                        calculatorFound = true;
                        proc.Kill();
                    }
                }
                catch
                {
                    // Belirli bir süreç için erişim hatası oluşursa, bu işlemi atla.
                }
            }

            // Hesap makinesi bulunduysa ve bekleme süresi dolduysa uyarı göster
            if (calculatorFound)
            {
                bool canWarn = false;

                if (!hasWarnedCalculator)
                {
                    // Daha önce uyarı gösterilmemişse hemen uyar
                    canWarn = true;
                }
                else if (lastWarningTime.HasValue && DateTime.Now - lastWarningTime > warningCooldown)
                {
                    // Önceki uyarıdan belirli bir süre geçtiyse uyar
                    canWarn = true;
                }

                if (canWarn)
                {
                    MessageBox.Show(this,
                                    "Hesap makinesi kullanmak yasaktır ve kapatıldı.",
                                    "Uyarı",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    hasWarnedCalculator = true;
                    lastWarningTime = DateTime.Now;
                }
            }
            else
            {
                // Hesap makinesi bulunamadıysa, uyarı bayrağını sıfırla
                hasWarnedCalculator = false;
            }
        }


        private int chaseCounter = 0;
        private const int chaseThreshold = 70; // Örneğin, 5 kez kovalanma eşik değeri
        private void Timer1_Tick(object sender, EventArgs e)
        {

            // Eğer doğru cevap alındıysa veya başka bir sebeple işlem yapılmamalıysa çık
            if (isAnswerCorrect)
                return;

            // Fare ve buton merkezi konumlarını hesapla
            Point mousePos = this.PointToClient(Cursor.Position);
            Point btnCenter = new Point(
                btn2.Location.X + btn2.Width / 2,
                btn2.Location.Y + btn2.Height / 2);

            // Fare ile buton merkezi arasındaki mesafeyi hesapla
            double dx = btnCenter.X - mousePos.X;
            double dy = btnCenter.Y - mousePos.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            // Fare belirli bir mesafeye yakınsa
            if (distance < distanceThreshold)
            {
                // Kovalanma sayaçını artır
                chaseCounter++;

                // Eğer kovalanma sayısı eşiği aştıysa siyah ekran göster
                if (chaseCounter >= chaseThreshold)
                {
                    ShowBlackout();      // Ekranı 1 saniyeliğine siyaha çeviren metod
                    chaseCounter = 0;    // Sayaç sıfırlanıyor
                }

                // Kaçış yönü hesaplamaları
                if (distance == 0)
                    distance = 1; // Bölme hatasını önle

                double normX = dx / distance;
                double normY = dy / distance;

                // Kaçışa rastgelelik eklemek için açısal sapma hesapla
                double angleOffset = rnd.NextDouble() * Math.PI / 4 - Math.PI / 8; // ±22.5 derece
                double cosOffset = Math.Cos(angleOffset);
                double sinOffset = Math.Sin(angleOffset);

                double adjustedX = normX * cosOffset - normY * sinOffset;
                double adjustedY = normX * sinOffset + normY * cosOffset;

                // Butonun yeni konumunu hesapla
                int newX = (int)(btn2.Location.X + adjustedX * fleeSpeed);
                int newY = (int)(btn2.Location.Y + adjustedY * fleeSpeed);

                // Form sınırlarını alarak yeni konumu sınırla
                int maxX = this.ClientSize.Width - btn2.Width;
                int maxY = this.ClientSize.Height - btn2.Height;

                newX = Math.Max(0, Math.Min(maxX, newX));
                newY = Math.Max(0, Math.Min(maxY, newY));

                int edgeThreshold = 50;
                bool nearHorizontalEdge = (newX < edgeThreshold) || (newX > maxX - edgeThreshold);
                bool nearVerticalEdge = (newY < edgeThreshold) || (newY > maxY - edgeThreshold);

                // Buton kenarlara yakınsa rastgele yeni bir konuma taşı
                if (nearHorizontalEdge || nearVerticalEdge)
                {
                    newX = rnd.Next(edgeThreshold, maxX - edgeThreshold);
                    newY = rnd.Next(edgeThreshold, maxY - edgeThreshold);
                }

                btn2.Location = new Point(newX, newY);
            }
        }



        // İsteğe bağlı: Eski MouseMove tabanlı yaklaşım
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            // Bu metod isteğe bağlıdır; Timer ile sürekli kontrol sağlanıyor.
            // Eğer sadece Timer ile kontrol edecekseniz, bu metodu kaldırabilirsiniz.
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            return; // Form kapandığından sonrası çalışmayacak
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // Hesap makinesini kontrol et ve kapat
            CheckAndCloseCalculator();
        }
    }


    public class BlackoutForm : Form
    {
        public BlackoutForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;  // Kenarlık yok
            this.BackColor = Color.Black;                 // Arka plan siyah
            this.WindowState = FormWindowState.Maximized; // Tam ekran
            this.TopMost = true;                          // Her zaman üstte
            this.ShowInTaskbar = false;                   // Görev çubuğunda görünmesin
            this.Cursor = Cursors.AppStarting;
        }
    }



}
