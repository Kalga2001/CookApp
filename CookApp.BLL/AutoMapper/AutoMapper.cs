using AutoMapper;
using CookApp.BLL.Dtos.AccountDtos;
using CookApp.Entity.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookApp.BLL.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, RegistrationDto>();
        }
    }
}
