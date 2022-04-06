using Microsoft.EntityFrameworkCore;
using MyTasks.WebApi.Models;
using MyTasks.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MyTasksDbContext _dbContext;

        public TaskRepository(MyTasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TaskDomain>> GetTasksAsync(string userId, string title = null, int categoryId = 0, bool isExecuted = false)
        {
            var tasks = _dbContext.Tasks
                    .Include(x => x.Category)
                    .Where(x => x.UserId == userId && x.IsExecuted == isExecuted);

            if (categoryId != 0)
                tasks = tasks.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                tasks = tasks.Where(x => x.Title.Contains(title));

            return await tasks.OrderBy(x => x.Term).ToListAsync();
        }

        public async Task<TaskDomain> GetTaskAsync(int taskId, string userId)
        {
            return await _dbContext.Tasks.SingleAsync(x => x.Id == taskId && x.UserId == userId);
        }

        public async Task EditTaskAsync(TaskDomain task)
        {
            var taskToEdit = await _dbContext.Tasks.SingleAsync(x => x.Id == task.Id && x.UserId == task.UserId);

            taskToEdit.Title = task.Title;
            taskToEdit.CategoryId = task.CategoryId;
            taskToEdit.Description = task.Description;
            taskToEdit.Term = task.Term;
            taskToEdit.IsExecuted = task.IsExecuted;
        }

        public async Task AddTaskAsync(TaskDomain task)
        {
            await _dbContext.Tasks.AddAsync(task);
        }

        public async Task DeleteAsync(int taskId, string userId)
        {
            var taskToDelete = await _dbContext.Tasks.SingleAsync(x => x.Id == taskId && x.UserId == userId);
            _dbContext.Tasks.Remove(taskToDelete);
        }

        public async Task FinishAsync(int taskId, string userId)
        {
            var taskToFinish = await _dbContext.Tasks.SingleAsync(x => x.Id == taskId && x.UserId == userId);
            taskToFinish.IsExecuted = true;
        }
    }
}
