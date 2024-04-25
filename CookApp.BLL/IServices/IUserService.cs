using CookApp.BLL.Dtos.UserManagementDto;
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
        Task<IQueryable<Role>> GetRoles();
        Task<User> GetUserById(int userId);
        Task<Role> GetRoleById(int userId);
        Task<IQueryable<Role>> GetRolesByUserId(int userId);
        Task UpdateUserInfo(User user);
        Task UpdateRoleInfo(Role role);
        Task UpdateUserRoles(int userId, List<int> rolesId);
        Task DeleteUser(int userId);
        Task CreateNewUser(UserDto userDto);
    }
}
