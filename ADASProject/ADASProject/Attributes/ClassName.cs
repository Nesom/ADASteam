using System;

namespace ADASProject.Attributes
{
    public class ClassName : Attribute
    {
        public string Name { get; set; }

        public ClassName(string name)
        {
            Name = name;
        }
    }
}
