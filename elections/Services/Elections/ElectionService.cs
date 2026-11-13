using elections.Commons.Constants;
using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Entities;
using elections.Models.Requests;
using elections.Models.Requests.ElectionRounds;
using elections.Repositories;
using elections.Services.ElectionRoundRecords;
using static elections.Commons.Constants.Constant.Election;

namespace elections.Services.Elections
{
    public class ElectionService : IElectionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IElectionRoundService _electionRound;

        public ElectionService(IRepositoryManager repository, IElectionRoundService electionRound)
        {
            _repository = repository;
            _electionRound = electionRound;
        }

        public async Task<ElectionRoundDto?> GetPresidentialElectionOverallResults(int year, int roundNr)
        {
            var electionRound = await _repository.ElectionRound.GetElectionRound(Role.President, year, roundNr, municipalityId: null, stateId: null);

            if (electionRound is null)
            {
                return null;
            }

            return await _electionRound.GetElectionOverallResults(electionRound, Role.President, AggregationLevel.Overall, 2);
        }

        public async Task<IEnumerable<ElectionOverview>> GetPresidentialElectionOverviews(ElectionOverviewFilter electionOverviewFilter, int? levelNr)
        {
            var ticketElectionRoundStatistics = await _repository.TicketElectionRoundStatistic.GetElectedTicketPresidentialElectionRoundStatistics(electionOverviewFilter.Year, electionOverviewFilter.Round);

            var electionRoundIds = ticketElectionRoundStatistics.Select(x => x.ElectionRoundId).ToList();
            var electionRounds = await _repository.ElectionRound.Get(x => electionRoundIds.Contains(x.Id));
            var electionsByRound = electionRounds.ToDictionary(x => x.ElectionId);

            var electionIds = electionRounds.Select(x => x.ElectionId).ToList();
            var elections = await _repository.Election.Get(x => electionIds.Contains(x.Id));

            (var tickets, var ticketCandidateParties, var candidateParties) = await _electionRound.GetTicketRelatedInformation(ticketElectionRoundStatistics, levelNr);

            var ticketElectionRoundStatisticsByRound = ticketElectionRoundStatistics.ToDictionary(x => x.ElectionRoundId);

            var ticketCandidatePartiesByTicket = ticketCandidateParties.ToDictionary(x => x.TicketId);

            var candidatePartiesDict = candidateParties.ToDictionary(x => x.Id);

            return elections.Select(election =>
            {
                if (!electionsByRound.TryGetValue(election.Id, out var electionRound))
                {
                    throw new InvalidOperationException($"Couldn't find election round for election with id {election.Id}");
                }

                if (!ticketElectionRoundStatisticsByRound.TryGetValue(electionRound.Id, out var ticketElectionRoundStatistic))
                {
                    throw new InvalidOperationException($"Error while fetching statistic from election round with id {electionRound.Id}");
                }

                var ticketId = ticketElectionRoundStatistic.TicketId;
                if (!ticketCandidatePartiesByTicket.TryGetValue(ticketId, out var ticketCandidateParty))
                {
                    throw new InvalidOperationException($"Error while fetching ticket candidate party from ticket with id {ticketId}");
                }

                var candidatePartyId = ticketCandidateParty.CandidatePartyId;
                if (!candidatePartiesDict.TryGetValue(candidatePartyId, out var candidateParty))
                {
                    throw new InvalidOperationException($"Error while fetching candidate party from ticket candidate party with id {candidatePartyId}");
                }

                var candidate = candidateParty.Candidate;
                var party = candidateParty.Party;

                return new ElectionOverview(election, electionRound, ticketElectionRoundStatistic, candidate, party);

            }).Sort(electionOverviewFilter.SortBy, electionOverviewFilter.SortDirection);
        }

        public async Task<IEnumerable<MunicipalityElectionRoundDto>?> GetPresidentialElectionMunicipalityLevelResults(int year, int roundNr, MunicipalityElectionRoundFilter municipalityElectionRoundFilter)
        {
            var electionRound = await _repository.ElectionRound.GetElectionRound(Role.President, year, roundNr, municipalityId: null, stateId: null);

            if (electionRound is null)
            {
                return null;
            }

            long? metropolitanAreaId = null;
            long? macroregionId = null;
            State? stateFromRequest = null;

            if (!string.IsNullOrEmpty(municipalityElectionRoundFilter.MetropolitanArea))
            {
                metropolitanAreaId = await _repository.MetropolitanArea.GetIdByUid(municipalityElectionRoundFilter.MetropolitanArea);
            }

            var macroregionAbbr = municipalityElectionRoundFilter.Macroregion?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(macroregionAbbr))
            {
                macroregionId = await _repository.Macroregion.GetId(x => string.Equals(x.Abbr, macroregionAbbr));
            }

