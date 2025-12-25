using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SayıTahminOyunu
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        private void Register_Load(object sender, EventArgs e)
        {

            foreach (Control c in pnlCard.Controls)
            {
                if (c is Panel panel)
                {
                    foreach (Control inner in panel.Controls)
                    {
                        if (inner is TextBox txt)
                        {
                            txt.Text = txt.Tag.ToString();
                            txt.ForeColor = Color.Gray;

                            txt.Enter += TextBox_Enter;
                            txt.Leave += TextBox_Leave;
                        }
                    }
                }
            }

        }
        private void btnKayit_Click(object sender, EventArgs e)
        {
            if (txtKullaniciAdi.Text == txtKullaniciAdi.Tag.ToString() || string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Kullanıcı adı zorunludur!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKullaniciAdi.Focus();
                return;
            }

            // Parola kontrolü
            if (txtParola.Text == txtParola.Tag.ToString() || string.IsNullOrWhiteSpace(txtParola.Text))
            {
                MessageBox.Show("Parola zorunludur!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtParola.Focus();
                return;
            }

            try
            {
                string hashliSifre = SecurityHelper.HashPassword(txtParola.Text);

                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    // SQL sorgun veritabanındaki kolon isimleriyle birebir aynı olmalı
                    string query = "INSERT INTO Users (Username, PasswordHash, FirstName, LastName, Phone, Email) " +
                                   "VALUES (@user, @pass, @fname, @lname, @phone, @email)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // DİKKAT: Buradaki TextBox isimlerinin senin formundakilerle eşleştiğinden emin ol
                    cmd.Parameters.AddWithValue("@user", txtKullaniciAdi.Text); // Kullanıcı adı
                    cmd.Parameters.AddWithValue("@pass", hashliSifre);           // Hashli şifre
                    cmd.Parameters.AddWithValue("@fname", txtAd.Text);           // İsim
                    cmd.Parameters.AddWithValue("@lname", txtSoyad.Text);        // Soyisim
                    cmd.Parameters.AddWithValue("@phone", txtTelefon.Text);      // Telefon
                    cmd.Parameters.AddWithValue("@email", txtEposta.Text);       // E-posta

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Kaydınız başarıyla oluşturuldu! Şimdi giriş yapabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                // Veritabanı bağlantısında bir sorun olursa uygulama kapanmaz, hata mesajı verir
                MessageBox.Show("Veritabanına kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            int r = 20;

            path.AddArc(0, 0, r, r, 180, 90);
            path.AddArc(pnlCard.Width - r, 0, r, r, 270, 90);
            path.AddArc(pnlCard.Width - r, pnlCard.Height - r, r, r, 0, 90);
            path.AddArc(0, pnlCard.Height - r, r, r, 90, 90);
            path.CloseFigure();

            pnlCard.Region = new Region(path);
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            

            if (txt.Text == txt.Tag.ToString())
            {
                txt.Text = "";
                txt.ForeColor = Color.Black;

                if (txt.Name == "txtParola")
                    txt.UseSystemPasswordChar = true;
            }
        }
        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
           

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Text = txt.Tag.ToString();
                txt.ForeColor = Color.Gray;

                if (txt.Name == "txtParola")
                    txt.UseSystemPasswordChar = false;
            }
        }

    





    }
}
