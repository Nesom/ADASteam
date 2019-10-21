using System;
using ADASProject.Attributes;

namespace ADASProject.Products
{
    public class ProductInfo
    {
        public ProductInfo(string name, int count, double price, 
            string guarantee, string producer, int yearOfEdition)
        {
            Name = name;
            Count = count;
            Price = price;
            Guarantee = guarantee;
            Producer = producer;
            YearOfEdition = yearOfEdition;
        }

        public int Id { get; set; }

        public string TableName { get; set; }
        public DateTime AddDate { get; set; }

        public int CountOfOrders { get; set; }

        public double Rating { get; set; }
        public int CountOfVotes { get; set; }

        [Category("Основное")]
        [Characteristic("Название")]
        public string Name { get; set; }
        [Characteristic("Количество товара")]
        public int Count { get; set; }
        [Characteristic("Цена")]
        public double Price { get; set; }

        [Category("Производственные параметры")]
        [Characteristic("Гарантия")]
        public string Guarantee { get; set; }
        [Characteristic("Производитель")]
        public string Producer { get; set; }
        [Characteristic("Год издания")]
        public int YearOfEdition { get; set; }

        public byte[] Image { get; set; }
    }
}
