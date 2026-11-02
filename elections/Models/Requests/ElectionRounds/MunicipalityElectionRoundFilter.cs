namespace elections.Models.Requests.ElectionRounds
{
    public record MunicipalityElectionRoundFilter : BaseRequest
    {
        public string? MetropolitanArea { get; init; }
        public string? State { get; init; }
        public string? Macroregion { get; init; }
        public bool OnlyNational { get; init; } = false;
        public MunicipalityElectionRoundSortableField? SortBy { get; init; }
    }
}
