using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class MinimalTicketDto(string ticketUid, TicketElectionRoundStatistic ticketElectionRoundStatistic) : CandidacyRecord(ticketElectionRoundStatistic)
    {
        public string Id { get; } = ticketUid;
    }
}
