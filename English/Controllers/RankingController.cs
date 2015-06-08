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
    public class RankingController : Controller
    {
        private EnglishContext db = new EnglishContext();

        // GET: Ranking
        public ActionResult Index()
        {
            return View(db.GameUsers.OrderByDescending(user=> user.Points).ToList());
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
