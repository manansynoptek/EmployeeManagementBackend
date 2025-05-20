using Employee.Data.Entities;
using Employee.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Entities.Employee>, IEmployeeRepository
    {
        #region Properties
        protected readonly EmployeeManagementContext _context;
        #endregion

        #region Constructor
        public EmployeeRepository(EmployeeManagementContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<Entities.Employee>> GetAllEmployees()
        {
            return await _context.Employees.Where(x => !x.IsDeleted).ToListAsync();
        }
        public async Task<Entities.Employee?> LoginEmployee(string email, string password)
        {
            return await _context.Employees.SingleOrDefaultAsync(x => x.EmployeeEmail == email && x.EmployeePassword == password);
        }
        #endregion
    }
}
