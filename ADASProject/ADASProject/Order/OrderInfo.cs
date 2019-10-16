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
            Products = new List<SubOrder>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public DateTime OrderTime { get; set; }

        public int AddressId { get; set; }

        public List<SubOrder> Products;
        public Status StatusInfo { get; set; }
        
        public bool IsDelivered
            => StatusInfo == Status.Доставлено;

        public bool IsRecieved
            => StatusInfo == Status.Получено;
    }
}
