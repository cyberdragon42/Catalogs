using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Catalogs.Models;

namespace Catalogs.Controllers
{
    public class CatalogsController : Controller
    {
        private CatalogsContext db = new CatalogsContext();

        public ActionResult Index()
        {
            var catalogs = db.Catalogs.Where(c => c.ParentId == null);
            return View(catalogs.ToList());
        }

        // GET: Catalogs/Create
        public ActionResult Create()
        {
            var folder0 = db.Catalogs.Where(c=>c.Name=="Graphic Products").FirstOrDefault();

            var folders1 = new List<Catalog>
            {
                new Catalog{Name="Process", Parent=folder0},
                new Catalog{Name="Final Product", Parent=folder0},
            };

            db.Catalogs.AddRange(folders1);
            db.SaveChanges();
            ViewBag.ParentId = new SelectList(db.Catalogs, "Id", "Name");
            return View();
        }

        public ActionResult DirectoryInfo(int? id)
        {
            var folder = db.Catalogs.Where(c => c.Id == id).Include(c => c.Children).FirstOrDefault();
            return View(folder);
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
