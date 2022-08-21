using Catalogs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Catalogs.Services
{
    public class CatalogsService
    {
        public void RetrieveCatalogs(string path, CatalogsContext context)
        {
            List<Catalog> list = new List<Catalog>();
            RetrieveCatalogsRecursive(path, null, list);
            context.Catalogs.AddRange(list);
            context.SaveChanges();
        }

        protected void RetrieveCatalogsRecursive(string path, Catalog parent, List<Catalog> list)
        {
            var name = path.Substring(path.LastIndexOf('\\') + 1);
            var catalog = new Catalog { Name = name, Parent = parent };
            list.Add(catalog);

            string[] subdirectories = Directory.GetDirectories(path);
            if (subdirectories.Length > 0)
            {
                foreach (var subdirectory in subdirectories)
                {
                    RetrieveCatalogsRecursive(subdirectory, catalog, list);
                }
            }

        }
    }
}