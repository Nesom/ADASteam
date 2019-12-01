using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.DatabaseContext
{
    public interface IDbProductWorkerAsync
    {
        Task AddProductAsync(Product<IDescription> product);
        Task RemoveProductAsync(int id);

        Task<bool> IsAvailable(int id);
        Task<bool> HasQuantity(int id, int count);

        Task<Product<IDescription>> GetProductAsync(int id);
        Task<ProductInfo> GetProductInfoAsync(int id);
        Task<IDescription> GetDescriptionAsync(int id);

        Task<IQueryable<IDescription>> GetDescriptionsAsync(string descriptionName);
        Task<IQueryable<ProductInfo>> GetProductInfosAsync();
    }
}
