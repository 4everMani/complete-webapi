﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool isTracking);

        Company? GetCompany(Guid companyId, bool trackChanges);

        void CreateCompany(Company company);
    }
}
