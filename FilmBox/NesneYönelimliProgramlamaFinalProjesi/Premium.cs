using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NYPfinalodev
{
    internal class Premium:Kullanici
    {
        public Premium(string ad, string soyad, BigInteger tc, DateTime dogumTarihi, char cinsiyet,string sifre, string kullaniciadi) : base (ad, soyad, tc, dogumTarihi, cinsiyet,sifre,kullaniciadi)
        {
        }

        public override double UcretHesapla()
        {
            return 100 + (100 * 0.25);
        }
        public override string KullaniciBilgileriGoruntule()
        {
            return base.KullaniciBilgileriGoruntule()+"premium kullanıcı";
        }


    }
}
