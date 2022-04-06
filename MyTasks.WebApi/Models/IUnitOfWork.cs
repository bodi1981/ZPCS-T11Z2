using MyTasks.WebApi.Repositories;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Models
{
    public interface IUnitOfWork
    {
        CategoryRepository Category { get; set; }
        TaskRepository Task { get; set; }

        Task CompleteAsync();
    }
}