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
            if (StandartValuesInfo == null)
                StandartValuesInfo = new CatalogValuesInfo();

            if (CustomValuesInfo == null)
                CustomValuesInfo = new CatalogValuesInfo();

            var parameters = ModelHelper.ParametersToFilter;
            StandartValuesInfo.Types = parameters
                .Select(par => par.Type)
                .ToArray();

            StandartValuesInfo.PropertyNames = parameters
                .Select(par => par.PropertyName)
                .ToArray();

            StandartValuesInfo.Descriptions = parameters
                .Select(par => par.Description)
                .ToArray();
        }

        public bool TryToFillCustomFields(string type)
        {
            if (type == null || !ModelHelper.SpecialParameters.ContainsKey(type))
                return false;

            var parameters = ModelHelper.SpecialParameters[type];

            CustomValuesInfo.Types = parameters
                .Select(par => par.Type)
                .ToArray();

            CustomValuesInfo.PropertyNames = parameters
                .Select(par => par.PropertyName)
                .ToArray();

            CustomValuesInfo.Descriptions = parameters
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
        public CatalogValuesInfo StandartValuesInfo { get; set; }
        // Custom values info
        public CatalogValuesInfo CustomValuesInfo { get; set; }
    }

    public class CatalogValuesInfo
    {
        public string[] FromValues { get; set; }
        public string[] ToValues { get; set; }
        public string[] PropertyNames { get; set; }
        public string[] Descriptions { get; set; }
        public Type[] Types { get; set; }
    }
}
