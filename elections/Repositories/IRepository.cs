using elections.Models.Entities;
using elections.Models.Pagination;
using System.Linq.Expressions;

namespace elections.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetById(long id);
        Task<long?> GetId(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<long>> GetIds(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetPaged(IQueryable<TEntity> query, PagedRequest pagedRequest);
    }
}
