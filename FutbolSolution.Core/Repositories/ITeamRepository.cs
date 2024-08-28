using FutbolSolution.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<TeamImage> GetImageAsync(int id);
        Task RemoveImageAsync(int id);
        Task AddImageAsync(TeamImage image);
        Task UpdateImageAsync(TeamImage image);
        Task<List<TeamImage>> GetImagesAsync();
    }
}
