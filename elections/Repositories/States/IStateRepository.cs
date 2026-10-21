using elections.Models.Entities;

namespace elections.Repositories.States
{
    public interface IStateRepository : IRepositoryWithUid<State>
    {
        Task<State?> GetStateByAbbr(string abbr);
        Task<string?> GetStateAbbr(long id);
    }
}
