namespace Employee.Model.Models
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
    }

    public class MyArray
    {
        public int departmentId { get; set; }
        public string departmentName { get; set; }
    }

    public class Root
    {
        public List<MyArray> MyArray { get; set; }
    }
}
