using ADASProject.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ADASProject.Controllers
{
    public static class ControllerHelper1
    {
        public static byte[] ConvertFileToBytes(IFormFile image)
        {
            var bytes = new List<byte>();
            var tempBytes = new byte[1024];
            using (var stream = image.OpenReadStream())
            {
                while (true)
                {
                    int count = stream.Read(tempBytes, 0, 1024);
                    bytes.AddRange(tempBytes.Take(count));
                    if (count != 1024)
                        break;
                }
            }
            return bytes.ToArray();
        }

        public static Dictionary<string, List<ProductInfo>> 
            GetProductsByCategory(IEnumerable<ProductInfo> products)
        {
            var dict = new Dictionary<string, List<ProductInfo>>();
            var types = ReflectionHelper.GetAllProductClasses();
            var typeToNameDict = ReflectionHelper
                .CreateNameToTypeDict(types, typeof(Attributes.ClassName))
                .ReverseKeysAndValues();
            foreach (var product in products)
            {
                var tableName = product.TableName
                    .Remove(product.TableName.Length - "Descriptions".Length);
                var name = typeToNameDict[tableName];
                if (!dict.ContainsKey(name))
                    dict.Add(name, new List<ProductInfo>());
                dict[name].Add(product);
            }
            return dict;
        }

        public static IQueryable<T> FilterValues<T>(IQueryable<T> values, Type type, string propertyName, object fromValue, object toValue)
        {
            if (fromValue == null && toValue == null || type == null)
                return values;
            // Property that need to filter
            var property = type.GetProperty(propertyName);
            if (property == null)
                return values;
            // Filter all values that < fromValue
            if (fromValue != null)
                values = values.Where(pr => ((IComparable)property.GetValue(pr)).CompareTo(fromValue) >= 0);
            // Filter all values that > toValue
            if (toValue != null)
                values = values.Where(pr => ((IComparable)property.GetValue(pr)).CompareTo(toValue) <= 0);

            return values;
        }

        public static IQueryable<ProductInfo> OrderValuesByParameter(IQueryable<ProductInfo> values, string parameter, bool byDescenting)
        {
            switch (parameter)
            {
                case "Price":
                    return OrderValues(values, x => x.Price, byDescenting);
                case "Rating":
                    return OrderValues(values, x => x.Rating, byDescenting);
                default:
                    return values;
            }
        }

        public static IQueryable<ProductInfo> OrderValues<TKey>
            (IQueryable<ProductInfo> values, Expression<Func<ProductInfo, TKey>> filter, bool byDestenition)
        {
            return byDestenition ? 
                values.OrderByDescending(filter) : values.OrderBy(filter);
        }
    }
}
