using elections.Models.Entities;

namespace elections.Models.DTOs.ElectionOverviews
{
    public class CandidateElectionOverview : BaseElectionOverview
    {
        public string Role { get; set; } = string.Empty;
        public MinimalPartyDto Party { get; set; } = default!;
        public string Status { get; set; } = string.Empty;

        public static CandidateElectionOverview New(Election election, ElectionRound electionRound, TicketElectionRoundStatistic ticketElectionRoundStatistic, string role, Party party)
        {
            var response = New<CandidateElectionOverview>(election, electionRound, ticketElectionRoundStatistic);

            response.Role = role;

            response.Status = ticketElectionRoundStatistic.TicketStatus;

            response.Party = new MinimalPartyDto(party);

            return response;
        }
    }
}
