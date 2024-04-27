using CookApp.BLL.Dtos.AccountDtos;
using CookApp.BLL.Dtos.UserManagementDto;
using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using CookApp.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IAccountService _accountService;

        public UserService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository, IGenericRepository<UserRole> userRoleRepository, IAccountService accountService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _accountService = accountService;
        }

        public async Task<IQueryable<User>> GetUsers()
        {
            var users = _userRepository.GetAllAsyncQuery();
            return users;
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _roleRepository.GetByIdAsync(roleId);
        }

        public async Task UpdateUserInfo(UserDto userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(userDto.UserId);

            if (existingUser == null)
                throw new Exception("User not found");

            existingUser.Name = userDto.Name;
            existingUser.Email = userDto.Email;
            
            await _userRepository.Update(existingUser);
        }
 
        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteAsync(userId);
        }

 
        public async Task<IQueryable<Role>> GetRolesByUserId(int userId)
        {
            var roles = _userRoleRepository.GetAllAsyncQuery()
                                           .Where(x => x.UserId == userId)
                                           .Select(ur => ur.Role);
            return roles;
        }

        public async Task UpdateUserRoles(int userId, List<int> rolesId)
        {
            foreach(int role in rolesId)
            {
                UserRole userRole = new UserRole { RoleId = role, UserId = userId };
               await _userRoleRepository.AddAsync(userRole);
            }
        }

        public async Task CreateNewUser(UserDto userDto)
        {
            RegistrationDto registrationDto = new RegistrationDto();
            registrationDto.Name = userDto.Name;
            registrationDto.Email = userDto.Email;
            registrationDto.Password = userDto.Password;
            await _accountService.Registration(registrationDto);
        }

         


    }

}
