using BasicRedisLeaderboardDemoDotNetCore.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Services.Interfaces
{
    public interface IRankService
    {
        Task<List<RankResponseModel>> Range(int start, int ent, bool isDesc);

        Task<(string, string)> GetCompanyBySymbol(string symbol);

        Task<List<RankResponseModel>> GetBySymbols(List<string> symbols);

        Task<bool> Update(string symbol, double amount);
    }
}
