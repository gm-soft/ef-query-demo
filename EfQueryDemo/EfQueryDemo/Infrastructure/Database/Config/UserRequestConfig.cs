using EfQueryDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfQueryDemo.Infrastructure.Database.Config
{
    public class UserRequestConfig : IEntityTypeConfiguration<UserRequest>
    {
        public void Configure(EntityTypeBuilder<UserRequest> builder)
        {
            builder.ToTable("UserRequests");

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