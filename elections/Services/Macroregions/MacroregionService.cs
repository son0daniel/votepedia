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

namespace elections.Services.Macroregions
{
    public class MacroregionService : IMacroregionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IElectionRoundService _electionRoundRecord;

        public MacroregionService(IRepositoryManager repository, IElectionRoundService electionRoundRecord)
        {
            _repository = repository;
            _electionRoundRecord = electionRoundRecord;
        }

        private static Expression<Func<Macroregion, object?>> ResolveKeySelector(MacroregionSortableField? sortBy)
        {
            return sortBy switch
            {
                MacroregionSortableField.Abbr => x => x.Abbr,
                _ => x => x.Name
            };
        }

        public async Task<PagedResponse<MacroregionDto>> GetMacroregions(MacroregionFilter macroregionFilter, PagedRequest pagedRequest)
        {
            var queryMacroregions = _repository.Macroregion.GetQueryable(x => !string.Equals(x.Abbr, "ZZ"));

            var macroregionName = macroregionFilter.Name?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(macroregionName))
            {
                if (macroregionFilter.SearchMode == SearchMode.Exact)
                {
                    queryMacroregions = queryMacroregions.Where(x => string.Equals(x.Name, macroregionName));
                }
                else
                {
                    queryMacroregions = queryMacroregions.Where(x => x.Name.ToUpper().Contains(macroregionName));
                }
            }

            var totalCount = await queryMacroregions.CountAsync();

            var macroregions = await _repository.Macroregion.GetPaged(queryMacroregions, pagedRequest, ResolveKeySelector(macroregionFilter.SortBy), macroregionFilter.SortDirection);

            List<MacroregionDto> items = [.. macroregions.Select(x => new MacroregionDto(x))];
            items = [.. items.Sort(macroregionFilter.SortBy, macroregionFilter.SortDirection)];

            return new PagedResponse<MacroregionDto>(items, totalCount, pagedRequest);
        }

        public async Task<MacroregionDto?> GetMacroregion(string abbr)
        {
            var macroregion = await _repository.Macroregion.GetMacroregionByAbbr(abbr);

            return macroregion is null ? null : new MacroregionDto(macroregion);
        }

        public async Task<IEnumerable<CandidatePartyElectionRoundRecord>?> GetMacroregionElectionRoundRecords(string macroregionAbbr, ElectionRoundRecordFilter electionRoundRecordFilter)
        {
            var macroregionId = await _repository.Macroregion.GetId(x => string.Equals(x.Abbr, macroregionAbbr));

            if (!macroregionId.HasValue)
            {
                return null;
            }

            return await _electionRoundRecord.GetElectionRoundRecordsByLocation(electionRoundRecordFilter, electionRoundRecordFilter.Role, AggregationLevel.Macroregion, macroregionId: macroregionId.Value);
        }

        public async Task<ElectionRoundDto?> GetMacroregionPresidentialElectionRoundResults(string abbr, int year, int roundNr)
        {
            var macroregionId = await _repository.Macroregion.GetId(x => string.Equals(x.Abbr, abbr));

            if (!macroregionId.HasValue)
            {
                return null;
            }

            var electionRound = await _repository.ElectionRound.GetElectionRound(Role.President, year, roundNr, municipalityId: null, stateId: null);

            if (electionRound is null)
            {
                return null;
            }

            return await _electionRoundRecord.GetElectionOverallResults(electionRound, Role.President, AggregationLevel.Macroregion, 2, macroregionId: macroregionId.Value);
        }
    }
}
