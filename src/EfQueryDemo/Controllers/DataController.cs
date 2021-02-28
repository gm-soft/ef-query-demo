using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("users/with-tickets/include/dto")]
        public async Task<IActionResult> UsersWithTicketsWhereExecutorDtoAsync([FromQuery] int take = 100)
        {
            return Ok(
                await new QueryResponse<UserDto[]>(_context)
                    .ExecuteAsync(async c => (await c.Users
                        .Include(x => x.RequestsToExecute)
                        .Take(take)
                        .AsNoTracking()
                        .ToArrayAsync())
                        .Select(x => new UserDto(x))
                        .ToArray()));
        }

        [HttpGet("users/with-tickets/join/dto")]
        public async Task<IActionResult> UsersWithTicketsWhereExecutorDtoJoinAsync([FromQuery] int take = 100)
        {
            var query = from u in _context.Users
                         join t in _context.Tickets
                    on u.Id equals t.ExecutorId into grouping
                // from ts in grouping.DefaultIfEmpty()
                select new UserDto
                         {
                             Id = u.Id,
                             Email = u.Email,
                             RequestsToExecute = grouping.Select(x => new TicketDto
                             {
                                 Id = x.Id,
                                 ExecutorId = x.ExecutorId,
                                 AuthorId = x.AuthorId
                             })
                         };

            return Ok(
                await new QueryResponse<UserDto[]>(_context)
                    .ExecuteAsync(async c => await query
                            .Take(take)
                            .AsNoTracking()
                            .ToArrayAsync()));
        }

        [HttpGet("users/with-tickets/include")]
        public async Task<IActionResult> UsersWithTicketsWhereExecutorAsync([FromQuery] int take = 100)
        {
            return Ok(
                await new QueryResponse<User[]>(_context)
                    .ExecuteAsync(c => c.Users
                        .Include(x => x.RequestsToExecute)
                        .Take(take)
                        .AsNoTracking()
                        .ToArrayAsync()));
        }
    }
}