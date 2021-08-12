using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting.WindowsServices;
using System.Diagnostics;

namespace ApiGateWay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = Process.GetCurrentProcess().MainModule.FileName;
            var dirName = Path.GetDirectoryName(path);
            Directory.SetCurrentDirectory(dirName);
            var contentRoot = Directory.GetCurrentDirectory();

            CreateWebHostBuilder(args, contentRoot).Build().Run();
        }
        public static IHostBuilder CreateWebHostBuilder(string[] args, string contentRoot) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(contentRoot);
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config
                            .SetBasePath(contentRoot)
                            .AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                            .AddJsonFile("ocelot.json", true, true)
                            .AddEnvironmentVariables();
                    });
                    webBuilder.ConfigureLogging((hostingContext, logging) =>
                    {
                        //TODO - Add logging
                        logging.AddConsole();
                    });
                    webBuilder.UseStartup<Startup>();
                })
                .UseWindowsService();
    }
}
