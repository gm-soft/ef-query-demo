using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfQueryDemo.Models
{
    public class User
    {
        protected User()
        {
        }

        public User(string email)
        {
            Email = email;
        }

        public long Id { get; protected set; }

        [Required]
        public string Email { get; protected set; }

        public virtual ICollection<UserRequest> RequestsToExecute { get; protected set; }
    }
}