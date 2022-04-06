using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Tasks = new Collection<TaskDomain>();
            Categories = new Collection<Category>();
        }
        public ICollection<TaskDomain> Tasks { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
