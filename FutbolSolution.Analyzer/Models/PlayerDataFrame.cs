using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.Analyzer.Models
{
    public class PlayerDataFrame
    {
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Tackles { get; set; }
        public int Interceptions { get; set; }
        public int Clearances { get; set; }
        public int Shots { get; set; }
        public int ShotsOnTarget { get; set; }
        public int DribblesCompleted { get; set; }
        public int AerialDuelsWon { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int FoulsCommitted { get; set; }
        public int MinutesPlayed { get; set; }
        public double PassAccuracy { get; set; }
    }
}
