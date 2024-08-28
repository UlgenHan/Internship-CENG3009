using FutbolSolution.Core.Models;
using System;

namespace FutbolSolution.Core.DTOs.PlayerDTOs
{
    public class PlayerDTO : BasePlayerDTO
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
        public int PlayerStatsId { get; set; }
        public PlayerImage PlayerImage { get; set; }
    }
}
