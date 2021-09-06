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
    public partial class CategoryService : IService<Category, int>
    {
        private readonly DB_Context _Context;
        public CategoryService(DB_Context context)
        {
            _Context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            var result = await _Context.Categories.AddAsync(category);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(int CategoryId)
        {
            var category = await _Context.Categories.FindAsync(CategoryId);
            _Context.Categories.Remove(category);
            await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _Context.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _Context.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateAsync(int CategoryId, Category category)
        {
            // 1 serach recorde
            Category _category = await _Context.Categories.FindAsync(CategoryId);
            _category.CategoryId = category.CategoryId;
            _category.CategoryName = category.CategoryName;
            await _Context.SaveChangesAsync();
            return _category;
        }
    }
}
