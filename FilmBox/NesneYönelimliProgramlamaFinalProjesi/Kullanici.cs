using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NYPfinalodev
{
    internal abstract class Kullanici
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public BigInteger Tc { get; set; }
        public DateTime DogumTarihi { get; set; }
        public char Cinsiyet { get; set; }

        public string Sifre { get; set; }
        public string KullaniciAdi { get; set; }

        public Kullanici(string ad,string soyad, BigInteger tc, DateTime dogumTarihi, char cinsiyet,string sifre,string kullaniciadi)
        {
            Ad = ad;
            Soyad = soyad;
            Tc = tc;
            DogumTarihi = dogumTarihi;
            Cinsiyet = cinsiyet;
            Sifre = sifre;
            KullaniciAdi = kullaniciadi;
        }
        public abstract double UcretHesapla();

        public virtual string KullaniciBilgileriGoruntule()
        {
            return $"Ad: {Ad},Soyad:{Soyad} TC: {Tc}, Doğum Tarihi: {DogumTarihi}, Cinsiyet: {Cinsiyet}";
        }

    }
}
