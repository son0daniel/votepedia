using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class MetropolitanAreaDto(MetropolitanArea metropolitanArea, IEnumerable<string> states, IEnumerable<string> macroregions) : MinimalMetropolitanAreaDto(metropolitanArea)
    {
        public string Type { get; } = metropolitanArea.Type;
        public List<string> States { get; } = [.. states];
        public List<string> Macroregions { get; } = [.. macroregions];
    }
}
