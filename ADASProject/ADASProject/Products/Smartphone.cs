using ADASProject.Attributes;

namespace ADASProject.Products
{
    [ClassName("Смартфон")]
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

        [Category("Основная (тыловая) камера")]
        [Characteristic("Количество мегапикселей основной камеры")]
        public int MainMegapixelCount { get; set; }
        [Characteristic("Оптическая стабилизация")]
        public bool Stabilization { get; set; }

        [Category("Фронтальная камера")]
        [Characteristic("Количество мегапикселей фронтальной камеры")]
        public int FrontMegapixelCount { get; set; }
        [Characteristic("Автофокусировка")]
        public bool AutoFocus { get; set; }

        [Category("Питание")]
        [Characteristic("Емкость аккамулятора")]
        public string BatteryCapacity { get; set; }
    }
}
