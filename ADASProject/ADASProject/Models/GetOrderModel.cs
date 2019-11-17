using ADASProject.Order;
using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class GetOrderModel
    {
        public OrderInfo Order { get; set; }
        public Status Status { get; set; }
        public string Role { get; set; }

        public List<Tuple<ProductInfo, int>> SubOrders { get; set; }

        public GetOrderModel() { }
    }
}
