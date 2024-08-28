using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.Response;
using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Mapper
{
    public interface IMatchStatsMapper : IMapper<BaseMatchDTO,MatchStatistics>
    {
        
    }
}
