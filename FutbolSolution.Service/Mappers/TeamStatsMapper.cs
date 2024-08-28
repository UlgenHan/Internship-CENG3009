using FutbolSolution.Core.DTOs.TeamDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using System;

namespace FutbolSolution.Service.Mappers
{
    public class TeamStatsMapper : ITeamStatsMapper
    {
        public TeamStatistics Map(BaseTeamDTO source, object parameter)
        {
            if (source is TeamWithStatisticsDTO dto)
            {
                return new TeamStatistics
                {
                    TeamStatsId = dto.TeamStatsId,
                    TeamId = dto.TeamId,
                    SeasonYear = dto.SeasonYear,
                    GoalsScored = dto.GoalsScored,
                    GoalsConceded = dto.GoalsConceded,
                    Wins = dto.Wins,
                    Draws = dto.Draws,
                    Losses = dto.Losses,
                    HomeWins = dto.HomeWins,
                    AwayWins = dto.AwayWins,
                    RecentForm = dto.RecentForm
                };
            }
            throw new ArgumentException("Invalid type of source object. Expected TeamWithStatisticsDTO.");
        }

        public BaseTeamDTO Map(TeamStatistics destination, object parameter)
        {
            return new TeamWithStatisticsDTO
            {
                TeamStatsId = destination.TeamStatsId,
                TeamId = (int)destination.TeamId,
                SeasonYear = destination.SeasonYear,
                GoalsScored = destination.GoalsScored,
                GoalsConceded = destination.GoalsConceded,
                Wins = destination.Wins,
                Draws = destination.Draws,
                Losses = destination.Losses,
                HomeWins = destination.HomeWins,
                AwayWins = destination.AwayWins,
                RecentForm = destination.RecentForm
            };
        }
    }
}
