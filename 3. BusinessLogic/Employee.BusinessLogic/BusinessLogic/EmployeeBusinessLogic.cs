using Employee.BusinessLogic.IBusinessLogic;
using Employee.Data.Entities;
using Employee.Data.IRepositories;
using Employee.Model.Models;
using Employee.Model.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin;
using System.Net;

namespace Employee.BusinessLogic.BusinessLogic
{
    public class EmployeeBusinessLogic : IEmployeeBusinessLogic
    {
        #region Properties
        private readonly IUnitOfwork _unitOfWork;
        private readonly EmployeeManagementContext _context;
        private readonly IJwtUtilsLogic _jwtUtilsLogic;
        #endregion

        #region Constructor
        public EmployeeBusinessLogic(IUnitOfwork unitOfWork, EmployeeManagementContext context, IJwtUtilsLogic jwtUtilsLogic)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _jwtUtilsLogic = jwtUtilsLogic;
        }
        #endregion

        #region Public Methods
        public async Task<ResponseModel> AddEmployee(EmployeeRequestModel model)
        {
            ResponseModel result = new();

            Data.Entities.Employee employee = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DesignationId = model.DesignationId,
                DepartmentId = model.DepartmentId,
                KnowledgeOf = model.KnowledgeOf,
                Salary = model.Salary,
                JoiningDate = model.JoiningDate,
                ReportingPerson = model.ReportingPerson,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                IsDeleted = model.IsDeleted,
                EmployeeEmail = model.EmployeeEmail,
                EmployeePassword = model.EmployeePassword,
                EmployeeName = model.EmployeeName
            };

            await _unitOfWork.Employee.Add(employee);
            await _unitOfWork.CompleteAsync();

            result.IsSuccess = true;
            result.HttpStatus = HttpStatusCode.Created;
            result.Message = "Employee has been added successfully.";

            return result;
        }

        public async Task<ResponseModel> DeleteEmployeeById(int employeeId)
        {
            ResponseModel result = new();

            // Check employee is exist or not
            var employee = await _unitOfWork.Employee.GetById(employeeId);
            if (employee == null)
            {
                result.HttpStatus = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
                result.Message = "Employee not found";
                return result;
            }

            employee.IsDeleted = true;
            _unitOfWork.Employee.Update(employee);
            await _unitOfWork.CompleteAsync();

            result.IsSuccess = true;
            result.HttpStatus = HttpStatusCode.OK;
            result.Message = "Employee has been deleted successfully.";

            return result;
        }

        public async Task<ResponseModel> UpdateEmployee(EmployeeRequestModel model)
        {
            ResponseModel result = new();

            // Check employee is exist or not
            var employee = await _unitOfWork.Employee.GetById(model.EmployeeId);
            if (employee == null)
            {
                result.HttpStatus = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
                result.Message = "Employee not found";
                return result;
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.DesignationId = model.DesignationId;
            employee.DepartmentId = model.DepartmentId;
            employee.KnowledgeOf = model.KnowledgeOf;
            employee.Salary = model.Salary;
            employee.JoiningDate = model.JoiningDate;
            employee.ReportingPerson = model.ReportingPerson;
            employee.CreatedDate = model.CreatedDate;
            employee.UpdatedDate = model.UpdatedDate;
            employee.IsDeleted = model.IsDeleted;
            employee.EmployeeEmail = model.EmployeeEmail;
            employee.EmployeePassword = model.EmployeePassword;

            _unitOfWork.Employee.Update(employee);
            await _unitOfWork.CompleteAsync();

            result.IsSuccess = true;
            result.HttpStatus = HttpStatusCode.OK;
            result.Message = "Employee has been updated successfully.";

            return result;
        }

        public async Task<ResponseModel> GetAllEmployees()
        {
            ResponseModel result = new();
            var employees = await _unitOfWork.Employee.GetAllEmployees();
            var employeeModel = employees.Select(x => new EmployeeModel()
            {
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DesignationId = x.DesignationId,
                DepartmentId = x.DepartmentId,
                KnowledgeOf = x.KnowledgeOf,
                Salary = x.Salary,
                JoiningDate = x.JoiningDate,
                ReportingPerson = x.ReportingPerson,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                IsDeleted = x.IsDeleted
            }).ToList();
            result.IsSuccess = true;
            result.Data = employeeModel;
            return result;
        }

        public async Task<ResponseModel> GetEmployeeById(int employeeId)
        {
            ResponseModel result = new();
            var employee = await _unitOfWork.Employee.GetById(employeeId);

            EmployeeModel employeeModel = new();
            if (employee == null)
            {
                result.HttpStatus = HttpStatusCode.NotFound;
                result.Message = "Employee not found";
                result.Data = employeeModel;
                return result;
            }

            result.IsSuccess = true;

            employeeModel = new EmployeeModel()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DesignationId = employee.DesignationId,
                DepartmentId = employee.DepartmentId,
                KnowledgeOf = employee.KnowledgeOf,
                Salary = employee.Salary,
                JoiningDate = employee.JoiningDate,
                ReportingPerson = employee.ReportingPerson,
                CreatedDate = employee.CreatedDate,
                UpdatedDate = employee.UpdatedDate,
                IsDeleted = employee.IsDeleted
            };
            result.Data = employeeModel;

            return result;
        }

        public async Task<ResponseModel> AuthenticateEmployee(LoginRequestModel loginRequestModel)
        {
            ResponseModel result = new();
            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.EmployeeEmail == loginRequestModel.Email && x.EmployeePassword == loginRequestModel.Password);

            EmployeeModel employeeModel = new();
            if (employee == null)
            {
                result.HttpStatus = HttpStatusCode.NotFound;
                result.Message = "Employee not found";
                result.Data = employeeModel;
                return result;
            }

            result.IsSuccess = true;

            employeeModel = new EmployeeModel()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeEmail = employee.EmployeeEmail,
                EmployeeName = employee.EmployeeName,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DesignationId = employee.DesignationId,
                DepartmentId = employee.DepartmentId,
                KnowledgeOf = employee.KnowledgeOf,
                Salary = employee.Salary,
                JoiningDate = employee.JoiningDate,
                ReportingPerson = employee.ReportingPerson,
                CreatedDate = employee.CreatedDate,
                UpdatedDate = employee.UpdatedDate,
                IsDeleted = employee.IsDeleted
            };
            result.Data = employeeModel;

            // create a JWT token            
            employeeModel.AccessToken = _jwtUtilsLogic.GenerateJwtToken(employeeModel);
            return result;
        }
        #endregion
    }
}
