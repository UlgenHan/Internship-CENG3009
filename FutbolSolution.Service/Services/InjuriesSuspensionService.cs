using FutbolSolution.Core.DTOs.InjuresSuspensionDTOs;
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
    public class InjuriesSuspensionService : GenericService<BaseInjuriesSuspensionsDTO, InjuriesSuspensions>,
        IInjuresSuspensionService
    {
        private readonly IInjuriesSuspensionsRepository _injuresSuspensionRepository;
        private readonly IMapper<BaseInjuriesSuspensionsDTO, InjuriesSuspensions> _mapper;
        public InjuriesSuspensionService(IInjuriesSuspensionsRepository genericRepository,
            IMapper<BaseInjuriesSuspensionsDTO,InjuriesSuspensions> mapper)
            : base(genericRepository,mapper)
        {
            _injuresSuspensionRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<BaseInjuriesSuspensionsDTO>> Create(BaseInjuriesSuspensionsDTO baseInjuriesSuspensionsDTO)
        {
            var convertedDto = baseInjuriesSuspensionsDTO as InjuriesSuspensionsDTO;
            var entity = _mapper.Map(convertedDto, null);
            await _injuresSuspensionRepository.AddAsync(entity);

            await _injuresSuspensionRepository.AddInjuresLinkAsync(new InjuriesLink { 
                InjuriesSuspensionId = entity.InjurySuspensionId,
                Severity = convertedDto.InjuriesLink.Severity,
            });

            return ResponseDTO<BaseInjuriesSuspensionsDTO>.Success(true);
        }

        public async Task<ResponseDTO<IQueryable<BaseInjuriesSuspensionsDTO>>> GetInjuriesSuspensionsByPlayerID(int playerID)
        {
            
            var injuresCollection = await _injuresSuspensionRepository.GetAllAsync();
            List<BaseInjuriesSuspensionsDTO> resultList = new List<BaseInjuriesSuspensionsDTO>();

            foreach(var inj in injuresCollection)
            {
                if (inj.PlayerId.Equals(playerID))
                {
                    resultList.Add(_mapper.Map(inj,null));
                }
            }
            return ResponseDTO<IQueryable<BaseInjuriesSuspensionsDTO>>.Success(true,resultList.AsQueryable());
        }

        public async Task<ResponseDTO<BaseInjuriesSuspensionsDTO>> DeleteInjuryByPlayerID(int playerID)
        {
            var injuresCollection = await _injuresSuspensionRepository.GetAllAsync();
            foreach (var inj in injuresCollection)
            {
                await DeleteInjuresLinkByInjuriesSuspensionID(inj.InjurySuspensionId);
                if (inj.PlayerId.Equals(playerID))
                {
                    await _repository.RemoveAsync(inj);
                }
               
            }
            return ResponseDTO<BaseInjuriesSuspensionsDTO>.Success(true);
        }

        public async Task<ResponseDTO<List<InjuriesLink>>> GetAllInjuresLink()
        {

           var injuresLinkQuerable =  await _injuresSuspensionRepository.GetAllInjuresLinkAsync();
            return ResponseDTO<List<InjuriesLink>>.Success(true, injuresLinkQuerable.ToList());
        }

        public async Task<ResponseDTO<InjuriesLink>> GetByIdInjuresLink(int id)
        {
            var injuresLinkEntity = await _injuresSuspensionRepository.GetInjuresLinkByIdAsync(id);
            return ResponseDTO<InjuriesLink>.Success(true, injuresLinkEntity);
        }

        public async Task<ResponseDTO<InjuriesLink>> UpdateInjuresLink(InjuriesLink injuresLink)
        {
            await _injuresSuspensionRepository.UpdateInjuresLinkAsync(injuresLink);
            return ResponseDTO<InjuriesLink>.Success(true);
        }

        public async Task<ResponseDTO<InjuriesLink>> DeleteInjuresLink(int id)
        {
            await _injuresSuspensionRepository.RemoveInjuresLinkAsync(id);
            return ResponseDTO<InjuriesLink>.Success(true);
        }

        public async Task<ResponseDTO<InjuriesLink>> DeleteInjuresLinkByInjuriesSuspensionID(int id)
        {
            var injuresCollection = await _injuresSuspensionRepository.GetAllInjuresLinkAsync();
            foreach (var item in injuresCollection)
            {
                if(item.InjuriesSuspensionId == id)
                    await _injuresSuspensionRepository.RemoveInjuresLinkAsync(item.Id);
            }
            
            return ResponseDTO<InjuriesLink>.Success(true);
        }


        public async Task<ResponseDTO<InjuriesLink>> CreateInjuresLink(InjuriesLink injuriesLink)
        {
            await _injuresSuspensionRepository.AddInjuresLinkAsync(injuriesLink);
            return ResponseDTO<InjuriesLink>.Success(true);
        }

        public async Task<ResponseDTO<List<InjuriesLink>>> GetInjuresLinkBySuspensionId(int suspensionID)
        {
            var response = await GetAllInjuresLink();
            if(response != null)
            {
                var filteredInjuresLink = response.Data.Where(suspension => suspension.InjuriesSuspensionId == suspensionID).ToList();
                return ResponseDTO<List<InjuriesLink>>.Success(true, filteredInjuresLink);
            }
            return ResponseDTO<List<InjuriesLink>>.Success(false);
        }
    }
}
