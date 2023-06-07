using System;
using System.Threading;
using System.Threading.Tasks;
using BasicRedisLeaderboardDemoDotNetCore.BLL.DbContexts;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly AppDbContext _databaseContext;

        public UnitOfWork(AppDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            Companies = new CompanyRepository(_databaseContext);
        }

       
        public ICompanyRepository Companies { get; private set; }

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return _databaseContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }
    }
}

