namespace FutbolSolution.Core.DTOs.MatchDTOs
{
    public class MatchStatsDTO:BaseMatchDTO
    {
        public int MatchStatsId { get; set; }
        public int HomeGoals { get; set; } = 0;
        public int AwayGoals { get; set; } = 0;
        public decimal? HomePossession { get; set; }
        public decimal? AwayPossession { get; set; }
        public int HomeShots { get; set; } = 0;
        public int AwayShots { get; set; } = 0;
        public int HomeShotsOnTarget { get; set; } = 0;
        public int AwayShotsOnTarget { get; set; } = 0;
        public int HomeFouls { get; set; } = 0;
        public int AwayFouls { get; set; } = 0;
        public int HomeYellowCards { get; set; } = 0;
        public int AwayYellowCards { get; set; } = 0;
        public int HomeRedCards { get; set; } = 0;
        public int AwayRedCards { get; set; } = 0;
    }
}
