namespace FutbolSolution.Core.DTOs.RefereeDTOs
{
    public class RefereeDTO : BaseRefereeDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nationality { get; set; }
        public decimal? ExperienceYears { get; set; }
        public decimal? Bias { get; set; }
    }
}
