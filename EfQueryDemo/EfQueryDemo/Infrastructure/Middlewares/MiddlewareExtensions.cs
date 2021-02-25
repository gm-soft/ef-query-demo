using EfQueryDemo.Infrastructure.Middlewares.Error;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace EfQueryDemo.Infrastructure.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            return app.UseMiddleware<DebugExceptionHandlerMiddleware>();
        }

        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}