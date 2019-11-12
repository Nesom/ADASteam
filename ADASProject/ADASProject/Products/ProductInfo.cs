using System;
using ADASProject.Attributes;

namespace ADASProject.Products
{
    public class ProductInfo
    {
        public ProductInfo(string name, int count, double price, string description,
            string guarantee, string producer, int yearOfEdition)
        {
            Name = name;
            Count = count;
            Price = price;
            Description = description;
            Guarantee = guarantee;
            Producer = producer;
            YearOfEdition = yearOfEdition;
        }

        public int Id { get; set; }

        public string TableName { get; set; }
        public DateTime AddDate { get; set; }

        public int CountOfOrders { get; set; }

        public int CountOfVotes { get; set; }

        [Category("Main")]
        [Characteristic("Name")]
        public string Name { get; set; }
        [Characteristic("Products count")]
        public int Count { get; set; }
        [Characteristic("Price")]
        public double Price { get; set; }
        [Characteristic("Rating", true)]
        public double Rating { get; set; }
        [Characteristic("Description")]
        public string Description { get; set; }

        [Category("Production parameters")]
        [Characteristic("Guarantee")]
        public string Guarantee { get; set; }
        [Characteristic("Producer")]
        public string Producer { get; set; }
        [Characteristic("Year of edition")]
        public int YearOfEdition { get; set; }
        
        public byte[] Image { get; set; }
    }
}
