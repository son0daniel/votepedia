using elections.Commons.Constants;
using elections.Models.DTOs;
using elections.Models.Entities;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

            List<long> stateIds = [];
            List<long> macroregionIds = [];
            List<long> macroregionStateIds = [];

            if (metropolitanAreaFilter.States.Count > 0)
            {
                stateIds = [.. await _repository.State.GetIds(x => metropolitanAreaFilter.States.Contains(x.Abbr))];
            }

            if (metropolitanAreaFilter.Macroregions.Count > 0)
            {
                macroregionIds = [.. await _repository.Macroregion.GetIds(x => metropolitanAreaFilter.Macroregions.Contains(x.Abbr))];

                if (macroregionIds.Count > 0)
                {
                    macroregionStateIds = [.. await _repository.State.GetIds(x => macroregionIds.Contains(x.MacroregionId))];
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

            var metropolitanAreaStateIds = metropolitanAreaStates.Select(x => x.StateId).Distinct().ToList();
            var states = (await _repository.State.Get(x => metropolitanAreaStateIds.Contains(x.Id))).ToDictionary(x => x.Id);

            var metropolitanAreaMacroregionIds = states.Select(x => x.Value.MacroregionId).Distinct().ToList();
            var macroregions = (await _repository.Macroregion.Get(x => metropolitanAreaMacroregionIds.Contains(x.Id))).ToDictionary(x => x.Id);

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
                    if (!states.TryGetValue(stateId, out var state))
                    {
                        throw new InvalidOperationException($"Error fetching state with id {stateId}");
                    }

                    if (!macroregions.TryGetValue(state.MacroregionId, out var macroregion))
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
    }
}
