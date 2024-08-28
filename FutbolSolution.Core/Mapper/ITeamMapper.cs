using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Mapper
{
    public interface ITeamMapper : IMapper<BaseTeamDTO,Team>
    {
    }
}
