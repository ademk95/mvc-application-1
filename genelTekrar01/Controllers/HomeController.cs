using genelTekrar01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace genelTekrar01.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Home
        //Urun Listesi
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urunler()
        {

            var urunler = db.Urunler
                .OrderByDescending(x => x.UrunFiyat)
                .Where(x => x.Anasayfa == true && x.StoktaMi == true)
                .Select(x => new UrunModel()
                {
                    Id = x.Id,
                    UrunAdi = x.UrunAdi,
                    KategoriAdi = x.Kategori.KategoriAdi,
                    UrunAciklama = x.UrunAciklama,
                    UrunFiyat = x.UrunFiyat

                }).ToList();


            return View(urunler);
        }

        public ActionResult UrunDetay(int? id)
        {
            //var urunler = db.Urunler.Find(id);
            var urunler = db.Urunler
                .Where(x=>x.Id==id && x.Anasayfa==true)
                .Select(x => new UrunModel()
                {
                    Id=x.Id,
                    UrunAdi=x.UrunAdi,
                    UrunFiyat=x.UrunFiyat,
                    UrunAciklama=x.UrunAciklama,
                    KategoriId=x.KategoriId,
                    KategoriAdi=x.Kategori.KategoriAdi
                    

                }).FirstOrDefault();

            return View(urunler);
        }
        public ActionResult KategoriDetay(int? id)
        {
            //var seciliKategori = db.Kategoriler.Where(x => x.Id == id).FirstOrDefault();
            var seciliKategori = id;
            ViewBag.KategoriInfo = seciliKategori;

            var kategoriler = db.Kategoriler
                .Select(x => new UrunKategoriViewModel()
                {
                    KategoriId = x.Id,
                    KategoriAdi = x.KategoriAdi,
                    Urunler = x.Urunler
                    .Select(i => new UrunModel()
                    {
                        UrunAdi=i.UrunAdi,
                        Id=i.Id

                    }).ToList()
                    

                }).ToList();
            
            return View(kategoriler);
        }
        public ActionResult KategoriListe(int? id)
        {

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult KategoriList()
        {
            //Yeni Eklenen 4 ürünü Listeliyor.
            //var kategori = db.Kategoriler.ToList();
            var urunler = db.Urunler
                .OrderByDescending(x=>x.EklenmeTarihi)
                .Take(4)
                .Select(x=>new UrunModel()
                {
                    KategoriAdi=x.Kategori.KategoriAdi,
                    UrunAdi=x.UrunAdi,
                    UrunFiyat=x.UrunFiyat,
                    Id=x.Id
                    
                    
                }).ToList();


            return PartialView("KategoriToUrun",urunler);

        }

        [ChildActionOnly]
        public PartialViewResult KategoriUrun(int? id)
        {

            //var urunler = db.Urunler
            //    .Where(x => x.KategoriId == id).ToList();

            //int id = 13;

            var urunler = db.Urunler
                .Where(x=>x.KategoriId==id)
                .Take(3)
                .Select(x => new UrunModel()
                {
                    UrunAdi=x.UrunAdi,
                    Id=x.Id,
                    KategoriAdi=x.Kategori.KategoriAdi,
                    UrunFiyat=x.UrunFiyat

                }).ToList();

            return PartialView("KategoriUrunListesi", urunler);
            
            
        }

        [ChildActionOnly]
        public PartialViewResult KategoriDinamik()
        {
            //var kategoriler = db.Kategoriler.ToList();
            var kategoriler = db.Kategoriler
                .Select(x => new UrunKategoriViewModel()
                {
                    KategoriAdi = x.KategoriAdi,
                    KategoriId = x.Id,
                    UrunSayisi=x.Urunler.Count(),
                    Urunler = x.Urunler
                    .Select(i => new UrunModel() {

                        UrunAdi=i.UrunAdi,
                        Id=i.Id
                       
                        

                    }).ToList()


                }).ToList();

            

            return PartialView("KategoriHomePage",kategoriler);

        }
        
        public PartialViewResult UrunAra(string urunAdi)
        {
            ViewBag.ArananKelime = urunAdi;
            var sonuc = db.Urunler.Where(x => x.UrunAdi.Contains(urunAdi)).ToList();

            return PartialView("UrunSearchList",sonuc);
        }

        public ActionResult SearchSonuc(string urunAdi)
        {
            ViewBag.ArananKelime = urunAdi;
            
            var sonuc = db.Urunler.Where(x => x.UrunAdi.Contains(urunAdi)).ToList();
            
            //return RedirectToAction("Index", sonuc);
            return View(sonuc);
        }
        public PartialViewResult SearchHome()
        {

            return PartialView("SearchAll","Home");
        }

        [ChildActionOnly]
        public PartialViewResult UrunAllSlider()
        {
            var urunler = db.Urunler
                .Take(12)
                .Select(x => new UrunModel()
                {
                    Id=x.Id,
                    UrunAdi = x.UrunAdi,
                    UrunFiyat = x.UrunFiyat,
                    KategoriAdi = x.Kategori.KategoriAdi
                }).ToList();
                
            return PartialView("UrunlerSlider",urunler);
        }
    }
}