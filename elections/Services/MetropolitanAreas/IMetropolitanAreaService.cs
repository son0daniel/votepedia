using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;

namespace elections.Services.MetropolitanAreas
{
    public interface IMetropolitanAreaService
    {
        Task<PagedResponse<MetropolitanAreaDto>> GetMetropolitanAreas(MetropolitanAreaFilter metropolitanAreaFilter, PagedRequest pagedRequest);
    }
}
