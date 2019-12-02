using ADASProject.Attributes;

namespace ADASProject.Products
{
    [ClassName("Laptop")]
    [ImageSize(120, 180)]
    public class Laptop : Product<LaptopDescription>
    {
    }

    public class LaptopDescription : IDescription
    {
        public LaptopDescription() { }

        public LaptopDescription(string operatingSystem, double screenDiagonal, string line,
            string memory, bool cardModel, bool hdd, string ssd)
        {
            OperatingSystem = operatingSystem;
            ScreenDiagonal = screenDiagonal;
            CPULine = line;
            Memory = memory;
            CardModel = cardModel;
            HDD = hdd;
            SSD = ssd;
        }

        public int Id { get; set; }

        [Category("Classification")]
        [Characteristic("Operating system")]
        public string OperatingSystem { get; set; }
        
        [Category("Screen")]
        [Characteristic("Screen diagonal")]
        public double ScreenDiagonal { get; set; }

        [Category("CPU")]
        [Characteristic("CPU line")]
        public string CPULine { get; set; }

        [Category("RAM")]
        [Characteristic("RAM size")]
        public string Memory { get; set; }

        [Category("Graphics accelerator")]
        [Characteristic("Discrete graphics card model")]
        public bool CardModel{ get; set; }

        [Category("Data storage")]
        [Characteristic("Total hard drive space")]
        public bool HDD { get; set; }
        [Characteristic("Total solid state drives")]
        public string SSD { get; set; }
    }
}
