namespace elections.Models.Requests.ElectionRounds
{
    public record MetropolitanAreaElectionRoundFilter : BaseRequest
    {
        public string? State { get; init; }
        public string? Macroregion { get; init; }
        public MetropolitanAreaElectionRoundSortableField? SortBy { get; init; }
    }
}
