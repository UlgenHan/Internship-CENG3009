using System.Collections.Generic;
using System;
using FutbolSolution.Core.DTOs.InjuresSuspensionDTOs;

namespace FutbolSolution.Core.DTOs.PlayerDTOs
{
    public class GeneralPlayerDTO:BasePlayerDTO
    {
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string CurrentClub { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string PreferredFoot { get; set; }

        // Player image information
        public byte[] ImageData { get; set; } // For storing player image data

        // Player statistics
        public PlayerStatsDTO PlayerStats { get; set; }

        // Links to teams
        public ICollection<PlayerTeamLinkDTO> TeamLinks { get; set; }

        // Injuries and suspensions
        public ICollection<InjuriesSuspensionsDTO> Injuries { get; set; }

        // Constructor
        public GeneralPlayerDTO()
        {
            TeamLinks = new List<PlayerTeamLinkDTO>();
            Injuries = new List<InjuriesSuspensionsDTO>();
        }
    }
}


