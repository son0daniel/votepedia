using elections.Commons.Constants;
using elections.Commons.Extensions;
using elections.Commons.Utils;
using elections.Models.DTOs;
using elections.Models.DTOs.ElectionOverviews;
using elections.Models.Entities;
using elections.Models.Pagination;
using elections.Models.Requests;
using elections.Repositories;
using elections.Services.Elections;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace elections.Services.Candidates
{
    public class CandidateService : ICandidateService
    {
        private readonly IRepositoryManager _repository;

        public CandidateService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        private static Expression<Func<Candidate, object?>> ResolveKeySelector(CandidateSortableField? candidateSortableField)
        {
            return candidateSortableField switch
            {
                CandidateSortableField.Name => x => x.Name,
                CandidateSortableField.DisplayName => x => x.DisplayName,
                CandidateSortableField.BirthDate => x => x.BirthDate,
                CandidateSortableField.DeathDate => x => x.DeathDate,
                _ => x => x.Name
            };
        }

        public async Task<PagedResponse<CandidateDto>> GetCandidates(CandidateFilter candidateFilter, PagedRequest pagedRequest)
        {
            var nameQuery = candidateFilter.Name?.ToUpper();

            var queryCandidates = _repository.Candidate.GetQueryable(
                x => (!candidateFilter.IsDeceased.HasValue || x.IsDeceased == candidateFilter.IsDeceased.Value) 
                && (string.IsNullOrEmpty(nameQuery) || x.Name.ToUpper().Contains(nameQuery)));

            var totalCount = await queryCandidates.CountAsync();

            var candidates = (await _repository.Candidate.GetPaged(queryCandidates, pagedRequest, ResolveKeySelector(candidateFilter.SortBy), candidateFilter.SortDirection)).ToList();
            var items = candidates.Select(x => new CandidateDto(x)).Sort(candidateFilter.SortBy, candidateFilter.SortDirection).ToList();

            return new PagedResponse<CandidateDto>(items, totalCount, pagedRequest);
        }

        public async Task<IEnumerable<CandidateElectionOverview>> GetCandidatePresidentialElectionOverviews(string candidateUid, ElectionOverviewFilter electionOverviewFilter, bool? isElected)
        {
            var candidateId = (await _repository.Candidate.GetByUid(candidateUid))?.Id;
            if (!candidateId.HasValue)
            {
                return [];
            }

            var ticketElectionRoundStatistics = await _repository.TicketElectionRoundStatistic.GetCandidateTicketPresidentialElectionRoundStatistics(candidateId.Value, electionOverviewFilter.Year, electionOverviewFilter.Round, isElected);

            var electionRoundIds = ticketElectionRoundStatistics.Select(x => x.ElectionRoundId).ToList();
            var electionRounds = await _repository.ElectionRound.Get(x => electionRoundIds.Contains(x.Id));
            var electionsByRound = electionRounds.ToDictionary(x => x.ElectionId);

            var electionIds = electionRounds.Select(x => x.ElectionId).ToList();
            var elections = await _repository.Election.Get(x => electionIds.Contains(x.Id));

            var ticketIds = ticketElectionRoundStatistics.Select(x => x.TicketId).ToList();
            var ticketCandidateParties = (await _repository.TicketCandidateParty.Get(
                x => ticketIds.Contains(x.TicketId) 
                && x.CandidateParty.CandidateId == candidateId)).ToList();
            var candidatePartyIds = ticketCandidateParties.Select(x => x.CandidatePartyId).ToList();

            var ticketElectionRoundStatisticsByRound = ticketElectionRoundStatistics.ToDictionary(x => x.ElectionRoundId);

            var ticketCandidatePartiesByTicket = ticketCandidateParties.ToDictionary(x => x.TicketId);

            var candidateParties = (await _repository.CandidateParty.GetParties(x => candidatePartyIds.Contains(x.Id))).ToDictionary(x => x.Id);

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

                var officeRole = RoleResolver.GetOfficeRole(Constant.Election.Role.President, ticketCandidateParty.NrLevel);

                var party = candidateParty.Party;

                return new CandidateElectionOverview(election, electionRound, ticketElectionRoundStatistic, officeRole, party);

            }).Sort(electionOverviewFilter.SortBy, electionOverviewFilter.SortDirection);
        }
    }
}
