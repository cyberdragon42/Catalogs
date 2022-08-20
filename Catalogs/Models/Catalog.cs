using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogs.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ParentId { get; set; }
        public Catalog Parent { get; set; }

        public ICollection<Catalog> Children { get; set; }
    }
}