using elections.Data;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace elections.Repositories.States
{
    public class StateRepository : RepositoryWithUid<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<State?> GetStateByAbbr(string abbr)
        {
            return await _dbSet.Where(x => string.Equals(x.Abbr, abbr)).FirstOrDefaultAsync();
        }

        public async Task<string?> GetStateAbbr(long id)
        {
            return await _dbSet.Where(x => x.Id == id).Select(x => x.Abbr).FirstOrDefaultAsync();
        }
    }
}
