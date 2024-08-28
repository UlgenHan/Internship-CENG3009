using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using System;

namespace FutbolSolution.Service.Mappers
{
    public class MatchStatsMapper : IMatchStatsMapper
    {
        public MatchStatistics Map(BaseMatchDTO source, object parameter)
        {
            if (source is MatchStatsDTO matchStatsDTO)
            {
                return new MatchStatistics
                {
                    MatchStatsId = matchStatsDTO.MatchStatsId,
                    MatchId = matchStatsDTO.MatchId,
                    HomeGoals = matchStatsDTO.HomeGoals,
                    AwayGoals = matchStatsDTO.AwayGoals,
                    HomePossession = matchStatsDTO.HomePossession,
                    AwayPossession = matchStatsDTO.AwayPossession,
                    HomeShots = matchStatsDTO.HomeShots,
                    AwayShots = matchStatsDTO.AwayShots,
                    HomeShotsOnTarget = matchStatsDTO.HomeShotsOnTarget,
                    AwayShotsOnTarget = matchStatsDTO.AwayShotsOnTarget,
                    HomeFouls = matchStatsDTO.HomeFouls,
                    AwayFouls = matchStatsDTO.AwayFouls,
                    HomeYellowCards = matchStatsDTO.HomeYellowCards,
                    AwayYellowCards = matchStatsDTO.AwayYellowCards,
                    HomeRedCards = matchStatsDTO.HomeRedCards,
                    AwayRedCards = matchStatsDTO.AwayRedCards
                };
            }

            throw new ArgumentException("Invalid type of source object", nameof(source));
        }

        public BaseMatchDTO Map(MatchStatistics destination, object parameter)
        {
            if (destination != null)
            {
                return new MatchStatsDTO
                {
                    MatchStatsId = destination.MatchStatsId,
                    MatchId = destination.MatchId ?? 0, // Assuming MatchId is required in DTO
                    HomeGoals = destination.HomeGoals,
                    AwayGoals = destination.AwayGoals,
                    HomePossession = destination.HomePossession,
                    AwayPossession = destination.AwayPossession,
                    HomeShots = destination.HomeShots,
                    AwayShots = destination.AwayShots,
                    HomeShotsOnTarget = destination.HomeShotsOnTarget,
                    AwayShotsOnTarget = destination.AwayShotsOnTarget,
                    HomeFouls = destination.HomeFouls,
                    AwayFouls = destination.AwayFouls,
                    HomeYellowCards = destination.HomeYellowCards,
                    AwayYellowCards = destination.AwayYellowCards,
                    HomeRedCards = destination.HomeRedCards,
                    AwayRedCards = destination.AwayRedCards
                };
            }

            throw new ArgumentException("Invalid type of destination object", nameof(destination));
        }
    }
}
