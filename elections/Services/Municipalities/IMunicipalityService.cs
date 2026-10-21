using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;

namespace elections.Services.Municipalities
{
    public interface IMunicipalityService
    {
        Task<PagedResponse<MunicipalityDto>> GetMunicipalities(MunicipalityFilter municipalityFilter, PagedRequest pagedRequest);
        Task<MunicipalityDto?> GetMunicipality(long tseId);
    }
}
