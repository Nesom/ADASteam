using ADASProject.Products;

namespace ADASProject.Order
{
    public class SubOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
