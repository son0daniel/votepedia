using elections.Commons.Extensions;
using elections.Models.DTOs;
using elections.Models.Entities;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace elections.Services.Municipalities
{
    public class MunicipalityService : IMunicipalityService
    {
        private readonly IRepositoryManager _repository;

        public MunicipalityService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<PagedResponse<MunicipalityDto>> GetMunicipalities(MunicipalityFilter municipalityFilter, PagedRequest pagedRequest)
        {
            municipalityFilter.States = [.. municipalityFilter.States.Select(x => x.ToUpper())];
            municipalityFilter.Macroregions = [.. municipalityFilter.Macroregions.Select(x => x.ToUpper())];

            List<MetropolitanArea> metropolitanAreas = [];
            List<long> metropolitanAreaIds = [];
            List<State> states = [];
            List<long> stateIds = [];
            List<Macroregion> macroregions = [];
            List<long> macroregionIds = [];
            List<State> macroregionStates = [];
            List<long> macroregionStateIds = [];
            long? foreignMunicipalityStateId = null;

            if (municipalityFilter.MetropolitanAreas.Count > 0)
            {
                metropolitanAreas = [.. await _repository.MetropolitanArea.Get(x => municipalityFilter.MetropolitanAreas.Contains(x.Uid))];
                metropolitanAreaIds = [.. metropolitanAreas.Select(x => x.Id)];
            }

            if (municipalityFilter.States.Count > 0)
            {
                states = [.. await _repository.State.Get(x => municipalityFilter.States.Contains(x.Abbr))];
                stateIds = [.. states.Select(x => x.Id)];
            }

            if (municipalityFilter.Macroregions.Count > 0)
            {
                macroregions = [.. await _repository.Macroregion.Get(x => municipalityFilter.Macroregions.Contains(x.Abbr))];
                macroregionIds = [.. macroregions.Select(x => x.Id)];

                if (macroregionIds.Count > 0)
                {
                    macroregionStates = [.. await _repository.State.Get(x => macroregionIds.Contains(x.MacroregionId))];
                    macroregionStateIds = [.. macroregionStates.Select(x => x.Id)];
                }                
            }

            if (municipalityFilter.OnlyNational)
            {
                foreignMunicipalityStateId = await _repository.State.GetId(x => string.Equals(x.Abbr, "ZZ"));
            }

            var queryMunicipalities = _repository.Municipality.GetQueryable(
                x => (!metropolitanAreaIds.Any() || (x.MetropolitanAreaId.HasValue && metropolitanAreaIds.Contains(x.MetropolitanAreaId.Value)))
                && (!stateIds.Any() || stateIds.Contains(x.StateId))
                && (!macroregionStateIds.Any() || macroregionStateIds.Contains(x.StateId))
                && (!foreignMunicipalityStateId.HasValue || x.StateId != foreignMunicipalityStateId.Value));

            var municipalityName = municipalityFilter.Name?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(municipalityName))
            {
                if (municipalityFilter.SearchMode == SearchMode.Exact)
                {
                    queryMunicipalities = queryMunicipalities.Where(x => string.Equals(x.Name, municipalityName));
                }
                else
                {
                    queryMunicipalities = queryMunicipalities.Where(x => x.Name.ToUpper().Contains(municipalityName));
                }
            }

            var totalCount = await queryMunicipalities.CountAsync();

            var municipalities = (await _repository.Municipality.GetPaged(queryMunicipalities, pagedRequest, x => x.Name, municipalityFilter.SortDirection)).ToList();

            if (metropolitanAreas.Count == 0)
            {
                var municipalityMetropolitanAreaIds = municipalities.Select(x => x.MetropolitanAreaId).Distinct().ToList();
                metropolitanAreas = [.. await _repository.MetropolitanArea.Get(x => municipalityMetropolitanAreaIds.Contains(x.Id))];
            }
            var metropolitanAreasDict = metropolitanAreas.ToDictionary(x => x.Id);
            
            if (states.Count == 0 && macroregionStates.Count == 0)
            {
                var municipalityStateIds = municipalities.Select(x => x.StateId).Distinct().ToList();
                states = [.. await _repository.State.Get(x => municipalityStateIds.Contains(x.Id))];
            }
            var statesDict = states.ToDictionary(x => x.Id);
            var macroregionStatesDict = macroregionStates.ToDictionary(x => x.Id);
            
            if (macroregions.Count == 0)
            {
                var municipalityMacroregionIds = states.Select(x => x.MacroregionId).Distinct().ToList();
                macroregions = [.. await _repository.Macroregion.Get(x => municipalityMacroregionIds.Contains(x.Id))];
            }
            var macroregionsDict = macroregions.ToDictionary(x => x.Id);

            List<MunicipalityDto> items = [];

            foreach (var municipality in municipalities)
            {
                var metropolitanArea = metropolitanAreasDict.GetValueOrDefault(municipality.MetropolitanAreaId ?? long.MinValue);

                var state = (statesDict.GetValueOrDefault(municipality.StateId) ?? macroregionStatesDict.GetValueOrDefault(municipality.StateId)) ?? throw new InvalidOperationException($"Error fetching state with id {municipality.StateId} from municipality with id {municipality.Id}");

                if (!macroregionsDict.TryGetValue(state.MacroregionId, out var macroregion))
                {
                    throw new InvalidOperationException($"Error fetching macroregion with id {state.Id} from state with id {state.MacroregionId}");
                }

                items.Add(new MunicipalityDto(municipality, metropolitanArea, state.Abbr, macroregion.Abbr));
            }
            items = [.. items.Sort(municipalityFilter.SortBy, municipalityFilter.SortDirection)];

            return new PagedResponse<MunicipalityDto>(items, totalCount, pagedRequest);
        }

        public async Task<MunicipalityDto?> GetMunicipality(long tseId)
        {
            var municipality = await _repository.Municipality.GetMunicipalityByTseId(tseId);

            if (municipality is null)
            {
                return null;
            }

            MetropolitanArea? metropolitanArea = null;
            if (municipality.MetropolitanAreaId.HasValue)
            {
                metropolitanArea = await _repository.MetropolitanArea.GetById(municipality.MetropolitanAreaId.Value);

                if (metropolitanArea is null)
                {
                    throw new InvalidOperationException($"Error fetching metropolitan area with id {municipality.MetropolitanAreaId.Value}");
                }
            }

            var state = await _repository.State.GetById(municipality.StateId) ?? throw new InvalidOperationException($"Error fetching state with id {municipality.StateId}");

            var macroregionAbbr = await _repository.Macroregion.GetMacroregionAbbr(state.MacroregionId) ??  throw new InvalidOperationException($"Error fetching macroregion with id {state.MacroregionId}"); ;

            return new MunicipalityDto(municipality, metropolitanArea, state.Abbr, macroregionAbbr);
        }
    }
}
