namespace FutbolSolution.Analyzer.Models
{
    public class LogisticRegressionDataFrame
    {
        public int HomeGoalsScored { get; set; }
        public int HomeGoalsConceded { get; set; }
        public int HomeWins { get; set; }
        public int HomeDraws { get; set; }
        public int HomeLoses { get; set; }
        public int AwayGoalsScored { get; set; }
        public int AwayGoalsConceded { get; set; }
        public int AwayWins { get; set; }
        public int AwayDraws { get; set; }
        public int AwayLoses { get; set; }
        public string HomeRecentForm { get; set; }
        public string AwayRecentForm { get; set; }
        public int RefereeBias { get; set; }
        public string Importance { get; set; }
        public int InjuredPlayersHome { get; set; }
        public int InjuredPlayersAway { get; set; }
        public decimal HomeAveragePassAccuracy { get; set; }
        public decimal AwayAveragePassAccuracy { get; set; }
        public int HomeTotalShots { get; set; }
        public int AwayTotalShots { get; set; }
        public int HeadToHeadWinsHome { get; set; }
        public int HeadToHeadWinsAway { get; set; }

        // Add power scores
        public double HomePowerScore { get; set; }
        public double AwayPowerScore { get; set; }
    }
}
