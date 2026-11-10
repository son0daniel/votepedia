using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;
using elections.Models.Requests.ElectionRounds;

namespace elections.Services.Elections
{
    public interface IElectionService
    {
        Task<IEnumerable<ElectionOverview>> GetPresidentialElectionOverviews(ElectionOverviewFilter electionOverviewFilter, int? levelNr);
        Task<ElectionRoundDto?> GetPresidentialElectionOverallResults(int year, int roundNr);
        Task<IEnumerable<MunicipalityElectionRoundDto>?> GetPresidentialElectionMunicipalityLevelResults(int year, int roundNr, MunicipalityElectionRoundFilter municipalityElectionRoundFilter);
        Task<IEnumerable<MetropolitanAreaElectionRoundDto>?> GetPresidentialElectionMetropolitanAreaLevelResults(int year, int roundNr, MetropolitanAreaElectionRoundFilter metropolitanAreaElectionRoundFilter);
        Task<IEnumerable<StateElectionRoundDto>?> GetPresidentialElectionStateLevelResults(int year, int roundNr, StateElectionRoundFilter stateElectionRoundFilter);
        Task<IEnumerable<MacroregionElectionRoundDto>?> GetPresidentialElectionMacroregionLevelResults(int year, int roundNr, MacroregionElectionRoundFilter macroregionElectionRoundFilter);
        Task<IEnumerable<CandidatePartyElectionRoundRecord>> GetPresidentialElectionRoundRecords(PresidentialElectionRoundRecordFilter presidentialElectionRoundRecordFilter);
    }
}
