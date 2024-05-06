using Employee.BusinessLogic.IBusinessLogic;
using Employee.Model.Models;
using Employee.Model.RequestModels;
using Employee.WebApi.Extension;
using Employee.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using static Employee.Model.Enum.Enums;

namespace Employee.WebApi.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region Properties-
        private readonly IDepartmentBusinessLogic _departmentLogic;
        #endregion

        #region Constructor
        public DepartmentController(IDepartmentBusinessLogic departmentLogic)
        {
            _departmentLogic = departmentLogic;
        }
        #endregion

        #region Public Methods
        
        [HttpGet("GetAllDepartments")]
        [Authorize(Role.Admin)]
        public async Task<ResponseModel> GetAllDepartments()
        {
            var user = HttpContext.Items["Employee"];
            return await _departmentLogic.GetAllDepartments();
        }

        [HttpPost("AddDepartment")]
        public async Task<ResponseModel> AddDepartment(DepartmentRequestModel department)
        {
            ResponseModel result = new();
            TryValidateModel(department);
            if (!ModelState.IsValid)
            {
                result.Message = ModelState.GetModelStateErrors();
                result.HttpStatus = HttpStatusCode.BadRequest;
                return result;
            }

            return await _departmentLogic.AddDepartment(department);
        }

        [HttpPut("UpdateDepartment")]
        public async Task<ResponseModel> UpdateDepartment(DepartmentRequestModel department)
        {
            ResponseModel result = new();
            TryValidateModel(department);
            if (!ModelState.IsValid)
            {
                result.Message = ModelState.GetModelStateErrors();
                result.HttpStatus = HttpStatusCode.BadRequest;
                return result;
            }

            return await _departmentLogic.UpdateDepartment(department);
        }

        [HttpDelete("DeleteDepartmentById")]
        public async Task<ResponseModel> DeleteDepartmentById(int departmentId)
        {
            return await _departmentLogic.DeleteDepartmentById(departmentId);
        }

        [HttpGet("GetDepartmentById")]
        //[Produces("application/json")]
        public async Task<ResponseModel> GetDepartmentById(int departmentId)
        {
            return await _departmentLogic.GetDepartmentById(departmentId);
        }

        [HttpPost("Test")]
        public void Test(List<DepartmentModel> departs)
        {
            var tt = departs;
        }

        [HttpPost("TestJson")]
        public void TestJson(string departs)
        {
            // The given JSON string
            string jsonString = "[{\"departmentId\": 1, \"departmentName\": \"string\"}]";

            var obj = JsonConvert.DeserializeObject<List<MyArray>>(jsonString);
        }
        #endregion
    }
}
