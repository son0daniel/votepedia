using elections.Commons.Constants;
using elections.Commons.Utils;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Entities;
using elections.Models.Requests;
using elections.Repositories;

namespace elections.Services.ElectionResults
{
    public class ElectionResultService : IElectionResultService
    {
        public IRepositoryManager _repository;

        public ElectionResultService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BaseElectionOverview>> GetElectionOverviews(TicketElectionRoundStatistic ticketElectionRoundStatistic)
        {
            throw new NotImplementedException();
        }

        public async Task<InternalElectionFilter> ResolveElectionFilter(ElectionFilter electionFilter, string role, string? municipalityUid, string? stateAbbr)
        {
            Municipality? municipality = null;
            State? state = null;

            if (string.Equals(role, Constant.Election.Role.Mayor) || string.Equals(role, Constant.Election.Role.CityCouncilor))
            {
                if (!string.IsNullOrEmpty(municipalityUid))
                {
                    municipality = await _repository.Municipality.GetByUid(municipalityUid);

                    if (municipality == null)
                    {
                        throw new InvalidOperationException($"Invalid identifier for municipality: {municipalityUid}");
                    }
                }
                else
                {
                    throw new InvalidOperationException("A municipality identifier is required for municipality-level elections");
                }
            }
            else if (!string.Equals(role, Constant.Election.Role.President))
            {
                if (!string.IsNullOrEmpty(stateAbbr))
                {
                    state = (await _repository.State.Get(x => string.Equals(x.Abbr, stateAbbr))).FirstOrDefault();

                    if (state == null)
                    {
                        throw new InvalidOperationException($"Invalid identifier for state: {stateAbbr}");
                    }
                }
                else
                {
                    throw new InvalidOperationException("A state identifier is required for state-level elections");
                }
            }

            return new InternalElectionFilter
            {
                MunicipalityId = municipality?.Id,
                StateId = state?.Id,
                Year = electionFilter.Year,
                Role = role,
                Round = electionFilter.Round
            };
        }
    }
}
