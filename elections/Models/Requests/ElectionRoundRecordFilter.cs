namespace elections.Models.Requests
{
    public record ElectionRoundRecordFilter : BaseElectionRoundRecordFilter
    {
        public int? Role { get; init; }
    }
}
