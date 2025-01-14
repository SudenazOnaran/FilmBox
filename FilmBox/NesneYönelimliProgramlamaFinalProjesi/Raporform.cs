using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NYPfinalodev
{
    public partial class Raporform : Form
    {
        public Raporform()
        {
            InitializeComponent();
        }
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=FilmData;User Id = postgres; Password = lab1234");
        public void tabloyu_gosters(string txt)
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
                istatistikdata.DataSource = dt;
            }
            komut.Dispose();
            conn.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnhome_Click(object sender, EventArgs e)
        {
            string query = "SELECT \"FilmAdi\", \"IzlenmeSayisi\" FROM public.\"FilmBilgisi\" WHERE \"IzlenmeSayisi\" >=20000 ";
            tabloyu_gosters(query);
        }

        private void btnizle_Click(object sender, EventArgs e)
        {
            string sorgula = "SELECT \"FilmAdi\", \"DPuan\" FROM public.\"FilmBilgisi\" WHERE \"DPuan\" >=7 ";
            tabloyu_gosters(sorgula);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Sorgu = "SELECT fb.\"FilmTuru\", COUNT(*) as \"ToplamDegerlendirme\" " +
                                        "FROM public.\"FilmPuanYorum\" fp " +
                                        "JOIN public.\"FilmBilgisi\" fb ON fp.\"FilmAdi\" = fb.\"FilmAdi\" " +
                                        "GROUP BY fb.\"FilmTuru\" " +
                                        "ORDER BY \"ToplamDegerlendirme\" DESC " +
                                        "LIMIT 3;";
            tabloyu_gosters(Sorgu);
        }

 
    }
}
