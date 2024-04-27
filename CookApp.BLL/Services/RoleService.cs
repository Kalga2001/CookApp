using CookApp.BLL.Dtos.RoleManagementDto;
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

        public async Task<IQueryable<Role>> GetRoles()
        {
            var roles = _roleRepository.GetAllAsyncQuery();
            return roles;
        }

        public async Task<Role> GetRoleById (int roleId)
        {
            Role role = await _roleRepository.GetByIdAsync(roleId);
            return role;
        }
        public async Task UpdateRoleInfo(RoleDto roleDto)
        {
            var existingRole = await _roleRepository.GetByIdAsync(roleDto.RoleID);

            if (existingRole == null)
                throw new Exception("Role not found");

            existingRole.RoleName = roleDto.RoleName;

            await _roleRepository.Update(existingRole);
        }

        public async Task DeleteRole(int roleId)
        {
            await _roleRepository.DeleteAsync(roleId);
        }

        public async Task CreateNewRole(RoleDto roleDto)
        {
            Role newRole = new Role
            {
                RoleName = roleDto.RoleName
            };

            await _roleRepository.AddAsync(newRole);
        }
    }
}
