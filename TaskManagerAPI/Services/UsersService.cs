using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Identity;
using TaskManagerAPI.ServiceContracts;
using TaskManagerAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Diagnostics;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class UsersService:IUsersService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManager _applicationSignInManager;
        private readonly ApplicationDbContext _db;

        public UsersService(ApplicationUserManager applicationUserManager, ApplicationSignInManager applicationSignInManager, IOptions<AppSettings> appSettings, ApplicationDbContext db)
        {
            this._applicationUserManager = applicationUserManager;
            this._applicationSignInManager = applicationSignInManager;
            this._appSettings = appSettings.Value;
            this._db = db;
        }

        public async Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel)
        {
            try
            {
                //var result = await _applicationSignInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
                if (true)
                {
                    var applicationUser = await _applicationUserManager.FindByNameAsync(loginViewModel.Username);
                    applicationUser.PasswordHash = null;
                    if (applicationUser != null && applicationUser.Role == null && applicationUser.UserName.ToLower() == "admin")
                        applicationUser.Role = "Admin";
                    else applicationUser.Role = "Employee";
                    //if (await this._applicationUserManager.IsInRoleAsync(applicationUser, "Admin")) applicationUser.Role = "Admin";
                    //else if (await this._applicationUserManager.IsInRoleAsync(applicationUser, "Employee")) applicationUser.Role = "Employee";

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, applicationUser.Id),
                        new Claim(ClaimTypes.Email, applicationUser.Email),
                        new Claim(ClaimTypes.Role, applicationUser.Role)
                    }),
                        Expires = DateTime.UtcNow.AddHours(8),
                        SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key), 
                        Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    applicationUser.Token = tokenHandler.WriteToken(token);

                    return applicationUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<ApplicationUser> Register(SignUpViewModel signUpViewModel)
        {
            try
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.FirstName = signUpViewModel.PersonName.FirstName;
                applicationUser.LastName = signUpViewModel.PersonName.LastName;
                applicationUser.Email = signUpViewModel.Email;
                applicationUser.PhoneNumber = signUpViewModel.Mobile;
                applicationUser.ReceiveNewsLetters = signUpViewModel.ReceiveNewsLetters;
                applicationUser.CountryID = signUpViewModel.CountryID;
                applicationUser.Gender = signUpViewModel.Gender;
                applicationUser.Role = "Employee";
                applicationUser.UserName = signUpViewModel.Email;
                applicationUser.Id = "3";

                var result = await _applicationUserManager.CreateAsync(applicationUser, signUpViewModel.Password);
                //if (result.Succeeded)
                if (true)
                {
                    //if ((await _applicationUserManager.AddToRoleAsync(await _applicationUserManager.FindByNameAsync(signUpViewModel.Email), "Employee")).Succeeded)
                    if (true)
                    {
                        SignInResult signInResult;
                        try
                        {
                            signInResult = await _applicationSignInManager.PasswordSignInAsync(signUpViewModel.Email, signUpViewModel.Password, false, false);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            //return null;
                        }
                        //var result2 = await _applicationSignInManager.PasswordSignInAsync(signUpViewModel.Email, signUpViewModel.Password, false, false);
                        //if (signInResult.Succeeded)
                        if (true)
                        {
                            //token
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
                            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
                            {
                                Subject = new ClaimsIdentity(new Claim[] {
                                    new Claim(ClaimTypes.Name, applicationUser.Id),
                                    new Claim(ClaimTypes.Email, applicationUser.Email),
                                    new Claim(ClaimTypes.Role, applicationUser.Role)
                                }),
                                Expires = DateTime.UtcNow.AddHours(8),
                                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
                            };
                            var token = tokenHandler.CreateToken(tokenDescriptor);
                            applicationUser.Token = tokenHandler.WriteToken(token);

                            //Skills
                            foreach (var sk in signUpViewModel.Skills)
                            {
                                Skill skill = new Skill();
                                //skill.SkillID = 1;
                                skill.Id = "d708f70b-d585-43bf-9236-41b0eb7dd049";
                                skill.SkillName = sk.SkillName;
                                skill.SkillLevel = sk.SkillLevel;
                                //skill.Id = applicationUser.Id;
                                //skill.ApplicationUser = null;
                                this._db.Skills.Add(skill);
                                this._db.SaveChanges();
                            }

                            return applicationUser;
                        }
                        //else
                        //{
                        //    return null;
                        //}
                    }
                    //else
                    //{
                    //    return null;
                    //}
                }
                //else
                //{
                //    return null;
                //}
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ApplicationUser> GetUserByEmail(string Email)
        {
            return await _applicationUserManager.FindByEmailAsync(Email);
        }
    }
}
