using elections.Commons.Constants;
using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Entities;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static elections.Commons.Constants.Routes;

namespace elections.Services.Parties
{
    public class PartyService : IPartyService
    {
        private readonly IRepositoryManager _repository;

        public PartyService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        private static Expression<Func<Party, object?>> ResolveKeySelector(PartySortableField? partySortableField)
        {
            return partySortableField switch
            {
                PartySortableField.Name => x => x.Name,
                PartySortableField.Abbr => x => x.Abbr,
                PartySortableField.Nr => x => x.NrVoteable,
                PartySortableField.Registration => x => x.RegisteredAt,
                PartySortableField.Foundation => x => x.FoundedAt,
                PartySortableField.Dissolution => x => x.DissolvedAt,
                _ => x => x.Name
            };
        }

        private async Task<IEnumerable<PartyElectionRoundRecord>> GetPartyElectionRoundRecords(long partyId, ElectionRoundRecordFilter electionRoundRecordFilter, AggregationLevel aggregationLevel, long? municipalityId = null, long? metropolitanAreaId = null, long? stateId = null, long? macroregionId = null)
        {
            var year = electionRoundRecordFilter.Year;
            var role = RoleResolver.GetRole(electionRoundRecordFilter.Role);
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

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetPartyMunicipalityStatistics(partyId, municipalityId.Value, year, role, roundNr, levelNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.MetropolitanArea)
            {
                if (!metropolitanAreaId.HasValue)
                {
                    throw new InvalidOperationException("Metropolitan area id is required when the aggregation level is metropolitan area.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetPartyMetropolitanAreaStatistics(partyId, metropolitanAreaId.Value, year, role, roundNr, levelNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.State)
            {
                if (!stateId.HasValue)
                {
                    throw new InvalidOperationException("State id is required when the aggregation level is state.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetPartyStateStatistics(partyId, stateId.Value, year, role, roundNr, levelNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.Macroregion)
            {
                if (!macroregionId.HasValue)
                {
                    throw new InvalidOperationException("Macroregion id is required when the aggregation level is macroregion.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetPartyMacroregionStatistics(partyId, macroregionId.Value, year, role, roundNr, levelNr, positionNr, status)];
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

            var ticketCandidateParties = ticketElectionRoundStatistics.Select(x => x.Ticket).SelectMany(x => x.TicketCandidateParties).ToLookup(x => x.TicketId);

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).ToList();
            var coalitions = (await _repository.Coalition.Get(x => ticketIds.Contains(x.TicketId))).ToDictionary(x => x.TicketId);

            var candidatePartyIds = ticketCandidateParties.SelectMany(x => x).Select(x => x.CandidatePartyId).ToList();
            var candidateParties = (await _repository.CandidateParty.GetCandidates(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

            List<PartyElectionRoundRecord> electionRoundRecords = [];

            foreach (var ticketElectionRoundStatistic in ticketElectionRoundStatistics)
            {
                var electionRoundId = ticketElectionRoundStatistic.ElectionRoundId;
                var ticketId = ticketElectionRoundStatistic.TicketId;

                var electionRound = electionRounds.GetValueOrDefault(electionRoundId) ?? throw new InvalidOperationException($"Missing election round with id {electionRoundId}");

                var electionYear = year ?? elections?.GetValueOrDefault(electionRound.ElectionId)?.Year ?? throw new InvalidOperationException("Election year could not be obtained.");
                var electionRole = role ?? elections?.GetValueOrDefault(electionRound.ElectionId)?.Role ?? throw new InvalidOperationException("Election role could not be obtained.");

                List<TicketCandidateDto> ticketCandidates = [];
                foreach (var ticketCandidateParty in ticketCandidateParties[ticketId])
                {
                    var candidatePartyId = ticketCandidateParty.CandidatePartyId;
                    var candidateParty = candidateParties.GetValueOrDefault(candidatePartyId) ?? throw new InvalidOperationException($"Missing candidate party with id {candidatePartyId}");

                    ticketCandidates.Add(new TicketCandidateDto(candidateParty.Candidate, electionRole, ticketCandidateParty.NrLevel));
                }

                var coalition = coalitions.GetValueOrDefault(ticketId);

                electionRoundRecords.Add(new PartyElectionRoundRecord(ticketCandidates, electionYear, electionRound.NrRound, coalition, ticketElectionRoundStatistic));
            }

            return electionRoundRecords.Sort(electionRoundRecordFilter.SortBy, electionRoundRecordFilter.SortDirection);
        }

        public async Task<PagedResponse<PartyDto>> GetParties(PartyFilter partyFilter, PagedRequest pagedRequest)
        {
            var queryParties = _repository.Party.GetQueryable(
                x => (string.IsNullOrEmpty(partyFilter.Name) || x.Name.ToUpper().Contains(partyFilter.Name.ToUpper()))
                && (string.IsNullOrEmpty(partyFilter.Abbr) || string.Equals(x.Abbr, partyFilter.Abbr))
                && (!partyFilter.Nr.HasValue || x.NrVoteable == partyFilter.Nr.Value)
                && (!partyFilter.IsActive.HasValue || x.IsActive == partyFilter.IsActive.Value));

            var totalCount = await queryParties.CountAsync();

            var parties = (await _repository.Party.GetPaged(queryParties, pagedRequest, ResolveKeySelector(partyFilter.SortBy), partyFilter.SortDirection)).ToList();
            var items = parties.Select(x => new PartyDto(x)).Sort(partyFilter.SortBy, partyFilter.SortDirection).ToList();

            return new PagedResponse<PartyDto>(items, totalCount, pagedRequest);
        }

        public async Task<PartyDto?> GetParty(string uid)
        {
            var party = await _repository.Party.GetByUid(uid);

            if (party is null)
            {
                return null;
            }

            return new PartyDto(party);
        }

        public async Task<IEnumerable<PartyElectionOverview>> GetPartyPresidentialElectionOverviews(string partyUid, ElectionOverviewFilter electionOverviewFilter, bool? isElected)
        {
            var partyId = (await _repository.Party.GetByUid(partyUid))?.Id;
            if (!partyId.HasValue)
            {
                return [];
            }

            string? status = null;
            if (isElected.HasValue)
            {
                status = isElected.Value ? Constant.Election.Status.Elected : Constant.Election.Status.NotElected;
            }

            var ticketElectionRoundStatistics = await _repository.TicketElectionRoundStatistic.GetPartyTicketPresidentialElectionRoundStatistics(partyId.Value, electionOverviewFilter.Year, electionOverviewFilter.Round, levelNr: null, positionNr: null, status, isOverview: true);

            var electionRoundIds = ticketElectionRoundStatistics.Select(x => x.ElectionRoundId).ToList();
            var electionRounds = await _repository.ElectionRound.Get(x => electionRoundIds.Contains(x.Id));
            var electionsByRound = electionRounds.ToDictionary(x => x.ElectionId);

            var electionIds = electionRounds.Select(x => x.ElectionId).ToList();
            var elections = await _repository.Election.Get(x => electionIds.Contains(x.Id));

            var ticketCandidateParties = ticketElectionRoundStatistics.Select(x => x.Ticket).SelectMany(x => x.TicketCandidateParties);

            var candidatePartyIds = ticketCandidateParties.Select(x => x.CandidatePartyId).ToList();

            var ticketElectionRoundStatisticsByRound = ticketElectionRoundStatistics.ToDictionary(x => x.ElectionRoundId);

            var ticketCandidatePartiesByTicket = ticketCandidateParties.ToLookup(x => x.TicketId);

            var candidateParties = (await _repository.CandidateParty.GetCandidates(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

            return elections.Select(election =>
            {
                if (!electionsByRound.TryGetValue(election.Id, out var electionRound))
                {
                    throw new InvalidOperationException($"Couldn't find election round for election with id {election.Id}");
                }

                if (!ticketElectionRoundStatisticsByRound.TryGetValue(electionRound.Id, out var ticketElectionRoundStatistic))
                {
                    throw new InvalidOperationException($"Error while fetching statistic from election round with id {electionRound.Id}");
                }

                var ticketId = ticketElectionRoundStatistic.TicketId;
                var ticketCandidateParties = ticketCandidatePartiesByTicket[ticketId].ToList();
                if (ticketCandidateParties.Count == 0)
                {
                    throw new InvalidOperationException($"Error while fetching ticket candidate party from ticket with id {ticketId}");
                }

                List<TicketCandidateDto> candidates = [];

                foreach(var ticketCandidateParty in ticketCandidateParties)
                {
                    var candidatePartyId = ticketCandidateParty.CandidatePartyId;
                    if (!candidateParties.TryGetValue(candidatePartyId, out var candidateParty))
                    {
                        throw new InvalidOperationException($"Error while fetching candidate party from ticket candidate party with id {candidatePartyId}");
                    }
                    
                    var candidate = candidateParty.Candidate;

                    candidates.Add(new TicketCandidateDto(candidate, Constant.Election.Role.President, ticketCandidateParty.NrLevel));
                }

                return new PartyElectionOverview(election, electionRound, ticketElectionRoundStatistic, candidates);

            }).Sort(electionOverviewFilter.SortBy, electionOverviewFilter.SortDirection);
        }

        public async Task<IEnumerable<PartyElectionRoundRecord>?> GetPartyMunicipalityElectionRoundRecords(string partyUid, long tseId, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var partyId = await _repository.Party.GetIdByUid(partyUid);

            if (!partyId.HasValue)
            {
                return null;
            }

            var municipalityId = await _repository.Municipality.GetId(x => x.TseId == tseId);

            if (!municipalityId.HasValue)
            {
                return [];
            }

            return await GetPartyElectionRoundRecords(partyId.Value, electionRoundRecordFilter, AggregationLevel.Municipality, municipalityId: municipalityId.Value);
        }

        public async Task<IEnumerable<PartyElectionRoundRecord>?> GetPartyMetropolitanAreaElectionRoundRecords(string partyUid, string metropolitanAreaUid, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var partyId = await _repository.Party.GetIdByUid(partyUid);

            if (!partyId.HasValue)
            {
                return null;
            }

            var metropolitanAreaId = await _repository.MetropolitanArea.GetIdByUid(metropolitanAreaUid);

            if (!metropolitanAreaId.HasValue)
            {
                return [];
            }

            return await GetPartyElectionRoundRecords(partyId.Value, electionRoundRecordFilter, AggregationLevel.MetropolitanArea, metropolitanAreaId: metropolitanAreaId.Value);
        }

        public async Task<IEnumerable<PartyElectionRoundRecord>?> GetPartyStateElectionRoundRecords(string partyUid, string stateAbbr, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var partyId = await _repository.Party.GetIdByUid(partyUid);

            if (!partyId.HasValue)
            {
                return null;
            }

            var stateId = await _repository.State.GetId(x => string.Equals(x.Abbr, stateAbbr));

            if (!stateId.HasValue)
            {
                return [];
            }

            return await GetPartyElectionRoundRecords(partyId.Value, electionRoundRecordFilter, AggregationLevel.State, stateId: stateId.Value);
        }

        public async Task<IEnumerable<PartyElectionRoundRecord>?> GetPartyMacroregionElectionRoundRecords(string partyUid, string macroregionAbbr, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var partyId = await _repository.Party.GetIdByUid(partyUid);

            if (!partyId.HasValue)
            {
                return null;
            }

            var macroregionId = await _repository.Macroregion.GetId(x => string.Equals(x.Abbr, macroregionAbbr));

            if (!macroregionId.HasValue)
            {
                return [];
            }

            return await GetPartyElectionRoundRecords(partyId.Value, electionRoundRecordFilter, AggregationLevel.Macroregion, macroregionId: macroregionId.Value);
        }
        
    }
}
