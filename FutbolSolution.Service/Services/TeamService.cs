using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FutbolSolution.Service.Services
{
    public class TeamService : GenericService<BaseTeamDTO, Team>, ITeamService
    {
        private readonly IMapper<BaseTeamDTO, Team> _mapper;
        private readonly ITeamRepository _teamRepository;
 
        public TeamService(ITeamRepository genericRepository, IMapper<BaseTeamDTO, Team> mapper)
            : base(genericRepository, mapper)
        {
            _teamRepository = genericRepository;
            _mapper = mapper;
        }

        public override async Task<ResponseDTO<BaseTeamDTO>> Create(BaseTeamDTO dto)
        {
            var teamEntity = _mapper.Map(dto, null);
            await _teamRepository.AddAsync(teamEntity);

            // Check if the passed dto is of type TeamDTO
            if (dto is TeamDTO teamDto)
            {
                
                teamDto.TeamImage.TeamId = teamEntity.TeamId;

                // Then add team image entity to db
                await _teamRepository.AddImageAsync(teamDto.TeamImage);
            }

            // Return responseDTO to finish process
            return ResponseDTO<BaseTeamDTO>.Success(true);
        }

        public override async Task<ResponseDTO<IEnumerable<BaseTeamDTO>>> GetAll()
        {
            var teamEntities = await _teamRepository.GetAllAsync();
            List<TeamDTO> result = new List<TeamDTO>();
            foreach (var teamEntity in teamEntities)
            {
                var tempTeamDTO = (TeamDTO)_mapper.Map(teamEntity, null);
                var teamImage = await _teamRepository.GetImageAsync(teamEntity.TeamId);
                tempTeamDTO.TeamImage = teamImage;
                result.Add(tempTeamDTO);
            }
            return ResponseDTO<IEnumerable<BaseTeamDTO>>.Success(true, result);
        }

        public override async Task<ResponseDTO<BaseTeamDTO>> Update(BaseTeamDTO baseTeamDTO)
        {
            var teamDto = baseTeamDTO as TeamDTO;

            var teamEntity = _mapper.Map(teamDto, null);

            var teamImageEntity = new TeamImage { TeamId = teamEntity.TeamId, ImageData = teamDto.TeamImage.ImageData };

            if (teamDto != null)
            {
                await _teamRepository.UpdateAsync(teamEntity);
                await _teamRepository.UpdateImageAsync(teamImageEntity);
            }

            return ResponseDTO<BaseTeamDTO>.Success(true);
        }

        public override async Task<ResponseDTO<BaseTeamDTO>> Delete(BaseTeamDTO dto)
        {
            var teamEntity = _mapper.Map(dto, null);

            // Remove Team Images first
            var imageEntities = await _teamRepository.GetImagesAsync();
            foreach (var imageEntity in imageEntities)
            {
                if (imageEntity.TeamId == teamEntity.TeamId)
                {
                    await _teamRepository.RemoveImageAsync(imageEntity.TeamId);
                }
            }

            await _teamRepository.RemoveAsync(teamEntity);
            return ResponseDTO<BaseTeamDTO>.Success(true);
        }
    }
}
