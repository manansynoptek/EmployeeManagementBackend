using Employee.BusinessLogic.IBusinessLogic;
using Employee.Data.IRepositories;
using Employee.Model.Models;
using Employee.Model.RequestModels;
using System.Net;

namespace Employee.BusinessLogic.BusinessLogic
{
    public class DepartmentBusinessLogic : IDepartmentBusinessLogic
    {
        #region Properties
        private readonly IUnitOfwork _unitOfWork;
        #endregion

        #region Constructor
        public DepartmentBusinessLogic(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Methods
        public async Task<ResponseModel> AddDepartment(DepartmentRequestModel model)
        {
            ResponseModel result = new();

            Data.Entities.Department department = new()
            {
                DepartmentName = model.DepartmentName
            };

            await _unitOfWork.Department.Add(department);
            await _unitOfWork.CompleteAsync();

            result.IsSuccess = true;
            result.HttpStatus = HttpStatusCode.Created;
            result.Message = "Department has been added successfully.";

            return result;
        }

        public async Task<ResponseModel> DeleteDepartmentById(int DepartmentId)
        {
            ResponseModel result = new();

            // Check Department is exist or not
            var department = await _unitOfWork.Department.GetById(DepartmentId);
            if (department == null)
            {
                result.HttpStatus = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
                result.Message = "Department not found";
                return result;
            }

            _unitOfWork.Department.Update(department);
            await _unitOfWork.CompleteAsync();

            result.IsSuccess = true;
            result.HttpStatus = HttpStatusCode.OK;
            result.Message = "Department has been deleted successfully.";

            return result;
        }

        public async Task<ResponseModel> UpdateDepartment(DepartmentRequestModel model)
        {
            ResponseModel result = new();

            // Check Department is exist or not
            var department = await _unitOfWork.Department.GetById(model.DepartmentId);
            if (department == null)
            {
                result.HttpStatus = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NotFound;
                result.Message = "Department not found";
                return result;
            }

            department.DepartmentName = model.DepartmentName;

            _unitOfWork.Department.Update(department);
            await _unitOfWork.CompleteAsync();

            result.IsSuccess = true;
            result.HttpStatus = HttpStatusCode.OK;
            result.Message = "Department has been updated successfully.";

            return result;
        }

        public async Task<ResponseModel> GetAllDepartments()
        {
            ResponseModel result = new();
            var department = await _unitOfWork.Department.GetAllDepartments();
            var departmentModel = department.Select(x => new DepartmentModel()
            {
                DepartmentId = x.DepartmentId,
                DepartmentName = x.DepartmentName
            }).ToList();
            result.IsSuccess = true;
            result.Data = departmentModel;
            return result;
        }

        public async Task<ResponseModel> GetDepartmentById(int departmentId)
        {
            ResponseModel result = new();
            var department = await _unitOfWork.Department.GetById(departmentId);

            DepartmentModel departmentModel = new();
            if (department == null)
            {
                result.HttpStatus = HttpStatusCode.NotFound;
                result.Message = "DepartmentId not found";
                result.Data = departmentModel;
                return result;
            }

            result.IsSuccess = true;

            departmentModel = new DepartmentModel()
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
            result.Data = departmentModel;

            return result;
        }
        #endregion
    }
}
