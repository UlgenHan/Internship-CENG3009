using System.Data.Common;

namespace FutbolSolution.Core.Models
{
    public class InjuriesLink
    {
        public int Id { get; set; }
        public int InjuriesSuspensionId { get; set; }
        public int Severity { get; set; }
    }
}
