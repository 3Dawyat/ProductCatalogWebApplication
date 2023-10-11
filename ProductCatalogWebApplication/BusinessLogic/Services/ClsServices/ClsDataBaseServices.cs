namespace ProductCatalogWebApplication.BusinessLogic.Services.ClsServices
{
    public class ClsDataBaseServices<T> : IDataBaseServices<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public ClsDataBaseServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            try
            {
                await _context.Set<T>().AddRangeAsync(entities);
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                return await query.ToListAsync();
            }
            catch
            {
                return new List<T>();
            }
        }
        public async Task<T?> GetObjectByAsync(Expression<Func<T, bool>> expression, bool traking = false, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                if (!traking)
                    query = query.AsNoTracking();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.FirstOrDefaultAsync(expression);
            }
            catch
            {
                return null;
            }
        }
        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetListByAsync(Expression<Func<T, bool>> where)
        {
            try
            {
                return await _context.Set<T>().Where(where).ToListAsync();
            }
            catch
            {
                return new List<T>();
            }
        }
        public async Task<T?> FindAsync(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch
            {
                return null;
            }
        }
        public bool Edit(T entity)
        {
            try
            {
                _context.Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
