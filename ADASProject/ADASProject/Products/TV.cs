using ADASProject.Attributes;

namespace ADASProject.Products
{
    [ClassName("TV")]
    [ImageSize(120, 180)]
    public class TV : Product<TVDescription>
    {
    }

    public class TVDescription : IDescription
    {
        public TVDescription() { }

        public TVDescription(double screenDiagonal, string screenResolution, bool operatingSystem,
            bool supportSmartTV, string digitalTuners)
        {
            ScreenDiagonal = screenDiagonal;
            ScreenResolution = screenResolution;
            OperatingSystem = operatingSystem;
            SupportSmartTV = supportSmartTV;
            DigitalTuners = digitalTuners;
        }

        public int Id { get; set; }

        [Category("Screen")]
        [Characteristic("Screen diagonal")]
        public double ScreenDiagonal { get; set; }

        [Characteristic("Screen resolution")]
        public string ScreenResolution { get; set; }

        [Category("Smart TV")]
        [Characteristic("Operating system")]
        public bool OperatingSystem { get; set; }

        [Characteristic("Support Smart TV")]
        public bool SupportSmartTV { get; set; }

        [Category("Signal reception")]
        [Characteristic("Digital tuners")]
        public string DigitalTuners { get; set; }
    }
}

