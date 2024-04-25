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
        string url = null;

        if (refererHeader.Contains("Home"))
        {
            refererHeader = refererHeader.Replace("Home", "");
        }

        if (!refererHeader.Contains("token="))
        {
            if (!refererHeader.Contains("Account/Login"))
            {
                url = refererHeader + "Account/Login";
            }
            else
            {
                url = refererHeader;
            }
        }

 
        try
        {
            int tokenIndex = refererHeader.IndexOf("token=");
            string token = refererHeader.Substring(tokenIndex + 6);


            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);


            var rolesClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (rolesClaim == null)
            {
                throw new InvalidOperationException("Role claim not found in token.");
            }
            string[] roles = rolesClaim.Value.Split(',');

            if (!roles.Intersect(allowedRoles).Any())
            {
                filterContext.Result = new ForbidResult();
                return;
            }

            if (roles.Contains("Administrator"))
            {
                url = refererHeader + "Admin/Index";
            }
        }
        catch (Exception ex)
        {
            filterContext.Result = new RedirectResult(url); 
            
            return;
        }

    }
}

