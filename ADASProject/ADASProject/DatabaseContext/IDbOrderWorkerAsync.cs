using ADASProject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.DatabaseContext
{
    public interface IDbOrderWorkerAsync : IDbProductWorkerAsync, IDbRelationWorkerAsync, IDbUserWorkerAsync
    {
        Task ChangeStatusAsync(int orderId, Status status);

        Task<bool> TryToSaveOrderAsync(OrderInfo order);
        Task<bool> TryToChangeCountOfProductsAsync(IEnumerable<Tuple<int, int>> idsToValues);

        Task<OrderInfo> GetOrderAsync(int orderId);

        Task<IQueryable<OrderInfo>> GetOrdersAsync();
        Task<IQueryable<OrderInfo>> GetOrdersAsync(int userId);

        Task<List<SubOrder>> GetSubOrdersAsync(int orderId);
    }
}
