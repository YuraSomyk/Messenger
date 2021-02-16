using Messenger.DAL.DataBase.Models.Interface;
using Messenger.DAL.DataBase.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<TEntity>> GetAll() {
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
    }
}