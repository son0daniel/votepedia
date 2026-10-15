using elections.Models.Entities;

namespace elections.Repositories
{
    public interface IRepositoryWithUid<TEntity> : IRepository<TEntity> where TEntity : BaseEntityWithUid
    {
        Task<TEntity?> GetByUid(string uid);
        Task<long?> GetIdByUid(string uid);
    }
}
