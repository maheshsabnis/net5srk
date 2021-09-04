using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EF_Code_First.Models
{
	public class SRKLabDbContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Products> Products { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Patients { get; set; }

		public SRKLabDbContext()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SRKLab;Integrated Security=SSPI");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Set One-to-One Relationship from Product To Category
			// and One-To-Many relationships from Category-To-Product

			modelBuilder.Entity<Products>()
						.HasOne(p => p.Categtory) // One-to-One
						.WithMany(c => c.Products) // One-to-Many
						.HasForeignKey(f => f.CategoryRowId); // Foreign Key

			// Many-To-Many

			modelBuilder.Entity<Doctor>()
						.HasMany(d => d.Patients)
						.WithMany(p => p.Doctors)
						.UsingEntity(mtom => mtom.ToTable("DoctorsPatients")); // New Tabale Generated 
						

			base.OnModelCreating(modelBuilder);
		}
	}
}
