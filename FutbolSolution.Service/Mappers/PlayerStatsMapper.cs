using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using System;

namespace FutbolSolution.Service.Mappers
{
    namespace FutbolSolution.Service.Mappers
    {
        public class PlayerStatsMapper : IMapper<BasePlayerDTO, PlayerStats>
        {
            public PlayerStats Map(BasePlayerDTO source, object parameter = null)
            {
                if (source == null) throw new ArgumentNullException(nameof(source));

                if (source is PlayerStatsDTO playerStatsDTO)
                {
                    return MapPlayerStatsDTOToPlayerStats(playerStatsDTO);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported DTO type.");
                }
            }

            public BasePlayerDTO Map(PlayerStats source, object parameter = null)
            {
                if (source == null) throw new ArgumentNullException(nameof(source));

                if (source is PlayerStats playerStats)
                {
                    return MapPlayerStatsToPlayerStatsDTO(playerStats);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported PlayerStats type.");
                }
            }

            private PlayerStats MapPlayerStatsDTOToPlayerStats(PlayerStatsDTO playerStatsDTO)
            {
                return new PlayerStats
                {
                    PlayerStatsId = playerStatsDTO.Id,
                    Goals = playerStatsDTO.Goals,
                    Assists = playerStatsDTO.Assists,
                    TotalMinutesIn = playerStatsDTO.TotalMinutesIn,
                    PassAccuracy = playerStatsDTO.PassAccuracy,
                    Tackles = playerStatsDTO.Tackles,
                    Interceptions = playerStatsDTO.Interceptions,
                    Clearances = playerStatsDTO.Clearances,
                    Shots = playerStatsDTO.Shots,
                    ShotsOnTarget = playerStatsDTO.ShotsOnTarget,
                    DribblesCompleted = playerStatsDTO.DribblesCompleted,
                    AerialDuelsWon = playerStatsDTO.AerialDuelsWon,
                    YellowCards = playerStatsDTO.YellowCards,
                    RedCards = playerStatsDTO.RedCards,
                    FoulsCommitted = playerStatsDTO.FoulsCommitted,
                    FoulsSuffered = playerStatsDTO.FoulsSuffered,
                    Offsides = playerStatsDTO.Offsides,
                    Saves = playerStatsDTO.Saves,
                    CleanSheets = playerStatsDTO.CleanSheets
                };
            }

            private PlayerStatsDTO MapPlayerStatsToPlayerStatsDTO(PlayerStats playerStats)
            {
                return new PlayerStatsDTO
                {
                    Id = playerStats.PlayerStatsId,
                    Goals = playerStats.Goals,
                    Assists = playerStats.Assists,
                    TotalMinutesIn = playerStats.TotalMinutesIn,
                    PassAccuracy = playerStats.PassAccuracy,
                    Tackles = playerStats.Tackles,
                    Interceptions = playerStats.Interceptions,
                    Clearances = playerStats.Clearances,
                    Shots = playerStats.Shots,
                    ShotsOnTarget = playerStats.ShotsOnTarget,
                    DribblesCompleted = playerStats.DribblesCompleted,
                    AerialDuelsWon = playerStats.AerialDuelsWon,
                    YellowCards = playerStats.YellowCards,
                    RedCards = playerStats.RedCards,
                    FoulsCommitted = playerStats.FoulsCommitted,
                    FoulsSuffered = playerStats.FoulsSuffered,
                    Offsides = playerStats.Offsides,
                    Saves = playerStats.Saves,
                    CleanSheets = playerStats.CleanSheets
                };
            }
        }
    }

}
