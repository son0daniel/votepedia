using elections.Commons.Constants;
using elections.Commons.Utils;
using elections.Data;
using elections.Models.Entities;
using elections.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace elections.Repositories.TicketElectionRoundStatistics
{
    public class TicketElectionRoundStatisticRepository : Repository<TicketElectionRoundStatistic>, ITicketElectionRoundStatisticRepository
    {
        public TicketElectionRoundStatisticRepository(ApplicationDbContext context) : base(context)
        {
        }

        private IQueryable<TicketElectionRoundStatistic> QueryTicketElectionRoundStatisticByStatus(string status)
        {
            return _dbSet.Where(x => string.Equals(x.TicketStatus, status)).AsQueryable();
        }

        public async Task<IEnumerable<TicketElectionRoundStatistic>> GetElectedTicketElectionRoundStatisticsByRound(InternalElectionFilter internalElectionFilter)
        {
            return await QueryTicketElectionRoundStatisticByStatus(Constant.Election.Status.Elected)
                .Where(x => 
                    !x.MunicipalityId.HasValue 
                    && !x.MetropolitanAreaId.HasValue 
                    && !x.StateId.HasValue && !x.MacroregionId.HasValue
                    && (string.IsNullOrEmpty(internalElectionFilter.Role) || string.Equals(x.ElectionRound.Election.Role, internalElectionFilter.Role))
                    && (!internalElectionFilter.Year.HasValue || x.ElectionRound.Election.Year == internalElectionFilter.Year)
                    && (!internalElectionFilter.MunicipalityId.HasValue || x.ElectionRound.Election.MunicipalityId == internalElectionFilter.MunicipalityId)
                    && (!internalElectionFilter.StateId.HasValue || x.ElectionRound.Election.StateId == internalElectionFilter.StateId)
                    && (!internalElectionFilter.Round.HasValue || x.ElectionRound.NrRound == internalElectionFilter.Round))
                .ToListAsync();
        }
    }
}
