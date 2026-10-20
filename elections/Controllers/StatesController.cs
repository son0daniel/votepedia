using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Services.States;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route(Routes.States.Base)]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<StateDto>>> GetStates([FromQuery] StateFilter stateFilter, [FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _stateService.GetStates(stateFilter, pagedRequest));
        }
    }
}
