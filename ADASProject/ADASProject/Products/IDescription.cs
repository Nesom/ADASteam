using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Products
{
    public interface IDescription
    {
        int Id { get; set; }
        int MainTableId { get; set; }
    }
}
