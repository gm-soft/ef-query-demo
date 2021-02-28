using System.Collections.Generic;
using System.Threading.Tasks;
using EfQueryDemo.Infrastructure.Database;
using EfQueryDemo.Models;
using EfQueryDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfQueryDemo.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DataController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("tickets")]
        public async Task<IReadOnlyCollection<Ticket>> TicketsAsync()
        {
            return await _context.Tickets.AsNoTracking().ToArrayAsync();
        }

        [HttpGet("tickets-count")]
        public async Task<IActionResult> TicketsCountAsync()
        {
            return Ok(
                await new QueryResponse<int>(_context)
                    .ExecuteAsync(c => c.Tickets.CountAsync()));
        }

        [HttpGet("users")]
        public async Task<IActionResult> UsersAsync()
        {
            return Ok(
                await new QueryResponse<User[]>(_context)
                    .ExecuteAsync(c => c.Users.AsNoTracking().ToArrayAsync()));
        }
    }
}