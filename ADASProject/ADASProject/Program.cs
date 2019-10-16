using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ADASProject.Products;
using System.Threading;
using System.Diagnostics;

namespace ADASProject
{
    public class Program
    {
        public static async Task<int> Do()
        {
            var time = await Do1();
            Thread.Sleep(1000);
            Thread.Sleep(time * 1000);
            return 1;
        }

        public static async Task<int> Do1()
        {
            Thread.Sleep(1000);
            return 1;
        }


        public static void Main(string[] args)
        {
            var watcher = new Stopwatch();
            watcher.Start();
            Do();
            watcher.Stop();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
