using FutbolSolution.Core.DTOs.RefereeDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;

namespace FutbolSolution.Service.Services
{
    public class RefereeService : GenericService<BaseRefereeDTO,Referee>, IRefereeService
    {
        public RefereeService(IRefereeRepository genericRepository, IMapper<BaseRefereeDTO,Referee> mapper) : 
            base(genericRepository,mapper)
        {
        }
    }
}
