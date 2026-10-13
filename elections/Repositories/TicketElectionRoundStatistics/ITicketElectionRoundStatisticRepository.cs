using elections.Models.Entities;
using elections.Models.Requests;

namespace elections.Repositories.TicketElectionRoundStatistics
{
    public interface ITicketElectionRoundStatisticRepository : IRepository<TicketElectionRoundStatistic>
    {
        Task<IEnumerable<TicketElectionRoundStatistic>> GetElectedTicketPresidentialElectionRoundStatistics(int? year, int? roundNr);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateTicketPresidentialElectionRoundStatistics(long candidateId, int? year, int? roundNr, bool? isElected);
    }
}
