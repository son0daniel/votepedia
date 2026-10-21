namespace elections.Models.Requests
{
    public record MacroregionFilter : BaseRequest
    {
        public string? Name { get; init; }
        public MacroregionSortableField? SortBy { get; init; }
    }
}
