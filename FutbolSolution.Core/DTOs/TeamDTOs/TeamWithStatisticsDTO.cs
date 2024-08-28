using FutbolSolution.Core.Models;

namespace FutbolSolution.Core.DTOs.TeamDTOs
{
    public class TeamWithStatisticsDTO : BaseTeamDTO
    {

        public int TeamStatsId { get; set; }
        public string SeasonYear { get; set; }

        public decimal GoalsScored { get; set; } = 0;
        public decimal GoalsConceded { get; set; } = 0;
        public decimal Wins { get; set; } = 0;
        public decimal Draws { get; set; } = 0;
        public decimal Losses { get; set; } = 0;
        public decimal HomeWins { get; set; } = 0;
        public decimal AwayWins { get; set; } = 0;
        public string RecentForm { get; set; }
    }
}
