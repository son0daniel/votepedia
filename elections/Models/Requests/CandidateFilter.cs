namespace elections.Models.Requests
{
    public record CandidateFilter
    {
        public string? Name { get; set; }
        public bool? IsDeceased { get; init; }

        public CandidateSortableField? SortBy { get; init; }

        public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
    }
}
