using AAF.Helpers;
using AAF.Models;
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

    }
}
