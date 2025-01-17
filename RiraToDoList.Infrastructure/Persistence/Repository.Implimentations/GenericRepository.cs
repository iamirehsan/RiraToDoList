using Microsoft.EntityFrameworkCore;
using RiraToDoList.Domain.Repository.Interfaces;
using RiraToDoList.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace RiraToDoList.Infrastructure.Persistence.Repository.Implimentations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public ValueTask<TEntity> GetByIdAsync(Guid id)
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();
            var query = dbQuery.Where(where);
            return query;
        }

        public virtual IQueryable<TEntity> OrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();
            var query = dbQuery.OrderByDescending(orderByDescending);
            return query;
        }

        public virtual IQueryable<TEntity> OrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();
            var query = dbQuery.OrderBy(orderBy);
            return query;
        }

        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return includeExpressions.Aggregate(_dbSet.AsQueryable(), (query, path) => query.Include(path));
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();
            var query = dbQuery.Where(where).Any();
            return query;
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetListWithIncludeAsync(string includeProperties,
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetFirstWithIncludeAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperty)
        {
            IQueryable<TEntity> query = _dbSet;

            var ss = includeProperty.FirstOrDefault().ToString();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in includeProperty)
                query = query.Include(navigationProperty);

            return await query.Where(filter).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }
    }
}
