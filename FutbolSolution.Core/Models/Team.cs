namespace FutbolSolution.Core.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Stadium { get; set; }
        public string Coach { get; set; }
        public decimal FoundedYear { get; set; }
        public string City { get; set; }
    }

}
