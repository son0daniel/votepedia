using elections.Models.DTOs;
using elections.Models.Requests;

namespace elections.Commons.Extensions
{
    public static class ElectionRoundRecordSorter
    {
        public static IEnumerable<TElectionRoundRecord> Sort<TElectionRoundRecord>(this IEnumerable<TElectionRoundRecord> electionRoundRecords, ElectionRoundRecordSortableField? sortBy, SortDirection sortDirection) where TElectionRoundRecord : ElectionRoundRecord
        {
            Func<TElectionRoundRecord, object?> keySelector = sortBy switch
            {
                ElectionRoundRecordSortableField.Year => x => x.Year,
                ElectionRoundRecordSortableField.Position => x => x.PositionNr,
                ElectionRoundRecordSortableField.Votes => x => x.VotesCount,
                ElectionRoundRecordSortableField.ValidVotes => x => x.ValidVotesPp,
                ElectionRoundRecordSortableField.TotalVotes => x => x.TotalVotesPp,
                ElectionRoundRecordSortableField.RegisteredVotes => x => x.RegisteredVotesPp,
                _ => x => x.Year
            };

            return sortDirection == SortDirection.Ascending ? electionRoundRecords.OrderBy(keySelector) : electionRoundRecords.OrderByDescending(keySelector);
        }
    }
}
