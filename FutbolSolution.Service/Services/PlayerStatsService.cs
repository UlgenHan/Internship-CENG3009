using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;

namespace FutbolSolution.Service.Services
{
    public class PlayerStatsService : GenericService<BasePlayerDTO, PlayerStats>, IPlayerStatsService
    {
        private readonly IPlayerStatsRepository _playerStatsRepository;
        public PlayerStatsService(IPlayerStatsRepository genericRepository, IMapper<BasePlayerDTO, PlayerStats> mapper)
            : base(genericRepository, mapper)
        {
            _playerStatsRepository = genericRepository;
        }
    }
}
