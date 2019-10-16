using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADASProject.Products;
using ADASProject.Order;

namespace ADASProject
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProductInfo> Products { get; set; }
        public DbSet<OrderInfo> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<SmartphoneDescription> SmartphoneDescriptions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:adas-server.database.windows.net," +
                "1433;Initial Catalog=ADAS-DB;Persist Security Info=False;" +
                "User ID=ArtemS00@adas-server;Password=3k@zYghJ;" +
                "MultipleActiveResultSets=False;Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
