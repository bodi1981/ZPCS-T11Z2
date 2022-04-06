using MyTasks.WebApi.Models.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Repositories
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId, string userId);
        Task EditCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync(string userId);
        Task<Category> GetCategoryAsync(int categoryId, string userId);
    }
}