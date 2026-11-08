using Microsoft.AspNetCore.Mvc;

namespace elections.Models.Requests
{
    public record BaseMunicipalityFilter : BaseRequest
    {
        public string? Name { get; init; }
        [FromQuery(Name = "state")] public List<string> States { get; set; } = [];
        [FromQuery(Name = "metropolitanArea")] public List<string> MetropolitanAreas { get; init; } = [];
        [FromQuery(Name = "macroregion")] public List<string> Macroregions { get; set; } = [];
        public bool OnlyNational { get; init; } = false;
    }
}
