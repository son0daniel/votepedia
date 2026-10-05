using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.ElectionRounds
{
    public class ElectionRoundRepository : RepositoryWithUid<ElectionRound>, IElectionRoundRepository
    {
        public ElectionRoundRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
