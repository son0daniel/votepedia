using elections.Commons.Constants;
using elections.Commons.Extensions;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Requests;
using elections.Repositories;

namespace elections.Services.Elections
{
    public class ElectionService : IElectionService
    {
        private readonly IRepositoryManager _repository;

        public ElectionService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        private async Task<ElectionRoundDto?> GetElectionOverallResults(string role, int ticketMembersCount, int year, int roundNr, long? municipalityId, long? stateId)
        {
            var electionRound = await _repository.ElectionRound.GetElectionRound(role, year, roundNr, municipalityId, stateId);

            if (electionRound is null)
            {
                return null;
            }

            var ticketElectionRoundStatistics = await _repository.TicketElectionRoundStatistic.GetElectionRoundStatistics(electionRound.Id, municipalityId: null, metropolitanAreaId: null, stateId: null, macroregionId: null);
            var ticketElectionRoundStatistic = ticketElectionRoundStatistics.FirstOrDefault();
            if (ticketElectionRoundStatistic is null)
            {
                return null;
            }

            var electionRoundStatisticByTicket = ticketElectionRoundStatistics.ToDictionary(x => x.TicketId);

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).ToList();
            var tickets = await _repository.Ticket.Get(x => ticketIds.Contains(x.Id));
            var coalitions = (await _repository.Coalition.Get(x => ticketIds.Contains(x.TicketId))).ToDictionary(x => x.TicketId);

            var ticketCandidateParties = await _repository.TicketCandidateParty.Get(x => ticketIds.Contains(x.TicketId));
            var ticketCandidatePartiesByTicket = ticketCandidateParties.ToLookup(x => x.TicketId);

            var candidatePartyIds = ticketCandidateParties.Select(x => x.CandidatePartyId).ToList();
            var candidateParties = (await _repository.CandidateParty.GetCandidatesAndParties(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

            List<TicketDto> ticketDtos = [];

            foreach (var ticket in tickets)
            {
                if (!electionRoundStatisticByTicket.TryGetValue(ticket.Id, out var ticketElectionRoundStatisticFromDict))
                {
                    throw new InvalidOperationException($"Error fetching statistic from ticket with id {ticket.Id}");
                }

                var coalition = coalitions.GetValueOrDefault(ticket.Id);

                var ticketCandidatePartiesFromLookup = ticketCandidatePartiesByTicket[ticket.Id].ToList();

                if (ticketCandidatePartiesFromLookup.Count < ticketMembersCount)
                {
                    throw new InvalidOperationException($"Error fetching ticket candidate parties from ticket with id {ticket.Id}");
                }

                List<TicketCandidatePartyDto> members = [];

                foreach (var ticketCandidateParty in ticketCandidatePartiesFromLookup)
                {
                    if (!candidateParties.TryGetValue(ticketCandidateParty.CandidatePartyId, out var candidateParty))
                    {
                        throw new InvalidOperationException($"Error fetching candidate party with id {ticketCandidateParty.CandidatePartyId}");
                    }

                    var candidate = candidateParty.Candidate;
                    var party = candidateParty.Party;

                    members.Add(new TicketCandidatePartyDto(candidate, party, role, ticketCandidateParty.NrLevel));
                }

                members = [.. members.OrderBy(x => x.LevelNr)];

                ticketDtos.Add(new TicketDto(ticket.Uid, coalition?.Name, coalition is not null, ticketElectionRoundStatisticFromDict, members));
            }

            ticketDtos = [.. ticketDtos.OrderByDescending(x => x.VotesCount)];

            return new ElectionRoundDto(ticketDtos, electionRound.HeldAt, ticketElectionRoundStatistic);
        }

        public async Task<ElectionRoundDto?> GetPresidentialElectionOverallResults(int year, int roundNr)
        {
            return await GetElectionOverallResults(Constant.Election.Role.President, 2, year, roundNr, municipalityId: null, stateId: null);
        }

        public async Task<IEnumerable<ElectionOverview>> GetPresidentialElectionOverviews(ElectionOverviewFilter electionOverviewFilter, int? levelNr)
        {
            var ticketElectionRoundStatistics = await _repository.TicketElectionRoundStatistic.GetElectedTicketPresidentialElectionRoundStatistics(electionOverviewFilter.Year, electionOverviewFilter.Round);

            var electionRoundIds = ticketElectionRoundStatistics.Select(x => x.ElectionRoundId).ToList();
            var electionRounds = await _repository.ElectionRound.Get(x => electionRoundIds.Contains(x.Id));
            var electionsByRound = electionRounds.ToDictionary(x => x.ElectionId);

            var electionIds = electionRounds.Select(x => x.ElectionId).ToList();
            var elections = await _repository.Election.Get(x => electionIds.Contains(x.Id));

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).ToList();
            var ticketCandidateParties = (await _repository.TicketCandidateParty.Get(x => ticketIds.Contains(x.TicketId) && x.NrLevel == (levelNr.HasValue ? levelNr.Value : 1))).ToList();
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

                return new ElectionOverview(election, electionRound, ticketElectionRoundStatistic, candidate, party);

            }).Sort(electionOverviewFilter.SortBy, electionOverviewFilter.SortDirection);
        }
    }
}
