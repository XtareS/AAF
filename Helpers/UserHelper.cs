using AAF.Data.Entities;
using AAF.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }



        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }


        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await this.userManager.UpdateAsync(user);
        }



        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }



        public async Task LogOutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await this.signInManager.CheckPasswordSignInAsync(
                user,
                password,
                false);
        }

        public async Task CheckRoleAsync(string rolename)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(rolename);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole 
                {
                    Name = rolename
                });
            }

        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, User userex, string rolename)
        {
            await this.userManager.AddToRoleAsync(user, rolename);
            await this.userManager.AddToRoleAsync(userex, rolename);
        }

        public async Task<bool> IsUserInRoleAsync(User user, User userex, string rolename)
        {
            return await this.userManager.IsInRoleAsync(user, "Admin");
      
        }
    }
}
