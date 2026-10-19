using elections.Models.Entities;

namespace elections.Models.DTOs
{
    public class MinimalMetropolitanAreaDto(MetropolitanArea metropolitanArea)
    {
        public string Id { get; } = metropolitanArea.Uid;
        public string Name { get; } = metropolitanArea.Name;
    }
}
