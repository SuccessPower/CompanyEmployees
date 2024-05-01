using Contracts;
using Entities.Entities;

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



        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefault();

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
            FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(e => e.Name).ToList();

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
