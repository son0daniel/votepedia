
using elections.Data;
using elections.Models.Entities;
using elections.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace elections.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetById(long id)
        {
            return await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<long?> GetId(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task<IEnumerable<TEntity>> GetPaged(IQueryable<TEntity> query, PagedRequest pagedRequest)
        {
            return await query
                .Skip(pagedRequest.PageIndex * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize)
                .ToListAsync();
        }
    }
}
