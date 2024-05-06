using Employee.Data.Entities;
using Employee.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data.Repositories
{
    public class DepartmentRepository : GenericRepository<Entities.Department>, IDepartmentRepository
    {
        #region Properties
        protected new readonly EmployeeManagementContext _context;
        #endregion

        #region Constructor
        public DepartmentRepository(EmployeeManagementContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<List<Entities.Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }
        #endregion
    }
}
