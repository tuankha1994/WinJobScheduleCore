using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace WinJobScheduleCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            var pathToContentRoot = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            AppConstants.StorageRootPath = pathToContentRoot;
            ConfigDestinationLogs(AppConstants.StorageRootPath);
            var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());
            var host = builder.Build();

            var isDebug = false;
#if DEBUG
            isDebug = true;
#endif
            if (isDebug || (args != null && args.Contains("--console")))
                host.Run();
            else
                host.RunAsService();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
           .ConfigureLogging((hostingContext, logging) =>
           {
               //logging.AddEventLog();
           })
           .ConfigureAppConfiguration((context, config) =>
           {
               // Configure the app here.
               config.AddCommandLine(args);
           })
           .UseStartup<Startup>();

        public static void ConfigDestinationLogs(string path)
        {
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Information()
                  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                           .WriteTo.RollingFile(Path.Combine(path, @"./Logs/Info/Info-{Date}.log")))
                  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
                           .WriteTo.RollingFile(Path.Combine(path, @"./Logs/Debug/Debug-{Date}.log")))
                  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                           .WriteTo.RollingFile(Path.Combine(path, @"./Logs/Warning/Warning-{Date}.log")))
                  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                           .WriteTo.RollingFile(Path.Combine(path, @"./Logs/Error/Error-{Date}.log")))
                  .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
                           .WriteTo.RollingFile(Path.Combine(path, @"./Logs/Fatal/Fatal-{Date}.log")))
                  .CreateLogger();
        }
    }
}
