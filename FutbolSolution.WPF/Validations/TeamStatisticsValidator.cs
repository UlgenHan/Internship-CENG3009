using FutbolSolution.Core.Models;
using System.Collections.Generic;

namespace FutbolSolution.Core.Validations
{
    public class TeamStatisticsValidator
    {
        public (bool IsValid, Dictionary<string, string> ValidationMessages) Validate(TeamStatistics teamStats)
        {
            var isValid = true;
            var validationMessages = new Dictionary<string, string>();

            // Validate that numerical values are not negative
            if (teamStats.GoalsScored < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(teamStats.GoalsScored), "Goals scored cannot be negative.");
            }

            if (teamStats.GoalsConceded < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(teamStats.GoalsConceded), "Goals conceded cannot be negative.");
            }

            if (teamStats.Wins < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(teamStats.Wins), "Wins cannot be negative.");
            }

            if (teamStats.Draws < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(teamStats.Draws), "Draws cannot be negative.");
            }

            if (teamStats.Losses < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(teamStats.Losses), "Losses cannot be negative.");
            }

            if (teamStats.HomeWins < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(teamStats.HomeWins), "Home wins cannot be negative.");
            }

            if (teamStats.AwayWins < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(teamStats.AwayWins), "Away wins cannot be negative.");
            }

            // Additional validations can be added here if necessary

            return (isValid, validationMessages);
        }
    }
}
