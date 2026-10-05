using elections.Models.Entities;

namespace elections.Repositories
{
    public interface IRepositoryWithUid<TEntity> where TEntity : BaseEntityWithUid
    {
        Task<TEntity?> GetByUid(string uid);
    }
}
