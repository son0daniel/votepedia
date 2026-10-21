using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Services.Parties;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route(Routes.Parties.Base)]
    [ApiController]
    public class PartiesController : ControllerBase
    {
        private readonly IPartyService _partyService;

        public PartiesController(IPartyService partyService)
        {
            _partyService = partyService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<PartyDto>>> GetParties([FromQuery] PartyFilter partyFilter, [FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _partyService.GetParties(partyFilter, pagedRequest));
        }

        [HttpGet, Route(Routes.Parties.GetOne)]
        public async Task<ActionResult<PartyDto>> GetParty([FromRoute] string uid)
        {
            var party = await _partyService.GetParty(uid);

            return party is null ? NotFound() : Ok(party);
        }

        [HttpGet, Route(Routes.Parties.PresidentialElectionOverviews)]
        public async Task<ActionResult<IEnumerable<PartyElectionOverview>>> GetPartyPresidentialElectionOverviews([FromRoute] string uid, [FromQuery] ElectionOverviewFilter electionOverviewFilter, [FromQuery] bool? isElected)
        {
            return Ok(await _partyService.GetPartyPresidentialElectionOverviews(uid, electionOverviewFilter, isElected));
        }
    }
}
