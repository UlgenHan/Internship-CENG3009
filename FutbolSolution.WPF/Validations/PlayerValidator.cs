using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FutbolSolution.Core.Validations
{
    public class PlayerValidator
    {
        // Regex pattern to check for special characters
        private static readonly Regex SpecialCharacterRegex = new Regex(@"[^a-zA-Z\s]", RegexOptions.Compiled);

        public (bool IsValid, Dictionary<string, string> ValidationMessages) Validate(Player player)
        {
            var isValid = true;
            var validationMessages = new Dictionary<string, string>();

            // Validate Name
            if (string.IsNullOrWhiteSpace(player.Name))
            {
                isValid = false;
                validationMessages.Add(nameof(player.Name), "Player name is required.");
            }
            else if (HasInvalidCharacters(player.Name))
            {
                isValid = false;
                validationMessages.Add(nameof(player.Name), "Player name cannot contain numbers or special characters.");
            }

            // Validate Surname
            if (string.IsNullOrWhiteSpace(player.Surname))
            {
                isValid = false;
                validationMessages.Add(nameof(player.Surname), "Player surname is required.");
            }
            else if (HasInvalidCharacters(player.Surname))
            {
                isValid = false;
                validationMessages.Add(nameof(player.Surname), "Player surname cannot contain numbers or special characters.");
            }

            // Validate Age
            if (player.Age <= 0 || player.Age > 100)
            {
                isValid = false;
                validationMessages.Add(nameof(player.Age), "Age must be a positive number and realistically less than 100.");
            }

            // Validate Date of Birth
            if (player.DateOfBirth > DateTime.Now)
            {
                isValid = false;
                validationMessages.Add(nameof(player.DateOfBirth), "Date of birth cannot be in the future.");
            }

            // Validate Nationality
            if (string.IsNullOrWhiteSpace(player.Nationality))
            {
                isValid = false;
                validationMessages.Add(nameof(player.Nationality), "Nationality is required.");
            }
            else if (HasInvalidCharacters(player.Nationality))
            {
                isValid = false;
                validationMessages.Add(nameof(player.Nationality), "Nationality cannot contain numbers or special characters.");
            }

            // Validate Position
            if (string.IsNullOrWhiteSpace(player.Position))
            {
                isValid = false;
                validationMessages.Add(nameof(player.Position), "Position is required.");
            }


            // Validate Height
            if (player.Height <= 0)
            {
                isValid = false;
                validationMessages.Add(nameof(player.Height), "Height must be a positive number.");
            }

            // Validate Weight
            if (player.Weight <= 0)
            {
                isValid = false;
                validationMessages.Add(nameof(player.Weight), "Weight must be a positive number.");
            }

            // Validate Preferred Foot
            if (string.IsNullOrWhiteSpace(player.PreferredFoot))
            {
                isValid = false;
                validationMessages.Add(nameof(player.PreferredFoot), "Preferred foot is required.");
            }

            return (isValid, validationMessages);
        }

        private bool HasInvalidCharacters(string input)
        {
            return SpecialCharacterRegex.IsMatch(input);
        }
    }
}
