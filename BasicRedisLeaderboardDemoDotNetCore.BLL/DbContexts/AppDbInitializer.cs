using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.DbContexts
{
    public class AppDbInitializer
    {
        public static async Task Seed(IServiceScope serviceScope)
        {
            
            var RankEntites = new List<RankEntity>()
            {
                new RankEntity {
                    Company= "Apple",
                    Symbol= "AAPL",
                    MarketCap= 2222000000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Saudi Aramco",
                    Symbol= "2222.SR",
                    MarketCap= 2046000000,
                    Country= "S. Arabia",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Microsoft",
                    Symbol= "MSFT",
                    MarketCap= 1660000000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Amazon",
                    Symbol= "AMZN",
                    MarketCap= 1597000000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Alphabet (Google)",
                    Symbol= "GOOG",
                    MarketCap= 1218000000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Tesla",
                    Symbol= "TSLA",
                    MarketCap= 834170000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Facebook",
                    Symbol= "FB",
                    MarketCap= 762110000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Tencent",
                    Symbol= "TCEHY",
                    MarketCap= 742230000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Alibaba",
                    Symbol= "BABA",
                    MarketCap= 642220000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Berkshire Hathaway",
                    Symbol= "BRK-A",
                    MarketCap= 549580000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Samsung",
                    Symbol= "005930.KS",
                    MarketCap= 526630000,
                    Country= "S. Korea",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "TSMC",
                    Symbol= "TSM",
                    MarketCap= 511700000,
                    Country= "Taiwan",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Visa",
                    Symbol= "V",
                    MarketCap= 474940000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Johnson &amp; Johnson",
                    Symbol= "JNJ",
                    MarketCap= 421310000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Walmart",
                    Symbol= "WMT",
                    MarketCap= 414850000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "JPMorgan Chase",
                    Symbol= "JPM",
                    MarketCap= 414610000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Kweichow Moutai",
                    Symbol= "600519.SS",
                    MarketCap= 392630000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Mastercard",
                    Symbol= "MA",
                    MarketCap= 352760000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "UnitedHealth",
                    Symbol= "UNH",
                    MarketCap= 344790000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Procter &amp; Gamble",
                    Symbol= "PG",
                    MarketCap= 344140000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Nestlé",
                    Symbol= "NSRGY",
                    MarketCap= 333610000,
                    Country= "Switzerland",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "NVIDIA",
                    Symbol= "NVDA",
                    MarketCap= 328730000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "LVMH",
                    Symbol= "LVMUY",
                    MarketCap= 327040000,
                    Country= "France",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Walt Disney",
                    Symbol= "DIS",
                    MarketCap= 322900000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Roche",
                    Symbol= "RHHBY",
                    MarketCap= 293520000,
                    Country= "Switzerland",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Home Depot",
                    Symbol= "HD",
                    MarketCap= 289700000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "PayPal",
                    Symbol= "PYPL",
                    MarketCap= 284080000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Bank of America",
                    Symbol= "BAC",
                    MarketCap= 281410000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "ICBC",
                    Symbol= "1398.HK",
                    MarketCap= 266230000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Meituan-Dianping",
                    Symbol= "MPNGF",
                    MarketCap= 246210000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Verizon",
                    Symbol= "VZ",
                    MarketCap= 239180000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Ping An Insurance",
                    Symbol= "PNGAY",
                    MarketCap= 237140000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Comcast",
                    Symbol= "CMCSA",
                    MarketCap= 235810000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Adobe",
                    Symbol= "ADBE",
                    MarketCap= 232710000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Nike",
                    Symbol= "NKE",
                    MarketCap= 230710000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Netflix",
                    Symbol= "NFLX",
                    MarketCap= 225490000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Pinduoduo",
                    Symbol= "PDD",
                    MarketCap= 221680000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Coca-Cola",
                    Symbol= "KO",
                    MarketCap= 219510000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Novartis",
                    Symbol= "NVS",
                    MarketCap= 214590000,
                    Country= "Switzerland",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Toyota",
                    Symbol= "TM",
                    MarketCap= 212390000,
                    Country= "Japan",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "ASML",
                    Symbol= "ASML",
                    MarketCap= 212100000,
                    Country= "Netherlands",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Intel",
                    Symbol= "INTC",
                    MarketCap= 211660000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "L\" Oréal",
                    Symbol= "OR.PA",
                    MarketCap= 210160000,
                    Country= "France",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Merck",
                    Symbol= "MRK",
                    MarketCap= 210060000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "AT&amp;T",
                    Symbol= "T",
                    MarketCap= 206790000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Pfizer",
                    Symbol= "PFE",
                    MarketCap= 206380000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Salesforce",
                    Symbol= "CRM",
                    MarketCap= 203770000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Thermo Fisher Scientific",
                    Symbol= "TMO",
                    MarketCap= 203040000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Pepsico",
                    Symbol= "PEP",
                    MarketCap= 199250000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Abbott Laboratories",
                    Symbol= "ABT",
                    MarketCap= 197810000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "China Construction Bank",
                    Symbol= "CICHY",
                    MarketCap= 193710000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Exxon Mobil",
                    Symbol= "XOM",
                    MarketCap= 192210000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Oracle",
                    Symbol= "ORCL",
                    MarketCap= 190830000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Cisco",
                    Symbol= "CSCO",
                    MarketCap= 190400000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "AbbVie",
                    Symbol= "ABBV",
                    MarketCap= 189380000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "BHP Group",
                    Symbol= "BHP",
                    MarketCap= 186040000,
                    Country= "Australia",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Broadcom",
                    Symbol= "AVGO",
                    MarketCap= 181240000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "CM Bank",
                    Symbol= "3968.HK",
                    MarketCap= 180460000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "QUALCOMM",
                    Symbol= "QCOM",
                    MarketCap= 177150000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Reliance Industries",
                    Symbol= "RELIANCE.NS",
                    MarketCap= 177100000,
                    Country= "India",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Chevron",
                    Symbol= "CVX",
                    MarketCap= 175320000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Accenture",
                    Symbol= "ACN",
                    MarketCap= 175020000,
                    Country= "Ireland",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Danaher",
                    Symbol= "DHR",
                    MarketCap= 172960000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Agricultural Bank of China",
                    Symbol= "ACGBY",
                    MarketCap= 168730000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "T-Mobile US",
                    Symbol= "TMUS",
                    MarketCap= 167630000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Prosus",
                    Symbol= "PRX.VI",
                    MarketCap= 165690000,
                    Country= "Netherlands",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Costco",
                    Symbol= "COST",
                    MarketCap= 163860000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Novo Nordisk",
                    Symbol= "NVO",
                    MarketCap= 162260000,
                    Country= "Denmark",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Medtronic",
                    Symbol= "MDT",
                    MarketCap= 161130000,
                    Country= "Ireland",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "McDonald",
                    Symbol= "MCD",
                    MarketCap= 160840000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Unilever",
                    Symbol= "UL",
                    MarketCap= 160420000,
                    Country= "Netherlands",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Eli Lilly",
                    Symbol= "LLY",
                    MarketCap= 159180000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Nextera Energy",
                    Symbol= "NEE",
                    MarketCap= 158930000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Texas Instruments",
                    Symbol= "TXN",
                    MarketCap= 157110000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "SAP",
                    Symbol= "SAP",
                    MarketCap= 156750000,
                    Country= "Germany",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Tata",
                    Symbol= "TCS.NS",
                    MarketCap= 156350000,
                    Country= "India",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Shell",
                    Symbol= "RYDAF",
                    MarketCap= 155950000,
                    Country= "Netherlands",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "AIA",
                    Symbol= "AAIGF",
                    MarketCap= 153920000,
                    Country= "Hong Kong",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Union Pacific Corporation",
                    Symbol= "UNP",
                    MarketCap= 147450000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Honeywell",
                    Symbol= "HON",
                    MarketCap= 147370000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Jingdong Mall",
                    Symbol= "JD",
                    MarketCap= 146600000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Shopify",
                    Symbol= "SHOP",
                    MarketCap= 145120000,
                    Country= "Canada",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "SoftBank",
                    Symbol= "SFTBF",
                    MarketCap= 143310000,
                    Country= "Japan",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "China Life Insurance",
                    Symbol= "LFC",
                    MarketCap= 142650000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Linde",
                    Symbol= "LIN",
                    MarketCap= 141920000,
                    Country= "UK",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Anheuser-Busch Inbev",
                    Symbol= "BUD",
                    MarketCap= 141810000,
                    Country= "Belgium",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Bristol-Myers Squibb",
                    Symbol= "BMY",
                    MarketCap= 141210000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Amgen",
                    Symbol= "AMGN",
                    MarketCap= 138840000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Keyence",
                    Symbol= "KYCCF",
                    MarketCap= 137430000,
                    Country= "Japan",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Wells Fargo",
                    Symbol= "WFC",
                    MarketCap= 137220000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "United Parcel Service",
                    Symbol= "UPS",
                    MarketCap= 136910000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Morgan Stanley",
                    Symbol= "MS",
                    MarketCap= 136140000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Citigroup",
                    Symbol= "C",
                    MarketCap= 136090000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Astrazeneca",
                    Symbol= "AZN",
                    MarketCap= 135460000,
                    Country= "UK",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Bank of China",
                    Symbol= "BACHF",
                    MarketCap= 132660000,
                    Country= "China",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Philip Morris",
                    Symbol= "PM",
                    MarketCap= 129390000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Sony",
                    Symbol= "SNE",
                    MarketCap= 127620000,
                    Country= "Japan",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Charter Communications",
                    Symbol= "CHTR",
                    MarketCap= 126790000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "Starbucks",
                    Symbol= "SBUX",
                    MarketCap= 124020000,
                    Country= "USA",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  },
                new RankEntity {
                    Company= "NTT Docomo",
                    Symbol= "NTDMF",
                    MarketCap= 122470000,
                    Country= "Japan",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                  }
            };

            var redisConnection = serviceScope.ServiceProvider.GetService<IConnectionMultiplexer>();
            var redisDatabase = redisConnection.GetDatabase();
            IOptions<LeaderboardDemoOptions> options = serviceScope.ServiceProvider.GetService<IOptions<LeaderboardDemoOptions>>();


            if(options.Value.DeleteAllKeysOnLoad)
            await DeleteAllKeys(redisConnection, redisDatabase);

            if (options.Value.LoadInitialData && !options.Value.UseCacheAside)
            {
                for (var i = 0; i < RankEntites.Count; i++)
                {
                    var RankEntity = RankEntites[i];

                    try
                    {
                        // TODO: Optional use search to index and get sorted results
                        var key = $"company:{RankEntity.Symbol.ToLower()}";

                        await redisDatabase.SortedSetAddAsync(LeaderboardDemoOptions.RedisKey, key, RankEntity.MarketCap);

                        await redisDatabase.HashSetAsync(key, new HashEntry[]
                        {
                          new HashEntry(nameof(RankEntity.Company).ToLower(), RankEntity.Company),
                          new HashEntry(nameof(RankEntity.Country).ToLower(), RankEntity.Country),
                          new HashEntry(nameof(RankEntity.UpdatedAt).ToLower(), RankEntity.UpdatedAt.ToString()),
                          new HashEntry(nameof(RankEntity.CreatedAt).ToLower(), RankEntity.CreatedAt.ToString())

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
                    await context.AddRangeAsync(RankEntites);
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
