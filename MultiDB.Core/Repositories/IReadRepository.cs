namespace MultiDB.Core.Repositories
{
    public interface IReadRepository<T>
    {
        Task<T> GetById(int id, string tenantId);
        Task<List<T>> GetAll(string tenantId);
    }
}
