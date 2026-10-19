using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.MetropolitanAreaStates
{
    public class MetropolitanAreaStateRepository : Repository<MetropolitanAreaState>, IMetropolitanAreaStateRepository
    {
        public MetropolitanAreaStateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
