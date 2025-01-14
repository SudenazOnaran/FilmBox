using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace NYPfinalodev
{
    public partial class Form2 : Form
    {
        private Form3 form3Referansi;
        public Form2()
        {
            InitializeComponent();
        }
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=FilmData;User Id = postgres; Password = 1234");

        public void tabloyu_goster(string txt)
        {
            conn.Open();
            NpgsqlCommand komut = new NpgsqlCommand();
            komut.Connection = conn;
            komut.CommandType = CommandType.Text;
            komut.CommandText = txt;
            NpgsqlDataReader dr = komut.ExecuteReader();
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                filmtablo.DataSource = dt;
            }
            komut.Dispose();
            conn.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            filmtablo.Hide();
            bilgileri_getir();

        }

        public void button1_Click(object sender, EventArgs e)
        {
            filmtablo.Show();
            tabloyu_goster("SELECT * FROM public.\"FilmBilgisi\";");
        }

        private void eklebtn_Click(object sender, EventArgs e)
        {
            tabloyu_goster("SELECT * FROM public.\"FilmBilgisi\";\r\nINSERT INTO public.\"FilmBilgisi\"(\r\n\t\"FilmAdi\", \"Yönetmen\", \"Oyuncular\", \"FilmTuru\", \"DPuan\", \"YayinYili\")\r\n" +
            "values ('" + textad.Text + "','" + textyonetici.Text + "','" + textoyuncu.Text + "','" + texttur.Text + "'," + textpuan.Text + "," + texttarih.Text + ");");
            MessageBox.Show("Kayıt sisteme başarıyla eklendi");
        }

        private void guncellebtn_Click(object sender, EventArgs e)
        {
            string filmadi = textad.Text;

        
            string baslangıc = $"SELECT * FROM public.\"FilmBilgisi\" WHERE \"FilmAdi\" = '" + filmadi+ "';";
            tabloyu_goster(baslangıc);
            // Kullanıcının güncellediği bilgileri aldık
            string yeniAd = textad.Text;
            string yeniYonetmen = textyonetici.Text;
            string yeniOyuncu = textoyuncu.Text;
            string yeniTur = texttur.Text;
            string yeniPuan = textpuan.Text;
            string yeniTarih = texttarih.Text;

            // Güncelleme sorgusunu oluştururuz
            string ekleme = $"UPDATE public.\"FilmBilgisi\" SET ";
            if (!string.IsNullOrEmpty(yeniAd))
            {
                ekleme += $"\"FilmAdi\"= '" + yeniAd + "', ";
            }

            if (!string.IsNullOrEmpty(yeniYonetmen))
            {
                ekleme += $"\"Yonetmen\"= '" + yeniYonetmen + "', ";
            }
            if (!string.IsNullOrEmpty(yeniOyuncu))
            {
                ekleme += $"\"Oyuncular\" = '" + yeniOyuncu + "', ";
            }
            if (!string.IsNullOrEmpty(yeniTur))
            {
                ekleme += $"\"FilmTuru\" = '" + yeniTur + "', ";
            }

            if (!string.IsNullOrEmpty(yeniPuan))
            {
                ekleme += $"\"DPuan\" = " + yeniPuan + ", ";
            }
            if (!string.IsNullOrEmpty(yeniTarih))
            {
                ekleme += $"\"YayinYili\" = " + yeniTarih + ", ";
            }


            // Sonunda virgül varsa virgülü kaldırır
            if (ekleme.EndsWith(", "))
            {
                ekleme = ekleme.Substring(0, ekleme.Length - 2);
            }

            ekleme += $" WHERE \"FilmAdi\" = '" + filmadi + "';";

            // Güncelleme sorgusunu çalıştır
            conn.Open();
            NpgsqlCommand updateCommand = new NpgsqlCommand(ekleme, conn);
            updateCommand.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Film bilgileri güncellendi.");

            // Güncelleme sonrası tabloyu tekrar göster
            tabloyu_goster("select * from \"public\".\"FilmBilgisi\";");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // PostgreSQL veritabanı bağlantı dizesi
            string connString = "Server=localhost;Port=5432;Database=FilmData;User Id = postgres; Password = 1234";

            // PostgreSQL bağlantı nesnesi oluştur
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // PostgreSQL güncelleme sorgusu
                string sql = "UPDATE public.\"Kullanicilar\" SET \"KullaniciAdi\" = @kullaniciAd, \"Sifre\" = @sifre WHERE \"UyelikTuru\" = 'Yönetici';";

                if (textBox2.Text.Length > 10)
                {
                    MessageBox.Show("Şifre 10 karakterden uzun olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Şifre 10 karakterden uzunsa işlemi sonlandır
                }

                // PostgreSQL komut nesnesi oluştur
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    // Parametreleri ekle
                    cmd.Parameters.AddWithValue("@kullaniciAd", textBox1.Text);
                    cmd.Parameters.AddWithValue("@sifre", textBox2.Text);

                    // Güncelleme sorgusunu çalıştır
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Bilgiler başarıyla güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme başarısız.");
                    }
                }
            }
        }
        private void bilgileri_getir()
        {
            string connString = "Server=localhost;Port=5432;Database=FilmData;User Id = postgres; Password = 1234";

            // PostgreSQL bağlantı nesnesi oluştur
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // PostgreSQL sorgusu
                string sql = "select * from public.\"Kullanicilar\" where \"UyelikTuru\" = 'Yönetici'; ";

                // PostgreSQL komut nesnesi oluştur
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    // Verileri oku
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Verileri işle
                        while (reader.Read())
                        {
                            string kullaniciAd = reader["KullaniciAdi"].ToString();
                            string sifre = reader["Sifre"].ToString();
                            textBox1.Text = kullaniciAd;
                            textBox2.Text = sifre;
                        }

                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string hosgeldin;
            Form3 form3 =new Form3("hosgeldin");
            form3.Show();
            form3.deneme();

        }
    }

}
