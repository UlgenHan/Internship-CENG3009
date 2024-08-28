using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.DTOs.RefereeDTOs;
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
    public class MatchMapper : IMatchMapper
    {
        public Match Map(BaseMatchDTO source, object parameter = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source is MatchDTO matchDTO)
            {
                return MapMatchDTOToMatch(matchDTO);
            }
            else
            {
                throw new InvalidOperationException("Unsupported DTO type.");
            }
        }

        public BaseMatchDTO Map(Match source, object parameter = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source is Match match)
            {
                return MapMatchToMatchDTO(match);
            }
            else
            {
                throw new InvalidOperationException("Unsupported Match type.");
            }
        }

        private Match MapMatchDTOToMatch(MatchDTO matchDTO)
        {
            return new Match
            {
                MatchId = matchDTO.MatchId,
                HomeTeamId = matchDTO.HomeTeamId,
                AwayTeamId = matchDTO.AwayTeamId,
                MatchDate = matchDTO.MatchDate,
                Stadium = matchDTO.Stadium,
                RefereeId = matchDTO.RefereeId,
                WeatherConditions = matchDTO.WeatherConditions,
                Importance = matchDTO.Importance
            };
        }

        private MatchDTO MapMatchToMatchDTO(Match match)
        {
            return new MatchDTO
            {
                MatchId = match.MatchId,
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
                MatchDate = match.MatchDate,
                Stadium = match.Stadium,
                RefereeId = match.RefereeId,
                WeatherConditions = match.WeatherConditions,
                Importance = match.Importance
            };
        }
    }
}
