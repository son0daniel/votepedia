using elections.Models.DTOs;
using elections.Models.Requests;
using elections.Models.Requests.ElectionRounds;

namespace elections.Commons.Extensions
{
    public static class MetropolitanAreaElectionRoundSorter
    {
        public static IEnumerable<MetropolitanAreaElectionRoundDto> Sort(this IEnumerable<MetropolitanAreaElectionRoundDto> metropolitanAreaElectionRounds, MetropolitanAreaElectionRoundSortableField? sortBy, SortDirection sortDirection)
        {
            Func<MetropolitanAreaElectionRoundDto, object?> keySelector = sortBy switch
            {
                MetropolitanAreaElectionRoundSortableField.MetropolitanArea => x => x.MetropolitanArea,
                MetropolitanAreaElectionRoundSortableField.RegisteredVotersCount => x => x.RegisteredVotersCount,
                MetropolitanAreaElectionRoundSortableField.TurnoutCount => x => x.TurnoutCount,
                MetropolitanAreaElectionRoundSortableField.TurnoutPp => x => x.TurnoutPp,
                MetropolitanAreaElectionRoundSortableField.AbstentionCount => x => x.AbstentionCount,
                MetropolitanAreaElectionRoundSortableField.AbstentionPp => x => x.AbstentionPp,
                MetropolitanAreaElectionRoundSortableField.ElectoralAlienationCount => x => x.ElectoralAlienationCount,
                MetropolitanAreaElectionRoundSortableField.ValidVotesCount => x => x.ValidVotesCount,
                MetropolitanAreaElectionRoundSortableField.ValidVotesPp => x => x.ValidVotesPp,
                MetropolitanAreaElectionRoundSortableField.ValidVotesRegisteredPp => x => x.ValidVotesRegisteredPp,
                MetropolitanAreaElectionRoundSortableField.InvalidVotesCount => x => x.InvalidVotesCount,
                MetropolitanAreaElectionRoundSortableField.InvalidVotesPp => x => x.InvalidVotesPp,
                MetropolitanAreaElectionRoundSortableField.InvalidVotesRegisteredPp => x => x.InvalidVotesRegisteredPp,
                MetropolitanAreaElectionRoundSortableField.NullVotesCount => x => x.NullVotesCount,
                MetropolitanAreaElectionRoundSortableField.NullVotesPp => x => x.NullVotesPp,
                MetropolitanAreaElectionRoundSortableField.NullVotesRegisteredPp => x => x.NullVotesRegisteredPp,
                MetropolitanAreaElectionRoundSortableField.BlankVotesCount => x => x.BlankVotesCount,
                MetropolitanAreaElectionRoundSortableField.BlankVotesPp => x => x.BlankVotesPp,
                MetropolitanAreaElectionRoundSortableField.BlankVotesRegisteredPp => x => x.BlankVotesRegisteredPp,
                MetropolitanAreaElectionRoundSortableField.AnulledVotesCount => x => x.AnulledVotesCount,
                MetropolitanAreaElectionRoundSortableField.AnulledVotesPp => x => x.AnulledVotesPp,
                MetropolitanAreaElectionRoundSortableField.AnulledVotesRegisteredPp => x => x.AnulledVotesRegisteredPp,
                _ => x => x.MetropolitanArea
            };

            return sortDirection == SortDirection.Ascending ? metropolitanAreaElectionRounds.OrderBy(keySelector) : metropolitanAreaElectionRounds.OrderByDescending(keySelector);
        }
    }
}
