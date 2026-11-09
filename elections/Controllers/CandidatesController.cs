using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Services.Candidates;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route(Routes.Candidates.Base)]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<CandidateDto>>> GetCandidates([FromQuery] CandidateFilter candidateFilter, [FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _candidateService.GetCandidates(candidateFilter, pagedRequest));
        }

        [HttpGet, Route(Routes.Candidates.GetOne)]
        public async Task<ActionResult<CandidateDto>> GetCandidate([FromRoute] string uid)
        {
            var candidate = await _candidateService.GetCandidate(uid);

            return candidate is null ? NotFound() : Ok(candidate);
        }

        [HttpGet, Route(Routes.Candidates.PresidentialElectionOverviews)]
        public async Task<ActionResult<IEnumerable<CandidateElectionOverview>>> GetCandidatePresidentialElectionOverviews([FromRoute] string uid, [FromQuery] ElectionOverviewFilter electionOverviewFilter, [FromQuery] bool? isElected)
        {
            return Ok(await _candidateService.GetCandidatePresidentialElectionOverviews(uid, electionOverviewFilter, isElected));
        }

        [HttpGet, Route(Routes.Candidates.CandidateMunicipalityElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<CandidateElectionRoundRecord>>> GetCandidateMunicipalityElectionRoundRecords([FromRoute] string uid, [FromRoute] long tseId, [FromQuery] ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var municipalityElectionRoundRecords = await _candidateService.GetCandidateMunicipalityElectionRoundRecords(uid, tseId, electionRoundRecordFilter);

            if (municipalityElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(municipalityElectionRoundRecords);
        }

        [HttpGet, Route(Routes.Candidates.CandidateMetropolitanAreaElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<CandidateElectionRoundRecord>>> GetCandidateMetropolitanAreaElectionRoundRecords([FromRoute] string uid, [FromRoute] string metropolitanAreaUid, [FromQuery] ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var metropolitanAreaElectionRoundRecords = await _candidateService.GetCandidateMetropolitanAreaElectionRoundRecords(uid, metropolitanAreaUid, electionRoundRecordFilter);

            if (metropolitanAreaElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(metropolitanAreaElectionRoundRecords);
        }

        [HttpGet, Route(Routes.Candidates.CandidateStateElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<CandidateElectionRoundRecord>>> GetCandidateStateElectionRoundRecords([FromRoute] string uid, [FromRoute] string stateAbbr, [FromQuery] ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var stateElectionRoundRecords = await _candidateService.GetCandidateStateElectionRoundRecords(uid, stateAbbr, electionRoundRecordFilter);

            if (stateElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(stateElectionRoundRecords);
        }

        [HttpGet, Route(Routes.Candidates.CandidateMacroregionElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<CandidateElectionRoundRecord>>> GetCandidateMacroregionElectionRoundRecords([FromRoute] string uid, [FromRoute] string macroregionAbbr, [FromQuery] ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var macroregionElectionRoundRecords = await _candidateService.GetCandidateMacroregionElectionRoundRecords(uid, macroregionAbbr, electionRoundRecordFilter);

            if (macroregionElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(macroregionElectionRoundRecords);
        }

        [HttpGet, Route(Routes.Candidates.CandidatePresidentialElectionRoundRecords)]
        public async Task<ActionResult<IEnumerable<CandidateElectionRoundRecord>>> GetCandidatePresidentialElectionRoundRecords([FromRoute] string uid, [FromQuery] PresidentialElectionRoundRecordFilter presidentialElectionRoundRecordFilter)
        {
            var presidentialElectionRoundRecords = await _candidateService.GetCandidatePresidentialElectionRoundRecords(uid, presidentialElectionRoundRecordFilter);

            if (presidentialElectionRoundRecords is null)
            {
                return NotFound();
            }

            return Ok(presidentialElectionRoundRecords);
        }
    }
}
