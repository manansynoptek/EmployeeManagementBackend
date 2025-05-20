namespace Employee.Data.IRepositories
{
    public interface IEmployeeRepository : IGenericRepository<Entities.Employee>
    {
        Task<List<Entities.Employee>> GetAllEmployees();
        Task<Entities.Employee?> LoginEmployee(string email, string password);
    }
}
