using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class CatalogModel
    {
        public Dictionary<string, List<ProductInfo>> ProductsByCategory { get; set; }
    }
}
