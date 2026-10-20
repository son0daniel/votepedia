using elections.Models.DTOs;
using elections.Models.Requests;

namespace elections.Commons.Extensions
{
    public static class MetropolitanAreaSorter
    {
        public static IEnumerable<MetropolitanAreaDto> Sort(this IEnumerable<MetropolitanAreaDto> metropolitanAreas, MetropolitanAreaSortableField? sortBy, SortDirection sortDirection)
        {
            Func<MetropolitanAreaDto, object> keySelector = sortBy switch
            {
                MetropolitanAreaSortableField.Type => x => x.Type,
                _ => x => x.Name
            };

            return sortDirection == SortDirection.Ascending ? metropolitanAreas.OrderBy(keySelector) : metropolitanAreas.OrderByDescending(keySelector);
        }
    }
}
