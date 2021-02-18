using Messenger.DAL.DataBase.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Messenger.DAL.DataBase.Repository.Interface {

    public interface IRepository<T> where T : class, IEntity {

        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(int id);

        Task<int> Count(Expression<Func<T, bool>> where = null);

        Task<T> GetEntitesByParams(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetListByParams(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties);
    }
}