using System;


namespace FutbolSolution.WPF.Utils
{
    public class MatchHolder
    {
        public int MatchID { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string MatchDate { get; set; }
        public string Stadium { get; set; }
        public string Referee { get; set; }
        public string WeatherConditions { get; set; }
        public string Importance { get; set; } = string.Empty;
    }
}
