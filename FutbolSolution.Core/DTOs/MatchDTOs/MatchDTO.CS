using System;

namespace FutbolSolution.Core.DTOs.MatchDTOs
{
    public class MatchDTO : BaseMatchDTO
    {
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public DateTime? MatchDate { get; set; }
        public string Stadium { get; set; }
        public int? RefereeId { get; set; }
        public string WeatherConditions { get; set; }
        public string Importance { get; set; }
    }
}
