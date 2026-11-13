using elections.Models.Entities;
using elections.Models.Requests;

namespace elections.Repositories.TicketElectionRoundStatistics
{
    public interface ITicketElectionRoundStatisticRepository : IRepository<TicketElectionRoundStatistic>
    {
        Task<IEnumerable<TicketElectionRoundStatistic>> GetElectedTicketPresidentialElectionRoundStatistics(int? year, int? roundNr);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateTicketPresidentialElectionRoundStatistics(long candidateId, int? year, int? roundNr, int? levelNr, int? positionNr, string? status, bool isOverview);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyTicketPresidentialElectionRoundStatistics(long partyId, int? year, int? roundNr, int? levelNr, int? positionNr, string? status, bool isOverview);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetElectionRoundStatistics(long electionRoundId);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMunicipalityLevelResults(long electionRoundId, long? metropolitanAreaId, long? stateId, long? macroregionId, bool onlyNational);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMetropolitanAreaLevelResults(long electionRoundId, long? stateId, long? macroregionId);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetStateLevelResults(long electionRoundId, long? macroregionId, bool onlyNational);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMacroregionLevelResults(long electionRoundId, bool onlyNational);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateMunicipalityStatistics(long candidateId, long municipalityId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateMetropolitanAreaStatistics(long candidateId, long metropolitanAreaId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateStateStatistics(long candidateId, long stateId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateMacroregionStatistics(long candidateId, long macroregionId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyMunicipalityStatistics(long partyId, long municipalityId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyMetropolitanAreaStatistics(long partyId, long metropolitanAreaId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyStateStatistics(long partyId, long stateId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyMacroregionStatistics(long partyId, long macroregionId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMunicipalityStatistics(long municipalityId, int? year, string? role, int? roundNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMetropolitanAreaStatistics(long metropolitanAreaId, int? year, string? role, int? roundNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetStateStatistics(long stateId, int? year, string? role, int? roundNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMacroregionStatistics(long macroregionId, int? year, string? role, int? roundNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetPresidentialElectionStatistics(int? year, int? roundNr, int? positionNr, string? status);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMunicipalityStatisticsByElectionRound(long municipalityId, long electionRoundId);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMetropolitanAreaStatisticsByElectionRound(long metropolitanAreaId, long electionRoundId);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetStateStatisticsByElectionRound(long stateId, long electionRoundId);
        Task<IEnumerable<TicketElectionRoundStatistic>> GetMacroregionStatisticsByElectionRound(long macroregionId, long electionRoundId);
    }
}
