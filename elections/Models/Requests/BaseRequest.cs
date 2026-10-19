using elections.Models.Pagination;

namespace elections.Models.Requests
{
    public abstract record BaseRequest
    {
        public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
        public SearchMode SearchMode { get; init; } = SearchMode.Exact;
    }
}
