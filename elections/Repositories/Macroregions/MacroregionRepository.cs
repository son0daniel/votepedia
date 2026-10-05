using elections.Data;
using elections.Models.Entities;

namespace elections.Repositories.Macroregions
{
    public class MacroregionRepository : RepositoryWithUid<Macroregion>, IMacroregionRepository
    {
        public MacroregionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
