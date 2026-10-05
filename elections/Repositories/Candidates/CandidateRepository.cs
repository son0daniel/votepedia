using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.Candidates
{
    public class CandidateRepository : RepositoryWithUid<Candidate>, ICandidateRepository
    {
        public CandidateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
