using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.CandidateParties
{
    public class CandidatePartyRepository : Repository<CandidateParty>, ICandidatePartyRepository
    {
        public CandidatePartyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
