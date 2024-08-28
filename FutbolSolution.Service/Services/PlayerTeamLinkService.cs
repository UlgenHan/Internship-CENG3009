using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolSolution.Service.Services
{
    public class PlayerTeamLinkService : GenericService<PlayerTeamLinkDTO,PlayerTeamLink>, IPlayerTeamLink
    {
        private IPlayerTeamLinkRepository _playerTeamLinkRepository;
        private IMapper<PlayerTeamLinkDTO, PlayerTeamLink> _mapper;
        public PlayerTeamLinkService(IPlayerTeamLinkRepository genericRepository,
            IMapper<PlayerTeamLinkDTO, PlayerTeamLink> mapper) :
            base(genericRepository,mapper)
        {
            _playerTeamLinkRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<PlayerTeamLinkDTO>> FindByPlayerID(int playerID)
        {
            var allPlayerLinkEntities = await _playerTeamLinkRepository.GetAllAsync();

            var playerlink = allPlayerLinkEntities.Where(playerLink => playerLink.PlayerId == playerID).FirstOrDefault();

        
            if(playerlink is null)
            {
                return ResponseDTO<PlayerTeamLinkDTO>.Success(false);
            }
            var castedObject = (PlayerTeamLink)playerlink;
            return ResponseDTO<PlayerTeamLinkDTO>.Success(true, _mapper.Map(castedObject, null));
            
        }

        public async Task<ResponseDTO<List<PlayerTeamLinkDTO>>> FindByTeamID(int teamID)
        {
            var allPlayerLinkEntities = await _playerTeamLinkRepository.GetAllAsync();

            var playerlink = allPlayerLinkEntities.Where(playerLink => playerLink.TeamId == teamID).ToList();
            if(playerlink is null)
            {
                 return ResponseDTO<List<PlayerTeamLinkDTO>>.Success(false);
            }
            var resultList = new List<PlayerTeamLinkDTO>();
            foreach(var item in playerlink)
            {
               var mappedDTO =  _mapper.Map(item, null);
                resultList.Add(mappedDTO);
            }

            return ResponseDTO<List<PlayerTeamLinkDTO>>.Success(true, resultList);
        }
    }
}
