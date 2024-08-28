using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FutbolSolution.Service.Services
{
    public class PlayerService : GenericService<BasePlayerDTO,Player>, IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayerService(IPlayerRepository genericRepository, IMapper<BasePlayerDTO, Player> mapper) 
            : base(genericRepository,mapper)
        {
            _playerRepository = genericRepository;
        }

        public override async Task<ResponseDTO<BasePlayerDTO>> Create(BasePlayerDTO playerDTO)
        {
            // Take player entity as a dictionary 
            var playerEntity = _mapper.Map(playerDTO, null);

            // Firstly add player entity 
            await _playerRepository.AddAsync(playerEntity);

            // Check if the passed dto is of type PlayerDTO
            if (playerDTO is PlayerDTO playerDetails)
            {
                playerDetails.PlayerImage.PlayerId = playerEntity.Id;

                // Then add player image entity to db
                await _playerRepository.AddImageAsync(playerDetails.PlayerImage);
            }

            // Return responseDTO finish process
            return ResponseDTO<BasePlayerDTO>.Success(true,_mapper.Map(playerEntity,null));
        }

        public override async Task<ResponseDTO<BasePlayerDTO>> Delete(BasePlayerDTO dto)
        {
            var playerEntity = _mapper.Map(dto, null);

            // Remove Player Images firstly 
            var imageEntities = await _playerRepository.GetImagesAsync();
            foreach( var imageEntity in imageEntities)
            {
                if(imageEntity.PlayerId == playerEntity.Id)
                {
                    await _playerRepository.RemoveImageAsync(imageEntity.PlayerId);
                }
            }
            await _playerRepository.RemoveAsync(playerEntity);
            return ResponseDTO<BasePlayerDTO>.Success(true);
        }

        public override async Task<ResponseDTO<IEnumerable<BasePlayerDTO>>> GetAll()
        {
            var playerEntities = await _playerRepository.GetAllAsync();
            List<PlayerDTO> result = new List<PlayerDTO>();
            foreach (var playerEntity in playerEntities)
            {
                var temptPlayerDTO = (PlayerDTO)_mapper.Map(playerEntity, null);
                var playerImage = await _playerRepository.GetImageAsync(playerEntity.Id);
                temptPlayerDTO.PlayerImage = playerImage;
                result.Add(temptPlayerDTO);
             }
            return ResponseDTO<IEnumerable<BasePlayerDTO>>.Success(true,result);
        }

        public override async Task<ResponseDTO<BasePlayerDTO>> GetById(int id)
        {
            var entity = await _playerRepository.GetByIdAsync(id);
            var entityImage = await _playerRepository.GetImageAsync(id);

            var playerDTO = (PlayerDTO)_mapper.Map(entity, null);
            playerDTO.PlayerImage = new PlayerImage();
            playerDTO.PlayerImage.PlayerId = entityImage.PlayerId;
            playerDTO.PlayerImage.ImageData = entityImage.ImageData;

            return ResponseDTO<BasePlayerDTO>.Success(true, playerDTO);
        }

        public override async Task<ResponseDTO<BasePlayerDTO>> Update(BasePlayerDTO dto)
        {
            // update Image 
            var playerDto = dto as PlayerDTO;
            var playerEntity = _mapper.Map(playerDto, null);
            var imageEntiy = playerDto.PlayerImage;
            await _playerRepository.UpdateImageAsync(imageEntiy);

            await _playerRepository.UpdateAsync(playerEntity);
            return ResponseDTO<BasePlayerDTO>.Success(true);
        }
    }
}
