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
        public static void Main(string[] args)
        {
            //var products = new List<Product>()
            //{
            //    new Product(1, "Apple", "Just Apple"),
            //    new Product(2, "Apple 4", "Simple Apple 4"),
            //    new Product(3, "Apple 4 Max Pro", "Not simple apple 4"),
            //    new Product(4, "Tomato", "Just Tomato"),
            //    new Product(5, "Blue Tomato", "Not Red Tomato")
            //};

            //var t1 = Searcher.Search("Apple", products);
            //var t2 = Searcher.Search("Appel", products);
            //var t3 = Searcher.Search("Aplep", products);
            //var t4 = Searcher.Search("Appl 3", products);
            //var t5 = Searcher.Search("Appl 4", products);
            //var t6 = Searcher.Search("Applon 4 Pro", products);
            //var t7 = Searcher.Search("Apples 4 Max Pro", products);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
