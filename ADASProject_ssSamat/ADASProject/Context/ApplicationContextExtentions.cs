using System;
using System.Collections.Generic;
using System.Linq;
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

        public static Product<IDescription> GetProduct(this ApplicationContext context, int id)
        {
            if (!context.IsAvailable(id))
                return null;
            var productInfo = context.Products.Find(id);
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

        public static ProductInfo GetProductInfo(this ApplicationContext context, int id)
        {
            return context.Products.Find(id);
        }

        public static SubOrder[] GetSubOrders(this ApplicationContext context, int orderId)
        {
            return context.SubOrders
                .Where(or => or.OrderId == orderId)
                .ToArray();
        }

        public static Comment[] GetComments(this ApplicationContext context, int id)
        {
            return context.Comments
                .Where(comment => comment.ProductId == id)
                .ToArray();
        }

        public static Comment GetComment(this ApplicationContext context, int id)
        {
            return context.Comments.Find(id);
        }

        public static User GetUser(this ApplicationContext context, int id)
        {
            return context.Users.Find(id);
        }

        public static void Save(this ApplicationContext context, Address address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();
        }

        public static bool TryToSaveOrder(this ApplicationContext context, OrderInfo order)
        {
            if (order.SubOrders == null || order.SubOrders.Count == 0)
                return false;
            context.Orders.Add(order);
            context.SaveChanges();
            var id = context.Orders.Last().Id;
            foreach(var subOrder in order.SubOrders)
            {
                if (!context.TryToChangeCountOfProducts(subOrder.ProductId, subOrder.Count))
                {
                    context.SaveChanges();
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    return false;
                }
                subOrder.OrderId = id;
                context.SubOrders.Add(subOrder);
            }
            context.SaveChanges();
            return true;
        }

        public static void AddProduct(this ApplicationContext context, Product<IDescription> product)
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
            context.SaveChanges();

            var id = context.Products.Last().Id;
            product.Description.Id = id;

            // Find and invoke method "Add" in property type
            type.GetMethod("Add", new Type[1] { productType })
                .Invoke(propertyValue, new object[1] { product.Description });
            context.SaveChanges();
        }

        public static void AddComment(this ApplicationContext context, int userId, int productId, string text)
        {
            var comment = new Comment()
            {
                ProductId = productId,
                Rating = 0,
                Text = text,
                UserId = userId
            };
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public static void LikeComment(this ApplicationContext context, int userId, int commentId)
        {
            if (!context.CanLike(userId, commentId))
                return;
            var like = context.CommentLikes.Find(userId, commentId);
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
            var comment = context.GetComment(commentId);
            comment.Rating += 1;
            context.SaveChanges();
        }

        public static void UnlikeComment(this ApplicationContext context, int userId, int commentId)
        {
            if (context.CanLike(userId, commentId))
                return;
            var like = context.CommentLikes.Find(userId, commentId);
            if (like == null)
            {
                return;
            }
            like.IsLiked = false;
            var comment = context.GetComment(commentId);
            comment.Rating -= 1;
            context.SaveChanges();
        }

        public static void RemoveComment(this ApplicationContext context, int id)
        {
            var comment = context.Comments.Find(id);
            if (comment != null)
                context.Comments.Remove(comment);
            context.SaveChanges();
        }

        public static bool TryToChangeCountOfProducts(this ApplicationContext context, int id, int count)
        {
            var product = context.Products.Find(id);
            if (product != null && product.Count < count)
                return false;
            product.Count -= count;
            product.CountOfOrders += 1;
            return true;
        }

        public static bool IsAvailable(this ApplicationContext context, int id)
        {
            var value = context.Products.Find(id);
            return value != null && value.Count > 0;
        }

        public static bool HasQuantity(this ApplicationContext context, int id, int count)
        {
            var value = context.Products.Find(id);
            return value != null && value.Count >= count;
        }

        public static bool CanLike(this ApplicationContext context, int userId, int commentId)
        {
            var like = context.CommentLikes.Find(new object[2] { userId, commentId });
            if (like == null)
            {
                return true;
            }
            return !like.IsLiked;
        }
    }
}
