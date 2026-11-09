using elections.Commons.Constants;
using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Entities;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using elections.Services.Elections;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace elections.Services.Candidates
{
    public class CandidateService : ICandidateService
    {
        private readonly IRepositoryManager _repository;

        public CandidateService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        private static Expression<Func<Candidate, object?>> ResolveKeySelector(CandidateSortableField? candidateSortableField)
        {
            return candidateSortableField switch
            {
                CandidateSortableField.Name => x => x.Name,
                CandidateSortableField.DisplayName => x => x.DisplayName,
                CandidateSortableField.BirthDate => x => x.BirthDate,
                CandidateSortableField.DeathDate => x => x.DeathDate,
                _ => x => x.Name
            };
        }

        private async Task<IEnumerable<CandidateElectionRoundRecord>> GetCandidateElectionRoundRecords(long candidateId, BaseElectionRoundRecordFilter electionRoundRecordFilter, int? roleNr, AggregationLevel aggregationLevel, long? municipalityId = null, long? metropolitanAreaId = null, long? stateId = null, long? macroregionId = null)
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

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetCandidateMunicipalityStatistics(candidateId, municipalityId.Value, year, role, roundNr, levelNr, positionNr, status)];
            }
            
            if (aggregationLevel == AggregationLevel.MetropolitanArea)
            {
                if (!metropolitanAreaId.HasValue)
                {
                    throw new InvalidOperationException("Metropolitan area id is required when the aggregation level is metropolitan area.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetCandidateMetropolitanAreaStatistics(candidateId, metropolitanAreaId.Value, year, role, roundNr, levelNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.State)
            {
                if (!stateId.HasValue)
                {
                    throw new InvalidOperationException("State id is required when the aggregation level is state.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetCandidateStateStatistics(candidateId, stateId.Value, year, role, roundNr, levelNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.Macroregion)
            {
                if (!macroregionId.HasValue)
                {
                    throw new InvalidOperationException("Macroregion id is required when the aggregation level is macroregion.");
                }

                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetCandidateMacroregionStatistics(candidateId, macroregionId.Value, year, role, roundNr, levelNr, positionNr, status)];
            }

            if (aggregationLevel == AggregationLevel.Overall)
            {
                ticketElectionRoundStatistics = [.. await _repository.TicketElectionRoundStatistic.GetCandidateTicketPresidentialElectionRoundStatistics(candidateId, year, roundNr, levelNr, positionNr, status, isOverview: false)];
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

            var ticketCandidateParties = ticketElectionRoundStatistics.Select(x => x.Ticket).SelectMany(x => x.TicketCandidateParties).ToDictionary(x => x.TicketId);

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).ToList();
            var coalitions = (await _repository.Coalition.Get(x => ticketIds.Contains(x.TicketId))).ToDictionary(x => x.TicketId);

            var candidatePartyIds = ticketCandidateParties.Select(x => x.Value.CandidatePartyId).ToList();
            var candidateParties = (await _repository.CandidateParty.GetParties(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

            List<CandidateElectionRoundRecord> electionRoundRecords = [];

            foreach (var ticketElectionRoundStatistic in ticketElectionRoundStatistics)
            {
                var electionRoundId = ticketElectionRoundStatistic.ElectionRoundId;
                var ticketId = ticketElectionRoundStatistic.TicketId;

                var electionRound = electionRounds.GetValueOrDefault(electionRoundId) ?? throw new InvalidOperationException($"Missing election round with id {electionRoundId}");

                var electionYear = year ?? elections?.GetValueOrDefault(electionRound.ElectionId)?.Year ?? throw new InvalidOperationException("Election year could not be obtained.");
                var electionRole = role ?? elections?.GetValueOrDefault(electionRound.ElectionId)?.Role ?? throw new InvalidOperationException("Election role could not be obtained.");

                var ticketCandidateParty = ticketCandidateParties.GetValueOrDefault(ticketId) ?? throw new InvalidOperationException($"Missing ticket candidate party from ticket with id {ticketId}");

                var candidatePartyId = ticketCandidateParty.CandidatePartyId;
                var candidateParty = candidateParties.GetValueOrDefault(candidatePartyId) ?? throw new InvalidOperationException($"Missing candidate party with id {candidatePartyId}");

                var coalition = coalitions.GetValueOrDefault(ticketId);

                electionRoundRecords.Add(new CandidateElectionRoundRecord(candidateParty.Party, RoleResolver.GetOfficeRole(electionRole, ticketCandidateParty.NrLevel), electionYear, electionRound.NrRound, coalition, ticketElectionRoundStatistic));
            }

            return electionRoundRecords.Sort(electionRoundRecordFilter.SortBy, electionRoundRecordFilter.SortDirection);
        }

        public async Task<PagedResponse<CandidateDto>> GetCandidates(CandidateFilter candidateFilter, PagedRequest pagedRequest)
        {
            var nameQuery = candidateFilter.Name?.ToUpper();

            var queryCandidates = _repository.Candidate.GetQueryable(
                x => (!candidateFilter.IsDeceased.HasValue || x.IsDeceased == candidateFilter.IsDeceased.Value) 
                && (string.IsNullOrEmpty(nameQuery) || x.Name.ToUpper().Contains(nameQuery)));

            var totalCount = await queryCandidates.CountAsync();

            var candidates = (await _repository.Candidate.GetPaged(queryCandidates, pagedRequest, ResolveKeySelector(candidateFilter.SortBy), candidateFilter.SortDirection)).ToList();
            var items = candidates.Select(x => new CandidateDto(x)).Sort(candidateFilter.SortBy, candidateFilter.SortDirection).ToList();

            return new PagedResponse<CandidateDto>(items, totalCount, pagedRequest);
        }

        public async Task<CandidateDto?> GetCandidate(string uid)
        {
            var candidate = await _repository.Candidate.GetByUid(uid);

            if (candidate is null)
            {
                return null;
            }

            return new CandidateDto(candidate);
        }

        public async Task<IEnumerable<CandidateElectionOverview>> GetCandidatePresidentialElectionOverviews(string candidateUid, ElectionOverviewFilter electionOverviewFilter, bool? isElected)
        {
            var candidateId = (await _repository.Candidate.GetByUid(candidateUid))?.Id;
            if (!candidateId.HasValue)
            {
                return [];
            }

            string? status = null;
            if (isElected.HasValue)
            {
                status = isElected.Value ? Constant.Election.Status.Elected : Constant.Election.Status.NotElected;
            }

            var ticketElectionRoundStatistics = await _repository.TicketElectionRoundStatistic.GetCandidateTicketPresidentialElectionRoundStatistics(candidateId.Value, electionOverviewFilter.Year, electionOverviewFilter.Round, levelNr: null, positionNr: null, status, isOverview: true);

            var electionRoundIds = ticketElectionRoundStatistics.Select(x => x.ElectionRoundId).ToList();
            var electionRounds = await _repository.ElectionRound.Get(x => electionRoundIds.Contains(x.Id));
            var electionsByRound = electionRounds.ToDictionary(x => x.ElectionId);

            var electionIds = electionRounds.Select(x => x.ElectionId).ToList();
            var elections = await _repository.Election.Get(x => electionIds.Contains(x.Id));

            var ticketCandidateParties = ticketElectionRoundStatistics.Select(x => x.Ticket).SelectMany(x => x.TicketCandidateParties);
            var candidatePartyIds = ticketCandidateParties.Select(x => x.CandidatePartyId).ToList();

            var ticketElectionRoundStatisticsByRound = ticketElectionRoundStatistics.ToDictionary(x => x.ElectionRoundId);

            var ticketCandidatePartiesByTicket = ticketCandidateParties.ToDictionary(x => x.TicketId);

            var candidateParties = (await _repository.CandidateParty.GetParties(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

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
                if (!ticketCandidatePartiesByTicket.TryGetValue(ticketId, out var ticketCandidateParty))
                {
                    throw new InvalidOperationException($"Error while fetching ticket candidate party from ticket with id {ticketId}");
                }

                var candidatePartyId = ticketCandidateParty.CandidatePartyId;
                if (!candidateParties.TryGetValue(candidatePartyId, out var candidateParty))
                {
                    throw new InvalidOperationException($"Error while fetching candidate party from ticket candidate party with id {candidatePartyId}");
                }

                var officeRole = RoleResolver.GetOfficeRole(Constant.Election.Role.President, ticketCandidateParty.NrLevel);

                var party = candidateParty.Party;

                return new CandidateElectionOverview(election, electionRound, ticketElectionRoundStatistic, officeRole, party);

            }).Sort(electionOverviewFilter.SortBy, electionOverviewFilter.SortDirection);
        }        

        public async Task<IEnumerable<CandidateElectionRoundRecord>?> GetCandidateMunicipalityElectionRoundRecords(string candidateUid, long tseId, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var candidateId = await _repository.Candidate.GetIdByUid(candidateUid);

            if (!candidateId.HasValue)
            {
                return null;
            }

            var municipalityId = await _repository.Municipality.GetId(x => x.TseId == tseId);

            if (!municipalityId.HasValue)
            {
                return [];
            }

            return await GetCandidateElectionRoundRecords(candidateId.Value, electionRoundRecordFilter, electionRoundRecordFilter.Role, AggregationLevel.Municipality, municipalityId: municipalityId.Value);
        }

        public async Task<IEnumerable<CandidateElectionRoundRecord>?> GetCandidateMetropolitanAreaElectionRoundRecords(string candidateUid, string metropolitanAreaUid, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var candidateId = await _repository.Candidate.GetIdByUid(candidateUid);

            if (!candidateId.HasValue)
            {
                return null;
            }

            var metropolitanAreaId = await _repository.MetropolitanArea.GetIdByUid(metropolitanAreaUid);

            if (!metropolitanAreaId.HasValue)
            {
                return [];
            }

            return await GetCandidateElectionRoundRecords(candidateId.Value, electionRoundRecordFilter, electionRoundRecordFilter.Role, AggregationLevel.MetropolitanArea, metropolitanAreaId: metropolitanAreaId.Value);
        }

        public async Task<IEnumerable<CandidateElectionRoundRecord>?> GetCandidateStateElectionRoundRecords(string candidateUid, string stateAbbr, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var candidateId = await _repository.Candidate.GetIdByUid(candidateUid);

            if (!candidateId.HasValue)
            {
                return null;
            }

            var stateId = await _repository.State.GetId(x => string.Equals(x.Abbr, stateAbbr));

            if (!stateId.HasValue)
            {
                return [];
            }

            return await GetCandidateElectionRoundRecords(candidateId.Value, electionRoundRecordFilter, electionRoundRecordFilter.Role, AggregationLevel.State, stateId: stateId.Value);
        }

        public async Task<IEnumerable<CandidateElectionRoundRecord>?> GetCandidateMacroregionElectionRoundRecords(string candidateUid, string macroregionAbbr, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var candidateId = await _repository.Candidate.GetIdByUid(candidateUid);

            if (!candidateId.HasValue)
            {
                return null;
            }

            var macroregionId = await _repository.Macroregion.GetId(x => string.Equals(x.Abbr, macroregionAbbr));

            if (!macroregionId.HasValue)
            {
                return [];
            }

            return await GetCandidateElectionRoundRecords(candidateId.Value, electionRoundRecordFilter, electionRoundRecordFilter.Role, AggregationLevel.Macroregion, macroregionId: macroregionId.Value);
        }

        public async Task<IEnumerable<CandidateElectionRoundRecord>?> GetCandidatePresidentialElectionRoundRecords(string candidateUid, PresidentialElectionRoundRecordFilter presidentialElectionRoundRecordFilter)
        {
            var candidateId = await _repository.Candidate.GetIdByUid(candidateUid);

            if (!candidateId.HasValue)
            {
                return null;
            }

            return await GetCandidateElectionRoundRecords(candidateId.Value, presidentialElectionRoundRecordFilter, roleNr: 1, AggregationLevel.Overall);
        }
    }
}
