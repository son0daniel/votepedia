using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace elections.Models.Requests
{
    public record MunicipalityFilter : BaseRequest
    {
        public string? Name { get; init; }
        [FromQuery(Name = "state")] public List<string> States { get; set; } = [];
        [FromQuery(Name = "metropolitanArea")] public List<string> MetropolitanAreas { get; init; } = [];
        [FromQuery(Name = "macroregion")] public List<string> Macroregions { get; set; } = [];
        public bool OnlyNational { get; init; } = false;
        public MunicipalitySortableField? SortBy { get; init; }
    }
}
