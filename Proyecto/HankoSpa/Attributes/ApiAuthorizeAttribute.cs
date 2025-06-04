using HankoSpa.DTOs;
using HankoSpa.Helpers;
using HankoSpa.Models;
using HankoSpa.Nucleo;
using HankoSpa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Security.Claims;

namespace HankoSpa.Attributes
{
    public class ApiAuthorizeAttribute : TypeFilterAttribute
    {
        public ApiAuthorizeAttribute(string permission, string module) : base(typeof(ApiAuthorizeFilter))
        {
            Arguments = [permission, module];
        }
    }
    public class ApiAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly string _module;
        private readonly IUserService _userService;
        public ApiAuthorizeFilter(string permission, string module, IUserService userService)
        {
            _permission = permission;
            _module = module;
            _userService = userService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ClaimsPrincipal user = context.HttpContext.User;
            if (user?.Identity is null || !user.Identity.IsAuthenticated)
            {
                Response<object> response = ResponseHelper<object>.MakeResponseFail("No Autenticado", ["El tokeno no es valido o ha expirado"]);

                context.Result = new JsonResult(response)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }


            bool isAuthorized = _userService.CurrentUserIsAuthorizedAsync(_permission, _module).GetAwaiter().GetResult();
            if (!isAuthorized)
            {
                Response<object> response = ResponseHelper<object>.MakeResponseFail("Acceso denegado", ["No tienes permiso para acceder a este recurso"]);

                context.Result = new JsonResult(response)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}
