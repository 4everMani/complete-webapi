using AutoMapper;
using BusinessLogic.Contracts;
using Contracts;
using Entities;
using Entities.Exceptions;
using Shared.DataTransferObjects;

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

        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChange)
        {
            if (ids is null)
                throw new IdParametersbadRequestException();

            var companyEntities = _repositoryManager.CompanyRepository.GetByIds(ids, trackChange);
            if (ids.Count() != companyEntities.Count())
                throw new CollectionIdsBadRequestException();

            var comapinesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return comapinesToReturn;
        }

        public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach ( var company in companyEntities)
            {
                _repositoryManager.CompanyRepository.CreateCompany(company);
            }
            _repositoryManager.Save();

            var companiesToRetuen = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companiesToRetuen.Select(c => c.Id));
            return (companies: companiesToRetuen, ids: ids);
        }

        public void DeleteCompany(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            _repositoryManager.CompanyRepository.DeleteCompany(company);
            _repositoryManager.Save();
        }

    }
}
