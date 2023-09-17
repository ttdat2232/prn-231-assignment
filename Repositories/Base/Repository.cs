using Application.Exceptions;
using Domain.Interfaces.Repositories.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var result = await context.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? await context.Set<T>().CountAsync() : await context.Set<T>().CountAsync(expression);
        }

        public virtual T Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                context.Attach(entity);
            }
            context.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public virtual async Task<PaginationResult<T>> GetAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includeProperties = null, bool disableTracking = true, bool takeAll = false, int pageIndex = 0, int pageSize = 5)
        {
            IQueryable<T> query = context.Set<T>();
            var paginationResult = new PaginationResult<T>();
            paginationResult.TotalCount = await CountAsync(expression);
            if (paginationResult.TotalCount < pageSize * pageIndex)
                return new PaginationResult<T>();
            if (expression != null)
                query = query.Where(expression);
            if (disableTracking is true)
                query = query.AsNoTracking();
            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var includeItem in includeProperties)
                    query = query.Include(includeItem);
                query = query.AsSplitQuery();

            }
            if (takeAll is true)
            {
                paginationResult.PageSize = paginationResult.TotalCount;
                if (orderBy != null)
                    paginationResult.Values = await orderBy(query).ToListAsync();
                else
                    paginationResult.Values = await query.ToListAsync();
            }
            else
            {
                paginationResult.PageIndex = pageIndex;
                if (orderBy == null)
                    paginationResult.Values = await query.Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
                else
                    paginationResult.Values = await orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            }
            paginationResult.PageIndex = pageIndex;
            paginationResult.PageSize = paginationResult.Values.Count;
            return paginationResult;
        }

        public virtual async Task<T> GetByIdAsync(object[] keys)
        {
            return await context.Set<T>().FindAsync(keys) ?? throw new NotFoundException<T>(keys, GetType());
        }

        public virtual T Update(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                context.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }
            return entity;
        }
    }
}
