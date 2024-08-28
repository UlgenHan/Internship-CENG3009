using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutbolSolution.Service.Services
{
    public class MatchService : GenericService<BaseMatchDTO,Match>, IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper<BaseMatchDTO, Match> _mapper;
        public MatchService(IMatchRepository genericRepository,IMapper<BaseMatchDTO,Match> mapper) : 
            base(genericRepository,mapper)
        {
            _matchRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<List<BaseMatchDTO>>> GetMatchesByTeamId(int id)
        {
            var allMatchEntities = await _matchRepository.GetAllAsync();
            var result = new List<BaseMatchDTO>();
            foreach (var matchEntity in allMatchEntities)
            {
                if(matchEntity.AwayTeamId == id || matchEntity.HomeTeamId == id)
                {
                    result.Add(_mapper.Map(matchEntity,null));
                }
            }

            return ResponseDTO<List<BaseMatchDTO>>.Success(true,result);
        }

        public async Task<ResponseDTO<List<BaseMatchDTO>>> GetMatchesByRefereeId(int refereeId)
        {
            var allMatchesEntites = await _matchRepository.GetAllAsync();
            var result = new List<BaseMatchDTO>();
            foreach(var matchEntity in allMatchesEntites)
            {
                if (matchEntity.RefereeId == refereeId)
                {
                    result.Add(_mapper.Map(matchEntity, null));
                }
            }
            
            return ResponseDTO<List<BaseMatchDTO>>.Success(true, result);
        }
    }
}
