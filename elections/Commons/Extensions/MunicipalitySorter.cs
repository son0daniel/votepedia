using elections.Models.DTOs;
using elections.Models.Requests;

namespace elections.Commons.Extensions
{
    public static class MunicipalitySorter
    {
        public static IEnumerable<MunicipalityDto> Sort(this IEnumerable<MunicipalityDto> municipalities, MunicipalitySortableField? SortBy, SortDirection sortDirection)
        {
            Func<MunicipalityDto, object?> keyselector = SortBy switch
            {
                MunicipalitySortableField.Name => x => x.Name,
                MunicipalitySortableField.MetropolitanArea => x => x.MetropolitanArea?.Name,
                MunicipalitySortableField.State => x => x.State,
                MunicipalitySortableField.Macroregion => x => x.Macroregion,
                _ => x => x.Name
            };

            return sortDirection == SortDirection.Ascending ? municipalities.OrderBy(keyselector) : municipalities.OrderByDescending(keyselector);
        }
    }
}
