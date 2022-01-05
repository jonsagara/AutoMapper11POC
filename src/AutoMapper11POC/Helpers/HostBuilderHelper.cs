using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AutoMapper11POC.Helpers
{
    public static class HostBuilderHelper
    {
        public static IHost BuildHost(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);

            return new HostBuilder()
                .ConfigureHostConfiguration(cb => ConfigureHost(cb, args))
                .ConfigureAppConfiguration((ctx, cb) => ConfigureApp(ctx, cb, args))
                .ConfigureServices(ConfigureServices)
                .UseConsoleLifetime()
                .UseSerilog(ConfigureSerilog)
                .Build();
        }


        //
        // Private methods
        //

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // Configure AutoMapper.
            services.AddAutoMapper(new[] { typeof(Program) });
        }

        private static void ConfigureHost(IConfigurationBuilder configHost, string[] args)
        {
            configHost.SetBasePath(Directory.GetCurrentDirectory());
            configHost.AddJsonFile("hostsettings.json", optional: true);

            if (args is not null)
            {
                configHost.AddCommandLine(args);
            }
        }

        private static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder configApp, string[] args)
        {
            Console.WriteLine($"{nameof(HostBuilderHelper)} environment: {context.HostingEnvironment.EnvironmentName}");

            configApp
                .SetBasePath(context.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName.ToLower()}.json", optional: true);

            if (args is not null)
            {
                configApp.AddCommandLine(args);
            }

            configApp.AddEnvironmentVariables();
        }

        private static void ConfigureSerilog(HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfig)
        {
            // This is the .exe path in bin/{configuration}/{tfm}/
            var logDir = Directory.GetCurrentDirectory();

            // Log to the project directory.
            logDir = Path.Combine(logDir, @"..\..\..");
            Console.Out.WriteLine($"Logging directory: {logDir}");

            // Serilog is our application logger. Default to Verbose. If we need to control this dynamically at some point
            //   in the future, we can: https://nblumhardt.com/2014/10/dynamically-changing-the-serilog-level/

            var logFilePathFormat = Path.Combine(logDir, "Logs", "log.txt");

            // Always write to a rolling file.
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .WriteTo.Console()
                .WriteTo.File(logFilePathFormat, outputTemplate: "[{Level}] [{SourceContext:l}] {Message}{NewLine}{Exception}", rollingInterval: RollingInterval.Day);
        }
    }
}
