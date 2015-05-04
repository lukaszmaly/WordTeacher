using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using English.Models;

namespace English.Controllers
{
    public class WordUsagesController : Controller
    {
        private EnglishContext db = new EnglishContext();

        // GET: WordUsages
        public ActionResult Index()
        {
            var wordUsages = db.WordUsages.Include(w => w.Entry);
            return View(wordUsages.ToList());
        }

        // GET: WordUsages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WordUsage wordUsage = db.WordUsages.Find(id);
            if (wordUsage == null)
            {
                return HttpNotFound();
            }
            return View(wordUsage);
        }

        // GET: WordUsages/Create
        public ActionResult Create()
        {
            ViewBag.EntryId = new SelectList(db.Entries, "EntryId", "EnglishWord");
            return View();
        }

        // POST: WordUsages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WordUsageId,EntryId,Usage")] WordUsage wordUsage)
        {
            if (ModelState.IsValid)
            {
                db.WordUsages.Add(wordUsage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntryId = new SelectList(db.Entries, "EntryId", "EnglishWord", wordUsage.EntryId);
            return View(wordUsage);
        }

        // GET: WordUsages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WordUsage wordUsage = db.WordUsages.Find(id);
            if (wordUsage == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntryId = new SelectList(db.Entries, "EntryId", "EnglishWord", wordUsage.EntryId);
            return View(wordUsage);
        }

        // POST: WordUsages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WordUsageId,EntryId,Usage")] WordUsage wordUsage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wordUsage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntryId = new SelectList(db.Entries, "EntryId", "EnglishWord", wordUsage.EntryId);
            return View(wordUsage);
        }

        // GET: WordUsages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WordUsage wordUsage = db.WordUsages.Find(id);
            if (wordUsage == null)
            {
                return HttpNotFound();
            }
            return View(wordUsage);
        }

        // POST: WordUsages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WordUsage wordUsage = db.WordUsages.Find(id);
            db.WordUsages.Remove(wordUsage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
