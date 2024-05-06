namespace Employee.Data.IRepositories
{
    public interface IDepartmentRepository : IGenericRepository<Entities.Department>
    {
        Task<List<Entities.Department>> GetAllDepartments();
    }
}
