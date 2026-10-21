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

            List<long> metropolitanAreaIds = [];
            List<long> stateIds = [];
            List<long> macroregionIds = [];
            long? foreignMunicipalityStateId = null;
            List<long> macroregionStateIds = [];

            if (municipalityFilter.MetropolitanAreas.Count > 0)
            {
                metropolitanAreaIds = [.. await _repository.MetropolitanArea.GetIds(x => municipalityFilter.MetropolitanAreas.Contains(x.Uid))];
            }

            if (municipalityFilter.States.Count > 0)
            {
                stateIds = [.. await _repository.State.GetIds(x => municipalityFilter.States.Contains(x.Abbr))];
            }

            if (municipalityFilter.Macroregions.Count > 0)
            {
                macroregionIds = [.. await _repository.Macroregion.GetIds(x => municipalityFilter.Macroregions.Contains(x.Abbr))];

                if (macroregionIds.Count > 0)
                {
                    macroregionStateIds = [.. await _repository.State.GetIds(x => macroregionIds.Contains(x.MacroregionId))];
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

            var municipalityMetropolitanAreaIds = municipalities.Select(x => x.MetropolitanAreaId).Distinct().ToList();
            var metropolitanAreas = (await _repository.MetropolitanArea.Get(x => municipalityMetropolitanAreaIds.Contains(x.Id))).ToDictionary(x => x.Id);

            var municipalityStateIds = municipalities.Select(x => x.StateId).Distinct().ToList();
            var states = (await _repository.State.Get(x => municipalityStateIds.Contains(x.Id))).ToDictionary(x => x.Id);

            var municipalityMacroregionIds = states.Select(x => x.Value.MacroregionId).Distinct().ToList();
            var macroregions = (await _repository.Macroregion.Get(x => municipalityMacroregionIds.Contains(x.Id))).ToDictionary(x => x.Id);

            List<MunicipalityDto> items = [];

            foreach (var municipality in municipalities)
            {
                var metropolitanArea = metropolitanAreas.GetValueOrDefault(municipality.MetropolitanAreaId ?? long.MinValue);

                if (!states.TryGetValue(municipality.StateId, out var state))
                {
                    throw new InvalidOperationException($"Error fetching state with id {municipality.StateId} from municipality with id {municipality.Id}");
                }

                if (!macroregions.TryGetValue(state.MacroregionId, out var macroregion))
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
