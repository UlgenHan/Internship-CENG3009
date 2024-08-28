using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Core.Services;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolSolution.Service.Services
{
    public class GenericService<TDTO, TEntity> : IGenericService<TDTO> where TEntity : class, new()
                                                                      where TDTO : class, new()
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper<TDTO, TEntity> _mapper;

        public GenericService(IGenericRepository<TEntity> genericRepository, IMapper<TDTO, TEntity> mapper)
        {
            _repository = genericRepository;
            _mapper = mapper;
        }

        public virtual async Task<ResponseDTO<TDTO>> Create(TDTO dto)
        {
            try
            {
                var entity = _mapper.Map(dto,null);
                await _repository.AddAsync(entity);
                var createdDto = _mapper.Map(entity, null);
                return ResponseDTO<TDTO>.Success(true, createdDto);
            }
            catch (System.Exception ex)
            {
                return ResponseDTO<TDTO>.Fail(ex.Message, false, true);
            }
        }

        public virtual async Task<ResponseDTO<IEnumerable<TDTO>>> CreateRange(IEnumerable<TDTO> dtos)
        {
            try
            {
                var entities = dtos.Select(dto => _mapper.Map(dto, null)).ToList();
                await _repository.AddRangeAsync(entities);
                var createdDtos = entities.Select(entity => _mapper.Map(entity,null)).ToList();
                return ResponseDTO<IEnumerable<TDTO>>.Success(true, createdDtos);
            }
            catch (System.Exception ex)
            {
                return ResponseDTO<IEnumerable<TDTO>>.Fail(ex.Message, false, true);
            }
        }

        public virtual async Task<ResponseDTO<TDTO>> Delete(TDTO dto)
        {
            try
            {
                var entity = _mapper.Map(dto,null);
                await _repository.RemoveAsync(entity);
                return ResponseDTO<TDTO>.Success(true);
            }
            catch (System.Exception ex)
            {
                return ResponseDTO<TDTO>.Fail(ex.Message, false, true);
            }
        }

        public virtual async Task<ResponseDTO<IEnumerable<TDTO>>> DeleteRange(IEnumerable<TDTO> dtos)
        {
            try
            {
                var entities = dtos.Select(dto => _mapper.Map(dto, null)).ToList();
                await _repository.RemoveRangeAsync(entities);
                return ResponseDTO<IEnumerable<TDTO>>.Success(true);
            }
            catch (System.Exception ex)
            {
                return ResponseDTO<IEnumerable<TDTO>>.Fail(ex.Message, false, true);
            }
        }

        public virtual async Task<ResponseDTO<IEnumerable<TDTO>>> GetAll()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var dtos = entities.Select(entity => _mapper.Map(entity, null)).ToList();
                return ResponseDTO<IEnumerable<TDTO>>.Success(true, dtos);
            }
            catch (System.Exception ex)
            {
                return ResponseDTO<IEnumerable<TDTO>>.Fail(ex.Message, false, true);
            }
        }

        public virtual async Task<ResponseDTO<TDTO>> GetById(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return ResponseDTO<TDTO>.Fail("Entity not found", false, true);
                }
                var dto = _mapper.Map(entity, null);
                return ResponseDTO<TDTO>.Success(true, dto);
            }
            catch (System.Exception ex)
            {
                return ResponseDTO<TDTO>.Fail(ex.Message, false, true);
            }
        }

        public virtual async Task<ResponseDTO<TDTO>> Update(TDTO dto)
        {
            try
            {
                var entity = _mapper.Map(dto, null);
                await _repository.UpdateAsync(entity);
                var updatedDto = _mapper.Map(entity, null);
                return ResponseDTO<TDTO>.Success(true, updatedDto);
            }
            catch (System.Exception ex)
            {
                return ResponseDTO<TDTO>.Fail(ex.Message, false, true);
            }
        }
    }
}
