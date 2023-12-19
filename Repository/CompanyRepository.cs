using Contracts;
using Entities;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public IEnumerable<Company> GetAllCompanies(bool isTracking) =>
            FindAll(isTracking)
            .OrderBy(c => c.Name)
            .ToList();
        
    }
}
