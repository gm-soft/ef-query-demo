using System;

namespace EfQueryDemo.Infrastructure.Middlewares.Error
{
    public record ExceptionDto
    {
        public string Message { get; }

        public string StackTrace { get; }

        public ExceptionDto InnerException { get; }

        public ExceptionDto(Exception ex)
        {
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            InnerException = ex.InnerException != null ? new ExceptionDto(ex.InnerException) : null;
        }
    }
}