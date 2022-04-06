using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTasks.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.WebApi.Models
{
    public class MyTasksDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyTasksDbContext(DbContextOptions<MyTasksDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskDomain> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
