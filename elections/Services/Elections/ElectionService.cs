using elections.Commons.Constants;
using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Entities;
using elections.Models.Requests;
using elections.Repositories;
using elections.Services.ElectionResults;

namespace elections.Services.Elections
{
    public class ElectionService : IElectionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IElectionResultService _electionResultService;

        public ElectionService(IRepositoryManager repository, IElectionResultService electionResultService)
        {
            _repository = repository;
            _electionResultService = electionResultService;
        }

        public async Task<IEnumerable<ElectionOverview>> GetElectionOverviews(ElectionFilter electionFilter, string role, string? municipalityUid, string? stateAbbr)
        {
            var internalElectionFilter = await _electionResultService.ResolveElectionFilter(electionFilter, role, municipalityUid, stateAbbr);

            var ticketElectionRoundStatistics = await _repository.TicketElectionRoundStatistic.GetElectedTicketElectionRoundStatisticsByRound(internalElectionFilter);

            var electionRoundIds = ticketElectionRoundStatistics.Select(x => x.ElectionRoundId).ToList();
            var electionRounds = await _repository.ElectionRound.Get(x => electionRoundIds.Contains(x.Id));
            var electionsByRound = electionRounds.ToDictionary(x => x.ElectionId);

            var electionIds = electionRounds.Select(x => x.ElectionId).ToList();
            var elections = await _repository.Election.Get(x => electionIds.Contains(x.Id));

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).ToList();
            var ticketCandidateParties = (await _repository.TicketCandidateParty.Get(x => ticketIds.Contains(x.TicketId) && x.NrLevel == 1)).ToList();
            var candidatePartyIds = ticketCandidateParties.Select(x => x.CandidatePartyId).ToList();

            var ticketElectionRoundStatisticsByRound = ticketElectionRoundStatistics.ToDictionary(x => x.ElectionRoundId);

            var ticketCandidatePartiesByTicket = ticketCandidateParties.ToDictionary(x => x.TicketId);

            var candidateParties = (await _repository.CandidateParty.GetCandidatesAndParties(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

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
                if (!candidateParties.TryGetValue(candidatePartyId, out var candidateParty))
                {
                    throw new InvalidOperationException($"Error while fetching candidate party from ticket candidate party with id {candidatePartyId}");
                }

                var candidate = candidateParty.Candidate;
                var party = candidateParty.Party;

                return ElectionOverview.New(election, electionRound, ticketElectionRoundStatistic, candidate, party);

            }).Sort(electionFilter.SortBy, electionFilter.SortDirection);
        }
    }
}
