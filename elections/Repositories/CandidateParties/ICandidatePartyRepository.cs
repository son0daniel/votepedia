using elections.Models.Entities;
using System.Linq.Expressions;

namespace elections.Repositories.CandidateParties
{
    public interface ICandidatePartyRepository : IRepository<CandidateParty>
    {
        Task<IEnumerable<CandidateParty>> GetCandidatesAndParties(Expression<Func<CandidateParty, bool>> expression);
        Task<IEnumerable<CandidateParty>> GetParties(Expression<Func<CandidateParty, bool>> expression);
    }
}
