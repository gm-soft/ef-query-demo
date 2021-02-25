using System;
using System.Threading.Tasks;
using EfQueryDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EfQueryDemo.Infrastructure.Database
{
    public class DatabaseInitializator
    {
        private readonly IHostEnvironment _env;
        private readonly DatabaseContext _context;

        public DatabaseInitializator(IHostEnvironment env, DatabaseContext context)
        {
            _env = env;
            _context = context;
        }

        public DatabaseInitializator Migrate()
        {
            try
            {
                _context.Database.MigrateAsync()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();

                return this;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Cannot migrate database", exception);
            }
        }

        public DatabaseInitializator SeedOrIgnore()
        {
            if (_env.IsDevelopment())
            {
                ExecuteAsync()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }

            return this;
        }

        private async Task ExecuteAsync()
        {
            if (await _context.Tickets.AnyAsync())
            {
                return;
            }

            const int usersCount = 1000;
            const int ticketsForUserMin = 10;
            const int ticketsForUserMax = 100;

            for (int i = 0; i < 1000; i++)
            {
                var email = Faker.Internet.Email($"{Faker.Name.First()}.{Faker.Name.Last()}");
                await _context.Users.AddAsync(new User(email));
            }

            await _context.SaveChangesAsync();

            var usersList = await _context.Users.ToListAsync();

            foreach (User user in usersList)
            {
                var count = RandomIndex(ticketsForUserMin, ticketsForUserMax);
                for (int i = 0; i < count; i++)
                {
                    await _context.Tickets.AddAsync(
                        new Ticket(
                            user,
                            usersList[RandomIndex(0, usersCount - 1)]));
                }
            }

            await _context.SaveChangesAsync();
        }

        private int RandomIndex(int min, int max)
        {
            return new Random((int)DateTimeOffset.Now.Ticks + min + max).Next(min, max);
        }

        
    }
}