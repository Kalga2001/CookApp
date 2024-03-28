using CookApp.API.Extension;
using CookApp.BLL.AutoMapper;
using CookApp.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CookDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddServices();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<CookDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
       name: "menu",
       pattern: "Menu",
       defaults: new { controller = "Home", action = "Menu" });

app.MapControllerRoute(
        name: "about",
        pattern: "About",
        defaults: new { controller = "Home", action = "About" });

app.MapControllerRoute(
        name: "reservation",
        pattern: "Reservation",
        defaults: new { controller = "Home", action = "Reservation" });

app.MapControllerRoute(
        name: "services",
        pattern: "Services",
        defaults: new { controller = "Home", action = "Services" });

app.MapControllerRoute(
        name: "event",
        pattern: "Event",
        defaults: new { controller = "Home", action = "Event" });

app.MapControllerRoute(
        name: "specialDish",
        pattern: "SpecialDish",
        defaults: new { controller = "Home", action = "SpecialDish" });


app.Run();
