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
    public partial class ProductService : IService<Product, int>
    {
        private readonly DB_Context _Context;
        public ProductService(DB_Context context)
        {
            _Context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var result = await _Context.Products.AddAsync(product);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int ProductId)
        {
            Product product = await _Context.Products.FindAsync(ProductId);
            _Context.Products.Remove(product);
            await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _Context.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _Context.Products.FindAsync(id);
        }

        public async Task<Product> UpdateAsync(int ProductId, Product product)
        {
            Product _product = await _Context.Products.FindAsync(ProductId);
            _product.ProductId = product.ProductId;
            _product.ProductName = product.ProductName;
            _product.Price = product.Price;
            _product.CategoryId = product.CategoryId;
            await _Context.SaveChangesAsync();
            return _product;
        }
    }
}
