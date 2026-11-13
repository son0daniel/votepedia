using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.Entities;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using elections.Services.ElectionRoundRecords;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static elections.Commons.Constants.Constant.Election;

namespace elections.Services.States
{
    public class StateService : IStateService
    {
        private readonly IRepositoryManager _repository;
        private readonly IElectionRoundService _electionRoundRecord;

        public StateService(IRepositoryManager repository, IElectionRoundService electionRoundRecord)
        {
            _repository = repository;
            _electionRoundRecord = electionRoundRecord;
        }

        private static Expression<Func<State, object?>> ResolveKeySelector(StateSortableField? sortBy)
        {
            return sortBy switch
            {
                StateSortableField.Abbr => x => x.Abbr,
                _ => x => x.Name,
            };
        }

        public async Task<PagedResponse<StateDto>> GetStates(StateFilter stateFilter, PagedRequest pagedRequest)
        {
            stateFilter.Macroregions = [.. stateFilter.Macroregions.Select(x => x.ToUpper())];

            List<Macroregion> macroregions = [];
            List<long> macroregionIds = [];
            if (stateFilter.Macroregions.Count > 0)
            {
                macroregions = [.. await _repository.Macroregion.Get(x => stateFilter.Macroregions.Contains(x.Abbr) && !string.Equals(x.Abbr, "ZZ"))];
                macroregionIds = [.. macroregions.Select(x => x.Id)];
            }

            var queryStates = _repository.State.GetQueryable(
                x => (!macroregionIds.Any() || macroregionIds.Contains(x.MacroregionId))
                && !string.Equals(x.Abbr, "ZZ"));

            var stateName = stateFilter.Name?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(stateName))
            {
                if (stateFilter.SearchMode == SearchMode.Exact)
                {
                    queryStates = queryStates.Where(x => string.Equals(x.Name, stateName));
                }
                else
                {
                    queryStates = queryStates.Where(x => x.Name.ToUpper().Contains(stateName));
                }
            }

            var totalCount = await queryStates.CountAsync();

            var states = await _repository.State.GetPaged(queryStates, pagedRequest, ResolveKeySelector(stateFilter.SortBy), stateFilter.SortDirection);

            if (macroregions.Count == 0)
            {
                var stateMacroregionIds = states.Select(x => x.MacroregionId).Distinct().ToList();
                macroregions = [.. await _repository.Macroregion.Get(x => stateMacroregionIds.Contains(x.Id))];
            }

            var macroregionsDict = macroregions.ToDictionary(x => x.Id);

            List<StateDto> items = [];
            foreach (var state in states)
            {
                if (!macroregionsDict.TryGetValue(state.MacroregionId, out var macroregion))
                {
                    throw new InvalidOperationException($"Error fetching macroregion with id {state.MacroregionId}");
                }

                items.Add(new StateDto(state, macroregion.Abbr));
            }
            items = [.. items.Sort(stateFilter.SortBy, stateFilter.SortDirection)];

            return new PagedResponse<StateDto>(items, totalCount, pagedRequest);
        }

        public async Task<StateDto?> GetState(string abbr)
        {
            var state = await _repository.State.GetStateByAbbr(abbr);

            if (state is null)
            {
                return null;
            }

            var macroregionAbbr = await _repository.Macroregion.GetMacroregionAbbr(state.MacroregionId) ?? throw new InvalidOperationException($"Error fetching macroregion with id {state.MacroregionId}");

            return new StateDto(state, macroregionAbbr);
        }

        public async Task<IEnumerable<CandidatePartyElectionRoundRecord>?> GetStateElectionRoundRecords(string stateAbbr, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var stateId = await _repository.State.GetId(x => string.Equals(x.Abbr, stateAbbr));

            if (!stateId.HasValue)
            {
                return null;
            }

            return await _electionRoundRecord.GetElectionRoundRecordsByLocation(electionRoundRecordFilter, electionRoundRecordFilter.Role, AggregationLevel.State, stateId: stateId.Value);
        }

        public async Task<ElectionRoundDto?> GetStatePresidentialElectionRoundResults(string abbr, int year, int roundNr)
        {
            var stateId = await _repository.State.GetId(x => string.Equals(x.Abbr, abbr));

            if (!stateId.HasValue)
            {
                return null;
            }

            var electionRound = await _repository.ElectionRound.GetElectionRound(Role.President, year, roundNr, municipalityId: null, stateId: null);

            if (electionRound is null)
            {
                return null;
            }

            return await _electionRoundRecord.GetElectionOverallResults(electionRound, Role.President, AggregationLevel.State, 2, stateId: stateId.Value);
        }
    }
}
