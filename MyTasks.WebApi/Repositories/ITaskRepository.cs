using MyTasks.WebApi.Models.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Repositories
{
    public interface ITaskRepository
    {
        Task AddTaskAsync(TaskDomain task);
        Task DeleteAsync(int taskId, string userId);
        Task EditTaskAsync(TaskDomain task);
        Task FinishAsync(int taskId, string userId);
        Task<TaskDomain> GetTaskAsync(int taskId, string userId);
        Task<IEnumerable<TaskDomain>> GetTasksAsync(string userId, string title = null, int categoryId = 0, bool isExecuted = false);
    }
}