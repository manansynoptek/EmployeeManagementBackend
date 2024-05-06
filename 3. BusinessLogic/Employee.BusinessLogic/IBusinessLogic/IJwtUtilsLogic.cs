using Employee.Model.Models;

namespace Employee.BusinessLogic.IBusinessLogic
{
    public interface IJwtUtilsLogic
    {
        public string GenerateJwtToken(EmployeeModel employee);
        public EmployeeClaimsModel? ValidateJwtToken(string token);
    }
}
