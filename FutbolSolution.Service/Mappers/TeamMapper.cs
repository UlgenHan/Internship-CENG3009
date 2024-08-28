using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Service.Mappers
{
    public class TeamMapper : ITeamMapper
    {
        public Team Map(BaseTeamDTO source, object parameter = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source is TeamDTO teamDTO)
            {
                return MapTeamDTOToTeam(teamDTO);
            }
            else
            {
                throw new InvalidOperationException("Unsupported DTO type.");
            }
        }

        public BaseTeamDTO Map(Team source, object parameter = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source is Team team)
            {
                return MapTeamToTeamDTO(team, parameter as TeamImage);
            }
            else
            {
                throw new InvalidOperationException("Unsupported Team type.");
            }
        }

        private Team MapTeamDTOToTeam(TeamDTO teamDTO)
        {
            return new Team
            {
                TeamId = teamDTO.TeamId,
                Name = teamDTO.Name,
                Stadium = teamDTO.Stadium,
                Coach = teamDTO.Coach,
                FoundedYear = teamDTO.FoundedYear,
                City = teamDTO.City,
            };
        }

        private TeamDTO MapTeamToTeamDTO(Team team, TeamImage teamImage)
        {
            return new TeamDTO
            {
                TeamId = team.TeamId,
                Name = team.Name,
                Stadium = team.Stadium,
                Coach = team.Coach,
                FoundedYear = team.FoundedYear,
                City = team.City,
                TeamImage = teamImage
            };
        }
    }
}
