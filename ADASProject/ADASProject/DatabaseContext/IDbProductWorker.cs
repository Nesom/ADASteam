using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.DatabaseContext
{
    public interface IDbProductWorkerAsync
    {
        Task<bool> TryToAddProductAsync(Product<IDescription> product);
        Task<bool> TryToRemoveProductAsync(int id);

        Task<bool> IsAvailable(int id);
        Task<bool> HasQuantity(int id, int count);

        Task<Product<IDescription>> GetProductAsync(int id);
        Task<ProductInfo> GetProductInfoAsync(int id);
        Task<IDescription> GetDescriptionAsync(int id);

        Task<IQueryable<IDescription>> GetDescriptionsAsync(string descriptionName);
        Task<IQueryable<ProductInfo>> GetProductInfosAsync();
    }
}
