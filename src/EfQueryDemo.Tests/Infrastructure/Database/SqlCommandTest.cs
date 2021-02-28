using EfQueryDemo.Infrastructure.Database;
using Xunit;

namespace EfQueryDemo.Tests.Infrastructure.Database
{
    public class SqlCommandTest
    {
        [Fact]
        public void ToString_Purified()
        {
            var target = new SqlCommand("info: 2021-02-28 09:12:10.410 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) \r\n      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']\r\n      SELECT COUNT(*)::INT\r\n      FROM \\\"Tickets\\\" AS t");
            Assert.Equal("SELECT COUNT(*)::INT FROM \"Tickets\" AS t", target.ToString());
        }
    }
}
