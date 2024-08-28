using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.DTOs.PlayerDTOs
{
    public class PlayerTeamLinkDTO
    {
        public int PlayerTeamLinkId { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string IsLoan { get; set; } = "FALSE";
    }
}
