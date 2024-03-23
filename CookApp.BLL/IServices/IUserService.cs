using CookApp.Entity.Entity;
using CookApp.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IUserService
    {
        Task<IQueryable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task UpdateUserInfo(User user);
        Task UpdateRole(int userId, RoleName newRole);
        Task DeleteUser(int userId);
    }
}
