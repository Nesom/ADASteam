using System;
using System.Collections.Generic;
using ADASProject.Order;
using ADASProject.Products;


namespace ADASProject.Models
{
    public class GetOrdersModel
    {
        public GetOrdersModel() =>
            Products = new Dictionary<OrderInfo, List<Tuple<ProductInfo, int>>>();

        public Dictionary<OrderInfo, List<Tuple<ProductInfo, int>>> Products { get; set; }

        public bool IsAdmin { get; set; }
    }
}
