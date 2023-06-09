using System;
using System.Collections.Generic;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces
{
	public interface ICompanyRepository : IGenericRepository<RankEntity>
	{
        RankEntity GetCompanyBySymbol(string symbol);
		IEnumerable<RankEntity> GetByRange(int start, int ent, bool isDesc);
		IEnumerable<RankEntity> GetAllSorted(bool isDesc);
	}
}

