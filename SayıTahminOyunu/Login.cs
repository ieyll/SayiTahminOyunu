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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
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

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == txtUsername.Tag.ToString() || txtPassword.Text == txtPassword.Tag.ToString())
            {
                MessageBox.Show("Kullanıcı adı ve parola giriniz!", "Uyarı");
                return;
            }
            // 1. Kullanıcının girdiği şifreyi hash'liyoruz (Veritabanındakilerle karşılaştırmak için)
            string hashedPassword = SecurityHelper.HashPassword(txtPassword.Text);

            using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                // 2. Kullanıcı adı ve şifre eşleşiyor mu?
                string query = "SELECT UserId FROM Users WHERE Username = @user AND PasswordHash = @pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                cmd.Parameters.AddWithValue("@pass", hashedPassword);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar(); // Tek bir değer (UserId) dönerse giriş başarılıdır

                    if (result != null)
                    {
                        // 3. GİRİŞ BAŞARILI
                        // Giriş yapan kullanıcının ID'sini bir yerde saklamalıyız ki skorları ona kaydedelim
                        KullaniciBilgisi.AktifKullaniciId = (int)result;
                        KullaniciBilgisi.AktifKullaniciAdi = txtUsername.Text;

                        MessageBox.Show("Hoş geldiniz, " + txtUsername.Text);

                        // Oyun formunu (Form1) aç ve bu formu kapat
                        Form1 anaOyun = new Form1();
                        anaOyun.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }

        private void btnYeniKln_Click(object sender, EventArgs e)
        {
            Register kayitFormu = new Register();
            kayitFormu.ShowDialog(); // Kullanıcı kaydı bitirene kadar Login'e dönemez
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 20;

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(pnlCard.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(pnlCard.Width - radius, pnlCard.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, pnlCard.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            pnlCard.Region = new Region(path);
        }

        private void btnGiris_MouseEnter(object sender, EventArgs e)
        {
            btnGiris.BackColor = Color.FromArgb(30, 64, 175);
        }

        private void btnGiris_MouseLeave(object sender, EventArgs e)
        {
            btnGiris.BackColor = Color.FromArgb(37, 99, 235);
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
