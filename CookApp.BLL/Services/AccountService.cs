using CookApp.BLL.IServices;
using CookApp.DAL.IGenericRepository;
using CookApp.Dtos.AccountDtos;
using CookApp.Enums;
using CookApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        public AccountService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<User> Login(LoginDto login)
        {
            return await _userRepository.GetAllAsyncQuery().FirstOrDefaultAsync(x => x.Email == login.Email);
        }


        public async Task Registration(RegistrationDto registration)
        {
            var user = new User
            {
                Name = registration.Name,
                Email = registration.Email,
                Password = registration.Password
            };

            var defaultRole = await _roleRepository.GetAllAsyncQuery()
                                                   .FirstOrDefaultAsync(x => x.RoleName == RoleName.Client);

            if (defaultRole == null)
            {
                throw new InvalidOperationException("Default role 'Client' not found.");
            }

            var userRole = new UserRole
            {
                User = user,
                Role = defaultRole
            };

            user.UserRoles = new List<UserRole> { userRole };
            await _userRepository.AddAsync(user);
        }
    }
}
