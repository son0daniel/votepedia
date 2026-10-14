using elections.Commons.Constants;
using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using Microsoft.EntityFrameworkCore;

namespace elections.Services.Parties
{
    public class PartyService : IPartyService
    {
        private readonly IRepositoryManager _repository;

        public PartyService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<PagedResponse<PartyDto>> GetParties(PartyFilter partyFilter, PagedRequest pagedRequest)
        {
            var queryParties = _repository.Party.GetQueryable(
                x => (string.IsNullOrEmpty(partyFilter.Name) || x.Name.ToUpper().Contains(partyFilter.Name.ToUpper()))
                && (string.IsNullOrEmpty(partyFilter.Abbr) || string.Equals(x.Abbr, partyFilter.Abbr))
                && (!partyFilter.Nr.HasValue || x.NrVoteable == partyFilter.Nr.Value)
                && (!partyFilter.IsActive.HasValue || x.IsActive == partyFilter.IsActive.Value));

            var totalCount = await queryParties.CountAsync();

            var parties = (await _repository.Party.GetPaged(queryParties, pagedRequest)).ToList();
            var items = parties.Select(x => new PartyDto(x)).Sort(partyFilter.SortBy, partyFilter.SortDirection).ToList();

            return new PagedResponse<PartyDto>(items, totalCount, pagedRequest);
        }

        public async Task<IEnumerable<PartyElectionOverview>> GetPartyPresidentialElectionOverviews(string partyUid, ElectionOverviewFilter electionOverviewFilter, bool? isElected)
        {
            var partyId = (await _repository.Party.GetByUid(partyUid))?.Id;
            if (!partyId.HasValue)
            {
                return [];
            }

            var ticketElectionRoundStatistics = await _repository.TicketElectionRoundStatistic.GetPartyTicketPresidentialElectionRoundStatistics(partyId.Value, electionOverviewFilter.Year, electionOverviewFilter.Round, isElected);

            var electionRoundIds = ticketElectionRoundStatistics.Select(x => x.ElectionRoundId).ToList();
            var electionRounds = await _repository.ElectionRound.Get(x => electionRoundIds.Contains(x.Id));
            var electionsByRound = electionRounds.ToDictionary(x => x.ElectionId);

            var electionIds = electionRounds.Select(x => x.ElectionId).ToList();
            var elections = await _repository.Election.Get(x => electionIds.Contains(x.Id));

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).ToList();
            var ticketCandidateParties = (await _repository.TicketCandidateParty.Get(
                x => ticketIds.Contains(x.TicketId)
                && x.CandidateParty.PartyId == partyId)).ToList();
            var candidatePartyIds = ticketCandidateParties.Select(x => x.CandidatePartyId).ToList();

            var ticketElectionRoundStatisticsByRound = ticketElectionRoundStatistics.ToDictionary(x => x.ElectionRoundId);

            var ticketCandidatePartiesByTicket = ticketCandidateParties.ToLookup(x => x.TicketId);

            var candidateParties = (await _repository.CandidateParty.GetCandidates(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

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
                var ticketCandidateParties = ticketCandidatePartiesByTicket[ticketId].ToList();
                if (ticketCandidateParties.Count == 0)
                {
                    throw new InvalidOperationException($"Error while fetching ticket candidate party from ticket with id {ticketId}");
                }

                List<TicketCandidateDto> candidates = [];

                foreach(var ticketCandidateParty in ticketCandidateParties)
                {
                    var candidatePartyId = ticketCandidateParty.CandidatePartyId;
                    if (!candidateParties.TryGetValue(candidatePartyId, out var candidateParty))
                    {
                        throw new InvalidOperationException($"Error while fetching candidate party from ticket candidate party with id {candidatePartyId}");
                    }

                    var officeRole = RoleResolver.GetOfficeRole(Constant.Election.Role.President, ticketCandidateParty.NrLevel);
                    
                    var candidate = candidateParty.Candidate;

                    candidates.Add(new TicketCandidateDto(candidate, officeRole));
                }

                return new PartyElectionOverview(election, electionRound, ticketElectionRoundStatistic, candidates);

            }).Sort(electionOverviewFilter.SortBy, electionOverviewFilter.SortDirection);
        }
    }
}
