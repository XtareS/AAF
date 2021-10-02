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

            var user = await this.userHelper.GetUserByEmailAsync("irma.mendonca.sr@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Sara",
                    LastName = "Roque",
                    Email = "irma.mendonca.sr@gmail.com",
                    UserName = "irma.mendonca.sr@gmail.com",
                    PhoneNumber = "967315706"
                };

                var rslt = await this.userHelper.AddUserAsync(user, "01122010");
                if (rslt != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Ops! ocorreu um problema com a conta no Seed");
                }

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
