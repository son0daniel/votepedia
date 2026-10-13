using elections.Models.DTOs;
using elections.Models.Requests;
using elections.Services.Candidates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDto>>> GetCandidates([FromQuery] CandidateFilter candidateFilter)
        {
            return Ok(await _candidateService.GetCandidates(candidateFilter));
        }

        [HttpGet]
        [Route("{uid:guid}/elections/role/president/overviews")]
        public async Task<ActionResult<IEnumerable<CandidateDto>>> GetCandidates([FromRoute] string uid, [FromQuery] ElectionOverviewFilter electionOverviewFilter, [FromQuery] bool? isElected)
        {
            return Ok(await _candidateService.GetCandidatePresidentialElectionOverviews(uid, electionOverviewFilter, isElected));
        }
    }
}
