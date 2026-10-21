using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;

namespace elections.Services.Macroregions
{
    public interface IMacroregionService
    {
        Task<PagedResponse<MacroregionDto>> GetMacroregions(MacroregionFilter macroregionFilter, PagedRequest pagedRequest);
        Task<MacroregionDto?> GetMacroregion(string abbr);
    }
}
