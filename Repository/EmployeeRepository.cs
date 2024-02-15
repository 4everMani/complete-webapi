﻿using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
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

        public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChange) =>
            await FindByConditon(
                e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId),
                trackChange)
            .SingleOrDefaultAsync();

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChange)
        {
            var employees = await FindByConditon(e => e.CompanyId.Equals(companyId), trackChange)
                                .FilterEmployee(employeeParameters.MinAge, employeeParameters.MaxAge)
                                .Search(employeeParameters.SearchTerm)
                                .Sort(employeeParameters.OrderBy)
                                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                                .Take(employeeParameters.PageSize)
                                .ToListAsync();

            var count = await FindByConditon(e => e.CompanyId.Equals(companyId), trackChange).CountAsync();

            return new PagedList<Employee>(employees, count, employeeParameters.PageNumber, employeeParameters.PageSize);
        }
            

            

        public void DeleteEmployee(Employee employee) =>
            Delete(employee);

    }
}
