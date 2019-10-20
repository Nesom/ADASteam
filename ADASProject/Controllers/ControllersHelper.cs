using ADASProject.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Controllers
{
    public static class ControllerHelper
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
    }
}
