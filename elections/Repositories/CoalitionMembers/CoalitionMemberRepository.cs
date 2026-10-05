using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.CoalitionMembers
{
    public class CoalitionMemberRepository : Repository<CoalitionMember>, ICoalitionMemberRepository
    {
        public CoalitionMemberRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
