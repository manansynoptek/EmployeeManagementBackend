using Employee.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using static Employee.Model.Enum.Enums;

namespace Employee.WebApi.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            EmployeeClaimsModel? userClaimsModel = (EmployeeClaimsModel?)context.HttpContext.Items["Employee"];
            if (userClaimsModel?.EmployeeId == null ||
                (_roles.Any() && !_roles.Contains(userClaimsModel.Role)))
            {
                context.Result = new JsonResult(new JsonResult(new ResponseModel()
                {
                    HttpStatus = HttpStatusCode.Unauthorized,
                    IsSuccess = false,
                    Message = "You are not authorized."
                }))
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
