using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.States
{
    public class StateRepository : RepositoryWithUid<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
