using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace EfQueryDemo.Infrastructure.Logging
{
    public class AppLogger
    {
        private readonly IConfiguration _config;

        private readonly LoggerConfiguration _loggerConfig;

        private Logger _logger;

        public AppLogger(IConfiguration configuration)
        {
            // https://github.com/serilog/serilog-settings-configuration
            // https://www.vivienfabing.com/aspnetcore/2019/02/21/how-to-add-logging-on-azure-with-aspnetcore-and-serilog.html
            _config = configuration;
            _loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(_config)
                .WriteTo.Debug();
        }

        public Logger Build()
        {
            _logger ??= _loggerConfig.CreateLogger();

            return _logger;
        }
    }
}