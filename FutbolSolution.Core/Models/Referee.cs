using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Core.Models
{
        public class Referee
        {
            public int RefereeId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Nationality { get; set; }

            public decimal? ExperienceYears { get; set; }
            public decimal? Bias { get; set; }
        }
}
