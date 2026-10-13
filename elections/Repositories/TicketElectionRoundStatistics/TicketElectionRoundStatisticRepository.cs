using elections.Commons.Constants;
using elections.Commons.Utils;
using elections.Data;
using elections.Models.Entities;
using elections.Models.Requests;
using Microsoft.EntityFrameworkCore;
using static elections.Commons.Constants.Constant.Election;

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

        private IQueryable<TicketElectionRoundStatistic> QueryTicketByCandidateId(IQueryable<TicketElectionRoundStatistic> query, long candidateId)
        {
            return query.Where(
                x => x.Ticket.TicketCandidateParties.Any(
                    tcp => tcp.CandidateParty.CandidateId == candidateId));
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetElectedTicketPresidentialElectionRoundStatistics(int? year, int? roundNr)
        {
            var query = _dbSet.AsQueryable();

            query = QueryTicketElectionRoundByRole(query, Constant.Election.Role.President);

            query = QueryBroadTicketElectionRoundStatistic(query);

            query = QueryTicketElectionRoundStatisticByStatus(query, Constant.Election.Status.Elected);

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

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetCandidateTicketPresidentialElectionRoundStatistics(long candidateId, int? year, int? roundNr, bool? isElected)
        {
            var query = _dbSet.AsQueryable();

            query = QueryTicketElectionRoundByRole(query, Constant.Election.Role.President);

            query = QueryBroadTicketElectionRoundStatistic(query);

            query = QueryTicketByCandidateId(query, candidateId);

            if (isElected.HasValue)
            {
                query = query.Where(x => string.Equals(x.TicketStatus, Constant.Election.Status.Elected) == isElected.Value);
            }
            else
            {
                query = query.Where(
                    x => string.Equals(x.TicketStatus, Constant.Election.Status.Elected)
                    || string.Equals(x.TicketStatus, Constant.Election.Status.NotElected));
            }

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
    }
}
