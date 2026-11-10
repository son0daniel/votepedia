using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.Requests;

namespace elections.Services.ElectionRoundRecords
{
    public interface IElectionRoundRecordService
    {
        Task<IEnumerable<CandidatePartyElectionRoundRecord>> GetElectionRoundRecordsByLocation(BaseElectionRoundRecordFilter electionRoundRecordFilter, int? roleNr, AggregationLevel aggregationLevel, long? municipalityId = null, long? metropolitanAreaId = null, long? stateId = null, long? macroregionId = null);
    }
}
