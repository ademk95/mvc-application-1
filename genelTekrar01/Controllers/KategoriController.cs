using genelTekrar01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace genelTekrar01.Controllers
{
    public class KategoriController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Kategori
        public ActionResult Index()
        {
            var kategoriler = db.Kategoriler
                .Select(x => new UrunKategoriViewModel()
                {
                    KategoriId=x.Id,
                    KategoriAdi=x.KategoriAdi,
                    UrunSayisi=x.Urunler.Count()

                }).ToList();


            ViewBag.KategoriSayisi = kategoriler.Count();

            return View(kategoriler);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategori kategori)
        {
            //formdan model geliyor.butona basınca submit.
            if (ModelState.IsValid)
            {
                db.Kategoriler.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(kategori);
        }

        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var kategori = db.Kategoriler.Find(id);

            if (kategori == null)
            {
                return HttpNotFound();
            }

            return View(kategori);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                var kategoriEdit = db.Kategoriler.Find(kategori.Id);
                kategoriEdit.KategoriAdi = kategori.KategoriAdi;
                db.SaveChanges();
                TempData["Kategoris"] = kategoriEdit;
                return RedirectToAction("Index");
            }

            return View(kategori);
        }


        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var kategori = db.Kategoriler.Find(id);
            if (kategori==null)
            {
                return HttpNotFound();

            }

            return View(kategori);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var urunler = db.Urunler.Where(x => x.KategoriId == id).ToList();
            foreach (var urun in urunler)
            {
                db.Urunler.Remove(urun);
            }
            var kategori = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}