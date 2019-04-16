using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace genelTekrar01.Models
{
    public class UrunModel
    {
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public string UrunAciklama { get; set; }
        public string Icerik { get; set; }
        public string UrunResim { get; set; }
        public double UrunFiyat { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool StoktaMi { get; set; }
        public bool Anasayfa { get; set; }
        //1-n ilişki n in ilişkisi
        public string KategoriAdi { get; set; }
        public int KategoriId { get; set; }
        public int UrunSayisi { get; set; }

    }
}