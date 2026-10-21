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
    }
}
