using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Domain.Interfaces;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Models;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Services
{
    public class RankServiceCacheAside : RankServiceAbstract, IRankService
    {
        private const string _splitCharacter = ":";
        private readonly IUnitOfWork _uow;

        public RankServiceCacheAside(IConnectionMultiplexer redis, ILogger<RankService> logger, IOptions<LeaderboardDemoOptions> options, IUnitOfWork uow)
             : base(redis, logger, options)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
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
            for (var i = 0; i < symbols.Count; i++)
            {
                var score = await _db.SortedSetScoreAsync(LeaderboardDemoOptions.RedisKey, symbols[i]);
                var company = await GetCompanyBySymbol(symbols[i]);
                results.Add(
                     new RankResponseModel
                     {
                         Company = company.Item1,
                         Country = company.Item2,
                         Rank = i + 1,
                         Symbol = symbols[i],
                         MarketCap = (double)score
                     });
            }

            return results;
        }

        public override async Task<List<RankResponseModel>> Range(int start, int ent, bool isDesc)
        {
           
            var cachedResults = await _db.SortedSetRangeByRankWithScoresAsync(LeaderboardDemoOptions.RedisKey, start, ent, isDesc ? Order.Descending : Order.Ascending);

            if(cachedResults.Count() > 0)
            {
                return await GetData(start, ent, isDesc, cachedResults);
            }

            var databaseResults = _uow.Companies.GetAllSorted(isDesc);
            var results = await GetData(start, ent, isDesc, databaseResults);

            // Populate Cache with entries
            await PopulateCache(results);

            return results.Skip(start).Take(ent + 1).ToList();


        }

        public override async Task<bool> Update(string symbol, double amount)
        {
            bool result = false;

            try
            {
                var record = _uow.Companies.GetCompanyBySymbol(symbol);
                var newAmount = record.MarketCap + (long)amount;
                record.MarketCap = newAmount;

                _uow.Companies.Update<RankEntity>(record);
                await _uow.CommitAsync();
                await base.Update(symbol, newAmount);

                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        private async Task<List<RankResponseModel>> GetData(int start, int ent, bool isDesc, IEnumerable<RankEntity> data)
        {
            var results = new List<RankResponseModel>();
            var startRank = isDesc ? start + 1 : data.Count();
            var increaseFactor = isDesc ? 1 : -1;
            var items = data.ToList();

            for (var i = 0; i < items.Count; i++)
            {
                results.Add(
                    new RankResponseModel
                    {
                        Company = items[i].Company,
                        Country = items[i].Country,
                        Rank = startRank,
                        Symbol = items[i].Symbol,
                        MarketCap = items[i].MarketCap,
                    });
                startRank += increaseFactor;
            }

            return results;
        }

        private async Task<List<RankResponseModel>> GetData(int start, int ent, bool isDesc, SortedSetEntry[] data)
        {
            var results = new List<RankResponseModel>();
            var startRank = isDesc ? start + 1 : data.Count();
            var increaseFactor = isDesc ? 1 : -1;
            var items = data.ToList();

            for (var i = 0; i < items.Count; i++)
            {
                var symbol = items[i].Element.ToString().Split(_splitCharacter)[1];
                var company = await GetCompanyBySymbol(items[i].Element);

                results.Add(
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

            return results;
        }

        private async Task PopulateCache(List<RankResponseModel> data)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var rank = data[i];

                try
                {
                    // TODO: Optional use search to index and get sorted results
                    var key = $"company:{rank.Symbol.ToLower()}";

                    await _db.SortedSetAddAsync(LeaderboardDemoOptions.RedisKey, key, rank.MarketCap);

                    await _db.HashSetAsync(key, new HashEntry[]
                    {
                          new HashEntry(nameof(rank.Company).ToLower(), rank.Company),
                          new HashEntry(nameof(rank.Country).ToLower(), rank.Country)
                    });
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}

