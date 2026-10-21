using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Services.Macroregions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route(Routes.Macroregions.Base)]
    [ApiController]
    public class MacroregionsController : ControllerBase
    {
        private readonly IMacroregionService _macroregionService;

        public MacroregionsController(IMacroregionService macroregionService)
        {
            _macroregionService = macroregionService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<MacroregionDto>>> GetMacroregions([FromQuery] MacroregionFilter macroregionFilter, [FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _macroregionService.GetMacroregions(macroregionFilter, pagedRequest));
        }
    }
}
