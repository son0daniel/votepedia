namespace elections.Models.Requests
{
    public record CandidateFilter
    {
        public bool? IsDeceased { get; init; }

        public CandidateSortableField SortBy { get; init; }

        public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
    }
}
