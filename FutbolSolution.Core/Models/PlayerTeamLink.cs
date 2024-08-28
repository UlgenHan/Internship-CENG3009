using System;


namespace FutbolSolution.Core.Models
{
    public class PlayerTeamLink
    {
        public int PlayerTeamLinkId { get; set; }

        public int? PlayerId { get; set; }
        public int? TeamId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string IsLoan { get; set; } = "FALSE";

        public PlayerTeamLink()
        {
            
        }
    }
}
