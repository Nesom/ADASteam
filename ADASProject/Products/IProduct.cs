namespace ADASProject.Products
{
    public class Product<TDescription>
        where TDescription : IDescription
    {
        public ProductInfo ProductInfo { get; set; }
        public TDescription Description { get; set; }
    }

    public static class ProductExtentions
    {
        public static Product<TDescription> GetProduct<TDescription>(TDescription description, ProductInfo productInfo)
            where TDescription : IDescription
        {
            return new Product<TDescription>() { Description = description, ProductInfo = productInfo };
        }
    }
}
