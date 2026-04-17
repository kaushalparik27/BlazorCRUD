using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace BlazorCRUD
{
    public class Program
    {
        private const string OutputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u11}] {SourceContext} | {Message:lj}{NewLine}{Exception}";

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: OutputTemplate)
                .WriteTo.Debug(outputTemplate: OutputTemplate)
                .WriteTo.File(
                    path: Path.Combine("Logs", "blazorcrud-.log"),
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 30,
                    outputTemplate: OutputTemplate)
                .CreateLogger();

            try
            {
                Log.Information("Application starting...");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Application shut down cleanly.");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}