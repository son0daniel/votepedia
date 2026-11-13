using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.Entities;
using elections.Models.Requests;

namespace elections.Services.ElectionRoundRecords
{
    public interface IElectionRoundService
    {
        Task<(IEnumerable<Ticket>, IEnumerable<TicketCandidateParty>, IEnumerable<CandidateParty>)> GetTicketRelatedInformation(IEnumerable<TicketElectionRoundStatistic> ticketElectionRoundStatistics, int? levelNr);
        Task<IEnumerable<CandidatePartyElectionRoundRecord>> GetElectionRoundRecordsByLocation(BaseElectionRoundRecordFilter electionRoundRecordFilter, int? roleNr, AggregationLevel aggregationLevel, long? municipalityId = null, long? metropolitanAreaId = null, long? stateId = null, long? macroregionId = null);
        Task<ElectionRoundDto?> GetElectionOverallResults(ElectionRound electionRound, string role, AggregationLevel aggregationLevel, int ticketMembersCount, long? municipalityId = null, long? metropolitanAreaId = null, long? stateId = null, long? macroregionId = null);
    }
}
