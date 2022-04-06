using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Models.Domains
{
    public class Category
    {
        public Category()
        {
            Tasks = new Collection<TaskDomain>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole nazwa jest wymagane")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        public string UserId { get; set; }

        public ICollection<TaskDomain> Tasks { get; set; }
        public ApplicationUser User { get; set; }
    }
}
