using System;
using System.Collections.Generic;
using System.Linq;
using ADASProject.Order;
using ADASProject.Products;

namespace ADASProject
{
    public static class ApplicationContextExtentions
    {
        public static Product<IDescription> GetById(this ApplicationContext context, int id)
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
                .Invoke(propertyValue, new object[1] { new object[1] { productInfo.SubId } });

            var description = (IDescription)value;

            var product = ProductExtentions.GetProduct(description, productInfo);
            product.Description = description;
            product.ProductInfo = productInfo;
            return product;
        }

        public static ProductInfo GetProductInfo(this ApplicationContext context, int id)
        {
            return context.Products.Find(id);
        }

        public static int SaveAndGetId(this ApplicationContext context, Address address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();
            return context.Addresses.Last().Id;
        }

        public static int SaveAndGetId(this ApplicationContext context, OrderInfo orderInfo)
        {
            context.Orders.Add(orderInfo);
            context.SaveChanges();
            return context.Orders.Last().Id;
        }

        public static bool TryToChangeCountOfProducts(this ApplicationContext context, int id, int count)
        {
            var product = context.Products.Find(id);
            if (product != null && product.Count < count)
                return false;
            product.Count -= count;
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
            // Find and invoke method "Add" in property type
            type.GetMethod("Add", new Type[1] { productType })
                .Invoke(propertyValue, new object[1] { product.Description });
            context.SaveChanges();

            var enumerable = (IEnumerable<object>)propertyValue;
            var elementId = ((IDescription)enumerable.Last()).Id;


            product.ProductInfo.SubId = elementId;
            product.ProductInfo.TableName = name;
            product.ProductInfo.AddDate = DateTime.Now;
            context.Products.Add(product.ProductInfo);
            context.SaveChanges();

            var id = context.Products.Last().Id;
            ((IDescription)enumerable.Last()).MainTableId = id;
            context.SaveChanges();
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
    }
}
