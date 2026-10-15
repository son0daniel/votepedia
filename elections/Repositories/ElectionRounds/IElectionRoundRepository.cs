using elections.Models.Entities;

namespace elections.Repositories.ElectionRounds
{
    public interface IElectionRoundRepository : IRepositoryWithUid<ElectionRound>
    {
        Task<ElectionRound?> GetElectionRound(string role, int year, int roundNr, long? municipalityId, long? stateId);
        Task<IEnumerable<ElectionRound>> GetRoundsByElections(List<long> electionIds, int roundNr);
    }
}
