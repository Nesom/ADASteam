using ADASProject.DatabaseContext;

namespace ADASProject
{
    public interface IDbContext : IDbOrderWorkerAsync, IDbCommentWorkerAsync, IDbProductVoteWorkerAsync
    {
    }
}
