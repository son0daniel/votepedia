using elections.Commons.Constants;
using elections.Commons.Utils;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;
using elections.Services.ElectionResults;
using elections.Services.Elections;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionsController : ControllerBase
    {
        private readonly IElectionService _electionService;

        public ElectionsController(IElectionService electionService)
        {
            _electionService = electionService;
        }

        [HttpGet]
        [Route("role/president/overview")]
        public async Task<ActionResult<IEnumerable<ElectionOverview>>> GetElectionOverviews([FromQuery] ElectionFilter electionFilter)
        {
            var role = Constant.Election.Role.President;

            var electionOverviews = await _electionService.GetElectionOverviews(electionFilter, role, municipalityUid: null, stateAbbr: null);

            return Ok(electionOverviews);
        }
    }
}
