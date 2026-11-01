namespace elections.Models.Requests.ElectionRounds
{
    public record StateElectionRoundFilter : BaseRequest
    {
        public string? Macroregion { get; init; }
        public StateElectionRoundSortableField? SortBy { get; init; }
    }
}
