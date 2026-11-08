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

        [HttpGet, Route(Routes.Parties.PartyMunicipalityElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<PartyElectionRoundRecord>>> GetPartyMunicipalityElectionRoundRecords([FromRoute] string uid, [FromRoute] long tseId, [FromQuery] ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var municipalityElectionRoundRecords = await _partyService.GetPartyMunicipalityElectionRoundRecords(uid, tseId, electionRoundRecordFilter);

            if (municipalityElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(municipalityElectionRoundRecords);
        }

        [HttpGet, Route(Routes.Parties.PartyMetropolitanAreaElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<PartyElectionRoundRecord>>> GetPartyMetropolitanAreaElectionRoundRecords([FromRoute] string uid, [FromRoute] string metropolitanAreaUid, [FromQuery] ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var metropolitanAreaElectionRoundRecords = await _partyService.GetPartyMetropolitanAreaElectionRoundRecords(uid, metropolitanAreaUid, electionRoundRecordFilter);

            if (metropolitanAreaElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(metropolitanAreaElectionRoundRecords);
        }

        [HttpGet, Route(Routes.Parties.PartyStateElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<PartyElectionRoundRecord>>> GetPartyStateElectionRoundRecords([FromRoute] string uid, [FromRoute] string stateAbbr, [FromQuery] ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var stateElectionRoundRecords = await _partyService.GetPartyStateElectionRoundRecords(uid, stateAbbr, electionRoundRecordFilter);

            if (stateElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(stateElectionRoundRecords);
        }

        [HttpGet, Route(Routes.Candidates.CandidateMacroregionElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<CandidateElectionRoundRecord>>> GetCandidateMacroregionElectionRoundRecords([FromRoute] string uid, [FromRoute] string macroregionAbbr, [FromQuery] ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var macroregionElectionRoundRecords = await _partyService.GetPartyMacroregionElectionRoundRecords(uid, macroregionAbbr, electionRoundRecordFilter);

            if (macroregionElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(macroregionElectionRoundRecords);
        }
    }
}
