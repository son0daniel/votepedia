using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;

namespace elections.Services.Elections
{
    public interface IElectionService
    {
        Task<IEnumerable<ElectionOverview>> GetPresidentialElectionOverviews(ElectionOverviewFilter electionOverviewFilter, int? levelNr);
    }
}
