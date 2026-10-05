using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.Coalitions
{
    public class CoalitionRepository : RepositoryWithUid<Coalition>, ICoalitionRepository
    {
        public CoalitionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
