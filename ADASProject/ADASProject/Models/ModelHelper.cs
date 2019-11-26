using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public static class ModelHelper
    {
        public static readonly List<TypeInfo> ParametersToFilter;
        public static readonly Dictionary<string, List<TypeInfo>> SpecialParameters;

        static ModelHelper()
        {
            ParametersToFilter = ReflectionHelper
                .GetCharacteristicInfo(typeof(ProductInfo), new HashSet<string>() { "Price", "Rating" })
                .ToList();
            SpecialParameters = ReflectionHelper.GetCharacteristicInfo
                    (ReflectionHelper.GetAllDescriptionClasses(), new HashSet<Type>() { typeof(int), typeof(double) });
        }

        public static string GetCategoryName(string tableName)
        {
            return tableName.Remove(tableName.Length - "Descriptions".Length);
        }
    }
}
