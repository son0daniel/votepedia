using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.Entities;
using elections.Models.Requests;
using elections.Repositories;

namespace elections.Services.ElectionRoundRecords
{
    public class ElectionRoundRecordService : IElectionRoundRecordService
    {
        private readonly IRepositoryManager _repository;

        public ElectionRoundRecordService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CandidatePartyElectionRoundRecord>> GetElectionRoundRecordsByLocation(BaseElectionRoundRecordFilter electionRoundRecordFilter, int? roleNr, AggregationLevel aggregationLevel, long? municipalityId = null, long? metropolitanAreaId = null, long? stateId = null, long? macroregionId = null)
        {
            var year = electionRoundRecordFilter.Year;
            var role = RoleResolver.GetRole(roleNr);
            var roundNr = electionRoundRecordFilter.Round;
            var levelNr = electionRoundRecordFilter.Level;
            var positionNr = electionRoundRecordFilter.Position;
            var status = StatusResolver.GetStatus(electionRoundRecordFilter.Status);
            Dictionary<long, Election>? elections = null;

            List<TicketElectionRoundStatistic> ticketElectionRoundStatistics = [];

            if (aggregationLevel == AggregationLevel.Municipality)
            {
                if (!municipalityId.HasValue)
                {
                    throw new InvalidOperationException("Municipality id is required when the aggregation level is municipality.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMunicipalityStatistics(municipalityId.Value, year, role, roundNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.MetropolitanArea)
            {
                if (!metropolitanAreaId.HasValue)
                {
                    throw new InvalidOperationException("Metropolitan area id is required when the aggregation level is metropolitan area.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMetropolitanAreaStatistics(metropolitanAreaId.Value, year, role, roundNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.State)
            {
                if (!stateId.HasValue)
                {
                    throw new InvalidOperationException("State id is required when the aggregation level is state.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetStateStatistics(stateId.Value, year, role, roundNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.Macroregion)
            {
                if (!macroregionId.HasValue)
                {
                    throw new InvalidOperationException("Macroregion id is required when the aggregation level is macroregion.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMacroregionStatistics(macroregionId.Value, year, role, roundNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.Overall)
            {
                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetPresidentialElectionStatistics(year, roundNr, positionNr, status)];
            }

            if (ticketElectionRoundStatistics.Count == 0)
            {
                return [];
            }

            if (!year.HasValue || string.IsNullOrEmpty(role))
            {
                elections = (await _repository.Election.Get(x => (!year.HasValue || x.Year == year.Value) && (string.IsNullOrEmpty(role) || string.Equals(x.Role, role)))).ToDictionary(x => x.Id);
            }

            var electionRoundIds = ticketElectionRoundStatistics.Select(x => x.ElectionRoundId).ToList();
            var electionRounds = (await _repository.ElectionRound.Get(x => electionRoundIds.Contains(x.Id))).ToDictionary(x => x.Id);

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).ToList();
            var ticketCandidateParties = (await _repository.TicketCandidateParty.Get(x => ticketIds.Contains(x.TicketId) && x.NrLevel == (levelNr ?? 1))).ToList();

            if (ticketCandidateParties.Count == 0)
            {
                throw new InvalidOperationException("No ticket candidate parties were found.");
            }
            var ticketCandidatePartiesDict = ticketCandidateParties.ToDictionary(x => x.TicketId);

            var coalitions = (await _repository.Coalition.Get(x => ticketIds.Contains(x.TicketId))).ToDictionary(x => x.TicketId);

            var candidatePartyIds = ticketCandidateParties.Select(x => x.CandidatePartyId).ToList();
            var candidateParties = (await _repository.CandidateParty.GetCandidatesAndParties(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

            List<CandidatePartyElectionRoundRecord> electionRoundRecords = [];

            foreach (var ticketElectionRoundStatistic in ticketElectionRoundStatistics)
            {
                var electionRoundId = ticketElectionRoundStatistic.ElectionRoundId;
                var ticketId = ticketElectionRoundStatistic.TicketId;

                var electionRound = electionRounds.GetValueOrDefault(electionRoundId) ?? throw new InvalidOperationException($"Missing election round with id {electionRoundId}");

                var electionYear = year ?? elections?.GetValueOrDefault(electionRound.ElectionId)?.Year ?? throw new InvalidOperationException("Election year could not be obtained.");
                var electionRole = role ?? elections?.GetValueOrDefault(electionRound.ElectionId)?.Role ?? throw new InvalidOperationException("Election role could not be obtained.");

                var ticketCandidateParty = ticketCandidatePartiesDict.GetValueOrDefault(ticketId) ?? throw new InvalidOperationException($"Missing ticket candidate party from ticket with id {ticketId}");

                var candidatePartyId = ticketCandidateParty.CandidatePartyId;
                var candidateParty = candidateParties.GetValueOrDefault(candidatePartyId) ?? throw new InvalidOperationException($"Missing candidate party with id {candidatePartyId}");

                var coalition = coalitions.GetValueOrDefault(ticketId);

                electionRoundRecords.Add(new CandidatePartyElectionRoundRecord(candidateParty.Candidate, candidateParty.Party, RoleResolver.GetOfficeRole(electionRole, ticketCandidateParty.NrLevel), electionYear, electionRound.NrRound, coalition, ticketElectionRoundStatistic));
            }

            return electionRoundRecords.Sort(electionRoundRecordFilter.SortBy, electionRoundRecordFilter.SortDirection);
        }
    }
}
