using System.ComponentModel.DataAnnotations;

namespace Employee.Model.RequestModels
{
    public class EmployeeRequestModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string? FirstName { get; set; }

        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Designation is required.")]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }
        public int? KnowledgeOf { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Salary for {0} must be an integer.")]
        public int? Salary { get; set; }

        [Required(ErrorMessage = "Joining date is required.")]
        public DateTime JoiningDate { get; set; }

        public int? ReportingPerson { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string? EmployeeEmail { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? EmployeePassword { get; set; }
        [Required(ErrorMessage = "Employee name is required.")]
        public string? EmployeeName { get; set; }

    }
}
