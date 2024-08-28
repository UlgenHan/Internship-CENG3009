using FutbolSolution.Core.DTOs.InjuresSuspensionDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace FutbolSolution.Core.Services
{
    public interface IInjuresSuspensionService : IGenericService<BaseInjuriesSuspensionsDTO>
    {
        Task<ResponseDTO<IQueryable<BaseInjuriesSuspensionsDTO>>> GetInjuriesSuspensionsByPlayerID(int playerID);
        Task<ResponseDTO<BaseInjuriesSuspensionsDTO>> DeleteInjuryByPlayerID(int playerID);
        Task<ResponseDTO<List<InjuriesLink>>> GetAllInjuresLink();
        Task<ResponseDTO<InjuriesLink>> GetByIdInjuresLink(int id);
        Task<ResponseDTO<InjuriesLink>> UpdateInjuresLink(InjuriesLink injuresLink);
        Task<ResponseDTO<InjuriesLink>> DeleteInjuresLink(int id);
        Task<ResponseDTO<InjuriesLink>> CreateInjuresLink(InjuriesLink injuriesLink);
        Task<ResponseDTO<List<InjuriesLink>>> GetInjuresLinkBySuspensionId(int suspensionID);
        Task<ResponseDTO<InjuriesLink>> DeleteInjuresLinkByInjuriesSuspensionID(int id);
    }
}
