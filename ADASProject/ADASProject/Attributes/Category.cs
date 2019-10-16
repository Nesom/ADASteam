using System;

namespace ADASProject.Attributes
{
    public class Category : Attribute
    {
        public string CategoryName { get; set; }

        public Category(string name)
        {
            CategoryName = name;
        }
    }
}
