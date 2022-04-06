using Microsoft.AspNetCore.Identity;
using MyTasks.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> SignInAsync(SignInModel signInModel);
    }
}
