using System;
using System.Threading.Tasks;
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
            if (_env.IsDevelopment() || _env.IsEnvironment("Demo"))
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
            await AddUsersAsync();
        }
    }
}