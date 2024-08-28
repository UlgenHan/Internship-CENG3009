using FutbolSolution.Analyzer.Models;
using FutbolSolution.Core.DTOs.MatchDTOs;
using FutbolSolution.Core.Enums;
using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;

namespace FutbolSolution.Analyzer.LogisticRegression
{
    public class NMHLogisticRegression
    {

        //private decimal intercept = -1.5m;
        //private decimal betaTeamStrength = 0.7m; // Coefficient for team strength (calculated from player stats)
        //private decimal betaTeamStats = 0.4m; // Coefficient for overall team stats
        //private decimal betaRefBias = 0.2m; // Coefficient for referee bias
        //private decimal betaMatchHistory = 0.3m; // Coefficient for match history


        private decimal intercept;
        private decimal betaTeamStrength;
        private decimal betaTeamStats;
        private decimal betaRefBias;
        private decimal betaMatchHistory;

        public NMHLogisticRegression(decimal interceptParam,decimal betaTeamStrengthParam,decimal betaTeamStatsParam,
            decimal betaRefBiasParam,decimal betaMatchHistoryParam)
        {
            intercept = interceptParam;
            betaTeamStrength = betaTeamStrengthParam;
            betaTeamStats = betaTeamStatsParam;
            betaRefBias = betaRefBiasParam;
            betaMatchHistory = betaMatchHistoryParam;

        }
        public decimal CalculateWinProbability(
            List<NMHPlayerDataFrame> teamAPlayers, NMHTeamDataFrame teamAStats,
            List<NMHPlayerDataFrame> teamBPlayers, NMHTeamDataFrame teamBStats,
            decimal refereeBias,
            Dictionary<int, List<NMHInjurySuspension>> teamAInjuriesSuspensions,
            Dictionary<int, List<NMHInjurySuspension>> teamBInjuriesSuspensions)
        {
            // Calculate power for both teams
            decimal teamAPower = CalculateTeamPower(teamAPlayers, teamAStats, teamAInjuriesSuspensions);
            decimal teamBPower = CalculateTeamPower(teamBPlayers, teamBStats, teamBInjuriesSuspensions);

            // Calculate referee impact (can apply this to both teams equally, or adjust if biased)
            decimal refereeImpact = refereeBias * betaRefBias;

            // Calculate log-odds for both teams
            decimal logOddsTeamA = intercept + (teamAPower * betaTeamStrength) + refereeImpact;
            decimal logOddsTeamB = intercept + (teamBPower * betaTeamStrength) + refereeImpact;

            // Convert log-odds to probabilities using the sigmoid function
            decimal probabilityTeamA = 1 / (1 + (decimal)Math.Exp(-(double)logOddsTeamA));
            decimal probabilityTeamB = 1 / (1 + (decimal)Math.Exp(-(double)logOddsTeamB));

            // Normalize probabilities so that they sum up to 1
            decimal totalProbability = probabilityTeamA + probabilityTeamB;
            probabilityTeamA /= totalProbability;
            probabilityTeamB /= totalProbability;

            // Return the win probability for team A (and implicitly, for team B as 1 - probabilityTeamA)
            return probabilityTeamA;
        }


        // Method to calculate team power
        private decimal CalculateTeamPower(List<NMHPlayerDataFrame> players, NMHTeamDataFrame teamStats,
            Dictionary<int, List<NMHInjurySuspension>> injuriesSuspensions)
        {
            decimal totalPlayerScore = 0.0m;

            for (int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                decimal playerScore = player.Goals * 0.4m + player.Assists * 0.3m +  // Increase weight for goals/assists
                              player.PassAccuracy * 0.2m + player.Tackles * 0.15m +
                              player.Interceptions * 0.1m + player.DribblesCompleted * 0.1m +
                              player.AerialDuelsWon * 0.1m + player.Clearances * 0.05m;

                // Adjust the player's score based on injuries or suspensions
                if (injuriesSuspensions.ContainsKey(i))
                {
                    foreach (var injury in injuriesSuspensions[i])
                    {
                        playerScore *= ApplyInjurySeverity(injury.InjureSeverity);
                    }
                }

                totalPlayerScore += playerScore;
            }

            // Combine team and player statistics
            decimal teamScore = teamStats.GoalsScored * 0.5m + teamStats.GoalsConceded * -0.3m +
                                teamStats.Wins * 0.3m + teamStats.Draws * 0.1m +
                                teamStats.Losses * -0.4m;

            decimal totalScore = (totalPlayerScore / players.Count) + (teamScore * betaTeamStats);
            return totalScore;
        }

