using CookApp.BLL.IServices;
using CookApp.BLL.Services;
using CookApp.DAL.IGenericRepository;
using CookApp.DAL.Repository;
using Microsoft.AspNetCore.Hosting;

namespace CookApp.Extension
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
 
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(Program));
        }

    }
}
