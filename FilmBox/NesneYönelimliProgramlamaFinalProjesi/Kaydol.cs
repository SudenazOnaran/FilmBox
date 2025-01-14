using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NYPfinalodev
{
    public partial class Kaydol : Form
    {
        public Kaydol()
        {
            InitializeComponent();
        }
        private void textad_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ad = textad.Text;
            string soyad = textsoyad.Text;
            string sifre = textsifre.Text;
            string tc = texttc.Text;
            string kullaniciadi = textkullanici.Text;
            string dogumTarihi = textdogum.Text;
            string cinsiyet = comboBox2.Text;
            bool premiumUye = premium.Checked;
            string uyelikTuru = premiumUye ? "Premium" : "Standart";
            try
            {
                if (sifre.Length > 10)
                {
                    MessageBox.Show("Şifre 10 karakterden uzun olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Şifre 10 karakterden uzunsa işlemi sonlandır
                }
                using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=FilmData;User Id = postgres; Password = lab1234"))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO public.\"Kullanicilar\" (\"KullaniciAdi\",\"Ad\", \"Soyad\",\"Tc\", \"DogumTarihi\", \"Cinsiyet\",\"UyelikTuru\",\"Sifre\") " +
                        "VALUES (@KullaniciAdi,@Ad, @Soyad, @TC, @DogumTarihi, @Cinsiyet, @UyelikTuru, @Sifre)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciadi);
                        cmd.Parameters.AddWithValue("@Ad", ad);
                        cmd.Parameters.AddWithValue("@Soyad", soyad);
                        cmd.Parameters.AddWithValue("@TC", BigInteger.Parse(tc));
                        cmd.Parameters.AddWithValue("@DogumTarihi", DateTime.Parse(dogumTarihi)); // DogumTarihi'ni uygun bir formata çevir
                        cmd.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                        cmd.Parameters.AddWithValue("@UyelikTuru", uyelikTuru);
                        cmd.Parameters.AddWithValue("@Sifre", sifre);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Kullanıcı başarıyla eklenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textkullanici_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void Kaydol_Load(object sender, EventArgs e)
        {

        }
    }
}
