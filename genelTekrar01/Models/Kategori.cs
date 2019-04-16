using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace genelTekrar01.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public string KategoriAdi { get; set; }
        //1-n de 1 lik ilişki
        public List<Urun> Urunler { get; set; }
    }
}