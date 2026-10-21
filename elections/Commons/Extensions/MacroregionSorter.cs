using elections.Models.DTOs;
using elections.Models.Requests;

namespace elections.Commons.Extensions
{
    public static class MacroregionSorter
    {
        public static IEnumerable<MacroregionDto> Sort(this IEnumerable<MacroregionDto> macroregions, MacroregionSortableField? sortBy, SortDirection sortDirection)
        {
            Func<MacroregionDto, object> keySelector = sortBy switch
            {
                MacroregionSortableField.Abbr => x => x.Abbr,
                _ => x => x.Name
            };

            return sortDirection == SortDirection.Ascending ? macroregions.OrderBy(keySelector) : macroregions.OrderByDescending(keySelector);
        }
    }
}
