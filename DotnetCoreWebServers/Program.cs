using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotnetCoreWebServers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureKestrel((context, options)=>
                    {
                        options.Limits.MaxConcurrentConnections = 50;
                        options.Limits.MaxConcurrentUpgradedConnections = 50;
                        options.Limits.MaxRequestBodySize = 1024 * 1024 * 2;
                        options.Limits.MinRequestBodyDataRate = new MinDataRate(100, new TimeSpan(0, 0, 10));
                        options.Limits.MinResponseDataRate = new MinDataRate(100, new TimeSpan(0, 0, 10));
                        options.Listen(IPAddress.Loopback, 5000);
                        options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                        {
                            //listenOptions.UseHttps("cert.pfx", "password");
                            listenOptions.UseHttps();
                        });
                        options.Limits.KeepAliveTimeout = new TimeSpan(0, 5, 0);
                        options.Limits.RequestHeadersTimeout = new TimeSpan(0, 2, 0);
                    });
                });
    }
}
