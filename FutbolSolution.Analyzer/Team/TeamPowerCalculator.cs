using FutbolSolution.Analyzer.Models;
using System;

namespace FutbolSolution.Analyzer.Team
{
    public static class TeamPowerCalculator
    {
        // Method to calculate team power score
        public static double CalculatePowerScore(TeamDataFrame stats)
        {
            if (stats.TotalMatches == 0) // Avoid division by zero
            {
                throw new ArgumentException("TotalMatches cannot be zero.");
            }

            double maxGoalsScored = 100; // Example max goals scored
            double maxGoalsConceded = 100; // Example max goals conceded
            double maxWins = 50; // Example max wins
            double maxDraws = 30; // Example max draws
            double maxLosses = 50; // Example max losses
            double maxPossession = 100; // Example max possession percentage
            double maxPassAccuracy = 100; // Example max pass accuracy percentage
            double maxYellowCards = 10; // Example max yellow cards (penalizing)
            double maxRedCards = 5; // Example max red cards (penalizing)

            double powerScore = 0;

            // Calculate contribution based on various metrics and normalize
            powerScore += (stats.GoalsScored / maxGoalsScored) * 25; // Goals scored weighted to 25%
            powerScore -= (stats.GoalsConceded / maxGoalsConceded) * 20; // Goals conceded weighted to -20%
            powerScore += (stats.Wins / maxWins) * 30; // Wins weighted to 30%
            powerScore += (stats.Draws / maxDraws) * 10; // Draws weighted to 10%
            powerScore -= (stats.Losses / maxLosses) * 10; // Losses weighted to -10%
            powerScore += (stats.AveragePossession / maxPossession) * 10; // Average possession weighted to 10%
            powerScore += (stats.AveragePassAccuracy / maxPassAccuracy) * 5; // Average pass accuracy weighted to 5%
            powerScore -= (stats.YellowCards / maxYellowCards) * 5; // Penalizing cards weighted to -5%
            powerScore -= (stats.RedCards / maxRedCards) * 10; // Penalizing for red cards weighted to -10%

            // Ensure the power score is between 0 and 100
            powerScore = Math.Max(0, Math.Min(100, powerScore));

            return Math.Round(powerScore, 2); // Return the power score rounded to two decimal places
        }
    }
}
