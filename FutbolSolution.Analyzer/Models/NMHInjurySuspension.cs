using FutbolSolution.Core.Enums;
namespace FutbolSolution.Analyzer.Models
{
    public class NMHInjurySuspension
    {
        public string Type { get; set; }
        public InjureSeverityEnum InjureSeverity { get; set; }
    }
}
