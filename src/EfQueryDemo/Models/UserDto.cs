using System;
using System.Collections.Generic;
using System.Linq;

namespace EfQueryDemo.Models
{
    public record UserDto
    {
        public UserDto()
        {
        }

        public UserDto(User user)
        {
            Id = user.Id;
            Email = user.Email;
            RequestsToExecute = user.RequestsToExecute?
                .Select(x => new TicketDto(x))
                .ToArray();
        }

        public long Id { get; init; }

        public string Email { get; init; }

        public IEnumerable<TicketDto> RequestsToExecute { get; init; }
    }
}