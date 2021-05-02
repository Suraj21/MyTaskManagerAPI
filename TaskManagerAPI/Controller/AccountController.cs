using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Identity;
using TaskManagerAPI.ServiceContracts;
using TaskManagerAPI.ViewModels;

namespace TaskManagerAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUsersService _usersService;
        private ApplicationSignInManager _applicationSignInManager;

        public AccountController(IUsersService usersService, ApplicationSignInManager applicationSignManager)
        {
            this._usersService = usersService;
            this._applicationSignInManager = applicationSignManager;
        }

        [HttpGet]
        public List<string> Get()
        {
            return new List<string> { "Get Test1", "Get Test2" };
        }

        [HttpGet]
        [Route("[action]")]
        public List<string> Account()
        {
            return new List<string> { "Test1", "Test2" };
        }

        [HttpPost]
        [Route("Account")]
        public void Account(object str)
        {
            Console.WriteLine(str);
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel.Username != null || loginViewModel.Password != null)
            {
                var user = await _usersService.Authenticate(loginViewModel);
                if (user != null && user.Role == null && user.UserName.ToLower() == "admin")
                    user.Role = "Admin";

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                //HttpContext.User = await _applicationSignInManager.CreateUserPrincipalAsync(user);
                //var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
                //Response.Headers.Add("Access-Control-Expose-Headers", "XSRF-REQUEST-TOKEN");
                //Response.Headers.Add("XSRF-REQUEST-TOKEN", tokens.RequestToken);

                return Ok(user);
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] SignUpViewModel signUpViewModel)
        {
            var user = await _usersService.Register(signUpViewModel);
            if (user == null)
                return BadRequest(new { message = "Invalid Data" });

            //HttpContext.User = await _applicationSignInManager.CreateUserPrincipalAsync(user);
            //var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            //Response.Headers.Add("Access-Control-Expose-Headers", "XSRF-REQUEST-TOKEN");
            //Response.Headers.Add("XSRF-REQUEST-TOKEN", tokens.RequestToken);

            return Ok(user);
        }

        [Route("getUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _usersService.GetUserByEmail(email);
            return Ok(user);
        }
    }
}
