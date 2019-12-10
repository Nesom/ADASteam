using ADASProject.Comments;
using ADASProject.DatabaseContext;
using ADASProject.Order;
using ADASProject.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject
{
    public class ApplicationContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ProductInfo> Products { get; set; }
        public DbSet<SmartphoneDescription> SmartphoneDescriptions { get; set; }
        public DbSet<TestProductDescription> TestProductDescriptions { get; set; }
        public DbSet<TVDescription> TVDescriptions { get; set; }
        public DbSet<LaptopDescription> LaptopDescriptions { get; set; }

        public DbSet<ProductVote> ProductVotes { get; set; }

        public DbSet<OrderInfo> Orders { get; set; }
        public DbSet<SubOrder> SubOrders { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

        public DbSet<Relation> Relations { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentLike>()
                .HasKey(c => new { c.UserId, c.CommentId });
            modelBuilder.Entity<Relation>()
                .HasKey(r => new { r.LesserId, r.BiggerId });
            modelBuilder.Entity<ProductVote>()
                .HasKey(pr => new { pr.ProductId, pr.UserId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:adas-server.database.windows.net," +
                "1433;Initial Catalog=ADAS-DB;Persist Security Info=False;" +
                "User ID=ArtemS00@adas-server;Password=3k@zYghJ;" +
                "MultipleActiveResultSets=False;Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;");
        }

        #region productWorker
        public async Task<bool> TryToAddProductAsync(Product<IDescription> product)
        {
            try
            {
                var name = product.Description.GetType().Name + 's';

                // Get DbSet<"TableName"> property
                var property = ReflectionHelper.GetProperty(this, name);
                // Get property value
                var propertyValue = property.GetValue(this);
                // Get property type
                var type = property.PropertyType;

                var productType = product.Description.GetType();

                product.ProductInfo.AddDate = DateTime.Now;
                product.ProductInfo.TableName = name;

                await Products.AddAsync(product.ProductInfo);
                await SaveChangesAsync();

                var id = Products.Last().Id;
                product.Description.Id = id;

                // Find and invoke method "Add" in property type
                type.GetMethod("Add", new Type[1] { productType })
                    .Invoke(propertyValue, new object[1] { product.Description });
                await SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> TryToRemoveProductAsync(int id)
        {
            var product = await Products.FindAsync(id);
            if (product == null)
                return false;
            Products.Remove(product);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsAvailable(int id)
        {
            var value = await Products.FindAsync(id);
            return value != null && value.Count > 0;
        }

        public async Task<bool> HasQuantity(int id, int count)
        {
            var value = await Products.FindAsync(id);
            return value != null && value.Count >= count;
        }

        public async Task<Product<IDescription>> GetProductAsync(int id)
        {
            // Get product info
            var productInfo = await GetProductInfoAsync(id);
            // Get description
            var description = await GetDescriptionAsync(productInfo.Id);

            // Chech for null values
            if (productInfo == null || description == null)
                return null;

            return Product<IDescription>.GetProduct(description, productInfo);
        }

        public async Task<ProductInfo> GetProductInfoAsync(int id)
        {
            return await Products.FindAsync(id);
        }

        public async Task<IDescription> GetDescriptionAsync(int id)
        {
            try
            {
                var product = await Products.FindAsync(id);
                // Get DbSet<"TableName"> property
                var property = ReflectionHelper.GetProperty(this, product.TableName);
                // Get property value
                var propertyValue = property.GetValue(this);
                // Get property Type
                var type = property.PropertyType;

                // Find and invoke method "Find" in property type
                var value = type
                    .GetMethod("Find", new Type[1] { typeof(object[]) })
                    .Invoke(propertyValue, new object[1] { new object[1] { id } });

                return (IDescription)value;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<IDescription>> GetDescriptionsAsync(string descriptionName)
        {
            try
            {
                // Get DbSet<"TableName"> property
                var property = ReflectionHelper.GetProperty(this, descriptionName);
                // Get property value
                var propertyValue = property.GetValue(this);
                return (IQueryable<IDescription>)propertyValue;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<ProductInfo>> GetProductInfosAsync()
        {
            return (IQueryable<ProductInfo>)Products;
        }
        #endregion
        #region userWorker
        public async Task<bool> TryToAddUserAsync(User user)
        {
            if (Users.FirstOrDefault(u => u.Email == user.Email) != null)
                return false;

            await Users.AddAsync(user);
            await SaveChangesAsync();

            return true;
        }

        public async Task RemoveUserAsync(int id)
        {
            var user = await Users.FindAsync(id);
            if (user == null)
                return;
            Users.Remove(user);
            await SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await Users.FindAsync(id);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return Users.FirstOrDefault(u => u.Email == email);
        }
        #endregion
        #region commentWorker
        public async Task AddCommentAsync(Comment comment)
        {
            await Comments.AddAsync(comment);
            await SaveChangesAsync();
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            var comment = await Comments.FindAsync(commentId);
            if (comment == null)
                return;
            Comments.Remove(comment);
            await SaveChangesAsync();
        }

        public async Task LikeCommentAsync(int userId, int commentId)
        {
            if (!await CanLikeAsync(userId, commentId))
                return;
            var like = await CommentLikes.FindAsync(userId, commentId);
            if (like == null)
            {
                CommentLikes.Add(new CommentLike()
                {
                    CommentId = commentId,
                    UserId = userId,
                    IsLiked = true
                });
            }
            else
            {
                like.IsLiked = true;
            }
            var comment = await Comments.FindAsync(commentId);
            comment.Rating += 1;
            await SaveChangesAsync();
        }

        public async Task UnlikeCommentAsync(int userId, int commentId)
        {
            if (await CanLikeAsync(userId, commentId))
                return;
            var like = await CommentLikes.FindAsync(userId, commentId);
            if (like == null)
            {
                return;
            }
            like.IsLiked = false;
            var comment = await Comments.FindAsync(commentId);
            comment.Rating -= 1;
            await SaveChangesAsync();
        }

        public async Task<bool> CanLikeAsync(int userId, int commentId)
        {
            var like = await CommentLikes.FindAsync(new object[2] { userId, commentId });
            if (like == null)
            {
                return true;
            }
            return !like.IsLiked;
        }

        public async Task<Comment> GetCommentAsync(int commentId)
        {
            return await Comments.FindAsync(commentId);
        }

        public async Task<Comment[]> GetCommentsAsync(int productId)
        {
            return Comments
                .Where(comment => comment.ProductId == productId)
                .ToArray();
        }
        #endregion
        #region relationWorker
        public async Task AddRelationAsync(Relation relation)
        {
            await Relations.AddAsync(relation);
            await SaveChangesAsync();
        }

        public async Task<Relation> GetRelationAsync(object[] keys)
        {
            return await Relations.FindAsync(keys);
        }

        public async Task<IQueryable<Relation>> GetRelationsAsync()
        {
            return Relations;
        }
        #endregion
        #region orderWorker
        public async Task ChangeStatusAsync(int orderId, Status status)
        {
            var order = await Orders.FindAsync(orderId);
            if (order == null)
                return;
            order.StatusInfo = status;
            await SaveChangesAsync();
        }

        public async Task<bool> TryToSaveOrderAsync(OrderInfo order)
        {
            if (order.SubOrders == null || order.SubOrders.Count == 0)
                return false;
            // Try to change count of products
            if (!await TryToChangeCountOfProductsAsync(order.SubOrders.Select(s => Tuple.Create(s.ProductId, s.Count))))
                return false;
            // Add order and get order id
            await Orders.AddAsync(order);
            await SaveChangesAsync();
            var id = Orders.Last().Id;
            // Order sub orders (to add relations)
            order.SubOrders = order.SubOrders
                .OrderBy(s => s.ProductId)
                .ToList();
            // Add relations and save sub orders
            for (int i = 0; i < order.SubOrders.Count; i++)
            {
                var subOrder = order.SubOrders[i];
                subOrder.OrderId = id;
                SubOrders.Add(subOrder);
                for (int j = i + 1; j < order.SubOrders.Count; j++)
                {
                    var relation = await GetRelationAsync(new object[] { subOrder.ProductId, order.SubOrders[j].ProductId });
                    if (relation == null)
                        await AddRelationAsync(new Relation()
                        {
                            LesserId = subOrder.ProductId,
                            BiggerId = order.SubOrders[j].ProductId,
                            Count = 1
                        });
                    else
                        relation.Count += 1;
                }
            }
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryToChangeCountOfProductsAsync(IEnumerable<Tuple<int, int>> idsToValues)
        {
            foreach (var value in idsToValues)
            {
                var product = await GetProductInfoAsync(value.Item1);
                if (product != null && product.Count < value.Item2)
                    return false;
                product.Count -= value.Item2;
                product.CountOfOrders += 1;
            }
            await SaveChangesAsync();
            return true;
        }

        public async Task<OrderInfo> GetOrderAsync(int orderId)
        {
            return await Orders.FindAsync(orderId);
        }

        public async Task<IQueryable<OrderInfo>> GetOrdersAsync(int userId)
        {
            return Orders
                .Where(or => or.UserId == userId);
        }

        public async Task<IQueryable<OrderInfo>> GetOrdersAsync()
        {
            return Orders;
        }

        public async Task<List<SubOrder>> GetSubOrdersAsync(int orderId)
        {
            return SubOrders
                .Where(or => or.OrderId == orderId)
                .ToList();
        }
        #endregion
        #region productVoteWorker
        public async Task VoteAsync(int productId, int userId, int vote)
        {
            if (!await IsVotedAsync(productId, userId) && vote > 0 && vote <= 5)
            {
                ProductVotes.Add(new ProductVote() { ProductId = productId, UserId = userId, Vote = vote });
                var product = Products.Find(productId);
                var sum = product.Rating * product.CountOfVotes++;
                product.Rating = (sum + vote) / product.CountOfVotes;
                await SaveChangesAsync();
            }
        }

        public async Task<bool> IsVotedAsync(int productId, int userId)
        {
            return (await ProductVotes.FindAsync(new object[] { productId, userId })) == null;
        }

        public async Task<int> GetVoteAsync(int productId, int userId)
        {
            if (!await IsVotedAsync(productId, userId))
                return (await ProductVotes.FindAsync(new object[] { productId, userId })).Vote;
            return -1;
        }

        public async Task ChangeVoteAsync(int productId, int userId, int newVote)
        {
            if (newVote < 0 || newVote > 5)
                return;

            var vote = await ProductVotes.FindAsync(new object[] { productId, userId });
            if (vote == null)
            {
                ProductVotes.Add(new ProductVote() { ProductId = productId, UserId = userId, Vote = newVote });
                var product = Products.Find(productId);
                var sum = product.Rating * product.CountOfVotes++;
                product.Rating = (sum + newVote) / product.CountOfVotes;
                await SaveChangesAsync();
            }
            else
            {
                vote.Vote = newVote;
                await SaveChangesAsync();
            }
        }
        #endregion
    }
}

