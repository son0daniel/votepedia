using elections.Data;
using elections.Migrations;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static elections.Commons.Constants.Constant.Election;
using static elections.Commons.Constants.Routes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace elections.Repositories.TicketElectionRoundStatistics
{
    public class TicketElectionRoundStatisticRepository : Repository<TicketElectionRoundStatistic>, ITicketElectionRoundStatisticRepository
    {
        public TicketElectionRoundStatisticRepository(ApplicationDbContext context) : base(context)
        {
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundStatisticByStatus(IQueryable<TicketElectionRoundStatistic> query, string status)
        {
            return query.Where(x => string.Equals(x.TicketStatus, status));
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundByRole(IQueryable<TicketElectionRoundStatistic> query, string role)
        {
            return query.Where(x => string.Equals(x.ElectionRound.Election.Role, role));
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundByYear(IQueryable<TicketElectionRoundStatistic> query, int year)
        {
            return query.Where(x => x.ElectionRound.Election.Year == year);
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundByRound(IQueryable<TicketElectionRoundStatistic> query, int roundNr)
        {
            return query.Where(x => x.ElectionRound.NrRound == roundNr);
        }

        private IQueryable<TicketElectionRoundStatistic> QueryBroadTicketElectionRoundStatistic(IQueryable<TicketElectionRoundStatistic> query)
        {
            return query
                .Where(x =>
                    !x.MunicipalityId.HasValue
                    && !x.MetropolitanAreaId.HasValue
                    && !x.StateId.HasValue
                    && !x.MacroregionId.HasValue);
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketByCandidateId(IQueryable<TicketElectionRoundStatistic> query, long candidateId, int? levelNr)
        {
            return query.Where(
                x => x.Ticket.TicketCandidateParties.Any(
                    tcp => tcp.CandidateParty.CandidateId == candidateId
                    && (!levelNr.HasValue || tcp.NrLevel == levelNr.Value)))
                .Include(x => x.Ticket)
                .ThenInclude(t => t.TicketCandidateParties.Where(tcp => tcp.CandidateParty.CandidateId == candidateId
                    && (!levelNr.HasValue || tcp.NrLevel == levelNr.Value)));
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketByPartyId(IQueryable<TicketElectionRoundStatistic> query, long partyId, int? levelNr)
        {
            return query.Where(
                x => x.Ticket.TicketCandidateParties.Any(
                    tcp => tcp.CandidateParty.PartyId == partyId
                    && (!levelNr.HasValue || tcp.NrLevel == levelNr.Value)))
                .Include(x => x.Ticket)
                .ThenInclude(t => t.TicketCandidateParties.Where(tcp => tcp.CandidateParty.PartyId == partyId
                    && (!levelNr.HasValue || tcp.NrLevel == levelNr.Value)));
        }

        private IQueryable<TicketElectionRoundStatistic> QueryElectionRoundRecords(int? year, string? role, int? roundNr, int? positionNr, string? status, bool isOverview)
        {
            var query = _dbSet.AsQueryable();

            if (year.HasValue)
            {
                query = QueryTicketElectionRoundByYear(query, year.Value);
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = QueryTicketElectionRoundByRole(query, role);
            }

            if (roundNr.HasValue)
            {
                query = QueryTicketElectionRoundByRound(query, roundNr.Value);
            }

            if (positionNr.HasValue)
            {
                query = QueryTicketElectionRoundStatisticByPositionNr(query, positionNr.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = QueryTicketElectionRoundStatisticByStatus(query, status);
            }
            else if (isOverview)
            {
                query = query.Where(x => string.Equals(x.TicketStatus, Status.Elected)
                || string.Equals(x.TicketStatus, Status.NotElected));
            }

            return query;
        }

        private IQueryable<TicketElectionRoundStatistic> GetTicketPresidentialElectionRoundStatistics(int? year, int? roundNr, int? positionNr, string? status, bool isOverview)
        {
            var query = QueryElectionRoundRecords(year, Role.President, roundNr, positionNr, status, isOverview);

            query = QueryBroadTicketElectionRoundStatistic(query);

            return query;
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundStatisticByMunicipality(IQueryable<TicketElectionRoundStatistic> query, long municipalityId)
        {
            return query.Where(x => x.MunicipalityId == municipalityId);
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundStatisticByMetropolitanArea(IQueryable<TicketElectionRoundStatistic> query, long metropolitanAreaId)
        {
            return query.Where(x => x.MetropolitanAreaId == metropolitanAreaId 
                && !x.MunicipalityId.HasValue);
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundStatisticByState(IQueryable<TicketElectionRoundStatistic> query, long stateId)
        {
            return query.Where(x => x.StateId == stateId 
                && !x.MunicipalityId.HasValue 
                && !x.MetropolitanAreaId.HasValue);
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundStatisticByMacroregion(IQueryable<TicketElectionRoundStatistic> query, long macroregionId)
        {
            return query.Where(x => x.MacroregionId == macroregionId
                && !x.MunicipalityId.HasValue
                && !x.MetropolitanAreaId.HasValue
                && !x.StateId.HasValue);
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundStatisticByPositionNr(IQueryable<TicketElectionRoundStatistic> query, int positionNr)
        {
            return query.Where(x => x.TicketPositionNr == positionNr);
        }

        private IQueryable<TicketElectionRoundStatistic> QueryCandidateStatistics(long candidateId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var query = QueryElectionRoundRecords(year, role, roundNr, positionNr, status, isOverview: false);

            query = QueryTicketByCandidateId(query, candidateId, levelNr);

            return query;
        }

        private IQueryable<TicketElectionRoundStatistic> QueryPartyStatistics(long partyId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var query = QueryElectionRoundRecords(year, role, roundNr, positionNr, status, isOverview: false);

            query = QueryTicketByPartyId(query, partyId, levelNr);

            return query;
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetElectedTicketPresidentialElectionRoundStatistics(int? year, int? roundNr)
        {
            var query = _dbSet.AsQueryable();

            query = QueryTicketElectionRoundByRole(query, Role.President);

            query = QueryBroadTicketElectionRoundStatistic(query);

            query = QueryTicketElectionRoundStatisticByStatus(query, Status.Elected);

            if (year.HasValue)
            {
                query = QueryTicketElectionRoundByYear(query, year.Value);
            }

            if (roundNr.HasValue)
            {
                query = QueryTicketElectionRoundByRound(query, roundNr.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateTicketPresidentialElectionRoundStatistics(long candidateId, int? year, int? roundNr, int? levelNr, int? positionNr, string? status, bool isOverview)
        {
            var query = GetTicketPresidentialElectionRoundStatistics(year, roundNr, positionNr, status, isOverview);

            query = QueryTicketByCandidateId(query, candidateId, levelNr);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyTicketPresidentialElectionRoundStatistics(long partyId, int? year, int? roundNr, int? levelNr, int? positionNr, string? status, bool isOverview)
        {
            var query = GetTicketPresidentialElectionRoundStatistics(year, roundNr, positionNr, status, isOverview);

            query = QueryTicketByPartyId(query, partyId, levelNr);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetElectionRoundStatistics(long electionRoundId)
        {
            return await _dbSet.Where(x => x.ElectionRoundId == electionRoundId).ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMunicipalityLevelResults(long electionRoundId, long? metropolitanAreaId, long? stateId, long? macroregionId, bool onlyNational)
        {
            return await _dbSet.Where(
                x => x.ElectionRoundId == electionRoundId
                && x.MunicipalityId.HasValue
                && (!metropolitanAreaId.HasValue || x.MetropolitanAreaId == metropolitanAreaId.Value)
                && (!stateId.HasValue || x.StateId == stateId.Value)
                && (!macroregionId.HasValue || x.MacroregionId == macroregionId.Value)
                && (!onlyNational || x.IsNationalStatistic.HasValue && x.IsNationalStatistic.Value)).ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMetropolitanAreaLevelResults(long electionRoundId, long? stateId, long? macroregionId)
        {
            return await _dbSet.Where(
                x => x.ElectionRoundId == electionRoundId
                && !x.MunicipalityId.HasValue
                && x.MetropolitanAreaId.HasValue
                && (!stateId.HasValue || x.StateId == stateId.Value)
                && (!macroregionId.HasValue || x.MacroregionId == macroregionId.Value)).ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetStateLevelResults(long electionRoundId, long? macroregionId, bool onlyNational)
        {
            return await _dbSet.Where(
                x => x.ElectionRoundId == electionRoundId
                && !x.MunicipalityId.HasValue
                && !x.MetropolitanAreaId.HasValue
                && x.StateId.HasValue
                && (!macroregionId.HasValue || x.MacroregionId == macroregionId.Value)
                && (!onlyNational || x.IsNationalStatistic.HasValue && x.IsNationalStatistic.Value)).ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMacroregionLevelResults(long electionRoundId, bool onlyNational)
        {
            return await _dbSet.Where(
                x => x.ElectionRoundId == electionRoundId
                && !x.MunicipalityId.HasValue
                && !x.MetropolitanAreaId.HasValue
                && !x.StateId.HasValue
                && x.MacroregionId.HasValue
                && (!onlyNational || x.IsNationalStatistic.HasValue && x.IsNationalStatistic.Value)).ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateMunicipalityStatistics(long candidateId, long municipalityId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var candidateStatisticsQuery = QueryCandidateStatistics(candidateId, year, role, roundNr, levelNr, positionNr, status);

            candidateStatisticsQuery = QueryTicketElectionRoundStatisticByMunicipality(candidateStatisticsQuery, municipalityId);

            return await candidateStatisticsQuery.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateMetropolitanAreaStatistics(long candidateId, long metropolitanAreaId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var candidateStatisticsQuery = QueryCandidateStatistics(candidateId, year, role, roundNr, levelNr, positionNr, status);

            candidateStatisticsQuery = QueryTicketElectionRoundStatisticByMetropolitanArea(candidateStatisticsQuery, metropolitanAreaId);

            return await candidateStatisticsQuery.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateStateStatistics(long candidateId, long stateId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var candidateStatisticsQuery = QueryCandidateStatistics(candidateId, year, role, roundNr, levelNr, positionNr, status);

            candidateStatisticsQuery = QueryTicketElectionRoundStatisticByState(candidateStatisticsQuery, stateId);

            return await candidateStatisticsQuery.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateMacroregionStatistics(long candidateId, long macroregionId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var candidateStatisticsQuery = QueryCandidateStatistics(candidateId, year, role, roundNr, levelNr, positionNr, status);

            candidateStatisticsQuery = QueryTicketElectionRoundStatisticByMacroregion(candidateStatisticsQuery, macroregionId);

            return await candidateStatisticsQuery.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyMunicipalityStatistics(long partyId, long municipalityId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var partyStatisticsQuery = QueryPartyStatistics(partyId, year, role, roundNr, levelNr, positionNr, status);

            partyStatisticsQuery = QueryTicketElectionRoundStatisticByMunicipality(partyStatisticsQuery, municipalityId);

            return await partyStatisticsQuery.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyMetropolitanAreaStatistics(long partyId, long metropolitanAreaId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var partyStatisticsQuery = QueryPartyStatistics(partyId, year, role, roundNr, levelNr, positionNr, status);

            partyStatisticsQuery = QueryTicketElectionRoundStatisticByMetropolitanArea(partyStatisticsQuery, metropolitanAreaId);

            return await partyStatisticsQuery.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyStateStatistics(long partyId, long stateId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var partyStatisticsQuery = QueryPartyStatistics(partyId, year, role, roundNr, levelNr, positionNr, status);

            partyStatisticsQuery = QueryTicketElectionRoundStatisticByState(partyStatisticsQuery, stateId);

            return await partyStatisticsQuery.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetPartyMacroregionStatistics(long partyId, long macroregionId, int? year, string? role, int? roundNr, int? levelNr, int? positionNr, string? status)
        {
            var partyStatisticsQuery = QueryPartyStatistics(partyId, year, role, roundNr, levelNr, positionNr, status);

            partyStatisticsQuery = QueryTicketElectionRoundStatisticByMacroregion(partyStatisticsQuery, macroregionId);

            return await partyStatisticsQuery.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMunicipalityStatistics(long municipalityId, int? year, string? role, int? roundNr, int? positionNr, string? status)
        {
            var query = QueryElectionRoundRecords(year, role, roundNr, positionNr, status, isOverview: false);

            query = QueryTicketElectionRoundStatisticByMunicipality(query, municipalityId);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMetropolitanAreaStatistics(long metropolitanAreaId, int? year, string? role, int? roundNr, int? positionNr, string? status)
        {
            var query = QueryElectionRoundRecords(year, role, roundNr, positionNr, status, isOverview: false);

            query = QueryTicketElectionRoundStatisticByMetropolitanArea(query, metropolitanAreaId);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetStateStatistics(long stateId, int? year, string? role, int? roundNr, int? positionNr, string? status)
        {
            var query = QueryElectionRoundRecords(year, role, roundNr, positionNr, status, isOverview: false);

            query = QueryTicketElectionRoundStatisticByState(query, stateId);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMacroregionStatistics(long macroregionId, int? year, string? role, int? roundNr, int? positionNr, string? status)
        {
            var query = QueryElectionRoundRecords(year, role, roundNr, positionNr, status, isOverview: false);

            query = QueryTicketElectionRoundStatisticByMacroregion(query, macroregionId);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetPresidentialElectionStatistics(int? year, int? roundNr, int? positionNr, string? status)
        {
            var query = GetTicketPresidentialElectionRoundStatistics(year, roundNr, positionNr, status, isOverview: false);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMunicipalityStatisticsByElectionRound(long municipalityId, long electionRoundId)
        {
            var query = _dbSet.Where(x => x.ElectionRoundId == electionRoundId).AsQueryable();

            return await QueryTicketElectionRoundStatisticByMunicipality(query, municipalityId).ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMetropolitanAreaStatisticsByElectionRound(long metropolitanAreaId, long electionRoundId)
        {
            var query = _dbSet.Where(x => x.ElectionRoundId == electionRoundId).AsQueryable();

            return await QueryTicketElectionRoundStatisticByMetropolitanArea(query, metropolitanAreaId).ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetStateStatisticsByElectionRound(long stateId, long electionRoundId)
        {
            var query = _dbSet.Where(x => x.ElectionRoundId == electionRoundId).AsQueryable();

            return await QueryTicketElectionRoundStatisticByState(query, stateId).ToListAsync();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetMacroregionStatisticsByElectionRound(long macroregionId, long electionRoundId)
        {
            var query = _dbSet.Where(x => x.ElectionRoundId == electionRoundId).AsQueryable();

            return await QueryTicketElectionRoundStatisticByMacroregion(query, macroregionId).ToListAsync();
        }
    }
}
