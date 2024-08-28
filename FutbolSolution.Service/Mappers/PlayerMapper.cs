using FutbolSolution.Core.DTOs.PlayerDTOs;
using FutbolSolution.Core.Mapper;
using FutbolSolution.Core.Models;
using System;


namespace FutbolSolution.Service.Mappers
{
    public class PlayerMapper : IPlayerMapper
    {
        public Player Map(BasePlayerDTO source, object parameter = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source is PlayerDTO playerDTO)
            {
                return MapPlayerDTOToPlayer(playerDTO);
            }
            else
            {
                throw new InvalidOperationException("Unsupported DTO type.");
            }
        }

        public BasePlayerDTO Map(Player source,object playerImage = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            if (source is Player player)
            {
                return MapPlayerToPlayerDTO(player, playerImage as PlayerImage);
            }
            else
            {
                throw new InvalidOperationException("Unsupported Player type.");
            }
        }

        private Player MapPlayerDTOToPlayer(PlayerDTO playerDTO)
        {
            return new Player
            {
                Id = playerDTO.Id,
                Name = playerDTO.Name,
                Surname = playerDTO.Surname,
                Age = playerDTO.Age,
                DateOfBirth = playerDTO.DateOfBirth,
                Nationality = playerDTO.Nationality,
                Position = playerDTO.Position,
                CurrentClub = playerDTO.CurrentClub,
                Height = playerDTO.Height,
                Weight = playerDTO.Weight,
                PreferredFoot = playerDTO.PreferredFoot,
                PlayerStatsId = playerDTO.PlayerStatsId,
            };
        }

        private PlayerDTO MapPlayerToPlayerDTO(Player player,PlayerImage playerImage)
        {
            return new PlayerDTO
            {
                Id = player.Id,
                Name = player.Name,
                Surname = player.Surname,
                Age = player.Age,
                DateOfBirth = player.DateOfBirth,
                Nationality = player.Nationality,
                Position = player.Position,
                CurrentClub = player.CurrentClub,
                Height = player.Height,
                Weight = player.Weight,
                PreferredFoot = player.PreferredFoot,
                PlayerStatsId = player.PlayerStatsId,
                PlayerImage = playerImage
            };
        }
    }


}
