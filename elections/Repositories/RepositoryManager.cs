using elections.Data;
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
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        private ICandidatePartyRepository? _candidateParty;

        private ICandidateRepository? _candidate;

        private ICoalitionMemberRepository? _coalitionMember;

        private ICoalitionRepository? _coalition;

        private IElectionRoundRepository? _electionRound;

        private IElectionRepository? _election;

        private IFederationPartyRepository? _federationParty;

        private IFederationRepository? _federation;

        private IMacroregionRepository? _macroregion;

        private IMetropolitanAreaRepository? _metropolitanArea;

        private IMunicipalityRepository? _municipality;

        private IPartyRepository? _party;

        private IStateRepository? _state;

        private ITicketCandidatePartyRepository? _ticketCandidateParty;

        private ITicketElectionRoundRepository? _ticketElectionRound;

        private ITicketElectionRoundStatisticRepository? _ticketElectionRoundStatistic;
        private ITicketRepository? _ticket;

        public ICandidatePartyRepository CandidateParty => _candidateParty ??= new CandidatePartyRepository(_context);
        public ICandidateRepository Candidate => _candidate ??= new CandidateRepository(_context);
        public ICoalitionMemberRepository CoalitionMember => _coalitionMember ??= new CoalitionMemberRepository(_context);
        public ICoalitionRepository Coalition => _coalition ??= new CoalitionRepository(_context);
        public IElectionRoundRepository ElectionRound => _electionRound ??= new ElectionRoundRepository(_context);
        public IElectionRepository Election => _election ??= new ElectionRepository(_context);
        public IFederationPartyRepository FederationParty => _federationParty ??= new FederationPartyRepository(_context);
        public IFederationRepository Federation => _federation ??= new FederationRepository(_context);
        public IMacroregionRepository Macroregion => _macroregion ??= new MacroregionRepository(_context);
        public IMetropolitanAreaRepository MetropolitanArea => _metropolitanArea ??= new MetropolitanAreaRepository(_context);
        public IMunicipalityRepository Municipality => _municipality ??= new MunicipalityRepository(_context);
        public IPartyRepository Party => _party ??= new PartyRepository(_context);
        public IStateRepository State => _state ??= new StateRepository(_context);
        public ITicketCandidatePartyRepository TicketCandidateParty => _ticketCandidateParty ??= new TicketCandidatePartyRepository(_context);
        public ITicketElectionRoundRepository TicketElectionRound => _ticketElectionRound ??= new TicketElectionRoundRepository(_context);
        public ITicketElectionRoundStatisticRepository TicketElectionRoundStatistic => _ticketElectionRoundStatistic ??= new TicketElectionRoundStatisticRepository(_context);
        public ITicketRepository Ticket => _ticket ??= new TicketRepository(_context);

    }
}
