using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Services.MetropolitanAreas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace elections.Controllers
{
    [Route(Routes.MetropolitanAreas.Base)]
    [ApiController]
    public class MetropolitanAreasController : ControllerBase
    {
        private readonly IMetropolitanAreaService _metropolitanAreaService;

        public MetropolitanAreasController(IMetropolitanAreaService metropolitanAreaService)
        {
            _metropolitanAreaService = metropolitanAreaService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<MetropolitanAreaDto>>> GetMetropolitanAreas([FromQuery] MetropolitanAreaFilter metropolitanAreaFilter, [FromQuery] PagedRequest pagedRequest)
        {
            return Ok(await _metropolitanAreaService.GetMetropolitanAreas(metropolitanAreaFilter, pagedRequest));
        }

        [HttpGet, Route(Routes.MetropolitanAreas.GetOne)]
        public async Task<ActionResult<MetropolitanAreaDto>> GetMetropolitanArea([FromRoute] string uid)
        {
            var metropolitanArea = await _metropolitanAreaService.GetMetropolitanArea(uid);

            return metropolitanArea is null ? NotFound() : Ok(metropolitanArea);
        }

    }
}
