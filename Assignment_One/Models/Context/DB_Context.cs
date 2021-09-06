using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_One.Models.Context
{
    public class DB_Context : DbContext
    {
        public DB_Context()
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorProduct> VendorsProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*Configure Sql Server Connection*/
            optionsBuilder.UseSqlServer(@"Data Source=SRKSUR5110LT\SA;Initial Catalog=AssOne;User Id=sa; Password=Srkay@2019;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Add One to Many Relationship between Product and Category*/
            modelBuilder.Entity<Product>()
                .HasOne(p => p.category)
                .WithMany(c => c.Products)
                .HasForeignKey(fk => fk.CategoryId);

            /*Add Many to Many Relationship between Vendor and Product*/
            modelBuilder.Entity<Product>()
                .HasMany(c => c.Vendors)
                .WithMany(v => v.Products)
                .UsingEntity(vc=> vc.ToTable("VendorProducts"));
                        
            base.OnModelCreating(modelBuilder);
        }

    }
}
