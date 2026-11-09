namespace elections.Models.Requests
{
    public abstract record BaseElectionRoundRecordFilter
    {
        public int? Year { get; init; }
        public int? Round { get; init; }
        public int? Status { get; init; }
        public int? Level { get; init; }
        public int? Position { get; init; }
        public ElectionRoundRecordSortableField? SortBy { get; init; }
        public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
    }
}
