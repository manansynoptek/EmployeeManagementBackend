namespace Employee.Model.Models
{
    public class DesignationModel
    {
        public int DesignationId { get; set; }

        public string DesignationName { get; set; } = null!;

        public virtual ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
    }
}
