namespace elections.Commons.Constants
{
    public static class Routes
    {
        private const string _Elections = "elections";
        private const string _Candidates = "candidates";
        private const string _Parties = "parties";
        private const string _Uid = "{uid}";
        private const string _Role = "role";
        private const string _President = "president";
        private const string _Overviews = "overviews";
        private const string _Overall = "overall";
        private const string _Results = "results";
        private const string _Presidential = $"{_Role}/{_President}";
        private const string _ElectionYear = "year/{year}";
        private const string _ElectionRound = "round/{roundNr}";
        private const string _PresidentialElectionRound = $"{_Presidential}/{_ElectionYear}/{_ElectionRound}";
        private const string _PresidentialOverviews = $"{_Presidential}/{_Overviews}";

        public static class Elections
        {
            public const string Base = $"api/{_Elections}";
            public const string PresidentialElectionOverviews = $"{_PresidentialOverviews}";
            public const string PresidentialElectionRoundOverallResults = $"{_PresidentialElectionRound}/{_Overall}/{_Results}";
        }

        public static class Candidates
        {
            public const string Base = $"api/{_Candidates}";
            public const string PresidentialElectionOverviews = $"{_Uid}/{_Elections}/{_PresidentialOverviews}";
        }

        public static class Parties
        {
            public const string Base = $"api/{_Parties}";
            public const string PresidentialElectionOverviews = $"{_Uid}/{_Elections}/{_PresidentialOverviews}";
        }
    }
}
