using Shared.DataTransferObjects;

namespace BusinessLogic.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackingChanges);

        Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);

        Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);

        Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> companyIds, bool trackChange);

        Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companyCollection);

        Task DeleteCompanyAsync(Guid companyId, bool trackChange);

        Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyforUpdateDto, bool trackChanges);
    }
}
