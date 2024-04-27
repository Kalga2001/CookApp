using CookApp.BLL.Dtos.RoleManagementDto;
using CookApp.BLL.Dtos.UserManagementDto;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IRoleService
    {
        string GetRolesName(int userId);
        Task<Role> GetRoleById(int roleId);
        Task<IQueryable<Role>> GetRoles();
        Task UpdateRoleInfo(RoleDto roleDto);
        Task DeleteRole(int roleId);
        Task CreateNewRole(RoleDto roleDto);
    }
}
