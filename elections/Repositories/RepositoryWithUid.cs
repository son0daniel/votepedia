using elections.Data;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace elections.Repositories
{
    public class RepositoryWithUid<TEntity> : Repository<TEntity>, IRepositoryWithUid<TEntity> where TEntity : BaseEntityWithUid
    {
        public RepositoryWithUid(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TEntity?> GetByUid(string uid)
        {
            return await _dbSet.Where(x => x.Uid == uid).FirstOrDefaultAsync();
        }
    }
}
