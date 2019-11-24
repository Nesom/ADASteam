using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.DatabaseContext
{
    public interface IDbUserWorkerAsync
    {
        Task RemoveUserAsync(int id);

        Task<bool> TryToAddUserAsync(User user);

        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string email);
    }
}
