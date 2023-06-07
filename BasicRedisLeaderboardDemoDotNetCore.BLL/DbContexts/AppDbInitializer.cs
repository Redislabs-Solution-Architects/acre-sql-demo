using BasicRedisLeaderboardDemoDotNetCore.BLL;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Models;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.DbContexts
{
    public class AppDbInitializer
    {
        public static async Task Seed(IServiceScope serviceScope)
        {
            
            var ranks = new List<Rank>()
            {
                new Rank {
                    Company= "Apple",
                    Symbol= "AAPL",
                    MarketCap= 2222000000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Saudi Aramco",
                    Symbol= "2222.SR",
                    MarketCap= 2046000000,
                    Country= "S. Arabia",
                  },
                new Rank {
                    Company= "Microsoft",
                    Symbol= "MSFT",
                    MarketCap= 1660000000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Amazon",
                    Symbol= "AMZN",
                    MarketCap= 1597000000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Alphabet (Google)",
                    Symbol= "GOOG",
                    MarketCap= 1218000000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Tesla",
                    Symbol= "TSLA",
                    MarketCap= 834170000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Facebook",
                    Symbol= "FB",
                    MarketCap= 762110000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Tencent",
                    Symbol= "TCEHY",
                    MarketCap= 742230000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Alibaba",
                    Symbol= "BABA",
                    MarketCap= 642220000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Berkshire Hathaway",
                    Symbol= "BRK-A",
                    MarketCap= 549580000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Samsung",
                    Symbol= "005930.KS",
                    MarketCap= 526630000,
                    Country= "S. Korea",
                  },
                new Rank {
                    Company= "TSMC",
                    Symbol= "TSM",
                    MarketCap= 511700000,
                    Country= "Taiwan",
                  },
                new Rank {
                    Company= "Visa",
                    Symbol= "V",
                    MarketCap= 474940000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Johnson &amp; Johnson",
                    Symbol= "JNJ",
                    MarketCap= 421310000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Walmart",
                    Symbol= "WMT",
                    MarketCap= 414850000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "JPMorgan Chase",
                    Symbol= "JPM",
                    MarketCap= 414610000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Kweichow Moutai",
                    Symbol= "600519.SS",
                    MarketCap= 392630000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Mastercard",
                    Symbol= "MA",
                    MarketCap= 352760000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "UnitedHealth",
                    Symbol= "UNH",
                    MarketCap= 344790000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Procter &amp; Gamble",
                    Symbol= "PG",
                    MarketCap= 344140000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Nestlé",
                    Symbol= "NSRGY",
                    MarketCap= 333610000,
                    Country= "Switzerland",
                  },
                new Rank {
                    Company= "NVIDIA",
                    Symbol= "NVDA",
                    MarketCap= 328730000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "LVMH",
                    Symbol= "LVMUY",
                    MarketCap= 327040000,
                    Country= "France",
                  },
                new Rank {
                    Company= "Walt Disney",
                    Symbol= "DIS",
                    MarketCap= 322900000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Roche",
                    Symbol= "RHHBY",
                    MarketCap= 293520000,
                    Country= "Switzerland",
                  },
                new Rank {
                    Company= "Home Depot",
                    Symbol= "HD",
                    MarketCap= 289700000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "PayPal",
                    Symbol= "PYPL",
                    MarketCap= 284080000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Bank of America",
                    Symbol= "BAC",
                    MarketCap= 281410000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "ICBC",
                    Symbol= "1398.HK",
                    MarketCap= 266230000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Meituan-Dianping",
                    Symbol= "MPNGF",
                    MarketCap= 246210000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Verizon",
                    Symbol= "VZ",
                    MarketCap= 239180000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Ping An Insurance",
                    Symbol= "PNGAY",
                    MarketCap= 237140000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Comcast",
                    Symbol= "CMCSA",
                    MarketCap= 235810000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Adobe",
                    Symbol= "ADBE",
                    MarketCap= 232710000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Nike",
                    Symbol= "NKE",
                    MarketCap= 230710000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Netflix",
                    Symbol= "NFLX",
                    MarketCap= 225490000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Pinduoduo",
                    Symbol= "PDD",
                    MarketCap= 221680000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Coca-Cola",
                    Symbol= "KO",
                    MarketCap= 219510000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Novartis",
                    Symbol= "NVS",
                    MarketCap= 214590000,
                    Country= "Switzerland",
                  },
                new Rank {
                    Company= "Toyota",
                    Symbol= "TM",
                    MarketCap= 212390000,
                    Country= "Japan",
                  },
                new Rank {
                    Company= "ASML",
                    Symbol= "ASML",
                    MarketCap= 212100000,
                    Country= "Netherlands",
                  },
                new Rank {
                    Company= "Intel",
                    Symbol= "INTC",
                    MarketCap= 211660000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "L\" Oréal",
                    Symbol= "OR.PA",
                    MarketCap= 210160000,
                    Country= "France",
                  },
                new Rank {
                    Company= "Merck",
                    Symbol= "MRK",
                    MarketCap= 210060000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "AT&amp;T",
                    Symbol= "T",
                    MarketCap= 206790000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Pfizer",
                    Symbol= "PFE",
                    MarketCap= 206380000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Salesforce",
                    Symbol= "CRM",
                    MarketCap= 203770000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Thermo Fisher Scientific",
                    Symbol= "TMO",
                    MarketCap= 203040000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Pepsico",
                    Symbol= "PEP",
                    MarketCap= 199250000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Abbott Laboratories",
                    Symbol= "ABT",
                    MarketCap= 197810000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "China Construction Bank",
                    Symbol= "CICHY",
                    MarketCap= 193710000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Exxon Mobil",
                    Symbol= "XOM",
                    MarketCap= 192210000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Oracle",
                    Symbol= "ORCL",
                    MarketCap= 190830000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Cisco",
                    Symbol= "CSCO",
                    MarketCap= 190400000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "AbbVie",
                    Symbol= "ABBV",
                    MarketCap= 189380000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "BHP Group",
                    Symbol= "BHP",
                    MarketCap= 186040000,
                    Country= "Australia",
                  },
                new Rank {
                    Company= "Broadcom",
                    Symbol= "AVGO",
                    MarketCap= 181240000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "CM Bank",
                    Symbol= "3968.HK",
                    MarketCap= 180460000,
                    Country= "China",
                  },
                new Rank {
                    Company= "QUALCOMM",
                    Symbol= "QCOM",
                    MarketCap= 177150000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Reliance Industries",
                    Symbol= "RELIANCE.NS",
                    MarketCap= 177100000,
                    Country= "India",
                  },
                new Rank {
                    Company= "Chevron",
                    Symbol= "CVX",
                    MarketCap= 175320000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Accenture",
                    Symbol= "ACN",
                    MarketCap= 175020000,
                    Country= "Ireland",
                  },
                new Rank {
                    Company= "Danaher",
                    Symbol= "DHR",
                    MarketCap= 172960000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Agricultural Bank of China",
                    Symbol= "ACGBY",
                    MarketCap= 168730000,
                    Country= "China",
                  },
                new Rank {
                    Company= "T-Mobile US",
                    Symbol= "TMUS",
                    MarketCap= 167630000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Prosus",
                    Symbol= "PRX.VI",
                    MarketCap= 165690000,
                    Country= "Netherlands",
                  },
                new Rank {
                    Company= "Costco",
                    Symbol= "COST",
                    MarketCap= 163860000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Novo Nordisk",
                    Symbol= "NVO",
                    MarketCap= 162260000,
                    Country= "Denmark",
                  },
                new Rank {
                    Company= "Medtronic",
                    Symbol= "MDT",
                    MarketCap= 161130000,
                    Country= "Ireland",
                  },
                new Rank {
                    Company= "McDonald",
                    Symbol= "MCD",
                    MarketCap= 160840000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Unilever",
                    Symbol= "UL",
                    MarketCap= 160420000,
                    Country= "Netherlands",
                  },
                new Rank {
                    Company= "Eli Lilly",
                    Symbol= "LLY",
                    MarketCap= 159180000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Nextera Energy",
                    Symbol= "NEE",
                    MarketCap= 158930000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Texas Instruments",
                    Symbol= "TXN",
                    MarketCap= 157110000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "SAP",
                    Symbol= "SAP",
                    MarketCap= 156750000,
                    Country= "Germany",
                  },
                new Rank {
                    Company= "Tata",
                    Symbol= "TCS.NS",
                    MarketCap= 156350000,
                    Country= "India",
                  },
                new Rank {
                    Company= "Shell",
                    Symbol= "RYDAF",
                    MarketCap= 155950000,
                    Country= "Netherlands",
                  },
                new Rank {
                    Company= "AIA",
                    Symbol= "AAIGF",
                    MarketCap= 153920000,
                    Country= "Hong Kong",
                  },
                new Rank {
                    Company= "Union Pacific Corporation",
                    Symbol= "UNP",
                    MarketCap= 147450000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Honeywell",
                    Symbol= "HON",
                    MarketCap= 147370000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Jingdong Mall",
                    Symbol= "JD",
                    MarketCap= 146600000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Shopify",
                    Symbol= "SHOP",
                    MarketCap= 145120000,
                    Country= "Canada",
                  },
                new Rank {
                    Company= "SoftBank",
                    Symbol= "SFTBF",
                    MarketCap= 143310000,
                    Country= "Japan",
                  },
                new Rank {
                    Company= "China Life Insurance",
                    Symbol= "LFC",
                    MarketCap= 142650000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Linde",
                    Symbol= "LIN",
                    MarketCap= 141920000,
                    Country= "UK",
                  },
                new Rank {
                    Company= "Anheuser-Busch Inbev",
                    Symbol= "BUD",
                    MarketCap= 141810000,
                    Country= "Belgium",
                  },
                new Rank {
                    Company= "Bristol-Myers Squibb",
                    Symbol= "BMY",
                    MarketCap= 141210000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Amgen",
                    Symbol= "AMGN",
                    MarketCap= 138840000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Keyence",
                    Symbol= "KYCCF",
                    MarketCap= 137430000,
                    Country= "Japan",
                  },
                new Rank {
                    Company= "Wells Fargo",
                    Symbol= "WFC",
                    MarketCap= 137220000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "United Parcel Service",
                    Symbol= "UPS",
                    MarketCap= 136910000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Morgan Stanley",
                    Symbol= "MS",
                    MarketCap= 136140000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Citigroup",
                    Symbol= "C",
                    MarketCap= 136090000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Astrazeneca",
                    Symbol= "AZN",
                    MarketCap= 135460000,
                    Country= "UK",
                  },
                new Rank {
                    Company= "Bank of China",
                    Symbol= "BACHF",
                    MarketCap= 132660000,
                    Country= "China",
                  },
                new Rank {
                    Company= "Philip Morris",
                    Symbol= "PM",
                    MarketCap= 129390000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Sony",
                    Symbol= "SNE",
                    MarketCap= 127620000,
                    Country= "Japan",
                  },
                new Rank {
                    Company= "Charter Communications",
                    Symbol= "CHTR",
                    MarketCap= 126790000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "Starbucks",
                    Symbol= "SBUX",
                    MarketCap= 124020000,
                    Country= "USA",
                  },
                new Rank {
                    Company= "NTT Docomo",
                    Symbol= "NTDMF",
                    MarketCap= 122470000,
                    Country= "Japan",
                  }
            };

            var redisConnection = serviceScope.ServiceProvider.GetService<IConnectionMultiplexer>();
            var redisDatabase = redisConnection.GetDatabase();
            IOptions<LeaderboardDemoOptions> options = serviceScope.ServiceProvider.GetService<IOptions<LeaderboardDemoOptions>>();


            if(options.Value.DeleteAllKeysOnLoad)
            await DeleteAllKeys(redisConnection, redisDatabase);

            if (options.Value.LoadInitialData && !options.Value.UseCacheAside)
            {
                for (var i = 0; i < ranks.Count; i++)
                {
                    var rank = ranks[i];

                    try
                    {
                        // TODO: Optional use search to index and get sorted results
                        var key = $"company:{rank.Symbol.ToLower()}";

                        await redisDatabase.SortedSetAddAsync(LeaderboardDemoOptions.RedisKey, key, rank.MarketCap);

                        await redisDatabase.HashSetAsync(key, new HashEntry[]
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

            if(options.Value.LoadInitialData && options.Value.UseCacheAside)
            {
                var services = serviceScope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();

                if (!context.Companies.Any())
                {
                    await context.AddRangeAsync(ranks);
                    await context.SaveChangesAsync();
                }
            }
        }

        private static async Task DeleteAllKeys(IConnectionMultiplexer redisConnection, IDatabase redisDatabase)
        {
            //StackEchange.Redis uses non bloking UNLINK if the feature is available
            // https://github.com/StackExchange/StackExchange.Redis/blob/main/src/StackExchange.Redis/RedisDatabase.cs

            foreach (var endPoint in redisConnection.GetEndPoints())
            {
                var server = redisConnection.GetServer(endPoint);              
                var keys = server.Keys();

                if (keys.Count() != 0)
                {
                    var keyArr = keys.ToArray();

                    try
                    {
                        var query = keyArr.GroupBy(x => x.GetHashCode());

                        foreach(IGrouping<int, RedisKey> keyGroup in query)
                        {
                            foreach (var key in keyGroup)
                            {
                                await redisDatabase.KeyDeleteAsync(key);
                            }
                        }

                    }
                    catch(Exception ex)
                    {
                        
                    }
                }
            }
        }
    }
}
