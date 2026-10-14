using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Pagination;
using elections.Models.Requests;

namespace elections.Services.Parties
{
    public interface IPartyService
    {
        Task<PagedResponse<PartyDto>> GetParties(PartyFilter partyFilter, PagedRequest pagedRequest);
        Task<IEnumerable<PartyElectionOverview>> GetPartyPresidentialElectionOverviews(string partyUid, ElectionOverviewFilter electionOverviewFilter, bool? isElected);
    }
}
