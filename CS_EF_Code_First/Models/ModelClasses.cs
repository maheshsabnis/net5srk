using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS_EF_Code_First.Models
{
	public class Category
	{
		[Key]
		public int CategoryRowId { get; set; }
		[Required]
		[StringLength(50)]
		public string CategoryId { get; set; }
		[Required]
		[StringLength(200)]
		public string CategoryName { get; set; }
		[Required]
		public int BasePrice { get; set; }
		// One-To-Many Relatioship
		public ICollection<Products> Products { get; set; }
	}

	public class Products
	{
		[Key]
		public int ProductRowId { get; set; }
		[Required]
		[StringLength(50)]
		public string ProductId { get; set; }
		[Required]
		[StringLength(200)]
		public string ProductName { get; set; }
		[Required]
		[StringLength(400)]
		public string Manufacturer { get; set; }
		[Required]
		[StringLength(600)]
		public string Description { get; set; }
		[Required]
		public int Price { get; set; }
		[Required]
		// Expected to be a Foreign Key
		public int CategoryRowId { get; set; }
		// The One-To-One Relatioship
		public Category Categtory { get; set; }
	}
}
