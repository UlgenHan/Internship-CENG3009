using FutbolSolution.Analyzer.Models;
using FutbolSolution.Core.DTOs.MatchDTOs;
using System.Collections.Generic;


namespace FutbolSolution.WPF.Utils.HelperModels
{
    public class RegressionCalculationDataFrame
    {
        public List<NMHPlayerDataFrame> teamPlayers { get; set; }
        public NMHTeamDataFrame teamStats { get; set; }
        public decimal refereeBias { get; set; }
        public Dictionary<int, List<NMHInjurySuspension>> teamInjuriesSuspensions { get; set; }

        public List<MatchStatsDTO> MatchStats { get; set; }
    }
}
