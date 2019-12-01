using ADASProject.Attributes;

namespace ADASProject.Products
{
    [ClassName("Smartphone")]
    [ImageSize(180, 120)]
    public class Smartphone : Product<SmartphoneDescription>
    {
    }

    public class SmartphoneDescription : IDescription
    {
        public SmartphoneDescription() { }

        public SmartphoneDescription(bool isUsing4G, string oSVersion, int coreCount, 
            string frequency, string memory, int mainMegapixelCount, 
            bool stabilization, int frontMegapixelCount, bool autoFocus, string batteryCapacity)
        {
            IsUsing4G = isUsing4G;
            OSVersion = oSVersion;
            CoreCount = coreCount;
            Frequency = frequency;
            Memory = memory;
            MainMegapixelCount = mainMegapixelCount;
            Stabilization = stabilization;
            FrontMegapixelCount = frontMegapixelCount;
            AutoFocus = autoFocus;
            BatteryCapacity = batteryCapacity;
        }

        public int Id { get; set; }

        [Category("Mobile connection")]
        [Characteristic("Support 4G")]
        public bool IsUsing4G { get; set; }

        [Category("System")]
        [Characteristic("OS version")]
        public string OSVersion { get; set; }
        [Characteristic("Core count")]
        public int CoreCount { get; set; }
        [Characteristic("CPU frequency")]
        public string Frequency { get; set; }
        [Characteristic("RAM size")]
        public string Memory { get; set; }

        [Category("Main camera")]
        [Characteristic("The number of megapixels on main camera")]
        public int MainMegapixelCount { get; set; }
        [Characteristic("Optical stabilization")]
        public bool Stabilization { get; set; }

        [Category("Frontal camera")]
        [Characteristic("The number of megapixels on frontal camera")]
        public int FrontMegapixelCount { get; set; }
        [Characteristic("Autofocus")]
        public bool AutoFocus { get; set; }

        [Category("Battery")]
        [Characteristic("Battary capacity")]
        public string BatteryCapacity { get; set; }
    }
}
