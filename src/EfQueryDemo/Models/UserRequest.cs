namespace EfQueryDemo.Models
{
    public class UserRequest
    {
        protected UserRequest()
        {
        }

        public UserRequest(User author, User executor)
        {
            AuthorId = author.Id;
            ExecutorId = executor.Id;
        }

        public long Id { get; protected set; }

        public long AuthorId { get; protected set; }

        public virtual User Author { get; protected set; }

        public long ExecutorId { get; protected set; }

        public virtual User Executor { get; protected set; }
    }
}