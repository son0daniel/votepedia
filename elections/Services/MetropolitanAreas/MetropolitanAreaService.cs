using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.Entities;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static elections.Commons.Constants.Routes;

namespace elections.Services.MetropolitanAreas
{
    public class MetropolitanAreaService : IMetropolitanAreaService
    {
        private readonly IRepositoryManager _repository;

        public MetropolitanAreaService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        private static Expression<Func<MetropolitanArea, object?>> ResolveKeySelector(MetropolitanAreaSortableField? sortBy)
        {
            return sortBy switch
            {
                MetropolitanAreaSortableField.Type => x => x.Type,
                _ => x => x.Name
            };
        }

        private static string? ResolveMetropolitanAreaType(int? type)
        {
            if (!type.HasValue || (type.HasValue && type.Value > 2))
            {
                return null;
            }

            return type.Value == 1 ? Constant.MetropolitanAreaType.Standard : Constant.MetropolitanAreaType.IntegratedAreaDevelopment;
        }

        public async Task<PagedResponse<MetropolitanAreaDto>> GetMetropolitanAreas(MetropolitanAreaFilter metropolitanAreaFilter, PagedRequest pagedRequest)
        {
            metropolitanAreaFilter.States = [.. metropolitanAreaFilter.States.Select(x => x.ToUpper())];
            metropolitanAreaFilter.Macroregions = [.. metropolitanAreaFilter.Macroregions.Select(x => x.ToUpper())];

            List<State> states = [];
            List<long> stateIds = [];
            List<Macroregion> macroregions = [];
            List<long> macroregionIds = [];
            List<State> macroregionStates = [];
            List<long> macroregionStateIds = [];

            if (metropolitanAreaFilter.States.Count > 0)
            {
                states = [.. await _repository.State.Get(x => metropolitanAreaFilter.States.Contains(x.Abbr))];
                stateIds = [.. states.Select(x => x.Id)];
            }

            if (metropolitanAreaFilter.Macroregions.Count > 0)
            {
                macroregions = [.. await _repository.Macroregion.Get(x => metropolitanAreaFilter.Macroregions.Contains(x.Abbr))];
                macroregionIds = [.. macroregions.Select(x => x.Id)];

                if (macroregionIds.Count > 0)
                {
                    macroregionStates = [.. await _repository.State.Get(x => macroregionIds.Contains(x.MacroregionId))];
                    macroregionStateIds = [.. macroregionStates.Select(x => x.Id)];
                }
            }

            var metropolitanAreaStates = await _repository.MetropolitanAreaState.Get(
                x => (!stateIds.Any() || stateIds.Contains(x.StateId))
                && (!macroregionStateIds.Any() || macroregionStateIds.Contains(x.StateId)));

            var metropolitanAreaStateMetropolitanAreaIds = metropolitanAreaStates.Select(x => x.MetropolitanAreaId).Distinct().ToList();

            var metropolitanAreaType = ResolveMetropolitanAreaType(metropolitanAreaFilter.Type);

            var queryMetropolitanAreas = _repository.MetropolitanArea.GetQueryable(
                x => metropolitanAreaStateMetropolitanAreaIds.Contains(x.Id)
                && (string.IsNullOrEmpty(metropolitanAreaType) || string.Equals(x.Type, metropolitanAreaType)));

            var metropolitanAreaName = metropolitanAreaFilter.Name?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(metropolitanAreaName))
            {
                if (metropolitanAreaFilter.SearchMode == SearchMode.Exact)
                {
                    queryMetropolitanAreas = queryMetropolitanAreas.Where(x => string.Equals(x.Name, metropolitanAreaName));
                }
                else
                {
                    queryMetropolitanAreas = queryMetropolitanAreas.Where(x => x.Name.ToUpper().Contains(metropolitanAreaName));
                }
            }

            var totalCount = await queryMetropolitanAreas.CountAsync();

