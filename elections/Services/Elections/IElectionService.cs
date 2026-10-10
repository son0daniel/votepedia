using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;

namespace elections.Services.Elections
{
    public interface IElectionService
    {
        Task<IEnumerable<ElectionOverview>> GetElectionOverviews(ElectionFilter electionFilter, string role, string? municipalityUid, string? stateAbbr );
    }
}
