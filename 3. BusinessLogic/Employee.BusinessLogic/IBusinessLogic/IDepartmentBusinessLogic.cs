using Employee.Model.Models;
using Employee.Model.RequestModels;

namespace Employee.BusinessLogic.IBusinessLogic
{
    public interface IDepartmentBusinessLogic
    {
        Task<ResponseModel> GetAllDepartments();
        Task<ResponseModel> AddDepartment(DepartmentRequestModel employee);
        Task<ResponseModel> UpdateDepartment(DepartmentRequestModel departmentRequestModel);
        Task<ResponseModel> GetDepartmentById(int departmentId);
        Task<ResponseModel> DeleteDepartmentById(int departmentId);
    }
}
