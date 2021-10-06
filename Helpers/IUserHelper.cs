using AAF.Data.Entities;
using AAF.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Helpers
{
   public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogOutAsync();


        Task<IdentityResult> UpdateUserAsync(User user);



        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);


        Task<SignInResult> ValidatePasswordAsync(User user, string password);
        Task CheckRoleAsync(string rolename);
        Task AddUserToRoleAsync(User user, User userex, string rolename);
        Task<bool> IsUserInRoleAsync(User user, User userex, string rolename);
    }
}
