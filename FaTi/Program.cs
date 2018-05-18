using System;
using System.IO;
using BVirtual.FaTi.Common.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BVirtual.FaTi
{
    class Program
    {
        public static AppConfig AppConfig;

        static int Main(string[] args)
        {
            // Set the config
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            // Set the logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            AppConfig = config.Get<AppConfig>();

            try
            {
                Log.Information("Starting host, please wait ...");

                var host = new WebHostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseUrls("http://*:" + (AppConfig.Port ?? 80))
                    .UseConfiguration(config)
                    .UseKestrel()
                    .UseStartup<Startup>()
                    .UseSerilog()
                    .Build();

                host.Run();

                return 0;
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}
