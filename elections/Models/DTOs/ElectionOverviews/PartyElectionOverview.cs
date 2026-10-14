using elections.Models.Entities;

namespace elections.Models.DTOs.ElectionOverviews
{
    public class PartyElectionOverview : BaseElectionOverview
    {
        public List<TicketCandidateDto> Candidates { get; set; } = [];
        public string Status { get; set; } = string.Empty;

        public static PartyElectionOverview New(Election election, ElectionRound electionRound, TicketElectionRoundStatistic ticketElectionRoundStatistic, IEnumerable<TicketCandidateDto> candidates)
        {
            var response = New<PartyElectionOverview>(election, electionRound, ticketElectionRoundStatistic);

            response.Candidates = [.. candidates];

            response.Status = ticketElectionRoundStatistic.TicketStatus;

            return response;
        }
    }
}
