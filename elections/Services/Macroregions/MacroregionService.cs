using elections.Commons.Extensions;
using elections.Models.DTOs;
using elections.Models.Entities;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace elections.Services.Macroregions
{
    public class MacroregionService : IMacroregionService
    {
        private readonly IRepositoryManager _repository;

        public MacroregionService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        private static Expression<Func<Macroregion, object?>> ResolveKeySelector(MacroregionSortableField? sortBy)
        {
            return sortBy switch
            {
                MacroregionSortableField.Abbr => x => x.Abbr,
                _ => x => x.Name
            };
        }

        public async Task<PagedResponse<MacroregionDto>> GetMacroregions(MacroregionFilter macroregionFilter, PagedRequest pagedRequest)
        {
            var queryMacroregions = _repository.Macroregion.GetQueryable(x => !string.Equals(x.Abbr, "ZZ"));

            var macroregionName = macroregionFilter.Name?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(macroregionName))
            {
                if (macroregionFilter.SearchMode == SearchMode.Exact)
                {
                    queryMacroregions = queryMacroregions.Where(x => string.Equals(x.Name, macroregionName));
                }
                else
                {
                    queryMacroregions = queryMacroregions.Where(x => x.Name.ToUpper().Contains(macroregionName));
                }
            }

            var totalCount = await queryMacroregions.CountAsync();

            var macroregions = await _repository.Macroregion.GetPaged(queryMacroregions, pagedRequest, ResolveKeySelector(macroregionFilter.SortBy), macroregionFilter.SortDirection);

            List<MacroregionDto> items = [.. macroregions.Select(x => new MacroregionDto(x))];
            items = [.. items.Sort(macroregionFilter.SortBy, macroregionFilter.SortDirection)];

            return new PagedResponse<MacroregionDto>(items, totalCount, pagedRequest);
        }

        public async Task<MacroregionDto?> GetMacroregion(string abbr)
        {
            var macroregion = await _repository.Macroregion.GetMacroregionByAbbr(abbr);

            return macroregion is null ? null : new MacroregionDto(macroregion);
        }
    }
}
