using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_One.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }
        [Required]
        [StringLength(128)]
        public string VendorName { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
