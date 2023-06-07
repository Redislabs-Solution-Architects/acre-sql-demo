using System;
using System.ComponentModel.DataAnnotations;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Entities
{
    public record IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

