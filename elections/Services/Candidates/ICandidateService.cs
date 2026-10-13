using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;

namespace elections.Services.Candidates
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetCandidates(CandidateFilter candidateFilter);
        Task<IEnumerable<CandidateElectionOverview>> GetCandidatePresidentialElectionOverviews(string candidateUid, ElectionOverviewFilter electionOverviewFilter, bool? isElected);
    }
}
