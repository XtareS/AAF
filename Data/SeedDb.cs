using AAF.Data.Entities;
using AAF.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data
{
    public class SeedDb
    {
        private readonly DataContext context;

        private readonly IUserHelper userHelper;
      
        private readonly Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            var user = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
            var userex = await this.userHelper.GetUserByEmailAsync("xtare16.soares@gmail.com");
            if (user == null && userex==null)
            {
                user = new User
                {
                    FirstName = "Sara",
                    LastName = "Roque",
                    Email = "irma.mendonca.sr@gmail.com",
                    UserName = "SaraRoque",
                    PhoneNumber = "967315706"
                };
                userex = new User
                {
                    FirstName = "Tiago",
                    LastName = "Soares",
                    Email = "xtare16.soares@gmail.com",
                    UserName = "XtareS",
                    PhoneNumber = "967587958"
                };


                var rslt = await this.userHelper.AddUserAsync(user, "01122010");
                if (rslt != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Ops! ocorreu um problema com a conta no Seed");
                }

                await this.userHelper.AddUserToRoleAsync(user, userex, "Admin");

            }

            var isRole = await this.userHelper.IsUserInRoleAsync(user,userex, "Admin");

            if (!isRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, userex, "Admin");
            }


            if (!this.context.Texteis.Any())
            {
                this.AddTextei("Amendoin fofo", user);
                await this.context.SaveChangesAsync();
            }


        }

        private void AddTextei(string text, User user)
        {
            this.context.Texteis.Add(new Textei
            {
                Name = text,
                Price = this.random.Next(50),
                Disponivel = true,
                Stock = this.random.Next(10),
                User = user
            });
        }
    }
}
