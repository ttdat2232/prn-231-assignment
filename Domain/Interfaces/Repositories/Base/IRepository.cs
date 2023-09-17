using Domain.Models;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(object[] Keys);
        public Task<PaginationResult<T>> GetAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string[] includeProperties = null,
            bool disableTracking = true,
            bool takeAll = false,
            int pageIndex = 0,
            int pageSize = 5);
        public Task<T> AddAsync(T entity);
        public Task<int> CountAsync(Expression<Func<T, bool>> expression = null);
        public T Update(T entity);
        public T Delete(T entity);

    }
}