            var stateAbbr = municipalityElectionRoundFilter.State?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(stateAbbr))
            {
                stateFromRequest = await _repository.State.GetStateByAbbr(stateAbbr);
            }

            var ticketElectionRoundStatistics = (await _repository.TicketElectionRoundStatistic.GetMunicipalityLevelResults(electionRound.Id, metropolitanAreaId, stateFromRequest?.Id, macroregionId, municipalityElectionRoundFilter.OnlyNational)).ToList();

            var municipalityIds = ticketElectionRoundStatistics.Select(x => x.MunicipalityId).ToList();
            var municipalities = await _repository.Municipality.Get(x => municipalityIds.Contains(x.Id));

            Dictionary<long, State>? statesDict = null;
            if (stateFromRequest is null)
            {
                var stateIds = municipalities.Select(x => x.StateId).Distinct().ToList();
                statesDict = (await _repository.State.Get(x => stateIds.Contains(x.Id))).ToDictionary(x => x.Id);
            }
            

            if (ticketElectionRoundStatistics.Count == 0)
            {
                return [];
            }

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).Distinct();
            var tickets = (await _repository.Ticket.Get(x => ticketIds.Contains(x.Id))).ToDictionary(x => x.Id);

            var statisticsByMunicipality = ticketElectionRoundStatistics.GroupBy(x => x.MunicipalityId).Select(g => new { MunicipalityId = g.Key, Statistics = g.Select(x => x) }).ToDictionary(x => x.MunicipalityId!.Value, x => x.Statistics);

            List<MunicipalityElectionRoundDto> electionRoundMunicipalities = [];

            foreach (var municipality in municipalities)
            {
                var municipalityStatistics = statisticsByMunicipality.GetValueOrDefault(municipality.Id) ?? throw new InvalidOperationException($"Error fetching statistics from municipality with id {municipality.Id}");

                var summaryStatistic = municipalityStatistics.FirstOrDefault() ?? throw new InvalidOperationException("Election summary is missing.");

                var state = stateFromRequest ?? statesDict?.GetValueOrDefault(municipality.StateId) ?? throw new InvalidOperationException($"Error fetching state with id {municipality.StateId}");

                List<MinimalTicketDto> ticketDtos = [];

                foreach(var ticketStatistic in municipalityStatistics)
                {
                    var ticket = tickets.GetValueOrDefault(ticketStatistic.TicketId) ?? throw new InvalidOperationException($"Ticket with id {ticketStatistic.TicketId} is missing");

                    ticketDtos.Add(new MinimalTicketDto(ticket.Uid, ticketStatistic));
                }

                electionRoundMunicipalities.Add(new MunicipalityElectionRoundDto(ticketDtos, municipality, state.Abbr, summaryStatistic));
            }

            return electionRoundMunicipalities.Sort(municipalityElectionRoundFilter.SortBy, municipalityElectionRoundFilter.SortDirection);
        }

        public async Task<IEnumerable<MetropolitanAreaElectionRoundDto>?> GetPresidentialElectionMetropolitanAreaLevelResults(int year, int roundNr, MetropolitanAreaElectionRoundFilter metropolitanAreaElectionRoundFilter)
        {
            var electionRound = await _repository.ElectionRound.GetElectionRound(Role.President, year, roundNr, municipalityId: null, stateId: null);

            if (electionRound is null)
            {
                return null;
            }

            long? macroregionId = null;
            long? stateId = null;

            var macroregionAbbr = metropolitanAreaElectionRoundFilter.Macroregion?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(macroregionAbbr))
            {
                macroregionId = await _repository.Macroregion.GetId(x => string.Equals(x.Abbr, macroregionAbbr));
            }

            var stateAbbr = metropolitanAreaElectionRoundFilter.State?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(stateAbbr))
            {
                stateId = await _repository.State.GetId(x => string.Equals(x.Abbr, stateAbbr));
            }

            var ticketElectionRoundStatistics = (await _repository.TicketElectionRoundStatistic.GetMetropolitanAreaLevelResults(electionRound.Id, stateId, macroregionId)).ToList();

            var metropolitanAreaIds = ticketElectionRoundStatistics.Select(x => x.MetropolitanAreaId).ToList();
            var metropolitanAreas = await _repository.MetropolitanArea.Get(x => metropolitanAreaIds.Contains(x.Id));

            if (ticketElectionRoundStatistics.Count == 0)
            {
                return [];
            }

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).Distinct();
            var tickets = (await _repository.Ticket.Get(x => ticketIds.Contains(x.Id))).ToDictionary(x => x.Id);

            var statisticsByMetropolitanArea = ticketElectionRoundStatistics.GroupBy(x => x.MetropolitanAreaId).Select(g => new { MetropolitanAreaId = g.Key, Statistics = g.Select(x => x) }).ToDictionary(x => x.MetropolitanAreaId!.Value, x => x.Statistics);

            List<MetropolitanAreaElectionRoundDto> electionRoundMetropolitanAreas = [];

            foreach (var metropolitanArea in metropolitanAreas)
            {
                var metropolitanAreaStatistics = statisticsByMetropolitanArea.GetValueOrDefault(metropolitanArea.Id) ?? throw new InvalidOperationException($"Error fetching statistics from metropolitan area with id {metropolitanArea.Id}");

                var summaryStatistic = metropolitanAreaStatistics.FirstOrDefault() ?? throw new InvalidOperationException("Election summary is missing.");

                List<MinimalTicketDto> ticketDtos = [];

                foreach (var ticketStatistic in metropolitanAreaStatistics)
                {
                    var ticket = tickets.GetValueOrDefault(ticketStatistic.TicketId) ?? throw new InvalidOperationException($"Ticket with id {ticketStatistic.TicketId} is missing");

                    ticketDtos.Add(new MinimalTicketDto(ticket.Uid, ticketStatistic));
                }

                electionRoundMetropolitanAreas.Add(new MetropolitanAreaElectionRoundDto(ticketDtos, metropolitanArea, summaryStatistic));
            }

            return electionRoundMetropolitanAreas.Sort(metropolitanAreaElectionRoundFilter.SortBy, metropolitanAreaElectionRoundFilter.SortDirection);
        }

        public async Task<IEnumerable<StateElectionRoundDto>?> GetPresidentialElectionStateLevelResults(int year, int roundNr, StateElectionRoundFilter stateElectionRoundFilter)
        {
            var electionRound = await _repository.ElectionRound.GetElectionRound(Role.President, year, roundNr, municipalityId: null, stateId: null);

            if (electionRound is null)
            {
                return null;
            }

            Macroregion? macroregionFromRequest = null;

            var macroregionAbbr = stateElectionRoundFilter.Macroregion?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(macroregionAbbr))
            {
                macroregionFromRequest = await _repository.Macroregion.GetMacroregionByAbbr(macroregionAbbr);
            }

            var ticketElectionRoundStatistics = (await _repository.TicketElectionRoundStatistic.GetStateLevelResults(electionRound.Id, macroregionFromRequest?.Id, stateElectionRoundFilter.OnlyNational)).ToList();

            if (ticketElectionRoundStatistics.Count == 0)
            {
                return [];
            }

            var stateIds = ticketElectionRoundStatistics.Select(x => x.StateId).Distinct().ToList();
            var states = await _repository.State.Get(x => stateIds.Contains(x.Id));

            Dictionary<long, Macroregion>? macroregionsDict = null;
            if (macroregionFromRequest is null)
            {
                var macroregionIds = states.Select(x => x.Id).Distinct().ToList();
                macroregionsDict = (await _repository.Macroregion.Get(x => macroregionIds.Contains(x.Id))).ToDictionary(x => x.Id);
            }

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).Distinct();
            var tickets = (await _repository.Ticket.Get(x => ticketIds.Contains(x.Id))).ToDictionary(x => x.Id);

            var statisticsByState = ticketElectionRoundStatistics.GroupBy(x => x.StateId).Select(g => new { StateId = g.Key, Statistics = g.Select(x => x) }).ToDictionary(x => x.StateId!.Value, x => x.Statistics);

            List<StateElectionRoundDto> electionRoundStates = [];

            foreach(var state in states)
            {
                var stateStatistics = statisticsByState.GetValueOrDefault(state.Id) ?? throw new InvalidOperationException($"Error fetching statistics from state with id {state.Id}");

                var summaryStatistic = stateStatistics.FirstOrDefault() ?? throw new InvalidOperationException("Election summary is missing.");

                var macroregion = macroregionFromRequest ?? macroregionsDict?.GetValueOrDefault(state.MacroregionId) ?? throw new InvalidOperationException($"Error fetching macroregion with id {state.MacroregionId}");

                List<MinimalTicketDto> ticketDtos = [];

                foreach (var ticketStatistic in stateStatistics)
                {
                    var ticket = tickets.GetValueOrDefault(ticketStatistic.TicketId) ?? throw new InvalidOperationException($"Ticket with id {ticketStatistic.TicketId} is missing");

                    ticketDtos.Add(new MinimalTicketDto(ticket.Uid, ticketStatistic));
                }

                electionRoundStates.Add(new StateElectionRoundDto(ticketDtos, state, macroregion.Abbr, summaryStatistic));
            }

            return electionRoundStates.Sort(stateElectionRoundFilter.SortBy, stateElectionRoundFilter.SortDirection);
        }

        public async Task<IEnumerable<MacroregionElectionRoundDto>?> GetPresidentialElectionMacroregionLevelResults(int year, int roundNr, MacroregionElectionRoundFilter macroregionElectionRoundFilter)
        {
            var electionRound = await _repository.ElectionRound.GetElectionRound(Role.President, year, roundNr, municipalityId: null, stateId: null);

            if (electionRound is null)
            {
                return null;
            }

            var ticketElectionRoundStatistics = (await _repository.TicketElectionRoundStatistic.GetMacroregionLevelResults(electionRound.Id, macroregionElectionRoundFilter.OnlyNational)).ToList();

            if (ticketElectionRoundStatistics.Count == 0)
            {
                return [];
            }

            var macroregionIds = ticketElectionRoundStatistics.Select(x => x.MacroregionId).Distinct().ToList();
            var macroregions = await _repository.Macroregion.Get(x => macroregionIds.Contains(x.Id));

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).Distinct();
            var tickets = (await _repository.Ticket.Get(x => ticketIds.Contains(x.Id))).ToDictionary(x => x.Id);

            var statisticsByMacroregion = ticketElectionRoundStatistics.GroupBy(x => x.MacroregionId).Select(g => new { MacroregionId = g.Key, Statistics = g.Select(x => x) }).ToDictionary(x => x.MacroregionId!.Value, x => x.Statistics);

            List<MacroregionElectionRoundDto> electionRoundMacroregions = [];

            foreach (var macroregion in macroregions)
            {
                var macroregionStatistics = statisticsByMacroregion.GetValueOrDefault(macroregion.Id) ?? throw new InvalidOperationException($"Error fetching statistics from macroregion with id {macroregion.Id}");

                var summaryStatistic = macroregionStatistics.FirstOrDefault() ?? throw new InvalidOperationException("Election summary is missing.");

                List<MinimalTicketDto> ticketDtos = [];

                foreach (var ticketStatistic in macroregionStatistics)
                {
                    var ticket = tickets.GetValueOrDefault(ticketStatistic.TicketId) ?? throw new InvalidOperationException($"Ticket with id {ticketStatistic.TicketId} is missing");

                    ticketDtos.Add(new MinimalTicketDto(ticket.Uid, ticketStatistic));
                }

                electionRoundMacroregions.Add(new MacroregionElectionRoundDto(ticketDtos, macroregion, summaryStatistic));
            }

            return electionRoundMacroregions.Sort(macroregionElectionRoundFilter.SortBy, macroregionElectionRoundFilter.SortDirection);
        }

        public async Task<IEnumerable<CandidatePartyElectionRoundRecord>> GetPresidentialElectionRoundRecords(PresidentialElectionRoundRecordFilter presidentialElectionRoundRecordFilter)
        {
            return await _electionRound.GetElectionRoundRecordsByLocation(presidentialElectionRoundRecordFilter, 1, AggregationLevel.Overall);
        }
    }
}
