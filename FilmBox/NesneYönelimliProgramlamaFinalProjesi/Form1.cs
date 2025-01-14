using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NYPfinalodev
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.PasswordChar = '*';
            if(textBox4.Text == "")
            {
                textBox4.Text = "kullanıcı adı ";

            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox4.Text;
            string sifre = textBox3.Text;

            using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=FilmData;User Id = postgres; Password = 1234"))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM public.\"Kullanicilar\" WHERE \"KullaniciAdi\" = @KullaniciAdi AND \"Sifre\" = @Sifre;";

                using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
                {
                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                    command.Parameters.AddWithValue("@Sifre", sifre);

                    // Veritabanında belirli bir kullanıcı adı ve şifre kombinasyonu olup olmadığını kontrol et
                    int sayac = Convert.ToInt32(command.ExecuteScalar());


                    if (sayac > 0)
                    {
                        string hosgeldinKullaniciAdi = kullaniciAdi;
                        if ((kullaniciAdi == "Yonetici") && (sifre == "123"))
                        {
                            Form2 form2 = new Form2();
                            form2.Show();
                            this.Hide();

                        }
                        else
                        {
                            // Giriş izni ver
                            Form3 form3 = new Form3(hosgeldinKullaniciAdi);
                            form3.Show();
                            this.Hide();
                        }

                    }
                    else
                    {
                       
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı");
                    }
                }
            }
            
        }

    

        private void button2_Click(object sender, EventArgs e)
        {
            Kaydol formm = new Kaydol();
            formm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
