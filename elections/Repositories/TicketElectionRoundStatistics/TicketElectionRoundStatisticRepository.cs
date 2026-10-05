using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.TicketElectionRoundStatistics
{
    public class TicketElectionRoundStatisticRepository : Repository<TicketElectionRoundStatistic>, ITicketElectionRoundStatisticRepository
    {
        public TicketElectionRoundStatisticRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
