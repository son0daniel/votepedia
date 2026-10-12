using elections.Data;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace elections.Repositories.ElectionRounds
{
    public class ElectionRoundRepository : RepositoryWithUid<ElectionRound>, IElectionRoundRepository
    {
        public ElectionRoundRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ElectionRound>> GetRoundsByElections(List<long> electionIds, int roundNr)
        {
            return await _dbSet.Where(x => electionIds.Contains(x.Election.Id) && x.NrRound == roundNr).ToListAsync();
        }
    }
}
