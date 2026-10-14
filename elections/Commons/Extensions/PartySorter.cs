using elections.Models.DTOs;
using elections.Models.Requests;

namespace elections.Commons.Extensions
{
    public static class PartySorter
    {
        public static IEnumerable<PartyDto> Sort(this IEnumerable<PartyDto> parties, PartySortableField? sortBy, SortDirection sortDirection)
        {
            Func<PartyDto, object?> keySelector = sortBy switch
            {
                PartySortableField.Name => x => x.Name,
                PartySortableField.Abbr => x => x.Abbr,
                PartySortableField.Nr => x => x.VoteableNr,
                PartySortableField.Foundation => x => x.Foundation,
                PartySortableField.Registration => x => x.Registration,
                PartySortableField.Dissolution => x => x.Registration,
                _ => x => x.Name
            };

            return sortDirection == SortDirection.Ascending ? parties.OrderBy(keySelector) : parties.OrderByDescending(keySelector);
        }
    }
}
