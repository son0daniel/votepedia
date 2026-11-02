namespace elections.Models.Requests.ElectionRounds
{
    public record MacroregionElectionRoundFilter : BaseRequest
    {
        public bool OnlyNational { get; init; } = false;
        public ElectionRoundSortableField? SortBy { get; init; }
    }
}
