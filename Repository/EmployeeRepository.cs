using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            :base(repositoryContext) { }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public Employee? GetEmployee(Guid companyId, Guid employeeId, bool trackChange) =>
            FindByConditon(
                e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId),
                trackChange)
            .SingleOrDefault();

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChange) =>
            FindByConditon(e => e.CompanyId.Equals(companyId), trackChange)
                .OrderBy(e => e.Name)
                .ToList();

    }
}
