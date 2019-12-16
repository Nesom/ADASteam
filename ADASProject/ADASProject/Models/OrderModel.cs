using ADASProject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class OrderModel
    {
        public Address Address { get; set; }
        public double Amount { get; set; }
        public string SecretCode { get; set; }
        public bool IsPaid { get; set; }

        public List<string> Places { get; set; }
        public string Place { get; set; }
        public int CityIndex { get; set; }
    }
}
