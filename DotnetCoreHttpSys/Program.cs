using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotnetCoreHttpSys
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
                    webBuilder.UseHttpSys(options =>
                    {
                        options.AllowSynchronousIO = true;
                        options.Authentication.AllowAnonymous = true;
                        options.Authentication.Schemes = AuthenticationSchemes.None;
                        options.EnableResponseCaching = true;
                        options.MaxAccepts = 10;
                        options.MaxConnections = 100;
                        options.MaxRequestBodySize = 1024 * 1024 * 2;
                        options.RequestQueueLimit = 1500;
                        options.ThrowWriteExceptions = true;
                        //options.UrlPrefixes.Add("http://localhost:5005");
                    })
                    .UseStartup<Startup>();
                });
    }
}
