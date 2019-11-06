using ADASProject.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Products
{
    [ClassName("TestClass")]
    [ImageSize(200, 350)]
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

        [Category("Мобильная связь")]
        [Characteristic("Поддерживает 4G")]
        public bool IsUsing4G { get; set; }

        [Category("Система")]
        [Characteristic("Версия ОС")]
        public string OSVersion { get; set; }
        [Characteristic("Количество ядер")]
        public int CoreCount { get; set; }
        [Characteristic("Частота работы процессора")]
        public string Frequency { get; set; }
        [Characteristic("Объем оперативной памяти")]
        public string Memory { get; set; }
    }
}
