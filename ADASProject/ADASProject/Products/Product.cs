namespace ADASProject.Products
{
    public class Product<TDescription>
        where TDescription : IDescription
    {
        public static Product<TDescription> GetProduct(TDescription description, ProductInfo productInfo)
        {
            return new Product<TDescription>() { Description = description, ProductInfo = productInfo };
        }

        public ProductInfo ProductInfo { get; private set; }
        public TDescription Description { get; private set; }
    }
}
