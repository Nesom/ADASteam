using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADASProject.Products;
using ADASProject.Order;
using ADASProject.Comments;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ADASProject
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ProductInfo> Products { get; set; }
        public DbSet<SmartphoneDescription> SmartphoneDescriptions { get; set; }

        public DbSet<OrderInfo> Orders { get; set; }
        public DbSet<SubOrder> SubOrders { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

        public static readonly ILoggerFactory loggerFactory =
            new LoggerFactory(new[]
            {
                new ConsoleLoggerProvider((_, __) => true, true)
            });

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentLike>()
                .HasKey(c => new { c.UserId, c.CommentId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer("Server=tcp:adas-server.database.windows.net," +
                "1433;Initial Catalog=ADAS-DB;Persist Security Info=False;" +
                "User ID=ArtemS00@adas-server;Password=3k@zYghJ;" +
                "MultipleActiveResultSets=False;Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
