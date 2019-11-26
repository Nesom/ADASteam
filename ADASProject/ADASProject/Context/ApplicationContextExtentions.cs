using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADASProject.Comments;
using ADASProject.Order;
using ADASProject.Products;

namespace ADASProject
{
    public static class ApplicationContextExtentions
    {
        public static IQueryable<IDescription> GetDescriptions(this ApplicationContext context, string descriptionName)
        {
            // Get DbSet<"TableName"> property
            var property = ReflectionHelper.GetProperty(context, descriptionName);
            // Get DbSet<"TableName"> value
            return (IQueryable<IDescription>)property.GetValue(context);
        }

        public static async Task<Product<IDescription>> GetProduct(this ApplicationContext context, int id)
        {
            if (!await context.IsAvailable(id))
                return null;
            var productInfo = await context.Products.FindAsync(id);
            // Get DbSet<"TableName"> property
            var property = ReflectionHelper.GetProperty(context, productInfo.TableName);
            // Get property value
            var propertyValue = property.GetValue(context);
            // Get property Type
            var type = property.PropertyType;

            // Find and invoke method "Find" in property type
            var value = type
                .GetMethod("Find", new Type[1] { typeof(object[]) })
                .Invoke(propertyValue, new object[1] { new object[1] { productInfo.Id } });

            var description = (IDescription)value;

            var product = Product<IDescription>.GetProduct(description, productInfo);
            return product;
        }

        public static async Task<ProductInfo> GetProductInfo(this ApplicationContext context, int id)
        {
            return await context.Products.FindAsync(id);
        }

        public static List<SubOrder> GetSubOrders(this ApplicationContext context, int orderId)
        {
            return context.SubOrders
                .Where(or => or.OrderId == orderId)
                .ToList();
        }

        public static Comment[] GetComments(this ApplicationContext context, int id)
        {
            return context.Comments
                .Where(comment => comment.ProductId == id)
                .ToArray();
        }

        public static async Task<Comment> GetComment(this ApplicationContext context, int id)
        {
            return await context.Comments.FindAsync(id);
        }

        public static async Task<User> GetUser(this ApplicationContext context, int id)
        {
            return await context.Users.FindAsync(id);
        }

        public static async Task Save(this ApplicationContext context, Address address)
        {
            context.Addresses.Add(address);
            await context.SaveChangesAsync();
        }

        public static async Task<bool> TryToSaveOrder(this ApplicationContext context, OrderInfo order)
        {
            if (order.SubOrders == null || order.SubOrders.Count == 0)
                return false;
            // Try to change count of products
            if (!await context.TryToChangeCountOfProducts(order.SubOrders.Select(s => Tuple.Create(s.ProductId, s.Count))))
                return false;
            // Add order and get order id
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            var id = context.Orders.Last().Id;
            // Order sub orders (to add relations)
            order.SubOrders = order.SubOrders
                .OrderBy(s => s.ProductId)
                .ToList();
            // Add relations and save sub orders
            for(int i = 0; i < order.SubOrders.Count; i++)
            {
                var subOrder = order.SubOrders[i];
                subOrder.OrderId = id;
                context.SubOrders.Add(subOrder);
                for (int j = i + 1; j < order.SubOrders.Count; j++)
                {
                    var relation = await context.Relations.FindAsync(new object[] { subOrder.ProductId, order.SubOrders[j].ProductId });
                    if (relation == null)
                        context.Relations.Add(new Relation()
                        {
                            LesserId = subOrder.ProductId,
                            BiggerId = order.SubOrders[j].ProductId,
                            Count = 1
                        });
                    else
                        relation.Count += 1;
                }
            }
            await context.SaveChangesAsync();
            return true;
        }

        public static async Task AddProduct(this ApplicationContext context, Product<IDescription> product)
        {
            var name = product.Description.GetType().Name + 's';

            // Get DbSet<"TableName"> property
            var property = ReflectionHelper.GetProperty(context, name);
            // Get property value
            var propertyValue = property.GetValue(context);
            // Get property type
            var type = property.PropertyType;

            var productType = product.Description.GetType();

            product.ProductInfo.AddDate = DateTime.Now;
            product.ProductInfo.TableName = name;

            context.Products.Add(product.ProductInfo);
            await context.SaveChangesAsync();

            var id = context.Products.Last().Id;
            product.Description.Id = id;

            // Find and invoke method "Add" in property type
            type.GetMethod("Add", new Type[1] { productType })
                .Invoke(propertyValue, new object[1] { product.Description });
            await context.SaveChangesAsync();
        }

        public static async Task AddComment(this ApplicationContext context, int userId, int productId, string text)
        {
            var comment = new Comment()
            {
                ProductId = productId,
                Rating = 0,
                Text = text,
                UserId = userId
            };
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }

        public static async Task LikeComment(this ApplicationContext context, int userId, int commentId)
        {
            if (!await context.CanLike(userId, commentId))
                return;
            var like = await context.CommentLikes.FindAsync(userId, commentId);
            if (like == null)
            {
                context.CommentLikes.Add(new CommentLike()
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
            var comment = await context.GetComment(commentId);
            comment.Rating += 1;
            await context.SaveChangesAsync();
        }

        public static async Task UnlikeComment(this ApplicationContext context, int userId, int commentId)
        {
            if (await context.CanLike(userId, commentId))
                return;
            var like = await context.CommentLikes.FindAsync(userId, commentId);
            if (like == null)
            {
                return;
            }
            like.IsLiked = false;
            var comment = await context.GetComment(commentId);
            comment.Rating -= 1;
            await context.SaveChangesAsync();
        }

        public static async Task RemoveComment(this ApplicationContext context, int id)
        {
            var comment = await context.Comments.FindAsync(id);
            if (comment != null)
                context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }

        public static async Task<bool> TryToChangeCountOfProducts(this ApplicationContext context, IEnumerable<Tuple<int, int>> idsToValues)
        {
            foreach (var value in idsToValues)
            {
                var product = context.Products.Find(value.Item1);
                if (product != null && product.Count < value.Item2)
                    return false;
                product.Count -= value.Item2;
                product.CountOfOrders += 1;
            }
            await context.SaveChangesAsync();
            return true;
        }

        public static async Task<bool> IsAvailable(this ApplicationContext context, int id)
        {
            var value = await context.Products.FindAsync(id);
            return value != null && value.Count > 0;
        }

        public static async Task<bool> HasQuantity(this ApplicationContext context, int id, int count)
        {
            var value = await context.Products.FindAsync(id);
            return value != null && value.Count >= count;
        }

        public static async Task<bool> CanLike(this ApplicationContext context, int userId, int commentId)
        {
            var like = await context.CommentLikes.FindAsync(new object[2] { userId, commentId });
            if (like == null)
            {
                return true;
            }
            return !like.IsLiked;
        }
    }
}
