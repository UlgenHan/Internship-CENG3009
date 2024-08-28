using System;

namespace FutbolSolution.Core.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }

        public DateTime? MatchDate { get; set; }
        public string Stadium { get; set; }

        public int? RefereeId { get; set; }
        public string WeatherConditions { get; set; }
        public string Importance { get; set; }
    }
}
