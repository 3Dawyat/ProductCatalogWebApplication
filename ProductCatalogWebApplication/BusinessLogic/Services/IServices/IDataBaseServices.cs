namespace ProductCatalogWebApplication.BusinessLogic.Services.IServices
{
    public interface IDataBaseServices<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        bool Edit(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task<T?> FindAsync(int id);
        Task<T?> GetObjectByAsync(Expression<Func<T, bool>> expression, bool traking = false, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetListByAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetQueryable();
        Task<bool> SaveChanges();

    }
}
