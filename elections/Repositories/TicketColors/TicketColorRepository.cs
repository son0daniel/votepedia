using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.TicketColors
{
    public class TicketColorRepository : RepositoryWithUid<TicketColor>, ITicketColorRepository
    {
        public TicketColorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
