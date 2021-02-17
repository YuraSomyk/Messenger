using Messenger.DAL.DataBase.Models.Interface;
using Messenger.DAL.DataBase.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Messenger.DAL.DataBase.Repository {

    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity where TContext : DbContext {

        protected readonly TContext context;
        DbSet<TEntity> _dbSet;

        public Repository(TContext context) {
            this.context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll() {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Get(int id) {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity entity) {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity) {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id) {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null) {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> GetEntitesByParams(Expression<Func<TEntity, bool>> expression) {

            return await context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<TEntity>> GetListByParams(Expression<Func<TEntity, bool>> expression) {

            return await context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetWithIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties) {

            return await Include(includeProperties);
        }

        private async Task<IEnumerable<TEntity>> Include(params Expression<Func<TEntity, object>>[] includeProperties) {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return await includeProperties.Aggregate(query, (current, includeProperty) 
                => current.Include(includeProperty)).ToListAsync();
        }
    }
}