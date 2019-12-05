using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Order
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Country { get; set; }
        public string Sity { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }

        public int PostOfficeIndex { get; set; }
    }
}
