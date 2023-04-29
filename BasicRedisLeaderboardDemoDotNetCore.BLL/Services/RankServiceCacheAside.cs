using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Models;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Services
{ 
    public class RankServiceCacheAside : RankServiceAbstract, IRankService
    {
        
        public RankServiceCacheAside(IConnectionMultiplexer redis, ILogger<RankService> logger, IOptions<LeaderboardDemoOptions> options)
             : base(redis, logger, options)
        {
        }

        public override Task<List<RankResponseModel>> GetBySymbols(List<string> symbols)
        {
            throw new NotImplementedException();
        }

        public override Task<(string, string)> GetCompanyBySymbol(string symbol)
        {
            throw new NotImplementedException();
        }

        public override Task<List<RankResponseModel>> Range(int start, int ent, bool isDesc)
        {
            throw new NotImplementedException();
        }
    }
}

