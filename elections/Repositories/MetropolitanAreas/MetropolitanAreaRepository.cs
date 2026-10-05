using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.MetropolitanAreas
{
    public class MetropolitanAreaRepository : RepositoryWithUid<MetropolitanArea>, IMetropolitanAreaRepository
    {
        public MetropolitanAreaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
