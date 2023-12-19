using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _serviceManager.CompanyService.GetAllCompanies(trackingChanges: false);
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error");
            }
        }
    }
}
