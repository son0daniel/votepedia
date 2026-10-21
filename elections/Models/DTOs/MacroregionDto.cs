using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class MacroregionDto(Macroregion macroregion)
    {
        public string Id { get; } = macroregion.Uid;
        public string Name { get; } = macroregion.Name;
        public string Abbr { get; } = macroregion.Abbr;
    }
}
