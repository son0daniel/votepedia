namespace elections.Models.Pagination
{
    public record PagedRequest
    {
        public int PageIndex { get; init; } = 0;
        public int PageSize { get; init; } = 25;
    }
}
