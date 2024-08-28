using FutbolSolution.Core.Models;
using System;
namespace FutbolSolution.Core.DTOs.PlayerDTOs
{
    public class PlayerWithStatisticInformationDTO : BasePlayerDTO
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
        public PlayerImage PlayerImage { get; set; }
        public int Goals { get; set; } = 0;
        public int Assists { get; set; } = 0;
        public int TotalMinutesIn { get; set; } = 0;
        public decimal? PassAccuracy { get; set; }
        public int Tackles { get; set; } = 0;
        public int Interceptions { get; set; } = 0;
        public int Clearances { get; set; } = 0;
        public int Shots { get; set; } = 0;
        public int ShotsOnTarget { get; set; } = 0;
        public int DribblesCompleted { get; set; } = 0;
        public int AerialDuelsWon { get; set; } = 0;
        public int YellowCards { get; set; } = 0;
        public int RedCards { get; set; } = 0;
        public int FoulsCommitted { get; set; } = 0;
        public int FoulsSuffered { get; set; } = 0;
        public int Offsides { get; set; } = 0;
        public int Saves { get; set; } = 0;
        public int CleanSheets { get; set; } = 0;
    }
}
