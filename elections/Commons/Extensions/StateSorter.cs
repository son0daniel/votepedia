using elections.Models.DTOs;
using elections.Models.Requests;

namespace elections.Commons.Extensions
{
    public static class StateSorter
    {
        public static IEnumerable<StateDto> Sort (this IEnumerable<StateDto> states, StateSortableField? sortBy, SortDirection sortDirection)
        {
            Func<StateDto, object> keySelector = sortBy switch
            {
                StateSortableField.Name => x => x.Name,
                StateSortableField.Abbr => x => x.Abbr,
                StateSortableField.Macroregion => x => x.Macroregion,
                _ => x => x.Name,
            };

            return sortDirection == SortDirection.Ascending ? states.OrderBy(keySelector) : states.OrderByDescending(keySelector);
        }
    }
}
