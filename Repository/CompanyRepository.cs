﻿using Contracts;
using Entities;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public void CreateCompany(Company company) =>
            Create(company);

        public IEnumerable<Company> GetAllCompanies(bool isTracking) =>
            FindAll(isTracking)
            .OrderBy(c => c.Name)
            .ToList();

        public Company? GetCompany(Guid companyId, bool trackChange) =>
            FindByConditon(c => c.Id.Equals(companyId), trackChange)
            .SingleOrDefault();
        
    }
}
