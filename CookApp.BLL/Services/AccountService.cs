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

namespace CookApp.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IGenericRepository<User> userRepository, IGenericRepository<Role> roleRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> Login(LoginDto login)
        {
            var user = await _userRepository.GetAllAsyncQuery().FirstOrDefaultAsync(x => x.Email == login.Email);

            if (user != null)
            {
                user.LoginDate = DateTime.Now;
                user.IsAuthenticated = true;
            }

            await _userRepository.Update(user);

            return user;
        }

        public async Task Registration(RegistrationDto registration)
        {
            var user = _mapper.Map<User>(registration);

            var defaultRole = await _roleRepository.GetAllAsyncQuery().FirstOrDefaultAsync(x => x.RoleName == RoleName.Client);

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

        public string GetCurrentUserEmail()
        {
            var lastLoggedInUser = _userRepository
              .GetAllAsyncQuery()
              .OrderByDescending(x => x.LoginDate)
              .FirstOrDefault();

            if (lastLoggedInUser != null)
            {
                return lastLoggedInUser.Email;
            }

            return null;
        }

        public bool IsAuthentificated()
        {
            string email = GetCurrentUserEmail();

            return _userRepository.GetAllAsyncQuery().FirstOrDefault(x => x.Email == email).IsAuthenticated;

        }
    }
}
