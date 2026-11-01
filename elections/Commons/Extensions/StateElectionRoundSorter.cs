using elections.Models.DTOs;
using elections.Models.Requests;
using elections.Models.Requests.ElectionRounds;

namespace elections.Commons.Extensions
{
    public static class StateElectionRoundSorter
    {
        public static IEnumerable<StateElectionRoundDto> Sort(this IEnumerable<StateElectionRoundDto> stateElectionRounds, StateElectionRoundSortableField? sortBy, SortDirection sortDirection)
        {
            Func<StateElectionRoundDto, object?> keySelector = sortBy switch
            {
                StateElectionRoundSortableField.State => x => x.State,
                StateElectionRoundSortableField.Macroregion => x => x.Macroregion,
                StateElectionRoundSortableField.RegisteredVotersCount => x => x.RegisteredVotersCount,
                StateElectionRoundSortableField.TurnoutCount => x => x.TurnoutCount,
                StateElectionRoundSortableField.TurnoutPp => x => x.TurnoutPp,
                StateElectionRoundSortableField.AbstentionCount => x => x.AbstentionCount,
                StateElectionRoundSortableField.AbstentionPp => x => x.AbstentionPp,
                StateElectionRoundSortableField.ElectoralAlienationCount => x => x.ElectoralAlienationCount,
                StateElectionRoundSortableField.ValidVotesCount => x => x.ValidVotesCount,
                StateElectionRoundSortableField.ValidVotesPp => x => x.ValidVotesPp,
                StateElectionRoundSortableField.ValidVotesRegisteredPp => x => x.ValidVotesRegisteredPp,
                StateElectionRoundSortableField.InvalidVotesCount => x => x.InvalidVotesCount,
                StateElectionRoundSortableField.InvalidVotesPp => x => x.InvalidVotesPp,
                StateElectionRoundSortableField.InvalidVotesRegisteredPp => x => x.InvalidVotesRegisteredPp,
                StateElectionRoundSortableField.NullVotesCount => x => x.NullVotesCount,
                StateElectionRoundSortableField.NullVotesPp => x => x.NullVotesPp,
                StateElectionRoundSortableField.NullVotesRegisteredPp => x => x.NullVotesRegisteredPp,
                StateElectionRoundSortableField.BlankVotesCount => x => x.BlankVotesCount,
                StateElectionRoundSortableField.BlankVotesPp => x => x.BlankVotesPp,
                StateElectionRoundSortableField.BlankVotesRegisteredPp => x => x.BlankVotesRegisteredPp,
                StateElectionRoundSortableField.AnulledVotesCount => x => x.AnulledVotesCount,
                StateElectionRoundSortableField.AnulledVotesPp => x => x.AnulledVotesPp,
                StateElectionRoundSortableField.AnulledVotesRegisteredPp => x => x.AnulledVotesRegisteredPp,
                _ => x => x.State
            };

            return sortDirection == SortDirection.Ascending ? stateElectionRounds.OrderBy(keySelector) : stateElectionRounds.OrderByDescending(keySelector);
        }
    }
}
