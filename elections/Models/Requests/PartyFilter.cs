namespace elections.Models.Requests
{
    public record PartyFilter
    {
        public string? Name { get; init; }
        public string? Abbr { get; init; }
        public int? Nr { get; set; }
        public bool? IsActive { get; init; }
        public PartySortableField? SortBy { get; init; }
        public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
    }
}
