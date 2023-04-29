using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
        IRepository Repository();
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}

