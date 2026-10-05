using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.Municipalities
{
    public class MunicipalityRepository : RepositoryWithUid<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
