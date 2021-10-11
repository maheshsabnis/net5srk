using Assignment_One.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_One.Services.impl
{
    public interface IVendor
    {
        Task<Vendor> SupplyProductAsync(int VendorId, int[] ProductIds);
    }
}
