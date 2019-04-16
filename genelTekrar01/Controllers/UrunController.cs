using genelTekrar01.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace genelTekrar01.Controllers
{
    public class UrunController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Urun
        public ActionResult Index()
        {
            //var urunler = db.Urunler.ToList();
            var urunler = db.Urunler
                .OrderByDescending(x=>x.EklenmeTarihi)
                .Select(x => new UrunModel()
                {
                    Id=x.Id,
                    UrunAdi =x.UrunAdi,
                    UrunAciklama=x.UrunAciklama,
                    UrunResim=x.UrunResim,
                    Anasayfa=x.Anasayfa,
                    EklenmeTarihi=x.EklenmeTarihi,
                    Icerik=x.Icerik,
                    UrunFiyat=x.UrunFiyat,
                    StoktaMi=x.StoktaMi,
                    KategoriAdi=x.Kategori.KategoriAdi

                }).ToList();

            return View(urunler);
        }
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urun urun)
        {
            
            if (ModelState.IsValid)
            {
                urun.EklenmeTarihi = DateTime.Now;
                db.Urunler.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", urun.KategoriId);
            return View(urun);
        }

        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urunler.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }

            //ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi");
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", urun.KategoriId);
            return View(urun);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Urun urun)
        {
            if (ModelState.IsValid)
            {

                var urunler = db.Urunler.Find(urun.Id);
                if (urunler != null)
                {
                    urunler.UrunAdi = urun.UrunAdi;
                    urunler.UrunAciklama = urun.UrunAciklama;
                    urunler.StoktaMi = urun.StoktaMi;
                    urunler.Anasayfa = urun.Anasayfa;
                    urunler.Icerik = urun.Icerik;
                    urunler.UrunFiyat = urun.UrunFiyat;
                    urunler.UrunResim = urun.UrunResim;
                    urunler.KategoriId = urun.KategoriId;
                    urunler.EklenmeTarihi = DateTime.Now;
                    db.SaveChanges();
                    TempData["Urun"] = urunler;
                    return RedirectToAction("Index");
                }

                //urun.EklenmeTarihi = DateTime.Now;
                //db.Entry(urun).State = EntityState.Modified;


            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", urun.KategoriId);
            return View(urun);
           
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var urun = db.Urunler.Find(id);

            if (urun == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", urun.KategoriId);
            return View(urun);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var urun = db.Urunler.Find(id);
            db.Urunler.Remove(urun);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [ChildActionOnly]
        public PartialViewResult UrunList()
        {
            var urunler = db.Urunler
                .OrderByDescending(x=>x.EklenmeTarihi)
                .Take(4)
                .Select(x => new UrunModel()
                {
                    UrunAdi=x.UrunAdi,
                    KategoriAdi=x.Kategori.KategoriAdi,
                    UrunFiyat=x.UrunFiyat

                }).ToList();

            return PartialView("UrunListesi",urunler);
        }
    }
}