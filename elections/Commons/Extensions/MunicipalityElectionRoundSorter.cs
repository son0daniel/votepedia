using elections.Models.DTOs;
using elections.Models.Requests;
using elections.Models.Requests.ElectionRounds;

namespace elections.Commons.Extensions
{
    public static class MunicipalityElectionRoundSorter
    {
        public static IEnumerable<MunicipalityElectionRoundDto> Sort(this IEnumerable<MunicipalityElectionRoundDto> municipalityElectionRounds, MunicipalityElectionRoundSortableField? sortBy, SortDirection sortDirection)
        {
            Func<MunicipalityElectionRoundDto, object?> keySelector = sortBy switch
            {
                MunicipalityElectionRoundSortableField.Municipality => x => x.Municipality,
                MunicipalityElectionRoundSortableField.IbgeId => x => x.IbgeId,
                MunicipalityElectionRoundSortableField.TseId => x => x.TseId,
                MunicipalityElectionRoundSortableField.State => x => x.State,
                MunicipalityElectionRoundSortableField.RegisteredVotersCount => x => x.RegisteredVotersCount,
                MunicipalityElectionRoundSortableField.TurnoutCount => x => x.TurnoutCount,
                MunicipalityElectionRoundSortableField.TurnoutPp => x => x.TurnoutPp,
                MunicipalityElectionRoundSortableField.AbstentionCount => x => x.AbstentionCount,
                MunicipalityElectionRoundSortableField.AbstentionPp => x => x.AbstentionPp,
                MunicipalityElectionRoundSortableField.ElectoralAlienationCount => x => x.ElectoralAlienationCount,
                MunicipalityElectionRoundSortableField.ValidVotesCount => x => x.ValidVotesCount,
                MunicipalityElectionRoundSortableField.ValidVotesPp => x => x.ValidVotesPp,
                MunicipalityElectionRoundSortableField.ValidVotesRegisteredPp => x => x.ValidVotesRegisteredPp,
                MunicipalityElectionRoundSortableField.InvalidVotesCount => x => x.InvalidVotesCount,
                MunicipalityElectionRoundSortableField.InvalidVotesPp => x => x.InvalidVotesPp,
                MunicipalityElectionRoundSortableField.InvalidVotesRegisteredPp => x => x.InvalidVotesRegisteredPp,
                MunicipalityElectionRoundSortableField.NullVotesCount => x => x.NullVotesCount,
                MunicipalityElectionRoundSortableField.NullVotesPp => x => x.NullVotesPp,
                MunicipalityElectionRoundSortableField.NullVotesRegisteredPp => x => x.NullVotesRegisteredPp,
                MunicipalityElectionRoundSortableField.BlankVotesCount => x => x.BlankVotesCount,
                MunicipalityElectionRoundSortableField.BlankVotesPp => x => x.BlankVotesPp,
                MunicipalityElectionRoundSortableField.BlankVotesRegisteredPp => x => x.BlankVotesRegisteredPp,
                MunicipalityElectionRoundSortableField.AnulledVotesCount => x => x.AnulledVotesCount,
                MunicipalityElectionRoundSortableField.AnulledVotesPp => x => x.AnulledVotesPp,
                MunicipalityElectionRoundSortableField.AnulledVotesRegisteredPp => x => x.AnulledVotesRegisteredPp,
                _ => x => x.TseId
            };

            return sortDirection == SortDirection.Ascending ? municipalityElectionRounds.OrderBy(keySelector) : municipalityElectionRounds.OrderByDescending(keySelector);
        }
    }
}
