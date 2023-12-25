﻿using Shared.DataTransferObjects;

namespace BusinessLogic.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackingChanges);

        CompanyDto GetCompany(Guid companyId, bool trackChanges);

        CompanyDto CreateCompany(CompanyForCreationDto company);

        IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> companyIds, bool trackChange);

        (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection);

        void DeleteCompany(Guid companyId, bool trackChange);
    }
}
