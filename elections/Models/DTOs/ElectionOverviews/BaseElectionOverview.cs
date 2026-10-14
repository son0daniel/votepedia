using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs.ElectionOverviews
{
    public abstract class BaseElectionOverview(Election election, ElectionRound electionRound, TicketElectionRoundStatistic ticketElectionRoundStatistic) : ElectionRoundSummary(ticketElectionRoundStatistic)
    {
        public string Id { get; } = election.Uid;
        public int Year { get; } = election.Year;
        [JsonPropertyName("round_nr")] public int RoundNr { get; } = electionRound.NrRound;
        [JsonPropertyName("ticket_votes_count")] public int TicketVotesCount { get; } = ticketElectionRoundStatistic.TicketVotesCount;
        [JsonPropertyName("ticket_valid_votes_pp")] public float? TicketValidVotesPp { get; } = ticketElectionRoundStatistic.ValidVotesPp;
        [JsonPropertyName("ticket_total_votes_pp")] public float? TicketTotalVotesPp { get; } = ticketElectionRoundStatistic.TicketTotalVotesPp;
        [JsonPropertyName("ticket_registered_votes_pp")] public float TicketRegisteredVotesPp { get; } = ticketElectionRoundStatistic.TicketRegisteredVotesPp;
    }
}
