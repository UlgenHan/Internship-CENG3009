using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FutbolSolution.Core.Validations
{
    public class MatchValidator
    {
        // Regex pattern to check for special characters
        private static readonly Regex SpecialCharacterRegex = new Regex(@"[^a-zA-Z\s]", RegexOptions.Compiled);

        public (bool IsValid, Dictionary<string, string> ValidationMessages) Validate(Models.Match match)
        {
            var isValid = true;
            var lowerDateLimit = DateTime.Parse("01/01/2000");
            var validationMessages = new Dictionary<string, string>();

            // Validate MatchDate
            if (match.MatchDate == default(DateTime))
            {
                isValid = false;
                validationMessages.Add(nameof(match.MatchDate), "Match date is required.");
            }

            // Validate HomeTeamId
            if (match.HomeTeamId <= 0)
            {
                isValid = false;
                validationMessages.Add(nameof(match.HomeTeamId), "Home team ID must be a positive integer.");
            }

            // Validate AwayTeamId
            if (match.AwayTeamId <= 0)
            {
                isValid = false;
                validationMessages.Add(nameof(match.AwayTeamId), "Away team ID must be a positive integer.");
            }

            // Validate Stadium
            if (string.IsNullOrWhiteSpace(match.Stadium))
            {
                isValid = false;
                validationMessages.Add(nameof(match.Stadium), "Stadium is required.");
            }
            else if (HasInvalidCharacters(match.Stadium))
            {
                isValid = false;
                validationMessages.Add(nameof(match.Stadium), "Stadium cannot contain numbers or special characters.");
            }

            // Validate RefereeId
            if (match.RefereeId <= 0)
            {
                isValid = false;
                validationMessages.Add(nameof(match.RefereeId), "Referee ID must be a positive integer.");
            }

            if(match.MatchDate >= DateTime.Now || match.MatchDate < lowerDateLimit)
            {
                isValid = false;
                validationMessages.Add(nameof(match.MatchDate), "Match date cannot be greater than today or smaller than 2000's.");
            }

            //Team can not be same
            if (match.AwayTeamId == match.HomeTeamId)
            {
                isValid = false;
                validationMessages.Add(nameof(match.HomeTeamId), "Home team and away team can not be same.");
            }

            // Validate WeatherConditions
            if (string.IsNullOrWhiteSpace(match.WeatherConditions))
            {
                isValid = false;
                validationMessages.Add(nameof(match.WeatherConditions), "Weather conditions are required.");
            }
            else if (HasInvalidCharacters(match.WeatherConditions))
            {
                isValid = false;
                validationMessages.Add(nameof(match.WeatherConditions), "Weather conditions cannot contain numbers or special characters.");
            }

            // Validate Importance
            if (string.IsNullOrWhiteSpace(match.Importance))
            {
                isValid = false;
                validationMessages.Add(nameof(match.Importance), "Importance is required.");
            }
            else if (HasInvalidCharacters(match.Importance))
            {
                isValid = false;
                validationMessages.Add(nameof(match.Importance), "Importance cannot contain numbers or special characters.");
            }

            return (isValid, validationMessages);
        }

        private bool HasInvalidCharacters(string input)
        {
            return SpecialCharacterRegex.IsMatch(input);
        }
    }
}
