using System;
using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using FutbolSolution.Service.Mappers;

namespace FutbolSolution.Service.Mappers
{
  public interface IMatchMapper : IMapper<BaseMatchDTO, Match>
    {

    }
}
