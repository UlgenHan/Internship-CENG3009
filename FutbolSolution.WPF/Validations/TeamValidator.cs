using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FutbolSolution.Core.Validations
{
    public class TeamValidator
    {
        // Regex pattern to check for special characters
        private static readonly Regex SpecialCharacterRegex = new Regex(@"[^a-zA-Z\s]", RegexOptions.Compiled);

        public (bool IsValid, Dictionary<string, string> ValidationMessages) Validate(Team team)
        {
            var isValid = true;
            var validationMessages = new Dictionary<string, string>();

            // Validate Name
            if (string.IsNullOrWhiteSpace(team.Name))
            {
                isValid = false;
                validationMessages.Add(nameof(team.Name), "Team name is required.");
            }
            else if (HasInvalidCharacters(team.Name))
            {
                isValid = false;
                validationMessages.Add(nameof(team.Name), "Team name cannot contain numbers or special characters.");
            }

            // Validate Stadium
            if (string.IsNullOrWhiteSpace(team.Stadium))
            {
                isValid = false;
                validationMessages.Add(nameof(team.Stadium), "Stadium name is required.");
            }
            else if (HasInvalidCharacters(team.Stadium))
            {
                isValid = false;
                validationMessages.Add(nameof(team.Stadium), "Stadium name cannot contain numbers or special characters.");
            }

            // Validate Coach
            if (string.IsNullOrWhiteSpace(team.Coach))
            {
                isValid = false;
                validationMessages.Add(nameof(team.Coach), "Coach name is required.");
            }
            else if (HasInvalidCharacters(team.Coach))
            {
                isValid = false;
                validationMessages.Add(nameof(team.Coach), "Coach name cannot contain numbers or special characters.");
            }

            // Validate Founded Year
            if (team.FoundedYear < 1800 || team.FoundedYear > DateTime.Now.Year)
            {
                isValid = false;
                validationMessages.Add(nameof(team.FoundedYear), "Founded year must be between 1800 and the current year.");
            }

            // Validate City
            if (string.IsNullOrWhiteSpace(team.City))
            {
                isValid = false;
                validationMessages.Add(nameof(team.City), "City is required.");
            }
            else if (HasInvalidCharacters(team.City))
            {
                isValid = false;
                validationMessages.Add(nameof(team.City), "City cannot contain numbers or special characters.");
            }

            return (isValid, validationMessages);
        }

        private bool HasInvalidCharacters(string input)
        {
            return SpecialCharacterRegex.IsMatch(input);
        }
    }
}
