using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public void CreateCompany(Company company) =>
            Create(company);

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool isTracking) =>
            await FindAll(isTracking)
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Company?> GetCompanyAsync(Guid companyId, bool trackChange) =>
            await FindByConditon(c => c.Id.Equals(companyId), trackChange)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChange) =>
            await FindByConditon(c => ids.Contains(c.Id), trackChange)
            .ToListAsync();

        public void DeleteCompany(Company company) =>
            Delete(company);
        
    }
}
