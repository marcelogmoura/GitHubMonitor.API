namespace GitHubMonitor.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task Add(TEntity obj);
        Task Update(TEntity obj);
        Task Delete(Guid id);
        Task<List<TEntity>?> GetAll();
        Task<TEntity?> GetById(Guid id);
    }
}
