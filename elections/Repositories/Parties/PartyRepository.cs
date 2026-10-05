using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.Parties
{
    public class PartyRepository : RepositoryWithUid<Party>, IPartyRepository
    {
        public PartyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
