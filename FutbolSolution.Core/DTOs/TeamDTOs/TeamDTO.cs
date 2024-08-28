using FutbolSolution.Core.Models;

namespace FutbolSolution.Core.DTOs.TeamDTOs
{
    public class TeamDTO : BaseTeamDTO
    {
        public string Name { get; set; }
        public string Stadium { get; set; }
        public string Coach { get; set; }
        public decimal FoundedYear { get; set; }
        public string City { get; set; }
        public TeamImage TeamImage { get; set; }
    }
}
