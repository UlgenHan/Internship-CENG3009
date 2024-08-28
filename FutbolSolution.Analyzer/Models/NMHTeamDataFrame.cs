using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Analyzer.Models
{
    public class NMHTeamDataFrame
    {
        public decimal GoalsScored { get; set; }
        public decimal GoalsConceded { get; set; }
        public decimal Wins { get; set; }
        public decimal Draws { get; set; }
        public decimal Losses { get; set; }
    }
}
