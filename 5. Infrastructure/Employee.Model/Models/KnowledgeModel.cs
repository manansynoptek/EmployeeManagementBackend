namespace Employee.Model.Models
{
    public class KnowledgeModel
    {
        public int KnowledgeId { get; set; }

        public string? KnowledgeName { get; set; }

        public virtual ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
    }
}
