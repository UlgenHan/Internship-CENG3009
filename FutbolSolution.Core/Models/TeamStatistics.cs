using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Models
{
    public class TeamStatistics
    {
        public int TeamStatsId { get; set; }

        public int? TeamId { get; set; }
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
