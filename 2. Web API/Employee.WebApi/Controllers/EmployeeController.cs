using Employee.BusinessLogic.IBusinessLogic;
using Employee.Model.Models;
using Employee.Model.RequestModels;
using Employee.WebApi.Extension;
using Employee.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Employee.Model.Enum.Enums;

namespace Employee.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Properties-
        private readonly IEmployeeBusinessLogic _employeeLogic;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeBusinessLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }
        #endregion

        #region Public Methods
        [HttpGet("GetAllEmployees")]
        public async Task<ResponseModel> GetAllEmployees()
        {
            return await _employeeLogic.GetAllEmployees();
        }

        [HttpPost("AddEmployee")]
        public async Task<ResponseModel> AddEmployee(EmployeeRequestModel employee)
        {
            ResponseModel result = new();
            TryValidateModel(employee);
            if (!ModelState.IsValid)
            {
                result.Message = ModelState.GetModelStateErrors();
                result.HttpStatus = HttpStatusCode.BadRequest;
                return result;
            }

            return await _employeeLogic.AddEmployee(employee);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ResponseModel> UpdateEmployee(EmployeeRequestModel employee)
        {
            ResponseModel result = new();
            TryValidateModel(employee);
            if (!ModelState.IsValid)
            {
                result.Message = ModelState.GetModelStateErrors();
                result.HttpStatus = HttpStatusCode.BadRequest;
                return result;
            }

            return await _employeeLogic.UpdateEmployee(employee);
        }

        [HttpDelete("DeleteEmployeeById")]
        public async Task<ResponseModel> DeleteEmployeeById(int employeeId)
        {
            return await _employeeLogic.DeleteEmployeeById(employeeId);
        }

        [HttpGet("GetEmployeeById")]
        //[Produces("application/json")]
        public async Task<ResponseModel> GetEmployeeById(int employeeId)
        {
            return await _employeeLogic.GetEmployeeById(employeeId);
        }

        [HttpPost("AuthenticateEmployee")]
        public async Task<ResponseModel> AuthenticateEmployee(LoginRequestModel loginRequestModel)
        {
            return  await _employeeLogic.AuthenticateEmployee(loginRequestModel);
        }

        [HttpGet("CheckAPI")]
        [Authorize(Role.Admin)]
        public async Task<ResponseModel> CheckAPI()
        {
            return new ResponseModel();
        }
        #endregion
    }
}
