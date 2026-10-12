using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;
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
        public async Task<ActionResult<IEnumerable<ElectionOverview>>> GetElectionOverviews([FromQuery] ElectionOverviewFilter electionOverviewFilter, [FromQuery] int? level)
        {
            var electionOverviews = await _electionService.GetPresidentialElectionOverviews(electionOverviewFilter, level);

            return Ok(electionOverviews);
        }
    }
}
