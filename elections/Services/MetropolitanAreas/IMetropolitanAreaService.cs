using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;

namespace elections.Services.MetropolitanAreas
{
    public interface IMetropolitanAreaService
    {
        Task<PagedResponse<MetropolitanAreaDto>> GetMetropolitanAreas(MetropolitanAreaFilter metropolitanAreaFilter, PagedRequest pagedRequest);
        Task<MetropolitanAreaDto?> GetMetropolitanArea(string uid);
        Task<IEnumerable<CandidatePartyElectionRoundRecord>?> GetMetropolitanAreaElectionRoundRecords(string uid, ElectionRoundRecordFilter electionRoundRecordFilter);
        Task<ElectionRoundDto?> GetMetropolitanAreaPresidentialElectionRoundResults(string uid, int year, int roundNr);
    }
}
