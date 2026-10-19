using elections.Models.Entities;
using System.Text.Json.Serialization;

namespace elections.Models.DTOs
{
    public class MunicipalityDto(Municipality municipality, MetropolitanArea? metropolitanArea, string stateAbbr, string macroregionAbbr)
    {
        public string Id { get; } = municipality.Uid;
        public string Name { get; } = municipality.Name;
        [JsonPropertyName("tse_id")] public long TseId { get; } = municipality.TseId;
        [JsonPropertyName("ibge_id")] public long? IbgeId { get; } = municipality.IbgeId;
        [JsonPropertyName("metropolitan_area")] public MinimalMetropolitanAreaDto? MetropolitanArea { get; } = metropolitanArea is null ? null : new MinimalMetropolitanAreaDto(metropolitanArea);
        public string State { get; } = stateAbbr;
        public string Macroregion { get; } = macroregionAbbr;
    }
}
