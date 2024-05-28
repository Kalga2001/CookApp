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
        string token = filterContext.HttpContext.Request.Cookies["accessToken"];

        if (string.IsNullOrEmpty(token))
        {
            filterContext.Result = new RedirectResult("~/Account/Login");
            return;
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var rolesClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
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
                // Redirect to Admin page
                filterContext.Result = new RedirectResult("~/Admin/Index");
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

