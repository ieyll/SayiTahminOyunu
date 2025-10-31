# SayıTahminOyunu
Bu oyun sayesinde dilediğiniz zaman rakamları birbirinden farklı olmak üzere 100 ile 1000 arasında 3 haneli bir sayı tahmin oyunu oynayabilirsiniz. Oyun içerisinde bulunan kontroller doğrultusunda toplam 10 tahmin hakkınız bulunmaktadır. Sayıyı tahmin ettikçe eğer tahmininizdeki sayı veya sayılar gizli sayı içerisinde varsa ve yeri doğruysa "artı" bölümünde kaç sayı doğruysa o yazar. Eğer tahmininizdeki rakamlar gizli sayıdaki rakamlar ile eşleşiyor fakat yeri yanlışsa "eksi" kısmında o kadar rakam yazar. Eğer tahminininiz doğruysa ekranda bir MessageBox ile kazandığınıza dair mesaj alırsınz ve oyun sonlanır, yeniden başlatıp oynayabilirsiniz.




using System;
using System.Windows.Forms;
namespace SayıTahminOyunu
{
    public partial class Form1 : Form
    {

        private int gizliSayi; 
        private int kalanHak;
        public Form1()
        {
            InitializeComponent(); //form oluşturuluyor
            btnTahminEt.Enabled = false; //başlangıçta tahmin et butonunu inaktif atıyorum.
        }
        //Girilen sayının rakamları birbirininden farklı mı kontrolü
        private bool RakamlarFarkliMi(int sayi)
        {
            string s = sayi.ToString();
            if (s[0] == s[1] || s[0] == s[2] || s[1] == s[2])
            {
                return false;
                MessageBox.Show("Rakamlar farklı olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        private (int artiPuan, int eksiPuan) HesaplaPuan(string gizliS, string tahminS)
        {
            int artiPuan = 0;
            int eksiPuan = 0;

            // Hata yapmamak için hangi rakamın kullanıldığını işaretleriz.
            bool[] gizliEslesme = new bool[3];
            bool[] tahminEslesme = new bool[3];

            //artı puanlarını hesapla
            for (int i = 0; i < 3; i++)
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
            for (int i = 0; i < 3; i++) 
            {
                //tahmindeki bu rakam daha önce artı almadıysa devam et
                if (tahminEslesme[i] == false)
                {
                    for (int j = 0; j < 3; j++) //gizli sayının her rakamıyla karşılaştır
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


       //Oyunu bitiren fonksiyon, tahmin et butonu inaktif 
        private void OyunuBitir()
        {
            btnTahminEt.Enabled = false; 
            btnBaslat.Text = "Yeniden Başlat";
        }

        //Oyunun bitip bitmediğini kontrol eden fonkiyon. Eğer artı pua 3 olduysa ekranda MessageBox ile "kazandınız" mesajı veriyor.Kaybettiyseniz "oyun  bitti"          uyarısı veriyor.
        private void KontrolEtOyunBittiMi(int artiPuan, int tahminSayisi)
        {
            
            if (artiPuan == 3)
            {
                MessageBox.Show($"TEBRİKLER! Gizli Sayı: {tahminSayisi}",
                                "KAZANDINIZ!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                OyunuBitir();
                return;
            }

            
            if (kalanHak == 0)
            {
                MessageBox.Show($"Hakkınız bitti! Kaybettiniz. Gizli sayı şuydu: {gizliSayi}",
                                "OYUN BİTTİ!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                OyunuBitir();
            }
        }


      

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            //Random bir sayı üretiyorum
            Random rnd = new Random();
            do
            {
                gizliSayi = rnd.Next(100, 1000);
            } while (RakamlarFarkliMi(gizliSayi) == false);
            kalanHak = 8;

            lblKalanHak.Text = "Kalan Hak: " + kalanHak.ToString();
            lstTahminler.Items.Clear();
            txtTahmin.Clear();
            txtTahmin.Focus();

            btnTahminEt.Enabled = true;
            btnBaslat.Text = "Yeniden Başlat";
        }

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

            //Girilen sayının 100 ile 1000 arasında 3 haneli bir sayı olup olmadığını kontrol etme
            if (tahminSayisi < 100 || tahminSayisi > 999)
            {

                MessageBox.Show("Lütfen 100 ile 999 arasında 3 haneli bir sayı giriniz.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTahmin.Clear();
                txtTahmin.Focus();
                return;
            }
            string gizliS = gizliSayi.ToString();
            string tahminS = tahminSayisi.ToString();

            // Artı ve Eksi puanlarını hesapla
            var (artiPuan, eksiPuan) = HesaplaPuan(gizliS, tahminS);

            

            kalanHak--;

            
            string sonucMetni = $"Tahmin: {tahminSayisi} → {artiPuan} + , {eksiPuan} -";
            lstTahminler.Items.Add(sonucMetni);

            lblKalanHak.Text = "Kalan Hak: " + kalanHak.ToString();

            

            KontrolEtOyunBittiMi(artiPuan, tahminSayisi);

            
            txtTahmin.Clear();
            txtTahmin.Focus();
        }


        //textbox nesnesine tıkladığımızda metnini silmesi için enter özelliği atadım .
        private void txtTahmin_Enter(object sender, EventArgs e)
        {
            TextBox t1 = (TextBox)sender;
            t1.Text=string.Empty;
        }
    }
}
