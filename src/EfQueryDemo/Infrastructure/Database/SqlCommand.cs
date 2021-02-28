using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EfQueryDemo.Infrastructure.Database
{
    public class SqlCommand
    {
        private static Regex _multispaces = new (@"[ ]{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private readonly string _source;

        public SqlCommand(string source)
        {
            _source = source;
        }

        public override string ToString()
        {
            var query = _multispaces.Replace(Command(), " ");
            return query.Replace(@"\", string.Empty);
        }

        private string Command()
        {
            IEnumerable<string> split = _source.Split("\r\n");

            if (split.Count() > 2)
            {
                split = split.Skip(2);
            }

            return string.Join(' ', split).Trim();

        }
    }
}