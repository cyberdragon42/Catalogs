using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Catalogs.Models
{
    public class CatalogsContext: DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }

        public CatalogsContext() : base("CatalogsContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>()
                .HasOptional(c => c.Parent)
                .WithMany(f => f.Children)
                .HasForeignKey(c => c.ParentId);
        }
    }
}