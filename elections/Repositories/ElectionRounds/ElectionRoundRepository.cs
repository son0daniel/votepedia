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

        public async Task<ElectionRound?> GetElectionRound(string role, int year, int roundNr, long? municipalityId, long? stateId)
        {
            return await _dbSet.Where(
                x => x.NrRound == roundNr
                && string.Equals(x.Election.Role, role)
                && x.Election.Year == year
                && (!municipalityId.HasValue || x.Election.MunicipalityId == municipalityId.Value)
                && (!stateId.HasValue || x.Election.StateId == stateId.Value)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ElectionRound>> GetRoundsByElections(List<long> electionIds, int roundNr)
        {
            return await _dbSet.Where(x => electionIds.Contains(x.Election.Id) && x.NrRound == roundNr).ToListAsync();
        }
    }
}
