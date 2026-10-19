using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Services.Municipalities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route(Routes.Municipalities.Base)]
    [ApiController]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly IMunicipalityService _municipalityService;

        public MunicipalitiesController(IMunicipalityService municipalityService)
        {
            _municipalityService = municipalityService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<MunicipalityDto>>> GetMunicipalities([FromQuery] MunicipalityFilter municipalityFilter, [FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _municipalityService.GetMunicipalities(municipalityFilter, pagedRequest));
        }
    }
}
