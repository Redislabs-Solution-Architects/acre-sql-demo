using System;
using System.ComponentModel.DataAnnotations;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Entities
{
	public record Rank : IEntity
	{
        public string Company { get; set; }

        [Key]
        public string Symbol { get; set; }
        public long MarketCap { get; set; }
        public string Country { get; set; }
    }
}

