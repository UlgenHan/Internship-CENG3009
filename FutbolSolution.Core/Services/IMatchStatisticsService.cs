using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Services
{
    public interface IMatchStatisticsService : IGenericService<BaseMatchDTO>
    {
        Task<ResponseDTO<BaseMatchDTO>> GetMatchStatsByMatchID(int matchID);
    }
}
