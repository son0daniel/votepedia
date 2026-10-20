using Microsoft.AspNetCore.Mvc;

namespace elections.Models.Requests
{
    public record StateFilter : BaseRequest
    {
        public string? Name { get; init; }
        [FromQuery(Name = "macroregion")] public List<string> Macroregions { get; set; } = [];
        public StateSortableField? SortBy { get; init; }
    }
}
