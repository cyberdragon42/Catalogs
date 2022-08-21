using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Catalogs.Models;
using Catalogs.Services;

namespace Catalogs.Controllers
{
    public class CatalogsController : Controller
    {
        private CatalogsContext db = new CatalogsContext();
        private CatalogsService service = new CatalogsService();

        public ActionResult Index()
        {
            var catalogs = db.Catalogs
                .Where(c => c.ParentId == null);
            return View(catalogs.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportCatalogs(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Incorrect directory path");
            }

            try
            {
                service.RetrieveCatalogs(path, db);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ImportCatalogs()
        {
            return View();
        }

        public ActionResult CatalogInfo(int? id)
        {
            var folder = db.Catalogs
                .Where(c => c.Id == id)
                .Include(c => c.Children)
                .FirstOrDefault();
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