            var metropolitanAreas = (await _repository.MetropolitanArea.GetPaged(queryMetropolitanAreas, pagedRequest, ResolveKeySelector(metropolitanAreaFilter.SortBy), metropolitanAreaFilter.SortDirection)).ToList();

            if (states.Count == 0 && macroregionStates.Count == 0)
            {
                var metropolitanAreaStateIds = metropolitanAreaStates.Select(x => x.StateId).Distinct().ToList();
                states = [.. await _repository.State.Get(x => metropolitanAreaStateIds.Contains(x.Id))];
            }
            var statesDict = states.ToDictionary(x => x.Id);
            var macroregionStatesDict = macroregionStates.ToDictionary(x => x.Id);

            if (macroregions.Count == 0)
            {
                var metropolitanAreaMacroregionIds = states.Select(x => x.MacroregionId).Distinct().ToList();
                macroregions = [.. await _repository.Macroregion.Get(x => metropolitanAreaMacroregionIds.Contains(x.Id))];
            }
            var macroregionsDict = macroregions.ToDictionary(x => x.Id);

            var metropolitanAreaStateByMetropolitanArea = metropolitanAreaStates
                .GroupBy(x => x.MetropolitanAreaId)
                .Select(g => new { Id = g.Key, States = g.Select(x => x.StateId) })
                .ToDictionary(x => x.Id, x => x.States);

            List<MetropolitanAreaDto> items = [];

            foreach (var metropolitanArea in metropolitanAreas)
            {
                if (!metropolitanAreaStateByMetropolitanArea.TryGetValue(metropolitanArea.Id, out var metropolitanAreaStateIdsFromDict))
                {
                    throw new InvalidOperationException($"Error fetching state ids from metropolitan area with id {metropolitanArea.Id}");
                }

                List<string> stateAbbrs = [];
                List<string> macroregionAbbrs = [];

                foreach (var stateId in metropolitanAreaStateIdsFromDict)
                {
                    var state = (statesDict.GetValueOrDefault(stateId) ?? macroregionStatesDict.GetValueOrDefault(stateId)) ?? throw new InvalidOperationException($"Error fetching state with id {stateId}");

                    if (!macroregionsDict.TryGetValue(state.MacroregionId, out var macroregion))
                    {
                        throw new InvalidOperationException($"Error fetching macroregion with id {state.MacroregionId}");
                    }

                    stateAbbrs.Add(state.Abbr);
                    if (!macroregionAbbrs.Contains(macroregion.Abbr))
                    {
                        macroregionAbbrs.Add(macroregion.Abbr);
                    }
                }

                items.Add(new MetropolitanAreaDto(metropolitanArea, stateAbbrs, macroregionAbbrs));
            };

            return new PagedResponse<MetropolitanAreaDto>(items, totalCount, pagedRequest);
        }

        public async Task<MetropolitanAreaDto?> GetMetropolitanArea(string uid)
        {
            var metropolitanArea = await _repository.MetropolitanArea.GetByUid(uid);

            if (metropolitanArea is null)
            {
                return null;
            }

            var metropolitanAreaStates = (await _repository.MetropolitanAreaState.Get(x => x.MetropolitanAreaId == metropolitanArea.Id)).ToList();
            if (metropolitanAreaStates.Count == 0)
            {
                throw new InvalidOperationException($"Error fetching states from metropolitan area with id {metropolitanArea.Id}");
            }

            var stateIds = metropolitanAreaStates.Select(x => x.StateId).ToList();
            var states = await _repository.State.Get(x => stateIds.Contains(x.Id));

            var macroregionIds = states.Select(x => x.MacroregionId).ToList();
            var macroregions = (await _repository.Macroregion.Get(x => macroregionIds.Contains(x.Id))).ToList();
            if (macroregions.Count == 0)
            {
                throw new InvalidOperationException("Error fetching macroregions");
            }

            return new MetropolitanAreaDto(metropolitanArea, states.Select(x => x.Abbr), macroregions.Select(x => x.Abbr));
        }
    }
}
