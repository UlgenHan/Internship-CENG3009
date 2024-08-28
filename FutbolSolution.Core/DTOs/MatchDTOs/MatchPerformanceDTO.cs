namespace FutbolSolution.Core.DTOs.MatchDTOs
{
    public class MatchPerformanceDTO : BaseMatchDTO
    {
            public int? PlayerId { get; set; }
            public int? MatchId { get; set; }
            public int Goals { get; set; } = 0;
            public int Assists { get; set; } = 0;
            public int MinutesPlayed { get; set; } = 0;
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
