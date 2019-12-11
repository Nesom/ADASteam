using ADASProject.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.DatabaseContext
{
    public interface IDbCommentWorkerAsync : IDbUserWorkerAsync
    {
        Task AddCommentAsync(Comment comment);
        Task RemoveCommentAsync(int commentId);
        Task LikeCommentAsync(int userId, int commentId);
        Task UnlikeCommentAsync(int userId, int commentId);

        Task<bool> IsCommentedAsync(int userId, int productId);
        Task<bool> CanLikeAsync(int userId, int commentId);

        Task<Comment> GetCommentAsync(int commentId);
        Task<Comment[]> GetCommentsAsync(int productId);
    }
}
