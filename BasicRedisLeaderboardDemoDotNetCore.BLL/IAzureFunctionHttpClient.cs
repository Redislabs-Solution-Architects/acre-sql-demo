﻿using System;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL
{
    public interface IAzureFunctionHttpClient
    {
        public Task<List<RankResponseModel>> GetByRange(int start, int ent, bool isDesc);
    }
}

