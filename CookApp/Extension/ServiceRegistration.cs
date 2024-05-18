using CookApp.BLL.IServices;
using CookApp.BLL.Services;
using CookApp.DAL.IRepository;
using CookApp.DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartItemService,CartItemService>();
            services.AddScoped<ICartService, CartService>();
            //Registration Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        }
    }
}
