using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.Tickets
{
    public class TicketRepository : RepositoryWithUid<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
