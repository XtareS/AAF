using AAF.Data.Entities;
using AAF.Helpers;
using AAF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Controllers
{
    public class AccountController:Controller
    {
        private readonly IUserHelper userHelper;

        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
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


    }
}
