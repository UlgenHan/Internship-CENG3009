using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;

namespace FutbolSolution.Core.Validations
{
    public class PlayerStatsValidator
    {
        public (bool IsValid, Dictionary<string, string> ValidationMessages) Validate(PlayerStats playerStats)
        {
            var isValid = true;
            var validationMessages = new Dictionary<string, string>();

            // Validate Goals
            if (playerStats.Goals < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.Goals), "Goals cannot be negative.");
            }

            // Validate Assists
            if (playerStats.Assists < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.Assists), "Assists cannot be negative.");
            }

            // Validate TotalMinutesIn
            if (playerStats.TotalMinutesIn < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.TotalMinutesIn), "Total minutes played cannot be negative.");
            }

            // Validate PassAccuracy
            if (playerStats.PassAccuracy.HasValue && (playerStats.PassAccuracy < 0 || playerStats.PassAccuracy > 100))
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.PassAccuracy), "Pass accuracy must be between 0 and 100.");
            }

            // Validate Tackles
            if (playerStats.Tackles < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.Tackles), "Tackles cannot be negative.");
            }

            // Validate Interceptions
            if (playerStats.Interceptions < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.Interceptions), "Interceptions cannot be negative.");
            }

            // Validate Clearances
            if (playerStats.Clearances < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.Clearances), "Clearances cannot be negative.");
            }

            // Validate Shots
            if (playerStats.Shots < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.Shots), "Shots cannot be negative.");
            }

            // Validate ShotsOnTarget
            if (playerStats.ShotsOnTarget < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.ShotsOnTarget), "Shots on target cannot be negative.");
            }

            // Validate DribblesCompleted
            if (playerStats.DribblesCompleted < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.DribblesCompleted), "Dribbles completed cannot be negative.");
            }

            // Validate AerialDuelsWon
            if (playerStats.AerialDuelsWon < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.AerialDuelsWon), "Aerial duels won cannot be negative.");
            }

            // Validate YellowCards
            if (playerStats.YellowCards < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.YellowCards), "Yellow cards cannot be negative.");
            }

            // Validate RedCards
            if (playerStats.RedCards < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.RedCards), "Red cards cannot be negative.");
            }

            // Validate FoulsCommitted
            if (playerStats.FoulsCommitted < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.FoulsCommitted), "Fouls committed cannot be negative.");
            }

            // Validate FoulsSuffered
            if (playerStats.FoulsSuffered < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.FoulsSuffered), "Fouls suffered cannot be negative.");
            }

            // Validate Offsides
            if (playerStats.Offsides < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.Offsides), "Offsides cannot be negative.");
            }

            // Validate Saves
            if (playerStats.Saves < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.Saves), "Saves cannot be negative.");
            }

            // Validate CleanSheets
            if (playerStats.CleanSheets < 0)
            {
                isValid = false;
                validationMessages.Add(nameof(playerStats.CleanSheets), "Clean sheets cannot be negative.");
            }

            return (isValid, validationMessages);
        }
    }
}
