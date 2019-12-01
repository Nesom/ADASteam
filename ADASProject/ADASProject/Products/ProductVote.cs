using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADASProject.Products
{
    public class ProductVote
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        public int Vote { get; set; }
    }
}
