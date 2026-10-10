using elections.Models.Entities;
using elections.Models.Requests;

namespace elections.Repositories.TicketElectionRoundStatistics
{
    public interface ITicketElectionRoundStatisticRepository : IRepository<TicketElectionRoundStatistic>
    {
        Task<IEnumerable<TicketElectionRoundStatistic>> GetElectedTicketElectionRoundStatisticsByRound(InternalElectionFilter internalElectionFilter);
    }
}
