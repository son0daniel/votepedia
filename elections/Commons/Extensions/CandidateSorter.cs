using elections.Models.DTOs;
using elections.Models.Requests;

namespace elections.Commons.Extensions
{
    public static class CandidateSorter
    {
        public static IEnumerable<CandidateDto> Sort(this IEnumerable<CandidateDto> candidates, CandidateSortableField? sortBy, SortDirection sortDirection)
        {
            Func<CandidateDto, object?> keySelector = sortBy switch
            {
                CandidateSortableField.Name => x => x.Name,
                CandidateSortableField.DisplayName => x => x.DisplayName,
                CandidateSortableField.BirthDate => x => x.BirthDate,
                CandidateSortableField.DeathDate => x => x.DeathDate,
                _ => x => x.Name
            };

            return sortDirection == SortDirection.Ascending ? candidates.OrderBy(keySelector) : candidates.OrderByDescending(keySelector);
        }
    }
}
