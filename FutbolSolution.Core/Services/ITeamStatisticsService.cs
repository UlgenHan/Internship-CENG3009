
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.DTOs.TeamDTOs;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Services
{
    public interface ITeamStatisticsService : IGenericService<BaseTeamDTO>
    {
        Task<ResponseDTO<BaseTeamDTO>> GetTeamStatsByTeamID(int teamID);
    }
}
