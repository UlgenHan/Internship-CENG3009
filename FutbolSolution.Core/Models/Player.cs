using System;

namespace FutbolSolution.Core.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string CurrentClub { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string PreferredFoot { get; set; }
        public int PlayerStatsId { get; set; }

        public Player()
        {
            
        }
    }
}
