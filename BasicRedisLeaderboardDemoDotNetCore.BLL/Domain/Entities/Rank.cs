using System;
using System.ComponentModel.DataAnnotations;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Entities
{
	public record RankEntity : IEntity
	{
        public string Company { get; set; }

        [Key]
        public string Symbol { get; set; }
        public double? MarketCap { get; set; }
        public int? Rank { get; set; }
        public string Country { get; set; }
    }
}

