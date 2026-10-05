using elections.Models.Entities;
using System.Linq.Expressions;

namespace elections.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetById(long id);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
    }
}
