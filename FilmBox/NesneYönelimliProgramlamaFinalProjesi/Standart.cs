using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NYPfinalodev
{
    internal class Standart : Kullanici
    {
        public Standart(string ad, string soyad, BigInteger tc, DateTime dogumTarihi, char cinsiyet,string sifre,string kullaniciadi) : base(ad, soyad, tc, dogumTarihi, cinsiyet,sifre,kullaniciadi)
        {
        }

        public override double UcretHesapla()
        {
            return 100;
        }
        public override string KullaniciBilgileriGoruntule()
        {
            return base.KullaniciBilgileriGoruntule()+"Standart Kullanıcı";
        }
    }
}
