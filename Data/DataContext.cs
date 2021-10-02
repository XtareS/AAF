using AAF.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AAF.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Textei> Texteis { get; set; }

        public DbSet<Craft> Crafts { get; set; }

        public DbSet<Ringer> Ringers { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
