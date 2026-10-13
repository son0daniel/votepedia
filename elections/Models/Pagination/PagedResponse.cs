using System.Text.Json.Serialization;

namespace elections.Models.Pagination
{
    public class PagedResponse<T>(List<T> items, int totalCount, PagedRequest pagedRequest)
    {
        public List<T> Items { get; set; } = items;
        [JsonPropertyName("page_index")] public int PageIndex { get; set; } = pagedRequest.PageIndex;
        [JsonPropertyName("page_size")] public int PageSize { get; set; } = pagedRequest.PageSize;
        [JsonPropertyName("total_count")] public int TotalCount { get; set; } = totalCount;
        [JsonPropertyName("total_pages")] public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        [JsonPropertyName("has_next_page")] public bool HasNextPage => (PageIndex + 1) * PageSize < TotalCount;
        [JsonPropertyName("has_previous_page")] public bool HasPreviousPage => PageIndex > 0;

    }
}
