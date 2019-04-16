using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace genelTekrar01.Models
{
    public class UrunKategoriViewModel
    {
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public int UrunSayisi { get; set; }
        public List<UrunModel> Urunler { get; set; }
    }
}