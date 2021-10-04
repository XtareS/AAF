using AAF.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Textei>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

             modelBuilder.Entity<Craft>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ringer>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");


            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach(var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

        
            base.OnModelCreating(modelBuilder);
        }
        


    }
}
