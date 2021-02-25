using EfQueryDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EfQueryDemo.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}