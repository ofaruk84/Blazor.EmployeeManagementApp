using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lib.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity> 
        where TContext : DbContext,new()
        where TEntity : class
    {
        private readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {


            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;

            await _context.SaveChangesAsync();

           

        }

        public async Task DeleteAsync(TEntity entity)
        {
     

            await Task.Run(() =>
            {
                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
            });


            await _context.SaveChangesAsync();

        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter)
        {
            
            
            return filter == null ? await _context.Set<TEntity>().SingleOrDefaultAsync() : await _context.Set<TEntity>().SingleOrDefaultAsync(filter);

        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter)
        {

            return filter == null
                ? await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
            });

            await _context.SaveChangesAsync();

        }
    }
}
