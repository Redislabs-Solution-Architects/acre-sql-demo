using System;
using System.Threading;
using System.Threading.Tasks;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
        ICompanyRepository Companies { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}

