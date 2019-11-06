using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ADASProject.Products;
using ADASProject.Models;
using ADASProject.Attributes;
using System.Text;
using System.Globalization;

namespace ADASProject
{
    public static class ReflectionHelper
    {
        public static Type FoundType(string type)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);
        }

        public static AddModel CreateAddModelByType(Type type)
        {
            var propertyType = GetPropertyType(type, "Description");
            var list = GetCharacteristicInfo(propertyType, true);
            return new AddModel()
            {
                AdditionalInfo = list,
                Values = new string[list.Count],
                StandartInfoValues = new string[AddModel.StandartInfo.Count]
            };
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

        public static ProductInfo CreateProductInfo(string[] values, bool onlyForShow = false)
        {
            var list = new List<Type>();
            foreach (var value in GetCharacteristicInfo(typeof(ProductInfo), onlyForShow))
                list.Add(value.Type);

            var types = list.ToArray();
            var objValues = ConvertTypes(values, types);

            return (ProductInfo)typeof(ProductInfo).GetConstructor(types).Invoke(objValues);
        }

        public static object[] ConvertTypes(string[] values, Type[] types)
        {
            var objValues = new object[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                if (values[i] != null)
                    try
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
                                objValues[i] = Convert.ToDouble(values[i], new NumberFormatInfo());
                                break;
                            case "Byte[]":
                                objValues[i] = Encoding.ASCII.GetBytes(values[i]);
                                break;
                            default:
                                objValues[i] = values[i];
                                break;
                        }
                    }
                    catch { }
            }
            return objValues;
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

        public static List<Models.TypeInfo> GetCharacteristicInfo(Type type, bool onlyForShow = false)
        {
            var properties = type
                .GetProperties();
            var list = new List<Models.TypeInfo>();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(Characteristic));
                if (attribute != null && (!onlyForShow || !((Characteristic)attribute).OnlyForShow))
                {
                    list.Add(new Models.TypeInfo
                    {
                        Description = ((Characteristic)attribute).CharacteristicName,
                        Type = property.PropertyType,
                        PropertyName = property.Name
                    });
                }
            }

            return list;
        }

        public static List<Models.TypeInfo> GetCharacteristicInfo(Type type, HashSet<string> parameters)
        {
            var characteristics = GetCharacteristicInfo(type);
            return characteristics
                .Where(ch => parameters.Contains(ch.Description))
                .ToList();
        }

        public static Dictionary<string, List<Models.TypeInfo>> GetCharacteristicInfo(IEnumerable<Type> types, HashSet<Type> filter)
        {
            var characteristics = new Dictionary<string, List<Models.TypeInfo>>();
            foreach(var type in types)
            {
                characteristics.Add(type.Name, 
                    GetCharacteristicInfo(type)
                    .Where(ch => filter.Contains(ch.Type))
                    .ToList());
            }
            return characteristics;
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

        public static Type[] GetAllProductClasses()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsClass && type.BaseType.Name == "Product`1")
                .ToArray();
        }

        public static Type[] GetAllDescriptionClasses()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsClass && type.GetInterface("IDescription") != null)
                .ToArray();
        }

        public static Attribute GetAttribute(Type valueType, Type attributeType)
        {
            return valueType.GetCustomAttribute(attributeType);
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
