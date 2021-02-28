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
            var join = (from u in _context.Users
                    join t in _context.Tickets
                        on u.Id equals t.ExecutorId
                    select new {u, t})
                    .GroupBy(x => x.u)
                    .Select(x => new UserDto
                    {
                        Id = x.Key.Id,
                        Email = x.Key.Email,
                        RequestsToExecute = x.Select(t => new TicketDto
                        {
                            Id = t.t.Id,
                            ExecutorId = t.t.ExecutorId,
                            AuthorId = t.t.AuthorId
                        })
                    });

            var groupJoin = _context.Users
                .GroupJoin(
                    _context.Tickets,
                    u => u.Id,
                    t => t.ExecutorId,
                    (u, t) => new { User = u, Tickets = t })
                .Select(x => new UserDto
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    RequestsToExecute = x.Tickets.Select(t => new TicketDto
                    {
                        Id = t.Id,
                        ExecutorId = t.ExecutorId,
                        AuthorId = t.AuthorId
                    })
                });

            return Ok(
                await new QueryResponse<UserDto[]>(_context)
                    .ExecuteAsync(async c => await join
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