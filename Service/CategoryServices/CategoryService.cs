using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CategoryServices
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(string name);
        Task<List<Category>> GetAllCategoryAsync();
        Task<List<Category>> GetAvailableAsync();
        Task<bool> DisableOrEnableAsync(string id);
    }
    public class CategoryService: ICategoryService
    {
        private readonly BookContext _context;
        public CategoryService(BookContext bookContext) {
            _context = bookContext;
        }

        public async Task<bool> CreateAsync(string name)
        {
            var categoryDb = await _context.Categories.Where(p => p.Name == name && p.IsActive).FirstOrDefaultAsync();
            if (categoryDb == null)
            {
                Category newCategory = new Category()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    IsActive = true
                };
                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        } 

        public async Task<List<Category>> GetAvailableAsync()
        {
            return await _context.Categories.Where(p => p.IsActive).ToListAsync();
        }

        public async Task<bool> DisableOrEnableAsync(string id)
        {
            var currentCategory = await _context.Categories.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (currentCategory != null)
            {
                currentCategory.IsActive = !currentCategory.IsActive;
                _context.Categories.Update(currentCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
      
    }
}
