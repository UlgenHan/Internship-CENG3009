using FutbolSolution.Core.DTOs.MatchDTOs;
using System;
using System.Collections.Generic;

namespace FutbolSolution.Core.Validations
{
    public class MatchStatsValidator
    {
        public (bool IsValid, Dictionary<string, string> ValidationMessages) Validate(MatchStatsDTO matchStats)
        {
            var isValid = true;
            var validationMessages = new Dictionary<string, string>();

            // Validate HomeGoals and AwayGoals
            if (!IsValidInteger(matchStats.HomeGoals))
            {
                isValid = false;
                validationMessages.Add(nameof(matchStats.HomeGoals), "Home goals must be a valid integer.");
            }
            else if (matchStats.HomeGoals < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(matchStats.HomeGoals), "Home goals cannot be negative.");
            }

            if (!IsValidInteger(matchStats.AwayGoals))
            {
                isValid = false;
                validationMessages.Add(nameof(matchStats.AwayGoals), "Away goals must be a valid integer.");
            }
            else if (matchStats.AwayGoals < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(matchStats.AwayGoals), "Away goals cannot be negative.");
            }

            // Validate Possession percentages
            if (matchStats.HomePossession.HasValue)
            {
                if (!IsValidDecimal(matchStats.HomePossession.Value))
                {
                    isValid = false;
                    validationMessages.Add(nameof(matchStats.HomePossession), "Home possession must be a valid decimal.");
                }
                else if (matchStats.HomePossession < 0 || matchStats.HomePossession > 100)
                {
                    isValid = false;
                    validationMessages.Add(nameof(matchStats.HomePossession), "Home possession must be between 0 and 100.");
                }
            }

            if (matchStats.AwayPossession.HasValue)
            {
                if (!IsValidDecimal(matchStats.AwayPossession.Value))
                {
                    isValid = false;
                    validationMessages.Add(nameof(matchStats.AwayPossession), "Away possession must be a valid decimal.");
                }
                else if (matchStats.AwayPossession < 0 || matchStats.AwayPossession > 100)
                {
                    isValid = false;
                    validationMessages.Add(nameof(matchStats.AwayPossession), "Away possession must be between 0 and 100.");
                }
            }

            // Validate shots, fouls, cards, etc.
            ValidateIntegerProperty(matchStats.HomeShots, nameof(matchStats.HomeShots), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.AwayShots, nameof(matchStats.AwayShots), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.HomeShotsOnTarget, nameof(matchStats.HomeShotsOnTarget), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.AwayShotsOnTarget, nameof(matchStats.AwayShotsOnTarget), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.HomeFouls, nameof(matchStats.HomeFouls), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.AwayFouls, nameof(matchStats.AwayFouls), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.HomeYellowCards, nameof(matchStats.HomeYellowCards), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.AwayYellowCards, nameof(matchStats.AwayYellowCards), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.HomeRedCards, nameof(matchStats.HomeRedCards), ref isValid, validationMessages);
            ValidateIntegerProperty(matchStats.AwayRedCards, nameof(matchStats.AwayRedCards), ref isValid, validationMessages);

            return (isValid, validationMessages);
        }

        private void ValidateIntegerProperty(int value, string propertyName, ref bool isValid, Dictionary<string, string> validationMessages)
        {
            if (!IsValidInteger(value))
            {
                isValid = false;
                validationMessages.Add(propertyName, $"{propertyName} must be a valid integer.");
            }
            else if (value < 0)
            {
                isValid = false;
                validationMessages.Add(propertyName, $"{propertyName} cannot be negative.");
            }
        }

        private bool IsValidInteger(int value)
        {
            return true; // All integer values are technically valid in C#, but we can extend this for specific ranges if needed.
        }

        private bool IsValidDecimal(decimal value)
        {
            return value >= (decimal.MinValue) && value <= (decimal.MaxValue);
        }
    }
}
