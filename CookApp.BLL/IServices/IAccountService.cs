using CookApp.BLL.Dtos.AccountDtos;
using CookApp.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.IServices
{
    public interface IAccountService
    {
        Task<User> Login(LoginDto loginDto);
        Task Registration(RegistrationDto registrationDto);
        string GetCurrentUserEmail();
        bool IsAuthentificated();
    }
}
