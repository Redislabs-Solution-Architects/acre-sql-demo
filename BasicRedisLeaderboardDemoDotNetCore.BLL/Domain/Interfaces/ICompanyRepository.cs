using System;
using System.Collections.Generic;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces
{
	public interface ICompanyRepository : IGenericRepository<Rank>
	{
		Rank GetCompanyBySymbol(string symbol);
		IEnumerable<Rank> GetByRange(int start, int ent, bool isDesc);
		IEnumerable<Rank> GetAllSorted(bool isDesc);
	}
}

