using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;

namespace elections.Services.States
{
    public interface IStateService
    {
        Task<PagedResponse<StateDto>> GetStates(StateFilter stateFilter, PagedRequest pagedRequest);
        Task<StateDto?> GetState(string abbr);
        Task<IEnumerable<CandidatePartyElectionRoundRecord>?> GetStateElectionRoundRecords(string stateAbbr, ElectionRoundRecordFilter electionRoundRecordFilter);
        Task<ElectionRoundDto?> GetStatePresidentialElectionRoundResults(string abbr, int year, int roundNr);
    }
}
