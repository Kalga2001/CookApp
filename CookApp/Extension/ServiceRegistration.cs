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
            //Registration HttpAccessors
            services.AddHttpContextAccessor();

            //Registration custom services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();

            //Registration Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
             
        }

    }
}
