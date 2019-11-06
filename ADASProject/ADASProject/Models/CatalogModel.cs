using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class CatalogModel
    {
        // Нужно удалить
        public static Dictionary<string, string> TypeToNameDict { get; }

        static CatalogModel()
        {
            TypeToNameDict = new Dictionary<string, string>();

            var types = ReflectionHelper.GetAllProductClasses();
            foreach(var type in types)
            {
                var attribute = ReflectionHelper.GetAttribute(type, typeof(Attributes.ClassName)) as Attributes.ClassName;
                if (attribute != null)
                    TypeToNameDict.Add(type.Name, attribute.Name);
            }
        }

        public CatalogModel() { }

        public void FillFields()
        {
            var parameters = ModelHelper.ParametersToFilter;
            Types = parameters
                .Select(par => par.Type)
                .ToArray();

            PropertyNames = parameters
                .Select(par => par.PropertyName)
                .ToArray();

            Descriptions = parameters
                .Select(par => par.Description)
                .ToArray();
        }

        public bool TryToFillCustomFields(string type)
        {
            if (type == null || !ModelHelper.SpecialParameters.ContainsKey(type))
                return false;
            var parameters = ModelHelper.SpecialParameters[type];

            CustomTypes = parameters
                .Select(par => par.Type)
                .ToArray();

            CustomPropertyNames = parameters
                .Select(par => par.PropertyName)
                .ToArray();

            CustomDescriptions = parameters
                .Select(par => par.Description)
                .ToArray();

            return true;
        }

        // Founded products
        public IEnumerable<ProductInfo> Products { get; set; }
        // Count of founded products
        public int Count { get; set; }
        // Category to Found
        public string CategoryName { get; set; }
        // Sorted by
        public string SortedBy { get; set; }
        public bool SortedByDescending { get; set; }
        // Standart values info
        public string[] FromValues { get; set; }
        public string[] ToValues { get; set; }
        public string[] PropertyNames { get; set; }
        public string[] Descriptions { get; set; }
        public Type[] Types { get; set; }
        // Custom values info
        public string[] CustomFromValues { get; set; }
        public string[] CustomToValues { get; set; }
        public string[] CustomPropertyNames { get; set; }
        public string[] CustomDescriptions { get; set; }
        public Type[] CustomTypes { get; set; }
    }
}
