using AutoMapper;
using BusinessLogic.Contracts;
using Contracts;
using Entities;
using Entities.Exceptions;
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

        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackingChanges)
        {
            var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackingChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }

        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            return _mapper.Map<CompanyDto>(company);
        }

        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repositoryManager.CompanyRepository.CreateCompany(companyEntity);
            _repositoryManager.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }
        
    }
}
