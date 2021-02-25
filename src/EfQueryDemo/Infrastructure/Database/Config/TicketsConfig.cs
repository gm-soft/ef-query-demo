using EfQueryDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfQueryDemo.Infrastructure.Database.Config
{
    public class TicketsConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder
                .HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId);

            builder
                .HasOne(x => x.Executor)
                .WithMany(x => x.RequestsToExecute)
                .HasForeignKey(x => x.ExecutorId);
        }
    }
}