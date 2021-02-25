using System;
using System.Collections.Generic;
using EfQueryDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EfQueryDemo.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public List<string> Queries { get; } = new List<string>();

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(x => Queries.Add(new SqlCommand(x).ToString()), new List<EventId>
            {
                RelationalEventId.CommandExecuted
            });
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