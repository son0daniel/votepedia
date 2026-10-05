using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.TicketCandidateParties
{
    public class TicketCandidatePartyRepository : Repository<TicketCandidateParty>, ITicketCandidatePartyRepository
    {
        public TicketCandidatePartyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
