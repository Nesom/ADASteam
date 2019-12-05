using System;

namespace ADASProject.Attributes
{ 
    public class Characteristic : Attribute
    {
        public string CharacteristicName { get; }
        public bool OnlyForShow { get; }

        public Characteristic(string name, bool forShow = false)
        {
            CharacteristicName = name;
            OnlyForShow = forShow;
        }
    }
}
