

namespace FutbolSolution.Analyzer.Models
{
    public class TeamDataFrame
    {
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int HomeWins { get; set; }
        public int AwayWins { get; set; }
        public double AveragePossession { get; set; } // Percentage
        public double AveragePassAccuracy { get; set; } // Percentage
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int TotalMatches { get; set; }
    }
}
