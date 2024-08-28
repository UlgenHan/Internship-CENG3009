using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Services
{
    public interface IPlayerTeamLink : IGenericService<PlayerTeamLinkDTO>
    {
        Task<ResponseDTO<PlayerTeamLinkDTO>> FindByPlayerID(int playerID);
        Task<ResponseDTO<List<PlayerTeamLinkDTO>>> FindByTeamID(int teamID);
    }
}
