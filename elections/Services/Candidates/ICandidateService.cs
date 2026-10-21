using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Pagination;
using elections.Models.Requests;

namespace elections.Services.Candidates
{
    public interface ICandidateService
    {
        Task<PagedResponse<CandidateDto>> GetCandidates(CandidateFilter candidateFilter, PagedRequest pagedRequest);
        Task<CandidateDto?> GetCandidate(string uid);
        Task<IEnumerable<CandidateElectionOverview>> GetCandidatePresidentialElectionOverviews(string candidateUid, ElectionOverviewFilter electionOverviewFilter, bool? isElected);
    }
}
