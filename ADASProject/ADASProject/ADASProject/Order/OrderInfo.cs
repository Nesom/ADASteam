using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Order
{
    public class OrderInfo
    {
        public OrderInfo()
        {
            SubOrders = new List<SubOrder>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public DateTime OrderTime { get; set; }

        public List<SubOrder> SubOrders;
        public Status StatusInfo { get; set; }
        
        public bool IsDelivered
            => StatusInfo == Status.Delivered;

        public bool IsRecieved
            => StatusInfo == Status.Received;
    }
}
