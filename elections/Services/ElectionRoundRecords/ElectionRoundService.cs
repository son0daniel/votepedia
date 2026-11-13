using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.Entities;
using elections.Models.Requests;
using elections.Repositories;
using static elections.Commons.Constants.Constant.Election;
using static elections.Commons.Constants.Routes;

namespace elections.Services.ElectionRoundRecords
{
    public class ElectionRoundService : IElectionRoundService
    {
        private readonly IRepositoryManager _repository;

        public ElectionRoundService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        private async Task<List<TicketElectionRoundStatistic>> GetTicketElectionRoundStatistics(AggregationLevel aggregationLevel, long? municipalityId = null, long? metropolitanAreaId = null, long? stateId = null, long? macroregionId = null, long? electionRoundId = null, int? year = null, string? role = null, int? roundNr = null, int? positionNr = null, string? status = null)
        {
            List<TicketElectionRoundStatistic> ticketElectionRoundStatistics = [];

            if (aggregationLevel == AggregationLevel.Municipality)
            {
                if (!municipalityId.HasValue)
                {
                    throw new InvalidOperationException("Municipality id is required when the aggregation level is municipality.");
                }

                if (electionRoundId.HasValue)
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMunicipalityStatisticsByElectionRound(municipalityId.Value, electionRoundId.Value)];
                }
                else
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMunicipalityStatistics(municipalityId.Value, year, role, roundNr, positionNr, status)];
                }  
            }

            if (aggregationLevel == AggregationLevel.MetropolitanArea)
            {
                if (!metropolitanAreaId.HasValue)
                {
                    throw new InvalidOperationException("Metropolitan area id is required when the aggregation level is metropolitan area.");
                }

                if (electionRoundId.HasValue)
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMetropolitanAreaStatisticsByElectionRound(metropolitanAreaId.Value, electionRoundId.Value)];
                }
                else
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMetropolitanAreaStatistics(metropolitanAreaId.Value, year, role, roundNr, positionNr, status)];
                }           
            }

            if (aggregationLevel == AggregationLevel.State)
            {
                if (!stateId.HasValue)
                {
                    throw new InvalidOperationException("State id is required when the aggregation level is state.");
                }

                if (electionRoundId.HasValue)
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetStateStatisticsByElectionRound(stateId.Value, electionRoundId.Value)];
                }
                else
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetStateStatistics(stateId.Value, year, role, roundNr, positionNr, status)];
                } 
            }

            if (aggregationLevel == AggregationLevel.Macroregion)
            {
                if (!macroregionId.HasValue)
                {
                    throw new InvalidOperationException("Macroregion id is required when the aggregation level is macroregion.");
                }

                if (electionRoundId.HasValue)
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMacroregionStatisticsByElectionRound(macroregionId.Value, electionRoundId.Value)];
                }
                else
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetMacroregionStatistics(macroregionId.Value, year, role, roundNr, positionNr, status)];
                }       
            }

            if (aggregationLevel == AggregationLevel.Overall)
            {
                if (electionRoundId.HasValue)
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetElectionRoundStatistics(electionRoundId.Value)];
                }
                else
                {
                    ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetPresidentialElectionStatistics(year, roundNr, positionNr, status)];
                }                    
            }

            return ticketElectionRoundStatistics;
        }

        public async Task<(IEnumerable<Ticket>, IEnumerable<TicketCandidateParty>, IEnumerable<CandidateParty>)> GetTicketRelatedInformation(IEnumerable<TicketElectionRoundStatistic> ticketElectionRoundStatistics, int? levelNr)
        {
            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).Distinct().ToList();
            var tickets = await _repository.Ticket.Get(x => ticketIds.Contains(x.Id));

            var ticketCandidateParties = await _repository.TicketCandidateParty.Get(x => ticketIds.Contains(x.TicketId) && (!levelNr.HasValue || x.NrLevel == levelNr.Value));

            var candidatePartyIds = ticketCandidateParties.Select(x => x.CandidatePartyId).ToList();
            var candidateParties = (await _repository.CandidateParty.GetCandidatesAndParties(x => candidatePartyIds.Contains(x.Id)));

            return (tickets, ticketCandidateParties, candidateParties);
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

            List<TicketElectionRoundStatistic> ticketElectionRoundStatistics = await GetTicketElectionRoundStatistics(aggregationLevel, municipalityId: municipalityId, metropolitanAreaId: metropolitanAreaId, stateId: stateId, macroregionId: macroregionId, year: year, role: role, roundNr: roundNr, positionNr: positionNr, status: status);

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

        public async Task<ElectionRoundDto?> GetElectionOverallResults(ElectionRound electionRound, string role, AggregationLevel aggregationLevel, int ticketMembersCount, long? municipalityId = null, long? metropolitanAreaId = null, long? stateId = null, long? macroregionId = null)
        {
            var ticketElectionRoundStatistics = await GetTicketElectionRoundStatistics(aggregationLevel, electionRoundId: electionRound.Id, municipalityId: municipalityId, metropolitanAreaId: metropolitanAreaId, stateId: stateId, macroregionId: macroregionId);

            var ticketElectionRoundStatistic = ticketElectionRoundStatistics.FirstOrDefault();
            if (ticketElectionRoundStatistic is null)
            {
                return null;
            }

            var electionRoundStatisticByTicket = ticketElectionRoundStatistics.ToDictionary(x => x.TicketId);

            (var tickets, var ticketCandidateParties, var candidateParties) = await GetTicketRelatedInformation(ticketElectionRoundStatistics, levelNr: null);

            var ticketIds = tickets.Select(x => x.Id).ToList();
            var coalitions = (await _repository.Coalition.Get(x => ticketIds.Contains(x.TicketId))).ToDictionary(x => x.TicketId);

            var ticketCandidatePartiesByTicket = ticketCandidateParties.ToLookup(x => x.TicketId);

            var candidatePartiesDict = candidateParties.ToDictionary(x => x.Id);

            List<TicketDto> ticketDtos = [];

            foreach (var ticket in tickets)
            {
                if (!electionRoundStatisticByTicket.TryGetValue(ticket.Id, out var ticketElectionRoundStatisticFromDict))
                {
                    throw new InvalidOperationException($"Error fetching statistic from ticket with id {ticket.Id}");
                }

                var coalition = coalitions.GetValueOrDefault(ticket.Id);

                var ticketCandidatePartiesFromLookup = ticketCandidatePartiesByTicket[ticket.Id].ToList();

                if (ticketCandidatePartiesFromLookup.Count < ticketMembersCount)
                {
                    throw new InvalidOperationException($"Error fetching ticket candidate parties from ticket with id {ticket.Id}");
                }

                List<TicketCandidatePartyDto> members = [];

                foreach (var ticketCandidateParty in ticketCandidatePartiesFromLookup)
                {
                    if (!candidatePartiesDict.TryGetValue(ticketCandidateParty.CandidatePartyId, out var candidateParty))
                    {
                        throw new InvalidOperationException($"Error fetching candidate party with id {ticketCandidateParty.CandidatePartyId}");
                    }

                    var candidate = candidateParty.Candidate;
                    var party = candidateParty.Party;

                    members.Add(new TicketCandidatePartyDto(candidate, party, role, ticketCandidateParty.NrLevel));
                }

                members = [.. members.OrderBy(x => x.LevelNr)];

                ticketDtos.Add(new TicketDto(ticket.Uid, coalition?.Name, coalition is not null, ticketElectionRoundStatisticFromDict, members));
            }

            ticketDtos = [.. ticketDtos.OrderByDescending(x => x.VotesCount)];

            return new ElectionRoundDto(ticketDtos, electionRound.HeldAt, ticketElectionRoundStatistic);
        }
    }
}
