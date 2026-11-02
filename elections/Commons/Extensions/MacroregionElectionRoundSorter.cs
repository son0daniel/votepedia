using elections.Models.DTOs;
using elections.Models.Requests;
using elections.Models.Requests.ElectionRounds;
using static elections.Commons.Constants.Routes;

namespace elections.Commons.Extensions
{
    public static class MacroregionElectionRoundSorter
    {
        public static IEnumerable<MacroregionElectionRoundDto> Sort(this IEnumerable<MacroregionElectionRoundDto> macroregionElectionRounds, ElectionRoundSortableField? sortBy, SortDirection sortDirection)
        {
            Func<MacroregionElectionRoundDto, object?> keySelector = sortBy switch
            {
                ElectionRoundSortableField.RegisteredVotersCount => x => x.RegisteredVotersCount,
                ElectionRoundSortableField.TurnoutCount => x => x.TurnoutCount,
                ElectionRoundSortableField.TurnoutPp => x => x.TurnoutPp,
                ElectionRoundSortableField.AbstentionCount => x => x.AbstentionCount,
                ElectionRoundSortableField.AbstentionPp => x => x.AbstentionPp,
                ElectionRoundSortableField.ElectoralAlienationCount => x => x.ElectoralAlienationCount,
                ElectionRoundSortableField.ValidVotesCount => x => x.ValidVotesCount,
                ElectionRoundSortableField.ValidVotesPp => x => x.ValidVotesPp,
                ElectionRoundSortableField.ValidVotesRegisteredPp => x => x.ValidVotesRegisteredPp,
                ElectionRoundSortableField.InvalidVotesCount => x => x.InvalidVotesCount,
                ElectionRoundSortableField.InvalidVotesPp => x => x.InvalidVotesPp,
                ElectionRoundSortableField.InvalidVotesRegisteredPp => x => x.InvalidVotesRegisteredPp,
                ElectionRoundSortableField.NullVotesCount => x => x.NullVotesCount,
                ElectionRoundSortableField.NullVotesPp => x => x.NullVotesPp,
                ElectionRoundSortableField.NullVotesRegisteredPp => x => x.NullVotesRegisteredPp,
                ElectionRoundSortableField.BlankVotesCount => x => x.BlankVotesCount,
                ElectionRoundSortableField.BlankVotesPp => x => x.BlankVotesPp,
                ElectionRoundSortableField.BlankVotesRegisteredPp => x => x.BlankVotesRegisteredPp,
                ElectionRoundSortableField.AnulledVotesCount => x => x.AnulledVotesCount,
                ElectionRoundSortableField.AnulledVotesPp => x => x.AnulledVotesPp,
                ElectionRoundSortableField.AnulledVotesRegisteredPp => x => x.AnulledVotesRegisteredPp,
                _ => x => x.Macroregion
            };

            return sortDirection == SortDirection.Ascending ? macroregionElectionRounds.OrderBy(keySelector) : macroregionElectionRounds.OrderByDescending(keySelector);
        }
    }
}
