using AutoMapper;
using CookApp.BLL.Dtos.AccountDtos;
using CookApp.BLL.IServices;
using CookApp.DAL.IRepository;
using CookApp.Entity.Entity;
using CookApp.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using CookApp.DAL.Helpers;

namespace CookApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor,IGenericRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepository = userRoleRepository;
        }

        public IQueryable<string> GetRolesByUserID(int UserId)
        {
            var roles = _userRoleRepository.GetAllAsyncQuery().Where(x => x.UserId == UserId).Select(x=>x.RoleId);
            var roleNames = _roleRepository.GetAllAsyncQuery().Where(x => roles.Contains(x.Id)).Select(x=>x.RoleName);
            return roleNames;
        }

        public async Task<User> Login(LoginDto login)
        {
            var user = await _userRepository.GetAllAsyncQuery().FirstOrDefaultAsync(x => x.Email == login.Email);

            if (user != null)
            {
                user.LoginDate = DateTime.Now;
                user.IsAuthenticated = true;
            }

            var users = await _userRepository.GetAllAsyncQuery().ToListAsync();
            foreach(var u in users)
            {
                if(u!=user)
                {
                    u.IsAuthenticated = false;
                }
            }

            await _userRepository.Update(user);

            return user;
        }

        public async Task Registration(RegistrationDto registration)
        {

            var user = _mapper.Map<User>(registration);

            var defaultRole = await _roleRepository.GetAllAsyncQuery().FirstOrDefaultAsync(x => x.RoleName.Contains("Client"));

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
            user.Password = Helper.HashPassword(user.Password);

            await _userRepository.AddAsync(user);
        }


    }
}
