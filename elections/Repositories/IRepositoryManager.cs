using elections.Repositories.CandidateParties;
using elections.Repositories.Candidates;
using elections.Repositories.CoalitionMembers;
using elections.Repositories.Coalitions;
using elections.Repositories.ElectionRounds;
using elections.Repositories.Elections;
using elections.Repositories.FederationParties;
using elections.Repositories.Federations;
using elections.Repositories.Macroregions;
using elections.Repositories.MetropolitanAreas;
using elections.Repositories.Municipalities;
using elections.Repositories.Parties;
using elections.Repositories.States;
using elections.Repositories.TicketCandidateParties;
using elections.Repositories.TicketElectionRounds;
using elections.Repositories.TicketElectionRoundStatistics;
using elections.Repositories.Tickets;

namespace elections.Repositories
{
    public interface IRepositoryManager
    {
        ICandidatePartyRepository CandidateParty { get; }
        ICandidateRepository Candidate { get; }
        ICoalitionMemberRepository CoalitionMember { get; }
        ICoalitionRepository Coalition { get; }
        IElectionRoundRepository ElectionRound { get; }
        IElectionRepository Election { get; }
        IFederationPartyRepository FederationParty { get; }
        IFederationRepository Federation { get; }
        IMacroregionRepository Macroregion { get; }
        IMetropolitanAreaRepository MetropolitanArea { get; }
        IMunicipalityRepository Municipality { get; }
        IPartyRepository Party { get; }
        IStateRepository State { get; }
        ITicketCandidatePartyRepository TicketCandidateParty { get; }
        ITicketElectionRoundRepository TicketElectionRound { get; }
        ITicketElectionRoundStatisticRepository TicketElectionRoundStatistic { get; }
        ITicketRepository Ticket { get; }
    }
}
