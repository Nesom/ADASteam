using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class SearchModel
    {
        public IEnumerable<ProductInfo> Products { get; set; }
    }
}
