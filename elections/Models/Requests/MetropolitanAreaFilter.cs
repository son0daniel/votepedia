using Microsoft.AspNetCore.Mvc;

namespace elections.Models.Requests
{
    public record MetropolitanAreaFilter : BaseRequest
    {
        public string? Name { get; init; }
        public int? Type { get; init; }
        [FromQuery(Name = "state")] public List<string> States { get; set; } = [];
        [FromQuery(Name = "macroregion")] public List<string> Macroregions { get; set; } = [];
        public MetropolitanAreaSortableField? SortBy { get; init; }
    }
}
