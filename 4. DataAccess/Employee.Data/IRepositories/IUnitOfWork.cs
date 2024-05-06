using Microsoft.EntityFrameworkCore;

namespace Employee.Data.IRepositories
{
    public interface IUnitOfwork : IDisposable
    {
        IEmployeeRepository Employee { get; }
        IDepartmentRepository Department { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
