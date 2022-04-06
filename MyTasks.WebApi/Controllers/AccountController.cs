using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApi.Models;
using MyTasks.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var result = await _accountRepository.SignUpAsync(signUpModel);

            if (result.Succeeded)
                return Ok(result.Succeeded);

            return Unauthorized();
        }


        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var result = await _accountRepository.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(result))
                return Unauthorized();

            return Ok(result);
        }
    }
}
