using CaseFlow.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {

        private readonly OrganizationService _organizationService;

        public OrganizationController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrganizations()
        {
            try
            {
                var orgs = await _organizationService.GetAllOrganizationsAsync();
                return Ok(orgs);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while fetching organizations.");
            }

        }
    }
}

