namespace EfQueryDemo.Models
{
    public record TicketDto
    {
        public TicketDto()
        {
        }

        public TicketDto(Ticket ticket)
        {
            Id = ticket.Id;
            AuthorId = ticket.AuthorId;
            ExecutorId = ticket.ExecutorId;
        }

        public long Id { get; init; }

        public long AuthorId { get; init; }

        public long ExecutorId { get; init; }
    }
}