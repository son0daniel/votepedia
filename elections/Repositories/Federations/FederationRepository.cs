using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.Federations
{
    public class FederationRepository : RepositoryWithUid<Federation>, IFederationRepository
    {
        public FederationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
