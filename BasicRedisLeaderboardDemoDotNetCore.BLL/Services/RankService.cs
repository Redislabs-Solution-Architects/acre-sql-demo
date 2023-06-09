using BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Models;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Services
{
    public class RankService : RankServiceAbstract, IRankService
    {
        private const string _splitCharacter = ":";
        private readonly IUnitOfWork _uow;

        public RankService(IConnectionMultiplexer redis,
            ILogger<RankService> logger,
            IOptions<LeaderboardDemoOptions> options,
            IUnitOfWork uow)
            : base(redis, logger, options)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public override async Task<List<RankResponseModel>> Range(int start, int ent, bool isDesc)
        {
            var data = new List<RankResponseModel>();            
            var results = await GetSortedSetData(start, ent, isDesc);
            var startRank = isDesc ? start + 1 : results.Count();
            var increaseFactor = isDesc ? 1 : -1;
            var items = results.ToList();

            for (var i = 0; i < items.Count; i++)
            {
                var symbol = items[i].Element.ToString().Split(_splitCharacter)[1];
                var company = await GetCompanyBySymbol(items[i].Element);

                data.Add(
                    new RankResponseModel
                    {
                        Company = company.Item1,
                        Country = company.Item2,
                        Rank = startRank,
                        Symbol = symbol,
                        MarketCap = items[i].Score,
                    });
                startRank += increaseFactor;
            }

            return data;
        }

        public override async Task<(string, string)> GetCompanyBySymbol(string symbol)
        {           
            HashEntry[] item = await _db.HashGetAllAsync(symbol);
            var companyEntry = item.Single(x => x.Name == "company");
            var countryEntry = item.Single(x => x.Name == "country");
            return (companyEntry.Value, countryEntry.Value);
        }

        public override async Task<List<RankResponseModel>> GetBySymbols(List<string> symbols)
        {
            var results = new List<RankResponseModel>();
            for (var i = 0;i< symbols.Count; i++)
            {
                var key = $"company:{symbols[i]}";
                var score = await _db.SortedSetScoreAsync(LeaderboardDemoOptions.RedisKey, key);
                var company = await GetCompanyBySymbol(key);
                results.Add(
                     new RankResponseModel
                     {
                         Company = company.Item1,
                         Country = company.Item2,
                         Rank = i+1,
                         Symbol = symbols[i],
                         MarketCap = (double)score
                     });
            }
                
            return results;
        }

        private async Task<SortedSetEntry[]> GetSortedSetData(int start, int end, bool isDesc)
        {
            //TODO: RDI don't have a way to add a stream at the moment.
            //We need to referesh the sorted set manually when using prefetch
            if (_options.Value.UsePrefetch)
            {              
                var companies = _uow.Companies.GetByRange(start, await _uow.Companies.Count<RankEntity>() -1, isDesc);

                foreach(var company in companies)
                {
                    var key = $"company:{company.Symbol.ToLower()}";
                    await _db.SortedSetAddAsync(LeaderboardDemoOptions.RedisKey, key, company.MarketCap);
                }
            }

            return await _db.SortedSetRangeByRankWithScoresAsync(LeaderboardDemoOptions.RedisKey, start, end, isDesc ? Order.Descending : Order.Ascending);
        }
    }
}
