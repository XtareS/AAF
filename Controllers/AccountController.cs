using AAF.Data.Entities;
using AAF.Helpers;
using AAF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AAF.Controllers
{
    public class AccountController:Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;

        public AccountController(IUserHelper userHelper, IConfiguration configuration)
        {
            this.userHelper = userHelper;
            this.configuration = configuration;
        }


        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index","Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rslt = await this.userHelper.LoginAsync(model);
                if (rslt.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");

                }
            }
            this.ModelState.AddModelError(string.Empty, "Falha ao Entrar na Conta");
            return this.View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogOutAsync();
            return this.RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.UserName,
                        Email = model.Email

                    };
                    var rslt = await this.userHelper.AddUserAsync(user, model.Password);
                    if(rslt != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty," A sua Conta não pode ser criada porfavor tente novamente.");

                        return this.View(model);
                    }

                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.UserName
                    };

                    var rsltad = await this.userHelper.LoginAsync(loginViewModel);

                    if (rsltad.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError(string.Empty, " não foi possivel entrar na sua conta.");
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty,"este Cliente já existe.");
               
            }
       
            return this.View(model);

        }

        public async Task<IActionResult> ChangeUser()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();

            if(user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.UserName = user.UserName;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult>ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if(user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    var rspns = await this.userHelper.UpdateUserAsync(user);
                    if (rspns.Succeeded)
                    {
                        this.ViewBag.UserMessage = "Alterações efectuadas com sucesso";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, rspns.Errors.FirstOrDefault().Description);
                    }
                    
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Não foi possivel encontrar o Cliente");
                }

            }
            return this.View(model);
        }


        public async Task<IActionResult> ChangePassword()
        {
            
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                   var rslt = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (rslt.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, rslt.Errors.FirstOrDefault().Description);
                    }

                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Não foi possivel encontrar o Cliente");
                }

            }
            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult>CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);
                if(user != null)
                {
                    var rslt = await this.userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);
                    if (rslt.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            this.configuration["Token:Issuer"],
                            this.configuration["Tokens:Audience"],
                            claims,
                            expires:DateTime.UtcNow.AddDays(30),
                            signingCredentials: credentials);

                        var rsltd = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, rsltd);

                    }
                }
            }

            return this.BadRequest();

        }



    }
}
