using static Employee.Model.Enum.Enums;

namespace Employee.Model.Models
{
    public class EmployeeClaimsModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
    }
}
