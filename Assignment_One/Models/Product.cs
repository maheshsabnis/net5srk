using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_One.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(64)]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
        public int Price { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
    }
}
