using System.Collections.Generic;

namespace EfQueryDemo.Services
{
    public abstract class QResponse
    {
        protected QResponse()
        {
        }

        public long QueryElapsed { get; protected set; }

        public IReadOnlyCollection<string> Queries { get; protected set; }
    }
}