using System;

namespace EfQueryDemo.Infrastructure.Middlewares.Error
{
    public record ErrorDetails
    {
        private const string DefaultServerErrorMessage = "Internal Server Error";

        public ErrorDetails(int status, string message = null)
        {
            Status = status;
            Message = message ?? DefaultServerErrorMessage;
        }

        public int Status { get; }

        public string Message { get; }

        public string RequestId => Guid.NewGuid().ToString();
    }
}