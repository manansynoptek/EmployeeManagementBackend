using Employee.Model.Models;
using Employee.Model.RequestModels;

namespace Employee.BusinessLogic.IBusinessLogic
{
    public interface IEmployeeBusinessLogic
    {
        Task<ResponseModel> GetAllEmployees();
        Task<ResponseModel> AddEmployee(EmployeeRequestModel employee);
        Task<ResponseModel> UpdateEmployee(EmployeeRequestModel employeeRequestModel);
        Task<ResponseModel> GetEmployeeById(int employeeId);
        Task<ResponseModel> DeleteEmployeeById(int employeeId);
        Task<ResponseModel> AuthenticateEmployee(LoginRequestModel loginRequestModel);

    }
}
