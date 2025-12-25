using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SayıTahminOyunu
{
    public partial class Form1 : Form
    {
        private int gizliSayi; //bilgisayarın tuttuğu gizli sayı
        private int kalanHak; //kullanıcının kalan hakkı
        private int haneSayisi = 3; //seçilen hane sayısı varsayılan 3(4 veya 5 de olabilir)
        private int gecenSure = 0;
        private int toplamSure;
        private static readonly Random rnd = new Random();

        public Form1()
        {
            InitializeComponent(); //form oluştu
            btnTahminEt.Enabled = false; //tahmin et butonu başlangıçta pasif

            txtTahmin.Enabled = false; //tahmin kutusu başlangıçta pasif
            grpSeviye.Enabled = false; //seviye seçim grubu başlangıçta pasif

            txtTahmin.MaxLength = 5; //maksimum 5 hane girişi
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lblKullaniciAdi.Text = KullaniciBilgisi.AktifKullaniciAdi; //aktif kullanıcı adını etiket olarak göster
            lblPuan.Text = "0"; //puan etiketi başlangıçta 0
            lblSureUst.Text = "00:00"; //süre etiketi başlangıçta 00:00
        }
        private void YeniGizliSayiUret()
        {
            // 3 hane için: 100 - 1000
            // 4 hane için: 1000 - 10000
            // 5 hane için: 10000 - 100000
            int min = (int)Math.Pow(10, haneSayisi - 1);
            int max = (int)Math.Pow(10, haneSayisi);

            do
            {
                gizliSayi = rnd.Next(min, max);
            } while (RakamlarFarkliMi(gizliSayi) == false);
        }

        //Girilen sayının rakamları birbirininden farklı mı kontrolü
        private bool RakamlarFarkliMi(int sayi)
        {
            string s = sayi.ToString();
            if (s.Distinct().Count() != s.Length)
            {

                return false;

            }
            return true;
        }

        //puan  hesaplama sistemi
        private (int artiPuan, int eksiPuan) HesaplaPuan(string gizliS, string tahminS)
        {
            int artiPuan = 0;
            int eksiPuan = 0;

            // Hata yapmamak için hangi rakamın kullanıldığını işaretleriz.
            bool[] gizliEslesme = new bool[haneSayisi];
            bool[] tahminEslesme = new bool[haneSayisi];

            //artı puanlarını hesapla
            for (int i = 0; i < haneSayisi; i++)
            {
                if (gizliS[i] == tahminS[i])
                {
                    artiPuan++;
                    //bu rakamlar kullanıldı eksi hesaplamasına dahil edilmeyecek
                    gizliEslesme[i] = true;
                    tahminEslesme[i] = true;
                }
            }

            // eksi puanlarını hesapla
            for (int i = 0; i < haneSayisi; i++)
            {
                //tahmindeki bu rakam daha önce artı almadıysa devam et
                if (tahminEslesme[i] == false)
                {
                    for (int j = 0; j < haneSayisi; j++) //gizli sayının her rakamıyla karşılaştır
                    {
                        //gizlideki bu rakam (+) veya (-) almadıysa ve rakamlar aynıysa
                        if (gizliEslesme[j] == false && tahminS[i] == gizliS[j])
                        {
                            eksiPuan++;
                            gizliEslesme[j] = true; //bu gizli rakam kullanıldı
                            break; //eşleşme bulundu bu tahmin rakamı için başka bir arama yapma
                        }
                    }
                }
            }

            return (artiPuan, eksiPuan);
        }

        //Oyunu başlatma butonuna bastıktan sonra çalışacak kodlar
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            
            lblPuan.Text = "0"; 
            lblSureUst.Text = $"{toplamSure / 60:00}:{toplamSure % 60:00}";
            
            // Ekranı ve eski oyun verilerini temizle
            lstTahminler.Items.Clear();
            txtTahmin.Clear();
            lblKalanHak.Text = "Kalan Hak: -"; // henüz hane seçilmediği için boş kalsın

            // Seviye seçim grubunu aktif et, giriş alanlarını pasif tut
            grpSeviye.Enabled = true;     // Kullanıcı artık 3, 4, 5 hane seçebilir
            txtTahmin.Enabled = false;    // Hane seçmeden sayı giremesin
            btnTahminEt.Enabled = false;  // Hane seçmeden tahmin yapamasın

            // RadioButton seçimlerini sıfırla (Eski oyundan kalan seçimi kaldır)
            // Bu sayede CheckedChanged olayı her seferinde temiz bir şekilde tetiklenir
            foreach (Control c in grpSeviye.Controls)
            {
                if (c is RadioButton rb)
                {
                    rb.Checked = false;
                }
            }

            //Kullanıcıya tek bir net mesaj ver
            MessageBox.Show("Yeni oyun için lütfen seviye (hane sayısı) seçin.", "Oyun Başlatıldı");

            //Butonun metnini güncelle
            btnBaslat.Text = "Yeniden Başlat";
        }
        //Tahmin et butonuna basıldıkan sonra çalışacak kodlar
        private void btnTahminEt_Click(object sender, EventArgs e)
        {
            //Girişin sayısal olup olmadığını kontrol etme
            string tahminText = txtTahmin.Text.Trim();
            int tahminSayisi;
            bool isNumber = int.TryParse(tahminText, out tahminSayisi);

            if (!isNumber)
            {
                MessageBox.Show("Lütfen sadece sayı giriniz.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTahmin.Clear();
                txtTahmin.Focus();
                return;
            }

            //Girilen sayının seçtiğimiz hane sayısına göre istenilen aralıkta bir sayı olup olmadığını kontrol etme
            if (tahminText.Length != haneSayisi)
            {

                MessageBox.Show("Lütfen 100 ile 999 arasında 3 haneli bir sayı giriniz.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTahmin.Clear();
                txtTahmin.Focus();
                return;
            }

            if (!RakamlarFarkliMi(tahminSayisi))
            {
                MessageBox.Show("Lütfen rakamları birbirinden farklı bir sayı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTahmin.Clear();
                txtTahmin.Focus();
                return; // Metodun geri kalanını çalıştırma
            }

            string gizliS = gizliSayi.ToString(); //gizli sayıyı stringe çevir  
            string tahminS = tahminSayisi.ToString(); //tahmin sayısını stringe çevir

            // Artı ve Eksi puanlarını hesapla
            var (artiPuan, eksiPuan) = HesaplaPuan(gizliS, tahminS);

            kalanHak--;

            TahminListesineEkle(tahminS, artiPuan, eksiPuan);
            lblKalanHak.Text = "Kalan Hak: " + kalanHak.ToString();
            KontrolEtOyunBittiMi(artiPuan, tahminSayisi);


            txtTahmin.Clear();
            txtTahmin.Focus();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Button basilanButon = (Button)sender;
            string deger = basilanButon.Text;

            if (deger == "Sil") // Eğer butonun metni "Sil" ise (Resimdeki 'Sil' butonu)
            {
                if (txtTahmin.Text.Length > 0)
                    txtTahmin.Text = txtTahmin.Text.Substring(0, txtTahmin.Text.Length - 1);
            }
            else if (deger == "OK") // Eğer buton "OK" ise tahmini gönder
            {
                btnTahminEt.PerformClick(); // Mevcut tahmin et butonunu tetikler
            }
            else // Diğerleri rakam butonlarıdır
            {
                // txtTahmin içeriği seçilen hane sayısını aşmamalı
                if (txtTahmin.Text.Length < haneSayisi)
                {
                    txtTahmin.Text += deger;
                }
            }
        }
        // Tahmin listesini güncelleyen metot
        private void TahminListesineEkle(string tahminS, int arti, int eksi)
        {
            string gizliS = gizliSayi.ToString();

            //Yeni bir satır oluşturuyoruz
            ListViewItem satir = new ListViewItem();
            satir.UseItemStyleForSubItems = false; // Hücre bazlı renklendirme için 

            //Rakamları sütunlara dağıt ve renklendir
            for (int i = 0; i < haneSayisi; i++)
            {
                ListViewItem.ListViewSubItem hucre = new ListViewItem.ListViewSubItem();
                hucre.Text = tahminS[i].ToString();
                hucre.Font = new Font(this.Font, FontStyle.Bold); // Rakamları kalın yapalım
                                                                  // RENK MANTIĞI:
                if (tahminS[i] == gizliS[i]) // Rakam doğru, yer doğru -> YEŞİL
                {
                    hucre.BackColor = Color.Green;
                    hucre.ForeColor = Color.White;
                }
                else if (gizliS.Contains(tahminS[i])) // Rakam var, yer yanlış -> SARI
                {
                    hucre.BackColor = Color.Yellow;
                    hucre.ForeColor = Color.Black;
                }
                else // Rakam yok -> KIRMIZI
                {
                    hucre.BackColor = Color.Red;
                    hucre.ForeColor = Color.White;
                }
                // İlk rakamı ana item olarak, diğerlerini subitem olarak ekle
                if (i == 0) satir.Text = hucre.Text;
                else satir.SubItems.Add(hucre);

                // Önemli: ListView yapısı gereği ilk item'ın stilini subitem'lara zorla uygular.
                // Bunu aşmak için ilk item'ın rengini de manuel setlemeliyiz:
                if (i == 0)
                {
                    satir.BackColor = hucre.BackColor;
                    satir.ForeColor = hucre.ForeColor;
                }
            }

            // 3. Son sütuna "+ / -" sonucunu ekle
            satir.SubItems.Add($"+{arti} / -{eksi}");

            // Satırı listenin en üstüne ekle
            lstTahminler.Items.Insert(0, satir);
        }

        private void ListViewSutunlariniHazirla()
        {
            lstTahminler.Columns.Clear(); // Önce her şeyi siliyoruz

            // Rakam sütunlarını hane sayısına göre ekle
            for (int i = 1; i <= haneSayisi; i++)
            {
                lstTahminler.Columns.Add(i + ". Rakam", 60); // Genişlik: 70
            }

            // Sonuç sütununu en sona ekle
            lstTahminler.Columns.Add("Sonuç (+/-)", 100);
        }
        private void Top5PaneliniDoldur()
        {
            pnlTop5List.Controls.Clear(); //Çakışmayı önlemek için önce temizle

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    string query = @"SELECT TOP 5 U.Username, S.Point, S.TimeSeconds
                             FROM Scores S
                             INNER JOIN Users U ON S.UserId = U.UserId
                             WHERE S.DigitCount = @digit
                             ORDER BY S.Point DESC, S.TimeSeconds ASC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@digit", haneSayisi);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    int y = 10;

                    while (dr.Read())
                    {
                        Label lbl = new Label();
                        lbl.AutoSize = false;
                        lbl.Width = pnlTop5List.Width - 10;
                        lbl.Height = 30;
                        lbl.Left = 10;
                        lbl.Top = y;
                        lbl.TextAlign = ContentAlignment.MiddleLeft;
                        lbl.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                        lbl.BackColor = Color.WhiteSmoke;
                        lbl.BorderStyle = BorderStyle.FixedSingle;

                        lbl.Text = $"{dr["Username"]}   |   {dr["Point"]} Puan   |   {dr["TimeSeconds"]} sn";

                        pnlTop5List.Controls.Add(lbl);
                        y += 30;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Top 5 yüklenirken hata: " + ex.Message);
            }
        }
        /*
        // TextBox'a tıklandığında içeriği temizleyecek. bu metodu txtTahmin'ın Enter olayına bağladım.
        private void txtTahmin_Enter(object sender, EventArgs e)
        {
            TextBox t1 = (TextBox)sender;
            t1.Text = string.Empty;
        } */

        // Sayı butonlarına basıldığında çalışacak kodlar.Keypad'deki her sayı butonuna tek tek Click olayı bağlamaktansa tek bir metotla bunu hallettim
       
        private void OyunuBitir()
        {
            btnTahminEt.Enabled = false; //tahmin et butonunu pasif yap
            btnBaslat.Text = "Yeniden Başlat"; //başlat butonunun metnini değiştir
        }

        private void KontrolEtOyunBittiMi(int artiPuan, int tahminSayisi)
        {
            if (artiPuan == haneSayisi)
            {
                timer1.Stop(); // Önce zamanı durduruyoruz ki skor kaydı netleşsin

                // Puanı hesapla (Örn: Kalan hak x 100)
                int finalPuan = kalanHak * 100;

                // Veritabanı kaydı için metodu çağırıyoruz
                SkoruKaydet(finalPuan);
                Top5PaneliniDoldur(); // Skor kaydından sonra en iyi 5'i güncelle

                MessageBox.Show($"TEBRİKLER! Gizli Sayı: {gizliSayi}\nSkorunuz: {finalPuan}",
                                "KAZANDINIZ!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                OyunuBitir();
                lblPuan.Text = finalPuan.ToString(); //puan etiketini güncelle

                return;
            }

            if (kalanHak == 0)
            {
                timer1.Stop(); // Kaybettiğinde de timer dursun
                MessageBox.Show($"Hakkınız bitti! Kaybettiniz. Gizli sayı şuydu: {gizliSayi}",
                                "OYUN BİTTİ!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                OyunuBitir();
            }
        }

        // Skoru veritabanına kaydeden metot
        private void SkoruKaydet(int puan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    // SQL sorgum veritabanındaki kolon isimleriyle birebir aynı olmalı
                    string query = "INSERT INTO Scores (UserId, Point, TimeSeconds, DigitCount, PlayDate) " +
                                   "VALUES (@uid, @point, @time, @digit, @date)";

                    // Harcanan süreyi hesapla: (Toplam Süre - Kalan Süre)
                    // toplamSure değişkenini rbHane_CheckedChanged içinde belirledik
                    int harcananSure = toplamSure - gecenSure;

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@uid", KullaniciBilgisi.AktifKullaniciId);
                    cmd.Parameters.AddWithValue("@point", puan);
                    cmd.Parameters.AddWithValue("@time", harcananSure); // Kalanı değil, harcananı yazıyoruz
                    cmd.Parameters.AddWithValue("@digit", haneSayisi);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Skor kaydolduktan sonra ekranın sol üstündeki listeyi yenilemek için:
                    Top5PaneliniDoldur();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Skor kaydedilirken bir hata oluştu: " + ex.Message);
            }
        }

        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender; // Hangi RadioButton'ın değiştiğini al
            if (rb.Checked)
            {
                // RadioButton ismine veya textine göre hane sayısını güncelle
                if (rb.Text.Contains("3")) haneSayisi = 3;
                else if (rb.Text.Contains("4")) haneSayisi = 4;
                else if (rb.Text.Contains("5")) haneSayisi = 5;
                txtTahmin.MaxLength = haneSayisi;  // TextBox limitini ayarla yoksa çöküyor
                txtTahmin.Enabled = true; // Artık yazabilir
                btnTahminEt.Enabled = true;
                Top5PaneliniDoldur();

                if (haneSayisi == 3) { kalanHak = 10; toplamSure = 40; }
                else if (haneSayisi == 4) { kalanHak = 8; toplamSure = 55; }
                else if (haneSayisi == 5) { kalanHak = 6; toplamSure = 70; }

                ListViewSutunlariniHazirla(); // ListView sütunlarını hane sayısına göre ayarla
                lstTahminler.Items.Clear(); // Eski tahminleri temizle

                lblKalanHak.Text = "Kalan Hak: " + kalanHak.ToString();
                lblSure.ForeColor = Color.Black; // Süre etiket rengini siyah yap
                gecenSure = toplamSure; // Geri sayım başlatacağımız için toplam süreden başlıyoruz
                lblSure.Text = "Kalan Süre: " + gecenSure + " sn";

                timer1.Start();

                grpSeviye.Enabled = false;
                
                //Yeni gizli sayıyı bu hane sayısına göre üret
                YeniGizliSayiUret();

                txtTahmin.Clear(); //Seçim değişince kutuyu temizle
                txtTahmin.Focus();
            }
        }

        // Zamanlayıcı kodları
        private void timer1_Tick(object sender, EventArgs e)
        {
            gecenSure--; //azalan süre olacak
            lblSure.Text = "Kalan Süre: " + gecenSure + " sn";

            int dakika = gecenSure / 60;
            int saniye = gecenSure % 60;
            lblSureUst.Text = $"{dakika:00}:{saniye:00}";
            if (gecenSure <= 10)
            {
                lblSure.ForeColor = Color.Red; // Son 10 saniye kala yazı kırmızı olsun
                Console.Beep();
            }

            if (gecenSure <= 0)
            {
                timer1.Stop();
                MessageBox.Show($"Süreniz bitti! Gizli sayı şuydu: {gizliSayi}", "KAYBETTİNİZ");
                OyunuBitir();
            }
        }

        
        
        // Panelin köşelerini ovalleştiren kod
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            int radius = 30; // Köşe ovalliği
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddArc(0, 0, radius, radius, 180, 90);
            gp.AddArc(pnlBaslik.Width - radius, 0, radius, radius, 270, 90);
            gp.AddArc(pnlBaslik.Width - radius, pnlBaslik.Height - radius, radius, radius, 0, 90);
            gp.AddArc(0, pnlBaslik.Height - radius, radius, radius, 90, 90);

            pnlBaslik.Region = new Region(gp);
        }

    }
}
