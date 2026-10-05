using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.Elections
{
    public class ElectionRepository : RepositoryWithUid<Election>, IElectionRepository
    {
        public ElectionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
