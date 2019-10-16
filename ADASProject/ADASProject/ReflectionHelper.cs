using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ADASProject.Products;
using ADASProject.Models;
using ADASProject.Attributes;
using System.Text;


namespace ADASProject
{
    public static class ReflectionHelper
    {
        public static Type FoundType(string productName)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type => type.Name == productName);
        }

        public static AddModel CreateAddModelByType(Type type)
        {
            var propertyType = GetPropertyType(type, "Description");
            var list = GetCharacteristicInfo(propertyType);
            return new AddModel()
            {
                AdditionalInfo = list,
                Values = new string[list.Count],
                StandartInfoValues = new string[AddModel.StandartInfo.Count]
            };
        }

        public static List<Models.TypeInfo> GetCharacteristicInfo(Type propertyType)
        {
            var properties = propertyType
                .GetProperties();
            var list = new List<Models.TypeInfo>();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(Characteristic));
                if (attribute != null)
                {
                    list.Add(new Models.TypeInfo
                    {
                        Description = ((Characteristic)attribute).CharacteristicName,
                        Type = property.PropertyType
                    });
                }
            }

            return list;
        }

        public static IDescription CreateProductDescription(string name, string[] values)
        {
            var type = FoundType(name);
            var propertyType = GetPropertyType(type, "Description");

            var list = new List<Type>();
            foreach(var value in GetCharacteristicInfo(propertyType))
                list.Add(value.Type);

            var types = list.ToArray();
            var objValues = ConvertTypes(values, types);

            return (IDescription)propertyType.GetConstructor(types).Invoke(objValues);
        }

        public static ProductInfo CreateProductInfo(string[] values)
        {
            var list = new List<Type>();
            foreach (var value in GetCharacteristicInfo(typeof(ProductInfo)))
                list.Add(value.Type);

            var types = list.ToArray();
            var objValues = ConvertTypes(values, types);

            return (ProductInfo)typeof(ProductInfo).GetConstructor(types).Invoke(objValues);
        }

        private static object[] ConvertTypes(string[] values, Type[] types)
        {
            var objValues = new object[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                switch (types[i].Name)
                {
                    case "Int32":
                        objValues[i] = Convert.ToInt32(values[i]);
                        break;
                    case "Boolean":
                        objValues[i] = Convert.ToBoolean(values[i]);
                        break;
                    case "Double":
                        objValues[i] = Convert.ToDouble(values[i]);
                        break;
                    case "Byte[]":
                        objValues[i] = Encoding.ASCII.GetBytes(values[i]);
                        break;
                    default:
                        objValues[i] = values[i];
                        break;
                }
            }
            return objValues;
        }

        public static Type GetPropertyType(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new Exception("Property is not found");
            return property.PropertyType;
        }

        public static PropertyInfo GetProperty(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property == null)
                throw new Exception("Property is not found");
            return property;
        }

        public static Dictionary<string, Dictionary<string, string>>
            GetCharacteristics(object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();
            string categoryName = null;
            var dict = new Dictionary<string, Dictionary<string, string>>();
            foreach (var property in properties)
            {
                var category = property.GetCustomAttribute(typeof(Category));
                if (category != null)
                {
                    categoryName = ((Category)category).CategoryName;
                    dict.Add(categoryName, new Dictionary<string, string>());
                }
                else if (categoryName == null)
                    continue;

                var characteristic = property.GetCustomAttribute(typeof(Characteristic));
                if (characteristic == null)
                    continue;
                var value = property.GetValue(obj);
                var valueStr = Convert.ToString(value);
                if (value is bool)
                    if ((bool)value)
                        valueStr = "Да";
                    else
                        valueStr = "Нет";
                var name = ((Characteristic)characteristic).CharacteristicName;
                dict[categoryName].Add(name, valueStr);
            }
            return dict;
        }

        public static Type[] GetAllProductClasses()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsClass && type.BaseType.Name == "Product`1")
                .ToArray();
        }

        public static Dictionary<string, string> CreateNameToTypeDict(Type[] types, Type attributeType)
        {
            var nameToTypeDict = new Dictionary<string, string>();
            foreach (var type in types)
            {
                var attributes = type.GetCustomAttributes(false);
                var attribute = attributes.FirstOrDefault(attr => attr.GetType() == attributeType);
                if (attribute != null)
                {
                    var category = attribute as ClassName;
                    nameToTypeDict.Add(category.Name, type.Name);
                }
            }
            return nameToTypeDict;
        }

        public static Dictionary<T2, T1> ReverseKeysAndValues<T1, T2>(this Dictionary<T1, T2> dict)
        {
            var reversedDict = new Dictionary<T2, T1>();
            foreach(var item in dict)
                reversedDict[item.Value] = item.Key;
            return reversedDict;
        }
    }
    
}
