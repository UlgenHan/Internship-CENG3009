using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;

namespace FutbolSolution.Core.Validations
{
    public class InjuriesSuspensionsValidator
    {
        public (bool IsValid, Dictionary<string, string> ValidationMessages) Validate(InjuriesSuspensions injuriesSuspensions)
        {
            var isValid = true;
            var validationMessages = new Dictionary<string, string>();

            // Validate Type
            if (string.IsNullOrWhiteSpace(injuriesSuspensions.Type))
            {
                isValid = false;
                validationMessages.Add(nameof(injuriesSuspensions.Type), "Injury/Suspension type is required.");
            }

            // Validate Description
            if (string.IsNullOrWhiteSpace(injuriesSuspensions.Description))
            {
                isValid = false;
                validationMessages.Add(nameof(injuriesSuspensions.Description), "Description is required.");
            }

            // Validate StartDate
            if (injuriesSuspensions.StartDate == null)
            {
                isValid = false;
                validationMessages.Add(nameof(injuriesSuspensions.StartDate), "Start date is required.");
            }
            else if (injuriesSuspensions.StartDate > DateTime.Now)
            {
                isValid = false;
                validationMessages.Add(nameof(injuriesSuspensions.StartDate), "Start date cannot be in the future.");
            }

            // Validate EndDate
            if (injuriesSuspensions.EndDate == null)
            {
                isValid = false;
                validationMessages.Add(nameof(injuriesSuspensions.EndDate), "End date is required.");
            }
            else if (injuriesSuspensions.EndDate < injuriesSuspensions.StartDate)
            {
                isValid = false;
                validationMessages.Add(nameof(injuriesSuspensions.EndDate), "End date must be after the start date.");
            }

            return (isValid, validationMessages);
        }
    }
}
