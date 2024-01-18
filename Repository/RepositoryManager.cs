using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;

        private readonly Lazy<ICompanyRepository> _companyRepository;

        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(_context));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_context));    
        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public ICompanyRepository CompanyRepository => _companyRepository.Value;

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
