using Asp.Versioning;
using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiversion}/companies")]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesV2Controller(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _service.CompanyService.GetAllCompaniesAsync(trackingChanges: false);
            var companiesV2 = companies.Select(x => $"{x.Name} V2").ToList();
            return Ok(companiesV2);
        }
    }
}
