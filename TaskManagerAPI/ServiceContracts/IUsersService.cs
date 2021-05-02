using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Identity;
using TaskManagerAPI.ViewModels;

namespace TaskManagerAPI.ServiceContracts
{
    public interface IUsersService
    {
        Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel);
        //Task<ApplicationUser> Authenticate(string userName, string password);

        Task<ApplicationUser> Register(SignUpViewModel signUpViewModel);
        Task<ApplicationUser> GetUserByEmail(string Email);
    }
}
