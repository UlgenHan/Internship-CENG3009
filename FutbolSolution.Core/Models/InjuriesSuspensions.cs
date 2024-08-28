using System;

namespace FutbolSolution.Core.Models
{
    public class InjuriesSuspensions
    {
        
        public int InjurySuspensionId { get; set; }

        public int? PlayerId { get; set; }
        public string Type { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
