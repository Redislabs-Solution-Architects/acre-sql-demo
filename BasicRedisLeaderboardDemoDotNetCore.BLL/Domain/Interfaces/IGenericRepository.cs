using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces
{
    public interface IGenericRepository<T> where T: IEntity
	{
        Task<T?> GetById<T>(int id) where T : IEntity;
        IQueryable<T> FindQueryable<T>(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null) where T : IEntity;
        Task<List<T>> FindListAsync<T>(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default) where T : class;
        Task<List<T>> FindAllAsync<T>(CancellationToken cancellationToken = default) where T : IEntity;
        Task<T?> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, string includeProperties) where T : IEntity;
        T Add<T>(T entity) where T : IEntity;
        void Update<T>(T entity) where T : IEntity;
        void UpdateRange<T>(IEnumerable<T> entities) where T : IEntity;
        void Delete<T>(T entity) where T : IEntity;
    }
}

