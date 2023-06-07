namespace BasicRedisLeaderboardDemoDotNetCore.BLL
{
	public class LeaderboardDemoOptions
	{
		public const string Section = "LeaderboardSettings";
        public const string RedisKey = "REDIS_LEADERBOARD";
		public string RedisHost { get; set; }
        public string RedisPort { get; set; }
		public string RedisPassword { get; set; }
		public bool IsACRE { get; set; }
        public bool IsSSL { get; set; }
        public bool AllowAdmin { get; set; }
        public bool DeleteAllKeysOnLoad { get; set; }
        public bool UseReadThrough { get; set; }
        public bool UseWriteBehind { get; set; }
        public bool UseCacheAside { get; set; }
        public string ReadThroughFunctionBaseUrl { get; set; }
        public bool LoadInitialData { get; set; }

        public string GetRedisEnpoint()
        {
            if (string.IsNullOrEmpty(RedisHost))
            {
                RedisHost = "127.0.0.1";
                RedisPort = "6379";
            }

            if (IsACRE)
            { 
                return $"{RedisHost}:{RedisPort},ssl={IsSSL},password={RedisPassword},allowAdmin={AllowAdmin},syncTimeout=5000,connectTimeout=1000";                
            }

            if (!string.IsNullOrEmpty(RedisPassword))
            {
                return $"{RedisPassword}@{RedisHost}:{RedisPort},ssl={IsSSL}";               
            }
            else
            {
                return $"{RedisHost}:{RedisPort},allowAdmin={AllowAdmin},ssl={IsSSL}";
            }
        }
    }
}

