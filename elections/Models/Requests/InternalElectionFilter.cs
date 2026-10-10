namespace elections.Models.Requests
{
    public record InternalElectionFilter
    {
        public long? MunicipalityId { get; init; }
        public long? StateId { get; init; }
        public int? Year { get; init; }
        public string? Role { get; init; }
        public int? Round { get; init; }
    }
}
