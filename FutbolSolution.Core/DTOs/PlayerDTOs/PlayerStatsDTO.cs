
namespace FutbolSolution.Core.DTOs.PlayerDTOs
{
    public class PlayerStatsDTO : BasePlayerDTO
    {
        public int Goals { get; set; } = 0; //1
        public int Assists { get; set; } = 0; //2
        public int TotalMinutesIn { get; set; } = 0; //3
        public decimal? PassAccuracy { get; set; } // 4
        public int Tackles { get; set; } = 0; //5
        public int Interceptions { get; set; } = 0; //6
        public int Clearances { get; set; } = 0; //7
        public int Shots { get; set; } = 0; //8
        public int ShotsOnTarget { get; set; } = 0; //9
        public int DribblesCompleted { get; set; } = 0; //10
        public int AerialDuelsWon { get; set; } = 0; //11
        public int YellowCards { get; set; } = 0; //12
        public int RedCards { get; set; } = 0; //13
        public int FoulsCommitted { get; set; } = 0; //14
        public int FoulsSuffered { get; set; } = 0; // 15
        public int Offsides { get; set; } = 0; // 16
        public int Saves { get; set; } = 0; // 18  
        public int CleanSheets { get; set; } = 0;//
    }
}
