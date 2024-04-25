using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository;

        public RoleService(IGenericRepository<Role> roleRepository, IGenericRepository<UserRole> userRoleRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }
        public string GetRolesName(int userId)
        {
            var userRoles = _userRoleRepository.GetAllAsyncQuery()
                                                .Where(x => x.UserId == userId)
                                                .Join(_roleRepository.GetAllAsyncQuery(),
                                                      ur => ur.RoleId,
                                                      r => r.Id,
                                                      (ur, r) => r.RoleName)
                                                .ToList();

            return string.Join(", ", userRoles);
        }
    }
}
