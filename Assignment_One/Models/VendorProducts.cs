using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_One.Models
{
    public class VendorProduct
    {
        [Key]
        [Column("VendorsVendorId")]
        public int VendorId { get; set; }
        //[Key]
        [Column("ProductsProductId")]
        public int ProductId { get; set; }
    }
}
