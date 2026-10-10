namespace elections.Models.Requests
{
    public record ElectionFilter
    {
        public int? Year { get; init; }
        public int? Round { get; init; }
        public ElectionOverviewSortableField SortBy { get; init; }
        public SortDirection SortDirection { get; init; } = SortDirection.Descending;
    }
}
