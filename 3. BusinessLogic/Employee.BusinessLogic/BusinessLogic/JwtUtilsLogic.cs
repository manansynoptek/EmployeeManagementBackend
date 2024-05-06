using Employee.BusinessLogic.IBusinessLogic;
using Employee.Model.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Employee.Model.Enum.Enums;

namespace Employee.BusinessLogic.BusinessLogic
{
    public class JwtUtilsLogic : IJwtUtilsLogic
    {
        private readonly JwtSettings _jwtSettings;

        #region Constructor
        [SetsRequiredMembers]
        public JwtUtilsLogic(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        #endregion

        #region Public Methods
        public string GenerateJwtToken(EmployeeModel user)
        {
            var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Sub, !string.IsNullOrEmpty(user.EmployeeName)? user.EmployeeName : "" ),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim("EmployeeId", user.EmployeeId.ToString()),
                                new Claim("EmployeeEmail", user.EmployeeEmail),
                                new Claim("EmployeeName", user.EmployeeName),
                                new Claim("Role", user.EmployeeName)
                                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public EmployeeClaimsModel? ValidateJwtToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var Id = jwtToken.Claims.First(x => x.Type == "EmployeeId").Value;

                EmployeeClaimsModel userClaimsModel = new()
                {
                    EmployeeId = Convert.ToInt16(jwtToken.Claims.First(x => x.Type == "EmployeeId").Value),
                    EmployeeEmail = jwtToken.Claims.First(x => x.Type == "EmployeeEmail").Value,
                    EmployeeName = jwtToken.Claims.First(x => x.Type == "EmployeeName").Value,
                    Role = (Role)Enum.Parse(typeof(Role), jwtToken.Claims.First(x => x.Type == "Role").Value)
                };

                // return email from JWT token if validation successful
                return userClaimsModel;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
        #endregion
    }
}
