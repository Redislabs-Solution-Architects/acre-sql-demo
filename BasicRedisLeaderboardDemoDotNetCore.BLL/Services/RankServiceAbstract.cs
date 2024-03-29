﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Models;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Services
{
    public abstract class RankServiceAbstract 
    {
        protected readonly IDatabase _db;
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly WriteBehind _wb;
        private readonly ILogger<RankService> _logger;
        private const string keyPrefix = "company";
        protected readonly IOptions<LeaderboardDemoOptions> _options;

        protected RankServiceAbstract(IConnectionMultiplexer redisConnection, ILogger<RankService> logger, IOptions<LeaderboardDemoOptions> options)
        {
            _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
            _db = _redisConnection.GetDatabase();
            _wb = new WriteBehind(_redisConnection, keyPrefix);
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public abstract Task<List<RankResponseModel>> GetBySymbols(List<string> symbols);

        public abstract Task<(string, string)> GetCompanyBySymbol(string symbol);

        public abstract Task<List<RankResponseModel>> Range(int start, int ent, bool isDesc);

        public virtual async Task<bool> Update(string symbol, double amount)
        {
            bool result = false;
            string key = $"{keyPrefix}:{symbol}";
            try
            {
                await _db.SortedSetAddAsync(LeaderboardDemoOptions.RedisKey, key, amount);
                var company = await GetCompanyBySymbol(key);
                var score = await _db.SortedSetScoreAsync(LeaderboardDemoOptions.RedisKey, key);
                var rank = await _db.SortedSetRankAsync(LeaderboardDemoOptions.RedisKey, key);

                HashEntry[] hashEntry = new HashEntry[]
                {
                    new HashEntry("marketcap", score),
                    new HashEntry("rank", rank),
                    new HashEntry("company", company.Item1),
                    new HashEntry("country", company.Item2)
                };

                if (_options.Value.UseWriteBehind)
                {
                    _wb.AddToStream(symbol, hashEntry);
                }

                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error happened during update");
            }

            return result;
        }
    }
}

