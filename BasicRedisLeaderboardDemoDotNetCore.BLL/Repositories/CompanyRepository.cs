using System;
using System.Collections.Generic;
using BasicRedisLeaderboardDemoDotNetCore.BLL.DbContexts;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;
using System.Linq;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Repositories
{
	public class CompanyRepository : GenericRepository<Rank>, ICompanyRepository
	{
		public CompanyRepository(AppDbContext context) : base(context)
		{
		}

        public Rank GetCompanyBySymbol(string symbol)
        {        
            return _dbContext.Companies.SingleOrDefault(x => x.Symbol.ToLower() == symbol);
        }

        public IEnumerable<Rank> GetAllSorted(bool isDesc)
        {
            return isDesc ? _dbContext.Companies.OrderByDescending(x => x.MarketCap) :
                           _dbContext.Companies.OrderBy(x => x.MarketCap);
        }
        public IEnumerable<Rank> GetByRange(int start, int ent, bool isDesc)
        {

            var result = isDesc ? _dbContext.Companies.OrderByDescending(x => x.MarketCap) :
                            _dbContext.Companies.OrderBy(x => x.MarketCap);
            return result
                .Skip(start)
                .Take(ent);
        }
    }
}

