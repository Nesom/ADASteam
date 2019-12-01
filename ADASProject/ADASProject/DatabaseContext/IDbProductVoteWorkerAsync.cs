using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.DatabaseContext
{
    public interface IDbProductVoteWorkerAsync
    {
        Task VoteAsync(int productId, int userId, int vote);

        Task<bool> IsVotedAsync(int productId, int userId);
        Task<int> GetVoteAsync(int productId, int userId);
    }
}
