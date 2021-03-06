﻿using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using LinqToDB;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using System.Web.Razor.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Dynamic;
using System.Windows.Forms;

namespace MyProject.Controllers
{

    public class HomeController : Controller
    {
        KullaniciDBEntities db = new KullaniciDBEntities();

        public ActionResult KullaniciOlusturma()
        {

            KullaniciDBEntities db = new KullaniciDBEntities();

            List<Kisi> kL = db.Kisi.ToList();
            ViewBag.kisiList = new SelectList(kL, "AdSoyad", "Email", "Yas", "Konum", "Telefon", "Parola");

            return View();
        }

        [HttpPost]
        public ActionResult KullaniciOlusturma(List<Kisi> modelList)
        {

            KullaniciDBEntities db = new KullaniciDBEntities();
            List<Kisi> eklenenList = new List<Kisi>();
            foreach (Kisi model in modelList)
            {
                Kisi k = new Kisi();

                k.ID = model.ID;
                k.AdSoyad = model.AdSoyad;
                k.Email = model.Email;
                k.Yas = model.Yas;
                k.Konum = model.Konum;
                k.Telefon = model.Telefon;
                k.Parola = model.Parola;

                db.Kisi.Add(k);
                db.SaveChanges();
                eklenenList.Add(k);
            }
            return RedirectToAction("KullaniciListeleme", eklenenList);


        }

        public ActionResult SoruSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KisiSoruları s = db.KisiSoruları.Find(id);

            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);

        }

        [HttpPost, ActionName("SoruSil")]
        [ValidateAntiForgeryToken]
        public ActionResult SoruSil(int id)
        {
            KisiSoruları s = db.KisiSoruları.Find(id);
            db.KisiSoruları.Remove(s);
            db.SaveChanges();
            return RedirectToAction("KullaniciSoruEkrani");
        }

        public ActionResult KullaniciListeleme()
        {
            Kisi k = new Kisi();
            List<Kisi> kisiListesi = db.Kisi.ToList();
            return View(kisiListesi);
        }

        public ActionResult ListedenKullaniciSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisi k = db.Kisi.Find(id);

            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }

        [HttpPost, ActionName("ListedenKullaniciSil")]
        [ValidateAntiForgeryToken]
        public ActionResult ListedenKullaniciSil(int id)
        {
            Kisi k = db.Kisi.Find(id);
            db.Kisi.Remove(k);
            db.SaveChanges();
            return RedirectToAction("KullaniciListeleme");
        }
        
        public ActionResult SoruKullaniciSecme()
        {
            KullaniciDBEntities db = new KullaniciDBEntities();
            List<SelectListItem> kisi = new List<SelectListItem>();


            Random rand = new Random();
            int id = rand.Next(db.Kisi.ToList().Count);
            int i = 0;
            foreach (var item in db.Kisi.ToList())
            {
                    kisi.Add(new SelectListItem { Text = item.AdSoyad, Value = item.ID.ToString(), Selected = false });

            }


            ViewBag.kisiler = kisi;

            Random rnd = new Random();
            int r = rnd.Next(ViewBag.kisiler.Count);
            TempData["r"] = r;
            TempData["kisi"] = kisi;

            List<SelectListItem> soru = new List<SelectListItem>();

            foreach (var item in db.Soru.ToList())
            {
                soru.Add(new SelectListItem { Text = item.SoruAd, Value = item.ID.ToString() });
                
            }

            ViewBag.sorular = soru;
            Random x = new Random();
            int y = rnd.Next(ViewBag.sorular.Count);
            TempData["y"] = y;

            return View();
        }


        [HttpPost]
        public ActionResult SoruKullaniciSecme(KisiSoruları m)
        {

            m.Kisi = m.Kisi;
            m.Soru = m.Soru;
            m.Cevap = m.Cevap;
            m.CevapTarihi = DateTime.Now;

            db.KisiSoruları.Add(m);
            db.SaveChanges();

            return RedirectToAction("SoruKullaniciSecme");
        }


        public ActionResult KullaniciSoruEkrani()
        {
            KisiSoruları ks = new KisiSoruları();
            List<KisiSoruları> kisiSoruları = db.KisiSoruları.ToList();
            return View(kisiSoruları);
        }
    }
}