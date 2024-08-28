using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using FutbolSolution.Core.DTOs.Response;

namespace FutbolSolution.Core.Services
{
    public interface IGenericService<TDTO>
    {
        Task<ResponseDTO<TDTO>> Create(TDTO dto);
        Task<ResponseDTO<TDTO>> Update(TDTO dto);
        Task<ResponseDTO<TDTO>> Delete(TDTO dto);
        Task<ResponseDTO<IEnumerable<TDTO>>> DeleteRange(IEnumerable<TDTO> dtos);
        Task<ResponseDTO<IEnumerable<TDTO>>> GetAll();
        Task<ResponseDTO<TDTO>> GetById(int id);
        Task<ResponseDTO<IEnumerable<TDTO>>> CreateRange(IEnumerable<TDTO> dtos); // Ensure return type matches
    }
}
