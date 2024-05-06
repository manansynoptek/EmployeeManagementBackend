using Employee.BusinessLogic.IBusinessLogic;
using Employee.Model.Models;

namespace Employee.WebApi.Middlewares
{
    public class JwtMiddleware
    {
        #region Properties
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Private Methods
        private static void AttachUserToContext(HttpContext context, IJwtUtilsLogic jwtUtilsLogic, string token)
        {
            try
            {
                EmployeeClaimsModel? employeeClaimsModel = jwtUtilsLogic.ValidateJwtToken(token);
                if (employeeClaimsModel?.EmployeeId > 0)
                {
                    context.Items["Employee"] = employeeClaimsModel;
                }
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }

        #endregion

        #region Public Method
        public async Task Invoke(HttpContext context, IJwtUtilsLogic jwtUtilsLogic)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, jwtUtilsLogic, token);

            await _next(context);
        }
        #endregion
    }
}
