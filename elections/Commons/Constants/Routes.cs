namespace elections.Commons.Constants
{
    public static class Routes
    {
        private const string _Elections = "elections";
        private const string _Candidates = "candidates";
        private const string _Parties = "parties";
        private const string _Municipalities = "municipalities";
        private const string _MetropolitanAreas = "metropolitan-areas";
        private const string _States = "states";
        private const string _Macroregions = "macroregions";
        private const string _Uid = "{uid}";
        private const string _MetropolitanAreaUid = "{metropolitanAreaUid}";
        private const string _TseId = "{tseId}";
        private const string _Abbr = "{abbr}";
        private const string _StateAbbr = "{stateAbbr}";
        private const string _MacroregionAbbr = "{macroregionAbbr}";
        private const string _Aggregation = "aggregation";
        private const string _Role = "role";
        private const string _RoleNr = "{roleNr}";
        private const string _President = "president";
        private const string _Overviews = "overviews";
        private const string _Overall = "overall";
        private const string _Results = "results";
        private const string _Records = "records";
        private const string _Presidential = $"{_Role}/{_President}";
        private const string _ElectionYear = "year/{year}";
        private const string _ElectionRound = "round/{roundNr}";
        private const string _PresidentialElectionRound = $"{_Presidential}/{_ElectionYear}/{_ElectionRound}";
        private const string _PresidentialOverviews = $"{_Presidential}/{_Overviews}";

        public static class Elections
        {
            public const string Base = $"api/{_Elections}";
            public const string PresidentialElectionOverviews = $"{_PresidentialOverviews}";
            public const string PresidentialElectionRoundOverallResults = $"{_PresidentialElectionRound}/{_Aggregation}/{_Overall}/{_Results}";
            public const string PresidentialElectionRoundMunicipalityAggregationResults = $"{_PresidentialElectionRound}/{_Aggregation}/{_Municipalities}/{_Results}";
            public const string PresidentialElectionRoundMetropolitanAreaAggregationResults = $"{_PresidentialElectionRound}/{_Aggregation}/{_MetropolitanAreas}/{_Results}";
            public const string PresidentialElectionRoundStateAggregationResults = $"{_PresidentialElectionRound}/{_Aggregation}/{_States}/{_Results}";
            public const string PresidentialElectionRoundMacroregionAggregationResults = $"{_PresidentialElectionRound}/{_Aggregation}/{_Macroregions}/{_Results}";
            public const string ElectionRoundRecords = $"{_Aggregation}/{_Overall}/{_Records}";
        }

        public static class Candidates
        {
            public const string Base = $"api/{_Candidates}";
            public const string GetOne = _Uid;
            public const string PresidentialElectionOverviews = $"{_Uid}/{_Elections}/{_PresidentialOverviews}";
            public const string CandidateMunicipalityElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_Municipalities}/{_TseId}/{_Records}";
            public const string CandidateMetropolitanAreaElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_MetropolitanAreas}/{_MetropolitanAreaUid}/{_Records}";
            public const string CandidateStateElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_States}/{_StateAbbr}/{_Records}";
            public const string CandidateMacroregionElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_Macroregions}/{_MacroregionAbbr}/{_Records}";
            public const string CandidatePresidentialElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_Overall}/{_Records}";
        }

        public static class Parties
        {
            public const string Base = $"api/{_Parties}";
            public const string GetOne = _Uid;
            public const string PresidentialElectionOverviews = $"{_Uid}/{_Elections}/{_PresidentialOverviews}";
            public const string PartyMunicipalityElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_Municipalities}/{_TseId}/{_Records}";
            public const string PartyMetropolitanAreaElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_MetropolitanAreas}/{_MetropolitanAreaUid}/{_Records}";
            public const string PartyStateElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_States}/{_StateAbbr}/{_Records}";
            public const string PartyMacroregionElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_Macroregions}/{_MacroregionAbbr}/{_Records}";
            public const string PartyPresidentialElectionRoundRecords = $"{_Uid}/{_Elections}/{_Aggregation}/{_Overall}/{_Records}";
        }

        public static class Municipalities
        {
            public const string Base = $"api/{_Municipalities}";
            public const string GetOne = _TseId;
            public const string ElectionRoundRecords = $"{_TseId}/{_Elections}/{_Records}";
            public const string PresidentialElectionRoundResults = $"{_TseId}/{_Elections}/{_PresidentialElectionRound}/{_Results}";
        }

        public static class MetropolitanAreas
        {
            public const string Base = $"api/{_MetropolitanAreas}";
            public const string GetOne = _Uid;
            public const string ElectionRoundRecords = $"{_Uid}/{_Elections}/{_Records}";
            public const string ElectionRoundResults = $"{_Uid}/{_Elections}/{_Role}/{_RoleNr}/{_ElectionYear}/{_ElectionRound}";
            public const string PresidentialElectionRoundResults = $"{_Uid}/{_Elections}/{_PresidentialElectionRound}/{_Results}";
        }

        public static class States
        {
            public const string Base = $"api/{_States}";
            public const string GetOne = _Abbr;
            public const string ElectionRoundRecords = $"{_Abbr}/{_Elections}/{_Records}";
            public const string ElectionRoundResults = $"{_Abbr}/{_Elections}/{_Role}/{_RoleNr}/{_ElectionYear}/{_ElectionRound}";
            public const string PresidentialElectionRoundResults = $"{_Abbr}/{_Elections}/{_PresidentialElectionRound}/{_Results}";
        }

        public static class Macroregions
        {
            public const string Base = $"api/{_Macroregions}";
            public const string GetOne = _Abbr;
            public const string ElectionRoundRecords = $"{_Abbr}/{_Elections}/{_Records}";
            public const string ElectionRoundResults = $"{_Abbr}/{_Elections}/{_Role}/{_RoleNr}/{_ElectionYear}/{_ElectionRound}";
            public const string PresidentialElectionRoundResults = $"{_Abbr}/{_Elections}/{_PresidentialElectionRound}/{_Results}";
        }
    }
}
