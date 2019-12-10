using ADASProject.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Products
{
    [ClassName("TestClass")]
    [ImageSize(120, 180)]
    public class TestProduct : Product<TestProductDescription>
    {
    }

    public class TestProductDescription : IDescription
    {
        public TestProductDescription() { }

        public TestProductDescription(bool isUsing4G, string oSVersion, int coreCount,
            string frequency, string memory)
        {
            IsUsing4G = isUsing4G;
            OSVersion = oSVersion;
            CoreCount = coreCount;
            Frequency = frequency;
            Memory = memory;
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
    }
}
