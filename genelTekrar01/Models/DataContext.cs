using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace genelTekrar01.Models
{
    public class DataContext:DbContext
    {
        //burası calısıp , vt yi olusturuyor.
        public DataContext():base("urunConnection")
        {
            Database.SetInitializer(new ProjeInitializer());
        }

        //veri tabanı tabloları.
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }

        
    }
}