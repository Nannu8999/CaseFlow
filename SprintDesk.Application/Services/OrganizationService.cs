using CaseFlow.Application.DTOs.Organization.Responses;
using CaseFlow.Domain.Entity;
using CaseFlow.Domain.Interfaces;

namespace CaseFlow.Application.Services;
public class OrganizationService
{

    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }


    public async Task<List<OrganizationResponse>> GetAllOrganizationsAsync()
    {
        try
        {
            var orgs = await _organizationRepository.GetAllOrganizationsAsync();

            return orgs.Select(o => new OrganizationResponse
            {
                Id = o.Id,
                Name = o.Name
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching organizations", ex);
        }
    }



}
