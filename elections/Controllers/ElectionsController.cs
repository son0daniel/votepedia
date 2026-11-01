using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;
using elections.Models.Requests.ElectionRounds;
using elections.Services.Elections;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route(Routes.Elections.Base)]
    [ApiController]
    public class ElectionsController : ControllerBase
    {
        private readonly IElectionService _electionService;

        public ElectionsController(IElectionService electionService)
        {
            _electionService = electionService;
        }

        [HttpGet, Route(Routes.Elections.PresidentialElectionOverviews)]
        public async Task<ActionResult<IEnumerable<ElectionOverview>>> GetElectionOverviews([FromQuery] ElectionOverviewFilter electionOverviewFilter, [FromQuery] int? level)
        {
            var electionOverviews = await _electionService.GetPresidentialElectionOverviews(electionOverviewFilter, level);

            return Ok(electionOverviews);
        }

        [HttpGet, Route(Routes.Elections.PresidentialElectionRoundOverallResults)]
        public async Task<ActionResult<ElectionRoundDto>> GetPresidentialElectionOverallResults([FromRoute] int year, [FromRoute] int roundNr)
        {
            var electionResults = await _electionService.GetPresidentialElectionOverallResults(year, roundNr);

            if (electionResults is null)
            {
                return NotFound();
            }

            return Ok(electionResults);
        }

        [HttpGet, Route(Routes.Elections.PresidentialElectionRoundMunicipalityAggreationResults)]
        public async Task<ActionResult<IEnumerable<MunicipalityElectionRoundDto>>> GetPresidentialElectionRoundMunicipalityResults([FromRoute] int year, [FromRoute] int roundNr, [FromQuery] MunicipalityElectionRoundFilter municipalityFilter)
        {
            var presidentialElectionMunicipalityResults = await _electionService.GetPresidentialElectionMunicipalityLevelResults(year, roundNr, municipalityFilter);

            if (presidentialElectionMunicipalityResults is null)
            {
                return NotFound();
            }

            return Ok(presidentialElectionMunicipalityResults);
        }
    }
}
