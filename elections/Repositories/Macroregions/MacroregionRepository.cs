using elections.Data;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace elections.Repositories.Macroregions
{
    public class MacroregionRepository : RepositoryWithUid<Macroregion>, IMacroregionRepository
    {
        public MacroregionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Macroregion?> GetMacroregionByAbbr(string abbr)
        {
            return await _dbSet.Where(x => string.Equals(x.Abbr, abbr)).FirstOrDefaultAsync();
        }

        public async Task<string?> GetMacroregionAbbr(long id)
        {
            return await _dbSet.Where(x => x.Id == id).Select(x => x.Abbr).FirstOrDefaultAsync();
        }
    }
}
