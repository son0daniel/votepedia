using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class StateDto(State state, string macroregionAbbr)
    {
        public string Id { get; } = state.Uid;
        public string Name { get; } = state.Name;
        public string Abbr { get; } = state.Abbr;
        public string Macroregion { get; } = macroregionAbbr;
    }
}
