using elections.Models.Entities;

namespace elections.Models.DTOs.ElectionOverviews
{
    public class ElectionOverview : BaseElectionOverview
    {
        public MinimalCandidateDto Candidate { get; set; } = default!;
        public MinimalPartyDto Party { get; set; } = default!;

        public static ElectionOverview New(Election election, ElectionRound electionRound, TicketElectionRoundStatistic ticketElectionRoundStatistic, Candidate candidate, Party party)
        {
           var response = New<ElectionOverview>(election, electionRound, ticketElectionRoundStatistic);

            response.Candidate = MinimalCandidateDto.New(candidate);
            response.Party = MinimalPartyDto.New(party);

            return response;
        }
    }
}
