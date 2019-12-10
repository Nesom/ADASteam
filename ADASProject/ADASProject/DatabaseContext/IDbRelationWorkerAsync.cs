using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.DatabaseContext
{
    public interface IDbRelationWorkerAsync
    {
        Task AddRelationAsync(Relation relation);
        Task<Relation> GetRelationAsync(object[] keys);
        Task<IQueryable<Relation>> GetRelationsAsync();
    }
}
