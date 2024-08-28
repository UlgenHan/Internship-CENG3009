using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using System;

namespace FutbolSolution.Service.Mappers
{
    public class PlayerTeamLinkMapper : IPlayerTeamLinkMapper
    {
        public PlayerTeamLink Map(PlayerTeamLinkDTO source, object parameter)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new PlayerTeamLink
            {
                PlayerTeamLinkId = source.PlayerTeamLinkId,
                PlayerId = source.PlayerId,
                TeamId = source.TeamId,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                IsLoan = source.IsLoan
            };
        }

        public PlayerTeamLinkDTO Map(PlayerTeamLink destination, object parameter)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            return new PlayerTeamLinkDTO
            {
                PlayerTeamLinkId = destination.PlayerTeamLinkId,
                TeamId = (int)destination.TeamId,
                PlayerId = (int)destination.PlayerId,
                StartDate = destination.StartDate,
                EndDate = destination.EndDate,
                IsLoan = destination.IsLoan
            };
        }
    }
}
