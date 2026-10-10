using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;

namespace elections.Commons.Extensions
{
    public static class ElectionOverviewSorter
    {
        public static IEnumerable<TOverview> Sort<TOverview>(this IEnumerable<TOverview> electionOverviews, ElectionOverviewSortableField sortBy, SortDirection sortDirection) where TOverview : BaseElectionOverview
        {
            Func<TOverview, object?> keySelector = sortBy switch
            {
                ElectionOverviewSortableField.Year => x => x.Year,
                ElectionOverviewSortableField.TicketVotesCount => x => x.TicketVotesCount,
                ElectionOverviewSortableField.TicketValidVotesPp => x => x.TicketValidVotesPp,
                ElectionOverviewSortableField.TicketTotalVotesPp => x => x.TicketTotalVotesPp,
                ElectionOverviewSortableField.TicketRegisteredVotesPp => x => x.TicketRegisteredVotesPp,
                ElectionOverviewSortableField.RegisteredVotersCount => x => x.RegisteredVotersCount,
                ElectionOverviewSortableField.TurnoutCount => x => x.TurnoutCount,
                ElectionOverviewSortableField.TurnoutPp => x => x.TurnoutPp,
                ElectionOverviewSortableField.AbstentionCount => x => x.AbstentionCount,
                ElectionOverviewSortableField.AbstentionPp => x => x.AbstentionPp,
                ElectionOverviewSortableField.ElectoralAlienationCount => x => x.ElectoralAlienationCount,
                ElectionOverviewSortableField.ElectoralAlienationPp => x => x.ElectoralAlienationPp,
                ElectionOverviewSortableField.ValidVotesCount => x => x.ValidVotesCount,
                ElectionOverviewSortableField.ValidVotesPp => x => x.ValidVotesPp,
                ElectionOverviewSortableField.ValidVotesRegisteredPp => x => x.ValidVotesRegisteredPp,
                ElectionOverviewSortableField.InvalidVotesCount => x => x.InvalidVotesCount,
                ElectionOverviewSortableField.InvalidVotesPp => x => x.InvalidVotesPp,
                ElectionOverviewSortableField.InvalidVotesRegisteredPp => x => x.InvalidVotesRegisteredPp,
                ElectionOverviewSortableField.NullVotesCount => x => x.NullVotesCount,
                ElectionOverviewSortableField.NullVotesPp => x => x.NullVotesPp,
                ElectionOverviewSortableField.NullVotesRegisteredPp => x => x.NullVotesRegisteredPp,
                ElectionOverviewSortableField.BlankVotesCount => x => x.BlankVotesCount,
                ElectionOverviewSortableField.BlankVotesPp => x => x.BlankVotesPp,
                ElectionOverviewSortableField.BlankVotesRegisteredPp => x => x.BlankVotesRegisteredPp,
                ElectionOverviewSortableField.AnulledVotesCount => x => x.AnulledVotesCount,
                ElectionOverviewSortableField.AnulledVotesPp => x => x.AnulledVotesPp,
                ElectionOverviewSortableField.AnulledVotesRegisteredPp => x => x.AnulledVotesRegisteredPp,
                _ => x => x.Year
            };

            return sortDirection == SortDirection.Ascending ? electionOverviews.OrderBy(keySelector) : electionOverviews.OrderByDescending(keySelector);
        }
    }
}