        public decimal CalculateWinProbabilityWithHistory(
            List<NMHPlayerDataFrame> teamAPlayers, NMHTeamDataFrame teamAStats,
            List<NMHPlayerDataFrame> teamBPlayers, NMHTeamDataFrame teamBStats,
            decimal refereeBias,
            Dictionary<int, List<NMHInjurySuspension>> teamAInjuriesSuspensions,
            Dictionary<int, List<NMHInjurySuspension>> teamBInjuriesSuspensions,
            List<MatchStatsDTO> teamAMatchHistory, List<MatchStatsDTO> teamBMatchHistory)
        {
            // Calculate power for both teams
            decimal teamAPower = CalculateTeamPower(teamAPlayers, teamAStats, teamAInjuriesSuspensions);
            decimal teamBPower = CalculateTeamPower(teamBPlayers, teamBStats, teamBInjuriesSuspensions);

            // Calculate match history influence for both teams
            decimal teamAHistoryScore = CalculateMatchHistoryImpact(teamAMatchHistory);
            decimal teamBHistoryScore = CalculateMatchHistoryImpact(teamBMatchHistory);

            // Calculate referee impact
            decimal refereeImpact = refereeBias * betaRefBias;

            // Calculate log-odds for both teams
            decimal logOddsTeamA = intercept + (teamAPower * betaTeamStrength) +
                                   (teamAHistoryScore * betaMatchHistory) + refereeImpact;
            decimal logOddsTeamB = intercept + (teamBPower * betaTeamStrength) +
                                   (teamBHistoryScore * betaMatchHistory) + refereeImpact;

            // Convert log-odds to probabilities using the sigmoid function
            decimal probabilityTeamA = 1 / (1 + (decimal)Math.Exp(-(double)logOddsTeamA));
            decimal probabilityTeamB = 1 / (1 + (decimal)Math.Exp(-(double)logOddsTeamB));

            // Normalize probabilities so that they sum up to 1
            decimal totalProbability = probabilityTeamA + probabilityTeamB;
            probabilityTeamA /= totalProbability;
            probabilityTeamB /= totalProbability;

            // Return the win probability for team A (and implicitly, for team B as 1 - probabilityTeamA)
            return probabilityTeamA;
        }

        // Method to calculate match history impact
        private decimal CalculateMatchHistoryImpact(List<MatchStatsDTO> matchHistory)
        {
            decimal matchHistoryScore = 0.0m;
            int matchCount = matchHistory.Count;

            foreach (var match in matchHistory)
            {
                // Use the difference between goals scored and goals conceded as a simple metric
                decimal matchScore = (match.HomeGoals - match.AwayGoals) * 0.4m +
                                     (match.HomeShots - match.AwayShots) * 0.1m +
                                     (match.HomePossession ?? 0m) * 0.1m;

                matchHistoryScore += matchScore;
            }

            // Normalize the score by the number of matches considered
            return matchCount > 0 ? matchHistoryScore / matchCount : 0.0m;
        }


        // Method to apply injury severity to the player's score
        private decimal ApplyInjurySeverity(InjureSeverityEnum severity)
        {
            switch (severity)
            {
                case InjureSeverityEnum.Light:
                    return 0.9m; // Light injury reduces the score by 10%
                case InjureSeverityEnum.Mid:
                    return 0.7m; // Medium injury reduces the score by 30%
                case InjureSeverityEnum.Critical:
                    return 0.4m; // Critical injury reduces the score by 60%
                default:
                    return 1.0m; // No injury
            }
        }
    }
}
