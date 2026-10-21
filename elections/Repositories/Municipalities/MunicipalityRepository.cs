using elections.Data;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace elections.Repositories.Municipalities
{
    public class MunicipalityRepository : RepositoryWithUid<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Municipality?> GetMunicipalityByTseId(long tseId)
        {
            return await _dbSet.Where(x => x.TseId == tseId).FirstOrDefaultAsync();
        }
    }
}
