using CookApp.BLL.IServices;
using CookApp.BLL.Services;
using CookApp.DAL.IRepository;
using CookApp.DAL.Repository;
using Microsoft.AspNetCore.Hosting;

namespace CookApp.API.Extension
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
             
        }

    }
}
