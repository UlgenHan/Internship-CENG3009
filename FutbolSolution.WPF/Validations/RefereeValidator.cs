using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FutbolSolution.WPF.Validations
{
    public class RefereeValidator : BaseEntityValidator<Referee>
    {
        public override (bool isValid, Dictionary<string, string> messages) Validate(Referee referee)
        {
            // Call base validation
            bool baseValid = base.Validate(referee).isValid;
            var messages = new Dictionary<string, string>();

            var isValid = baseValid;

            // Validate each field and collect messages
            if (!IsValidName(referee.Name, out var nameMessage))
            {
                isValid = false;
                messages["Name"] = nameMessage;
            }

            if (!IsValidSurname(referee.Surname, out var surnameMessage))
            {
                isValid = false;
                messages["Surname"] = surnameMessage;
            }

            if (!IsValidNationality(referee.Nationality, out var nationalityMessage))
            {
                isValid = false;
                messages["Nationality"] = nationalityMessage;
            }

            if (!IsValidExperienceYears(referee.ExperienceYears, out var experienceMessage))
            {
                isValid = false;
                messages["ExperienceYears"] = experienceMessage;
            }

            if (!IsValidBias(referee.Bias, out var biasMessage))
            {
                isValid = false;
                messages["Bias"] = biasMessage;
            }

            return (isValid, messages);
        }

        private bool IsValidName(string name, out string message)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Name cannot be empty.";
                return false;
            }

            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                message = "Name cannot include special characters or numbers.";
                return false;
            }

            message = string.Empty;
            return true;
        }

        private bool IsValidSurname(string surname, out string message)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                message = "Surname cannot be empty.";
                return false;
            }

            if (!Regex.IsMatch(surname, @"^[a-zA-Z]+$"))
            {
                message = "Surname cannot include special characters or numbers.";
                return false;
            }

            message = string.Empty;
            return true;
        }

        private bool IsValidNationality(string nationality, out string message)
        {
            if (string.IsNullOrWhiteSpace(nationality))
            {
                message = "Nationality cannot be empty.";
                return false;
            }

            if (!Regex.IsMatch(nationality, @"^[a-zA-Z]+$"))
            {
                message = "Nationality cannot include special characters or numbers.";
                return false;
            }

            message = string.Empty;
            return true;
        }

        private bool IsValidExperienceYears(decimal? experienceYears, out string message)
        {
            if (!experienceYears.HasValue || experienceYears.Value < 0 || experienceYears.Value > 50)
            {
                message = "Experience years must be a number between 0 and 50.";
                return false;
            }

            message = string.Empty;
            return true;
        }

        private bool IsValidBias(decimal? bias, out string message)
        {
            if (!bias.HasValue || bias.Value < 0 || bias.Value > 100)
            {
                message = "Bias must be a number between 0 and 100.";
                return false;
            }

            message = string.Empty;
            return true;
        }
    }
}
