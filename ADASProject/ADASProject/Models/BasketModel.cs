using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ADASProject.Products;

namespace ADASProject.Models
{
    public class BasketModel
    {
        public Dictionary<ProductInfo, int> Products { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
