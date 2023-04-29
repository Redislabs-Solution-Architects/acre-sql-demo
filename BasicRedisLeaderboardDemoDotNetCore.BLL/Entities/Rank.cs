using System;
namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Entities
{
	public record Rank : IEntity
	{
        public string Company { get; set; }
        public string Symbol { get; set; }
        public long MarketCap { get; set; }
        public string Country { get; set; }
    }
}

