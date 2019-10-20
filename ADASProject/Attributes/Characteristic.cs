using System;

namespace ADASProject.Attributes
{ 
    public class Characteristic : Attribute
    {
        public string CharacteristicName { get; set; }

        public Characteristic(string name)
        {
            CharacteristicName = name;
        }
    }
}
