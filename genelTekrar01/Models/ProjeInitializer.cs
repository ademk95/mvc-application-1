using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace genelTekrar01.Models
{
    public class ProjeInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        //iki tablodan birine bişey olursa tabloyu sil , seed metodu ile tabloyu bastan doldur.
        protected override void Seed(DataContext context)
        {
            List<Kategori> kategoriler = new List<Kategori>()
            {
                new Kategori(){ KategoriAdi="Test Kategori - 1" },
                new Kategori(){ KategoriAdi="Test Kategori - 2" },
                new Kategori(){ KategoriAdi="Test Kategori - 3" },

            };

            foreach (var kategori in kategoriler)
            {
                context.Kategoriler.Add(kategori);
            }

            context.SaveChanges();

            List<Urun> urunler = new List<Urun>()
            {

                new Urun(){UrunAdi="Urun 1",UrunAciklama="Urun Acıklamasıdır. - 1",UrunResim="1.jpg",Anasayfa=true,StoktaMi=true,EklenmeTarihi=DateTime.Now,Icerik="Burası urun icerigi",UrunFiyat=15,KategoriId=1},
                new Urun(){UrunAdi="Urun 2",UrunAciklama="Urun Acıklamasıdır. - 2",UrunResim="1.jpg",Anasayfa=false,StoktaMi=true,EklenmeTarihi=DateTime.Now,Icerik="Burası urun icerigi-2",UrunFiyat=25,KategoriId=2},
                new Urun(){UrunAdi="Urun 3",UrunAciklama="Urun Acıklamasıdır. - 3",UrunResim="1.jpg",Anasayfa=true,StoktaMi=true,EklenmeTarihi=DateTime.Now,Icerik="Burası urun icerigi-3",UrunFiyat=35,KategoriId=2},
                new Urun(){UrunAdi="Urun 4",UrunAciklama="Urun Acıklamasıdır. - 4",UrunResim="1.jpg",Anasayfa=true,StoktaMi=true,EklenmeTarihi=DateTime.Now,Icerik="Burası urun icerigi-4",UrunFiyat=45,KategoriId=2},
                new Urun(){UrunAdi="Urun 5",UrunAciklama="Urun Acıklamasıdır. - 5",UrunResim="1.jpg",Anasayfa=false,StoktaMi=true,EklenmeTarihi=DateTime.Now,Icerik="Burası urun icerigi-5",UrunFiyat=55,KategoriId=3},
                new Urun(){UrunAdi="Urun 6",UrunAciklama="Urun Acıklamasıdır. - 6",UrunResim="1.jpg",Anasayfa=true,StoktaMi=true,EklenmeTarihi=DateTime.Now,Icerik="Burası urun icerigi-6",UrunFiyat=65,KategoriId=3},
                new Urun(){UrunAdi="Urun 7",UrunAciklama="Urun Acıklamasıdır. - 7",UrunResim="1.jpg",Anasayfa=true,StoktaMi=true,EklenmeTarihi=DateTime.Now,Icerik="Burası urun icerigi-7",UrunFiyat=75,KategoriId=3}
            };

            foreach (var urun in urunler)
            {
                context.Urunler.Add(urun);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}