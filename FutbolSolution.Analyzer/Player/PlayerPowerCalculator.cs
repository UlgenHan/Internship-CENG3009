using FutbolSolution.Analyzer.Models;
using System;


namespace FutbolSolution.Analyzer.Player
{
    public static class PlayerPowerCalculator
    {

        public static double CalculatePowerScore(PlayerDataFrame stats)
        {
            if (stats.MinutesPlayed == 0) // Avoid division by zero
            {
                throw new ArgumentException("MinutesPlayed cannot be zero.");
            }

            double maxGoals = 50; // Example max goals
            double maxAssists = 30; // Example max assists
            double maxTackles = 100; // Example max tackles
            double maxInterceptions = 100; // Example max interceptions
            double maxClearances = 50; // Example max clearances
            double maxShots = 100; // Example max shots
            double maxShotsOnTarget = 50; // Example max shots on target
            double maxDribbles = 50; // Example max dribbles completed
            double maxAerialDuels = 50; // Example max aerial duels won
            double maxYellowCards = 5; // Example max yellow cards (penalizing)
            double maxRedCards = 2; // Example max red cards (penalizing)

            double powerScore = 0;

            // Calculate contribution based on various metrics and normalize
            powerScore += (stats.Goals / maxGoals) * 20; // Goals weighted to 20%
            powerScore += (stats.Assists / maxAssists) * 15; // Assists weighted to 15%
            powerScore += (stats.Tackles / maxTackles) * 10; // Defensive actions weighted to 10%
            powerScore += (stats.Interceptions / maxInterceptions) * 10; // Interceptions
            powerScore += (stats.Clearances / maxClearances) * 5; // Clearances
            powerScore += ((double)stats.ShotsOnTarget / stats.Shots) * 15; // Shot efficiency weighted to 15%
            powerScore += (stats.DribblesCompleted / maxDribbles) * 10; // Dribbles weighted to 10%
            powerScore += (stats.AerialDuelsWon / maxAerialDuels) * 5; // Aerial duels weighted to 5%
            powerScore -= (stats.YellowCards / maxYellowCards) * 5; // Penalizing cards weighted to -5%
            powerScore -= (stats.RedCards / maxRedCards) * 10; // Penalizing for red cards weighted to -10%

            // Ensure the power score is between 0 and 100
            powerScore = Math.Max(0, Math.Min(100, powerScore));

            return Math.Round(powerScore, 2); // Return the power score rounded to two decimal places
        }
    }
}
