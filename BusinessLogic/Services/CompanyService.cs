using BusinessLogic.Contracts;
using Contracts;
using Entities;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly ILoggerManager _loggerManager;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackingChanges)
        {
            try
            {
                var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackingChanges);
                var companiesDto = companies.Select(c => 
                new CompanyDto(
                    c.Id, c.Name ?? "", string.Join(' ',c.Address, c.Country))
                ).ToList();
                return companiesDto;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong in the {nameof(GetAllCompanies)} service method {ex}");
                throw;
            }
        }
    }
}
