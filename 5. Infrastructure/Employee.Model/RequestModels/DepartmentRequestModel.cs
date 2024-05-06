using System.ComponentModel.DataAnnotations;

namespace Employee.Model.RequestModels
{
    public class DepartmentRequestModel
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        public required string DepartmentName { get; set; }
    }
}
