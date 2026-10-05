using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.TicketElectionRounds
{
    public class TicketElectionRoundRepository : Repository<TicketElectionRound>, ITicketElectionRoundRepository
    {
        public TicketElectionRoundRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
