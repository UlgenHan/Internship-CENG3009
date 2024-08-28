using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Analyzer.Models
{
    public class NMHPlayerDataFrame
    {
        public int Goals { get; set; }
        public int Assists { get; set; }
        public decimal PassAccuracy { get; set; }
        public int Tackles { get; set; }
        public int Interceptions { get; set; }
        public int DribblesCompleted { get; set; }
        public int AerialDuelsWon { get; set; }
        public int Clearances { get; set; }
    }
}
