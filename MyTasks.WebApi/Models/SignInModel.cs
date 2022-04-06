using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Models
{
    public class SignInModel
    {
        [Required]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
