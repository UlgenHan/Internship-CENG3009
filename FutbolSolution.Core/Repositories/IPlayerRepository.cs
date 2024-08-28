using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Repositories
{
    public interface IPlayerRepository : IGenericRepository<Player> 
    {
        Task<PlayerImage> GetImageAsync(int id);
        Task RemoveImageAsync(int id);
        Task AddImageAsync(PlayerImage image);
        Task UpdateImageAsync(PlayerImage image);
        Task<IQueryable<PlayerImage>> GetImagesAsync();
    }
}
