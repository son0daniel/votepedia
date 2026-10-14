using elections.Data;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace elections.Repositories.CandidateParties
{
    public class CandidatePartyRepository : Repository<CandidateParty>, ICandidatePartyRepository
    {
        public CandidatePartyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CandidateParty>> GetCandidatesAndParties(Expression<Func<CandidateParty, bool>> expression)
        {
            return await _dbSet.Where(expression)
            .Include(cp => cp.Candidate)
            .Include(cp => cp.Party)
            .ToListAsync();
        }

        public async Task<IEnumerable<CandidateParty>> GetParties(Expression<Func<CandidateParty, bool>> expression)
        {
            return await _dbSet.Where(expression)
            .Include(cp => cp.Party)
            .ToListAsync();
        }

        public async Task<IEnumerable<CandidateParty>> GetCandidates(Expression<Func<CandidateParty, bool>> expression)
        {
            return await _dbSet.Where(expression)
            .Include(cp => cp.Candidate)
            .ToListAsync();
        }
    }
}
