using System;
using System.ComponentModel.DataAnnotations;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Entities
{
    public record IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

