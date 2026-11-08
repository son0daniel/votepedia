namespace elections.Models.Requests
{
    public record ElectionRoundRecordFilter
    {
        public int? Year { get; init; }
        public int? Round { get; init; }
        public int? Role { get; init; }
        public int? Status { get; init; }
        public int? Level { get; init; }
        public int? Position { get; init; }
        public ElectionRoundRecordSortableField? SortBy { get; init; }
        public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
    }
}
