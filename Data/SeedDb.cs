using AAF.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AAF.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private readonly UserManager<User> usermanager;
        private Random random;

        public SeedDb(DataContext context, UserManager<User> usermanager)
        {
            this.context = context;
            this.usermanager = usermanager;
            this.random = new Random();
        }
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.usermanager.FindByEmailAsync("irma.mendonca.sr@gmail.pt");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Sara",
                    LastName = "Roque",
                    Email = "irma.mendonca.sr@gmail.pt",
                    UserName = "irma.mendonca.sr@gmail.pt",
                    PhoneNumber = "967315706"
                };

                var rslt = await this.usermanager.CreateAsync(user, "01122010");
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
