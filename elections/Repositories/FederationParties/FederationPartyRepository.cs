using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.FederationParties
{
    public class FederationPartyRepository : Repository<FederationParty>, IFederationPartyRepository
    {
        public FederationPartyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
