using CookApp.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] allowedRoles;

    public CustomAuthorizeAttribute(params string[] roles)
    {
        allowedRoles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        string refererHeader = filterContext.HttpContext.Request.Headers["Referer"];

        // Проверяем наличие значения "Referer" и наличие в нем токена
        if (string.IsNullOrEmpty(refererHeader) || !refererHeader.Contains("token="))
        {
            filterContext.Result = new UnauthorizedResult();
            return;
        }

        // Извлекаем часть строки, содержащую токен
        int tokenIndex = refererHeader.IndexOf("token=");
        string token = refererHeader.Substring(tokenIndex + 6);

        try
        {
            // Расшифровываем токен
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Извлекаем роли из токена
            var rolesClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (rolesClaim == null)
            {
                throw new InvalidOperationException("Role claim not found in token.");
            }
            string[] roles = rolesClaim.Value.Split(',');

            // Проверяем, содержит ли токен разрешенные роли
            if (!roles.Intersect(allowedRoles).Any())
            {
                filterContext.Result = new ForbidResult();
                return;
            }
        }
        catch (Exception ex)
        {
            filterContext.Result = new UnauthorizedResult();
            return;
        }
    }
}

