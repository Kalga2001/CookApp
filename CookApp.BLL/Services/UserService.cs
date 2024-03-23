using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using CookApp.Entity.Enums;
using Microsoft.EntityFrameworkCore;
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

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IQueryable<User>> GetUsers()
        {
            var users = _userRepository.GetAllAsyncQuery();
            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task UpdateUserInfo(User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);

            if (existingUser == null)
                throw new Exception("User not found");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            
            await _userRepository.Update(existingUser);
        }

        public async Task UpdateRole(int userId, RoleName newRole)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            var userRole = user.UserRoles.FirstOrDefault();

            if (userRole == null)
                throw new Exception("User role not found");

            userRole.Role.RoleName = newRole.ToString(); 

            await _userRepository.Update(user);
        }


        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteAsync(userId);
        }
    }

}
