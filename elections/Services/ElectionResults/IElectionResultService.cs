using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Entities;
using elections.Models.Requests;

namespace elections.Services.ElectionResults
{
    public interface IElectionResultService
    {
        Task<IEnumerable<BaseElectionOverview>> GetElectionOverviews(TicketElectionRoundStatistic ticketElectionRoundStatistic);
        Task <InternalElectionFilter> ResolveElectionFilter(ElectionFilter electionFilter, string role, string? municipalityUid, string? stateAbbr);
    }
}
