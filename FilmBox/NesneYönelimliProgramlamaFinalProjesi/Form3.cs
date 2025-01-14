using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NYPfinalodev
{
    public partial class Form3 : Form
    {
        private List<Image> resimListesi;
        private int gosterilenResimIndex = 0;
        private string hosgeldinKullaniciAdi;

        public Form3(string kullaniciAdi)
        {
            InitializeComponent();
            FilmleriComboboxaEkle();
            hosgeldinKullaniciAdi = kullaniciAdi;
            resimListesi = new List<Image>
            {
                Properties.Resources.avatar,
                Properties.Resources.titanic,
                Properties.Resources.joker2,
                Properties.Resources.aquaman,
                Properties.Resources.yesilyol,
                Properties.Resources.toystory,
            };
            GosterilenResmiGuncelle();
        }
        public void deneme()
        {
            notifyIcon2.BalloonTipTitle = "baslik";
            notifyIcon2.BalloonTipText = "metin";
            notifyIcon2.ShowBalloonTip(4000);
        }
        public void FilmleriComboboxaEkle()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=FilmData;Username=postgres;Password=1234"))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT \"FilmAdi\" FROM public.\"FilmBilgisi\"";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string filmAdi = reader["FilmAdi"].ToString();
                                comboBoxFilmler.Items.Add(filmAdi);
                                comboBoxizleme.Items.Add(filmAdi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=FilmData;User Id = postgres; Password = 1234");
        public void tabloyu_gosterr(string txt, DataGridView dataGridView)
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
                dataGridView.DataSource = dt;
            }
            komut.Dispose();
            conn.Close();
        }

        private void GosterilenResmiGuncelle()
        {
            pictureBox12.Image = resimListesi[gosterilenResimIndex];
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            lblHosgeldin.Text = $"Bugün ne izlemek istersin {hosgeldinKullaniciAdi}?  Hadi seç ve izlemeye başla";
            degerata();
            tabloyu_gosterr("SELECT \"FilmAdi\" FROM public.\"FilmIzlemeListesi\"\r\nwhere \"KullaniciAdi\"= '" + hosgeldinKullaniciAdi + "' ", dataGridizleme);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            picturestar.Visible = false;
            dataGridViewara.Visible = true;
            tabloyu_gosterr("SELECT * FROM \"public\".\"FilmBilgisi\" WHERE LOWER(\"FilmAdi\") ILIKE '%" + textarama.Text.ToLower() + "%' OR LOWER(\"Yönetmen\") ILIKE '%" + textarama.Text.ToLower() + "%';", dataGridViewara);
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //türe göre filmleri listele
            if (comboBox1.SelectedItem != null) //seçili mi kontrol
            {
                picturestar.Visible = false;
                dataGridViewara.Visible = true;
                string secilenSecenek = comboBox1.SelectedItem.ToString();
                if (secilenSecenek == "Hepsi")
                {
                    tabloyu_gosterr("select * from \"public\".\"FilmBilgisi\";", dataGridViewara);
                }
                else
                {
                    tabloyu_gosterr("select * from \"public\".\"FilmBilgisi\" where \"FilmTuru\" like '%" + secilenSecenek + "%' ;", dataGridViewara);
                }

            }
        }


        private void textarama_Click(object sender, EventArgs e)
        {
            textarama.Text = "";
        }

        public void DegerlendirmeEkle()
        {
            string secilenfilmadi = comboBoxFilmler.SelectedItem?.ToString();
            string kullaniciadii = hosgeldinKullaniciAdi;
            string kullaniciyorumu = textyorum.Text;
            if (string.IsNullOrEmpty(secilenfilmadi))
            {
                MessageBox.Show("Lütfen bir film seçin.");
                return;
            }
            string yeniPuan = comboBoxPuanlar.SelectedItem?.ToString();
            string ekleme = $"UPDATE public.\"FilmPuanYorum\" SET ";
            if (!string.IsNullOrEmpty(kullaniciadii)) ekleme += $"\"KullaniciAdi\" = '{kullaniciadii}', ";
            if (!string.IsNullOrEmpty(yeniPuan)) ekleme += $"\"Puan\" = '{yeniPuan}', ";
            if (!string.IsNullOrEmpty(secilenfilmadi)) ekleme += $"\"FilmAdi\" = '{secilenfilmadi}', ";
            if (!string.IsNullOrEmpty(kullaniciyorumu)) ekleme += $"\"Yorum\" = '{kullaniciyorumu}', ";
            if (ekleme.EndsWith(", "))
            {
                ekleme = ekleme.Substring(0, ekleme.Length - 2);
            }
            ekleme += $" WHERE \"FilmAdi\" = '{secilenfilmadi}' AND \"KullaniciAdi\" = '{kullaniciadii}';";
            using (NpgsqlCommand updateCommand = new NpgsqlCommand(ekleme, conn))
            {
                conn.Open();
                int affectedRows = updateCommand.ExecuteNonQuery();
                conn.Close();

                if (affectedRows == 0)
                {
                    // Güncellenen satır yoksa, yeni bir satır ekleyelim
                    ekleme = $"INSERT INTO public.\"FilmPuanYorum\" (\"FilmAdi\", \"KullaniciAdi\", \"Puan\",\"Yorum\") VALUES " +
                             $"('{secilenfilmadi}', '{kullaniciadii}', '{yeniPuan}','{kullaniciyorumu}') RETURNING \"DegerlendirmeId\";";

                    using (NpgsqlCommand insertCommand = new NpgsqlCommand(ekleme, conn))
                    {
                        conn.Open();
                        // ExecuteScalar kullanarak otomatik artan DegerlendirmeID'yi alın
                        int degerlendirmeID = Convert.ToInt32(insertCommand.ExecuteScalar());
                        conn.Close();

                        MessageBox.Show($"Değerlendirme eklendi. Degerlendirme ID: {degerlendirmeID}");
                    }
                }
                else
                {
                    MessageBox.Show("Değerlendirme güncellendi.");
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string secilenfilmadi = comboBoxFilmler.SelectedItem?.ToString();
            tabloyu_gosterr("select * from \"public\".\"FilmPuanYorum\" where \"FilmAdi\"='" + secilenfilmadi + "' ;", puanyorumtablo);
        }

        private void btnyorum_Click(object sender, EventArgs e)
        {
            string kullaniciyorumu = textyorum.Text;
            if (string.IsNullOrEmpty(kullaniciyorumu))
            {
                MessageBox.Show("Lütfen bir yorum yazınız.");
                return;
            }
            DegerlendirmeEkle();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }



        private void comboBoxFilmler_SelectedIndexChanged(object sender, EventArgs e)
        {
            //film değerlendir
            string secilenfilmadi = comboBoxFilmler.SelectedItem?.ToString();
            if (secilenfilmadi == "Joker")
            {
                gosterilenResimIndex = 2;
                GosterilenResmiGuncelle();
            }
            else if (secilenfilmadi == "Avatar")
            {
                gosterilenResimIndex = 0;
                GosterilenResmiGuncelle();
            }
            else if (secilenfilmadi == "Titanik")
            {
                gosterilenResimIndex = 1;
                GosterilenResmiGuncelle();
            }
            else if (secilenfilmadi == "Yeşil Yol")
            {
                gosterilenResimIndex = 4;
                GosterilenResmiGuncelle();
            }
            else if (secilenfilmadi == "Aquaman")
            {
                gosterilenResimIndex = 3;
                GosterilenResmiGuncelle();
            }
            else if (secilenfilmadi == "Oyuncak Hikayesi")
            {
                gosterilenResimIndex = 5;
                GosterilenResmiGuncelle();
            }
        }

        private void btndegerlendir_Click_1(object sender, EventArgs e)
        {
            //değerlendir
            string yeniPuan = comboBoxPuanlar.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(yeniPuan))
            {
                MessageBox.Show("Lütfen bir puan seçin.");
                return;
            }
            DegerlendirmeEkle();
        }

        private void btnizle_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void btnprofil_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            Form1 formm1 = new Form1();
            formm1.Show();
            this.Hide();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void btnrapor_Click(object sender, EventArgs e)
        {
            Raporform formm4 = new Raporform();
            formm4.Show();
            this.Hide();
        }
        public void degerata()
        {
            Kullanici kullanici = null;

            using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=FilmData;User Id = postgres; Password = 1234"))
            {
                conn.Open();

                string sorgu = "SELECT * FROM public.\"Kullanicilar\" WHERE \"KullaniciAdi\" = @KullaniciAdi";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sorgu, conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciAdi", hosgeldinKullaniciAdi);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {


                        if (reader.Read())
                        {
                            // Veritabanından okunan verileri kullanarak uygun sınıfa atama yapın
                            string uyelikTuru = reader["UyelikTuru"].ToString();
                            textuyelik.Text = uyelikTuru;
                            if (uyelikTuru == "Standart")
                            {
                                kullanici = new Standart(
                                   reader["Ad"].ToString(),
                                   reader["Soyad"].ToString(),
                                   BigInteger.Parse(reader["Tc"].ToString()),
                                   DateTime.Parse(reader["DogumTarihi"].ToString()),
                                   reader["Cinsiyet"].ToString()[0],
                                   reader["Sifre"].ToString(),
                                   reader["KullaniciAdi"].ToString());
                                label16.Text = kullanici.UcretHesapla().ToString() + "TL";
                                panel2.Visible = false;
                                label5.Text = "Yapılan yorum ve değerlendirmeleri görüntüle";
                            }
                            else if (uyelikTuru == "Premium")
                            {
                                kullanici = new Premium(
                                   reader["Ad"].ToString(),
                                   reader["Soyad"].ToString(),
                                   BigInteger.Parse(reader["Tc"].ToString()),
                                   DateTime.Parse(reader["DogumTarihi"].ToString()),
                                   reader["Cinsiyet"].ToString()[0],
                                   reader["Sifre"].ToString(),
                                   reader["KullaniciAdi"].ToString()
                                   );
                                label16.Text = kullanici.UcretHesapla().ToString() + "TL";
                            }

                        }

                    }
                    if (kullanici != null)
                    {
                        textad.Text = kullanici.Ad;
                        textsoyad.Text = kullanici.Soyad;
                        texttc.Text = kullanici.Tc.ToString();
                        textdogum.Text = kullanici.DogumTarihi.ToString("yyyy-MM-dd");
                        textcinsiyet.Text = kullanici.Cinsiyet.ToString();
                        textkullaniciad.Text = kullanici.KullaniciAdi.ToString();
                        textsifre.Text = kullanici.Sifre.ToString();
                    }
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textad.Text) || string.IsNullOrWhiteSpace(textsoyad.Text) ||
           string.IsNullOrWhiteSpace(texttc.Text) || string.IsNullOrWhiteSpace(textdogum.Text) ||
           string.IsNullOrWhiteSpace(textcinsiyet.Text) || string.IsNullOrWhiteSpace(textsifre.Text) ||
           string.IsNullOrWhiteSpace(textkullaniciad.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Metodu burada sonlandır
            }
            else if (textsifre.Text.Length > 10)
            {
                MessageBox.Show("Şifre 10 karakterden uzun olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Şifre 10 karakterden uzunsa metodu sonlandır
            }
            else
            {
                using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=FilmData;User Id=postgres;Password=1234"))
                {
                    conn.Open();

                    string sorgu = "UPDATE public.\"Kullanicilar\" SET " +
                                   "\"Ad\" = @Ad, " +
                                   "\"Soyad\" = @Soyad, " +
                                   "\"Tc\" = @Tc, " +
                                   "\"DogumTarihi\" = @DogumTarihi, " +
                                   "\"Cinsiyet\" = @Cinsiyet, " +
                                   "\"Sifre\" = @Sifre, " +
                                   "\"UyelikTuru\" =@UyelikTuru," +
                                   "\"KullaniciAdi\" = @KullaniciAdi " +
                                   "WHERE \"KullaniciAdi\" = @KullaniciAdi";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sorgu, conn))
                    {
                        // Kullanıcının girdiği yeni değerleri parametre olarak ekleyin
                        cmd.Parameters.AddWithValue("@Ad", textad.Text);
                        cmd.Parameters.AddWithValue("@Soyad", textsoyad.Text);
                        cmd.Parameters.AddWithValue("@Tc", BigInteger.Parse(texttc.Text));
                        cmd.Parameters.AddWithValue("@DogumTarihi", DateTime.Parse(textdogum.Text));
                        cmd.Parameters.AddWithValue("@UyelikTuru", textuyelik.Text);
                        cmd.Parameters.AddWithValue("@Cinsiyet", textcinsiyet.Text[0]);
                        cmd.Parameters.AddWithValue("@Sifre", textsifre.Text);
                        cmd.Parameters.AddWithValue("@KullaniciAdi", textkullaniciad.Text);

                        // Güncelleme sorgusunu çalıştırın
                        int affectedRows = cmd.ExecuteNonQuery();

                        // Güncelleme başarılıysa kullanıcıya bilgi verin
                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Güncelleme başarıyla tamamlandı.");
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme sırasında bir hata oluştu.");
                        }
                    }
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string secilenFilmAdi = comboBoxizleme.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(secilenFilmAdi))
            {
                MessageBox.Show("Lütfen bir film seçin.");
                return;
            }

            using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=FilmData;Username=postgres;Password=1234"))
            {
                connection.Open();

                // İzleme listesinde aynı kaydın olup olmadığını kontrol et
                string checkQuery = "SELECT COUNT(*) FROM public.\"FilmIzlemeListesi\" WHERE \"FilmAdi\" = @filmAdi AND \"KullaniciAdi\" = @kullaniciAdi";
                using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@filmAdi", secilenFilmAdi);
                    checkCommand.Parameters.AddWithValue("@kullaniciAdi", hosgeldinKullaniciAdi);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Bu film zaten izleme listesinde var.");
                        return;
                    }
                }

                // İzleme listesine ekleme yapacak sorgu
                string insertsorgu = "INSERT INTO public.\"FilmIzlemeListesi\" (\"FilmAdi\", \"KullaniciAdi\") VALUES (@filmAdi, @kullaniciAdi)";
                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertsorgu, connection))
                {
                    insertCommand.Parameters.AddWithValue("@filmAdi", secilenFilmAdi);
                    insertCommand.Parameters.AddWithValue("@kullaniciAdi", hosgeldinKullaniciAdi);
                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Ekleme başarıyla gerçekleştirildi.");
                }
            }
            tabloyu_gosterr("SELECT \"FilmAdi\" FROM public.\"FilmIzlemeListesi\"\r\nwhere \"KullaniciAdi\"= '" + hosgeldinKullaniciAdi + "' ", dataGridizleme);
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            string secilenFilmAdi = comboBoxizleme.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(secilenFilmAdi))
            {
                MessageBox.Show("Lütfen bir film seçin.");
                return;
            }

            using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=FilmData;Username=postgres;Password=1234"))
            {
                connection.Open();

                // İzleme listesinden çıkarma yapacak sorgu
                string silmesorgu = "DELETE FROM public.\"FilmIzlemeListesi\" WHERE \"FilmAdi\" = @filmAdi AND \"KullaniciAdi\" = @kullaniciAdi";
                using (NpgsqlCommand deleteCommand = new NpgsqlCommand(silmesorgu, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@filmAdi", secilenFilmAdi);
                    deleteCommand.Parameters.AddWithValue("@kullaniciAdi", hosgeldinKullaniciAdi);

                    int deletedRows = deleteCommand.ExecuteNonQuery();

                    if (deletedRows > 0)
                    {
                        MessageBox.Show("Çıkarma başarıyla gerçekleştirildi.");
                    }
                    else
                    {
                        MessageBox.Show("Bu film izleme listesinde bulunmuyor.");
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // PostgreSQL veritabanı bağlantı dizesi
            string connString = "Server=localhost;Port=5432;Database=FilmData;User Id=postgres;Password=lab1234";

            // PostgreSQL bağlantı nesnesi oluştur
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // PostgreSQL silme sorgusu
                string sql = "DELETE FROM public.\"Kullanicilar\" WHERE \"KullaniciAdi\" = @kullaniciAd;";

                // PostgreSQL komut nesnesi oluştur
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    // Parametreyi ekle
                    cmd.Parameters.AddWithValue("@kullaniciAd", textkullaniciad.Text);

                    // Silme sorgusunu çalıştır
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Hesabın başarıyla silindi.");
                        Form1 formm = new Form1();
                        formm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Hesap silme başarısız.");
                    }
                }
            }
        }

        private void btnyukselt_Click_1(object sender, EventArgs e)
        {
            textuyelik.Text = "Premium";
            panel12.Visible = false;
            panel2.Visible = true;
        }
        private void picturejoker_Click(object sender, EventArgs e)
        {
            string videoDosyaYolu = @"C:\Users\DELL\Desktop\Nesne Yönelimli Programlama Final Ödevi\NesneYönelimliProgramlamaFinalProjesi\Resources\videolar\JOKER- Türkçe Altyazılı Son Fragman(_WVC1KC4Lauc_).mp4";

            // Windows Media Player'a videoyu yükle ve oynat
            axWindowsMediaPlayer1.URL = videoDosyaYolu;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            tabControl1.SelectedTab = tabPage2;
        }

       

        private void pictureyesilyol_Click_1(object sender, EventArgs e)
        {
            string videoDosyaYolu = @"C:\Users\DELL\Desktop\Nesne Yönelimli Programlama Final Ödevi\NesneYönelimliProgramlamaFinalProjesi\Resources\videolar\The Green Mile (1999) Official Trailer - Tom Hanks Movie HD(_Ki4haFrqSrw_).mp4";

            // Windows Media Player'a videoyu yükle ve oynat
            axWindowsMediaPlayer1.URL = videoDosyaYolu;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            tabControl1.SelectedTab = tabPage2;
        }

        private void picturetoy_Click_1(object sender, EventArgs e)
        {
            string videoDosyaYolu = @"C:\Users\DELL\Desktop\Nesne Yönelimli Programlama Final Ödevi\NesneYönelimliProgramlamaFinalProjesi\Resources\videolar\Toy Story (1995) Trailer #1 _ Movieclips Classic Trailers(_v-PjgYDrg70_) (1).mp4";

            // Windows Media Player'a videoyu yükle ve oynat
            axWindowsMediaPlayer1.URL = videoDosyaYolu;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            tabControl1.SelectedTab = tabPage2;
        }

        private void pictureaquaman_Click(object sender, EventArgs e)
        {
            string videoDosyaYolu = @"C:\Users\DELL\Desktop\Nesne Yönelimli Programlama Final Ödevi\NesneYönelimliProgramlamaFinalProjesi\Resources\videolar\Aquaman Türkçe Dublajlı Fragman(_qOu6a3kmLZQ_).mp4";

            // Windows Media Player'a videoyu yükle ve oynat
            axWindowsMediaPlayer1.URL = videoDosyaYolu;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            tabControl1.SelectedTab = tabPage2;
        }

        private void picturetitanic_Click(object sender, EventArgs e)
        {
            string videoDosyaYolu = @"C:\Users\DELL\Desktop\Nesne Yönelimli Programlama Final Ödevi\NesneYönelimliProgramlamaFinalProjesi\Resources\videolar\Titanik_ 25. Yıl 10 Şubat_ta Sinemalarda!(_8IBfR8N4piM_).mp4";

            // Windows Media Player'a videoyu yükle ve oynat
            axWindowsMediaPlayer1.URL = videoDosyaYolu;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            tabControl1.SelectedTab = tabPage2;
        }

        private void pictureavatar_Click_1(object sender, EventArgs e)
        {
            string videoDosyaYolu = @"C:\Users\DELL\Desktop\Nesne Yönelimli Programlama Final Ödevi\NesneYönelimliProgramlamaFinalProjesi\Resources\videolar\Avatar (İlk Film) _ Altyazılı Fragman(_jTPi-aZQ6R8_).mp4";

            // Windows Media Player'a videoyu yükle ve oynat
            axWindowsMediaPlayer1.URL = videoDosyaYolu;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            tabControl1.SelectedTab = tabPage2;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}

