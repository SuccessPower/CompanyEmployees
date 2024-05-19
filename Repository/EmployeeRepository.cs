using Contracts;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext RepositoryContext) : base(RepositoryContext)
        {
            RepositoryContext = RepositoryContext;
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }



        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, 
            EmployeeParameters employeeParameters, bool trackChanges) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(e => e.Name)
            .Skip((employeeParameters.PageNumber -1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .ToListAsync();

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
