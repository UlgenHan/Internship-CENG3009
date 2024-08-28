using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using System.Threading.Tasks;

namespace FutbolSolution.Service.Services
{
    public class MatchStatisticsService : GenericService<BaseMatchDTO,MatchStatistics>, IMatchStatisticsService
    {
        private readonly IMatchStatisticsRepository _matchStatisticsRepository;
        private readonly IMapper<BaseMatchDTO,MatchStatistics> _matchStatisticsMapper;
        public MatchStatisticsService(IMatchStatisticsRepository genericRepository,IMapper<BaseMatchDTO,MatchStatistics> mapper)
            : base(genericRepository,mapper)
        {
            _matchStatisticsRepository = genericRepository;
            _matchStatisticsMapper = mapper;
        }

        public async Task<ResponseDTO<BaseMatchDTO>> GetMatchStatsByMatchID(int matchID)
        {
            var matchEntities = await _matchStatisticsRepository.GetAllAsync();

            foreach (var matchEntity in matchEntities)
            {
                if (matchEntity.MatchId.Equals(matchID))
                {
                    return ResponseDTO<BaseMatchDTO>.Success(true, _matchStatisticsMapper.Map(matchEntity, null));
                }
            }
            return ResponseDTO<BaseMatchDTO>.Success(false);
        }
    }
}
