using System.Linq;

namespace EfQueryDemo.Infrastructure.Database
{
    public class SqlCommand
    {
        private readonly string _source;

        public SqlCommand(string source)
        {
            _source = source;
        }

        public override string ToString()
        {
            return string.Join(' ', _source.Split("\r\n").Skip(2));
        }
    }
}