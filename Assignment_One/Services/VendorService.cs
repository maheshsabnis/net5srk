using Assignment_One.Models;
using Assignment_One.Models.Context;
using Assignment_One.Services.impl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_One.Services
{
    public partial class VendorService : IService<Vendor, int>, IVendor
    {
        private readonly DB_Context _Context;
        public VendorService(DB_Context context)
        {
            _Context = context;
        }

        public async Task<Vendor> CreateAsync(Vendor vendor)
        {
            var result = await _Context.Vendors.AddAsync(vendor);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int VendorId)
        {
            Vendor vendor = await _Context.Vendors.FindAsync(VendorId);
            _Context.Vendors.Remove(vendor);
            await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vendor>> GetAsync()
        {
            return await _Context.Vendors.ToListAsync();
        }

        public async Task<Vendor> GetAsync(int id)
        {
            return await _Context.Vendors.FindAsync(id);
        }

        public async Task<Vendor> SupplyProductAsync(int VendorId, int[] ProductIds)
        {
            //List<int> ProductIds = products.Select(p => p.ProductId).ToList();
            List<Product> _products = await _Context.Products.Where(p => ProductIds.Contains(p.ProductId)).ToListAsync();
            Vendor _vendor = await _Context.Vendors.FindAsync(VendorId);
            _vendor.Products = _products;
            await _Context.SaveChangesAsync();
            return _vendor;
        }

        public async Task<Vendor> UpdateAsync(int VendorId, Vendor vendor)
        {
            Vendor _vendor = await _Context.Vendors.FindAsync(VendorId);
            _vendor.VendorId = vendor.VendorId;
            _vendor.VendorName = vendor.VendorName;
            await _Context.SaveChangesAsync();
            return _vendor;
        }
    }
}
