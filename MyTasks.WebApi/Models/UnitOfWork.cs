using MyTasks.WebApi.Models.Domains;
using MyTasks.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyTasksDbContext _dbContext;

        public UnitOfWork(MyTasksDbContext dbContext)
        {
            _dbContext = dbContext;
            Task = new TaskRepository(dbContext);
            Category = new CategoryRepository(dbContext);
        }

        public TaskRepository Task { get; set; }
        public CategoryRepository Category { get; set; }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
