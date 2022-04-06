using Microsoft.EntityFrameworkCore;
using MyTasks.WebApi.Models;
using MyTasks.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyTasksDbContext _dbContext;
        public CategoryRepository(MyTasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string userId)
        {
            return await _dbContext.Categories.Where(x => x.UserId == userId || x.UserId == null).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int categoryId, string userId)
        {
            return await _dbContext.Categories.SingleAsync(x => x.Id == categoryId && x.UserId == userId);
        }

        public async Task EditCategoryAsync(Category category)
        {
            var categoryToEdit = await _dbContext.Categories.SingleAsync(x => x.Id == category.Id && x.UserId == category.UserId);

            categoryToEdit.Name = category.Name;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId, string userId)
        {
            var categoryToDelete = await _dbContext.Categories.SingleAsync(x => x.Id == categoryId && x.UserId == userId);
            _dbContext.Categories.Remove(categoryToDelete);
        }
    }
}
