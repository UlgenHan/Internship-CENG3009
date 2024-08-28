using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Service.Services
{
    public class TeamStatisticService : GenericService<BaseTeamDTO, TeamStatistics>, ITeamStatisticsService
    {
        ITeamStatisticsRepository _teamStatsRepository;
        IMapper<BaseTeamDTO, TeamStatistics> _mapper;
        public TeamStatisticService(ITeamStatisticsRepository teamStatsRepository , IMapper<BaseTeamDTO,TeamStatistics> mapper)
            : base(teamStatsRepository, mapper)
        {
            _teamStatsRepository = teamStatsRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<BaseTeamDTO>> GetTeamStatsByTeamID(int teamID)
        {
            var allEntities = await _teamStatsRepository.GetAllAsync();
            foreach (var entity in allEntities)
            {
                if(entity.TeamId == teamID)
                {
                    return ResponseDTO<BaseTeamDTO>.Success(true, _mapper.Map(entity,null));
                }
            }
            return ResponseDTO<BaseTeamDTO>.Success(false);
        }
    }
}
